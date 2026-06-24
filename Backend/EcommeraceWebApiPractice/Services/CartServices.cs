using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Entites;
using EcommeraceWebApiPractice.@interface;
using Microsoft.EntityFrameworkCore;

namespace EcommeraceWebApiPractice.Services
{
    public class CartServices : ICartServices
    {
        private readonly AppDbContext _context;

        public CartServices(AppDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────
        // ADD ITEM
        // ─────────────────────────────────────────────
        public cart AddCartItem(string userId, int productId, int quantity)
        {
            // Load or create the cart for this user
            var myCart = _context.cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserID == userId);

            if (myCart == null)
            {
                myCart = new cart
                {
                    UserID = userId,
                    CreatedAt = DateTime.Now,
                    CartItems = new List<cartItem>()
                };
                _context.cart.Add(myCart);
                _context.SaveChanges();
            }

            // Item already in cart — return current cart instead of null
            var existingItem = myCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
                return ViewCart(userId);

            // Validate product exists
            var product = _context.products.Find(productId);
            if (product == null)
                throw new Exception($"Product with ID {productId} was not found.");

            var newItem = new cartItem
            {
                CartId = myCart.Id,
                ProductId = productId,
                quanity = 1,
                price = product.Price
            };

            myCart.CartItems.Add(newItem);

            RecalculateTotal(myCart);   // only assigns — does NOT save
            _context.SaveChanges();     // single save for everything

            return ViewCart(userId);
        }

        // ─────────────────────────────────────────────
        // VIEW CART
        // ─────────────────────────────────────────────
        public cart ViewCart(string userId)
        {
            return _context.cart
                .AsNoTracking()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Photos)
                .FirstOrDefault(c => c.UserID == userId);
        }

        // ─────────────────────────────────────────────
        // DELETE SINGLE ITEM
        // ─────────────────────────────────────────────
        public cart DeleteCartItem(int cartItemId, string userId)
        {
            var myCart = _context.cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Photos)
                .FirstOrDefault(c => c.UserID == userId);

            if (myCart == null)
                return null;

            // FIX: match on cart item's own id, not product id
            var toDelete = myCart.CartItems.FirstOrDefault(ci => ci.id == cartItemId);
            if (toDelete == null)
                return null;

            _context.cartItem.Remove(toDelete);
            myCart.CartItems.Remove(toDelete);  // keep in-memory list in sync

            RecalculateTotal(myCart);
            _context.SaveChanges();

            return ViewCart(userId);
        }

        // ─────────────────────────────────────────────
        // CLEAR CART
        // ─────────────────────────────────────────────
        public cart ClearCart(string userId)
        {
            var myCart = _context.cart
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserID == userId);

            if (myCart == null)
                return null;

            _context.cartItem.RemoveRange(myCart.CartItems);
            myCart.CartItems.Clear();   // FIX: keep in-memory list in sync
            myCart.Total = 0;

            _context.SaveChanges();
            return myCart;
        }

        // ─────────────────────────────────────────────
        // INCREASE QUANTITY
        // ─────────────────────────────────────────────
        public cart IncreaseQuantity(string userId, int cartItemId)
        {
            var myCart = _context.cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserID == userId);

            if (myCart == null)
                return null;

            var item = myCart.CartItems.FirstOrDefault(ci => ci.id == cartItemId);
            if (item == null)
                return null;

            item.quanity++;

            RecalculateTotal(myCart);
            _context.SaveChanges();

            return ViewCart(userId);
        }

        // ─────────────────────────────────────────────
        // DECREASE QUANTITY
        // ─────────────────────────────────────────────
        public cart DecreaseQuantity(string userId, int cartItemId)
        {
            var myCart = _context.cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserID == userId);

            if (myCart == null)
                return null;

            var item = myCart.CartItems.FirstOrDefault(ci => ci.id == cartItemId);
            if (item == null)
                return null;

            if (item.quanity <= 1)
            {
                // FIX: delete directly instead of saving quantity=0 first
                _context.cartItem.Remove(item);
                myCart.CartItems.Remove(item);  // keep in-memory list in sync
            }
            else
            {
                item.quanity--;
            }

            RecalculateTotal(myCart);   // recalculate on the updated list
            _context.SaveChanges();     // single save

            return ViewCart(userId);
        }

        // ─────────────────────────────────────────────
        // CONVERT TO DTO
        // ─────────────────────────────────────────────
        public CartDto ConvertToDto(cart cart)
        {
            // FIX: guard against null CartItems
            var items = cart.CartItems ?? new List<cartItem>();

            var cartItemDtos = items.Select(ci => new CartItemDto
            {
                id = ci.id,
                price = ci.price,
                quanity = ci.quanity,
                ProductId = ci.ProductId,
                Product = new productResponse
                {
                    ID = ci.Product.id,
                    Name = ci.Product.Name,
                    Description = ci.Product.Description,
                    CategoryId = ci.Product.CategoryId,
                    Price = ci.Product.Price,
                    thunmailImage = ci.Product.Photos?.FirstOrDefault()?.imageUrl
                }
            }).ToList();

            return new CartDto
            {
                Id = cart.Id,
                UserID = cart.UserID,
                CreatedAt = cart.CreatedAt,
                Total = cart.Total,
                cartItemDtos = cartItemDtos
            };
        }

        // ─────────────────────────────────────────────
        // PRIVATE HELPER — only assigns Total, never saves
        // ─────────────────────────────────────────────
        private void RecalculateTotal(cart cart)
        {
            decimal total = 0;
            if (cart == null)
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            foreach (var item in cart.CartItems)
            {
                total = total + item.price * item.quanity;
            }
            cart.Total = total;

        }


        public Order checkOut(cart cart, string address, string postalCode)
        {   
            if (cart == null)
                return null;

            if (!cart.CartItems.Any())
                return null;

            //create a Order Entity
            var orderItems = cart.CartItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.quanity,
                
                Price = item.price,
            }).ToList();
            _context.cartItem.RemoveRange(cart.CartItems);
            cart.CartItems.Clear();

            cart.Total = 0;

            Order newOrder = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                Address = address,
                userId = cart.UserID,
                PostalCode = postalCode,
                date = DateTime.UtcNow,
                OrderItems = orderItems
            };


            _context.Orders.Add(newOrder);

            _context.SaveChanges();

            return newOrder;
        }

        public OrderDto ConvertToOrderDto(Order order)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> getAllOrders(string userId)
        {
            return _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Photos)
            .AsNoTracking()
            .Where(o => o.userId == userId)
            .AsEnumerable()          // ← pull into memory first
            .Select(o => (OrderDto)o) // ← then cast
            .ToList();
        }
    }
}