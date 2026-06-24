using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Entites;
using EcommeraceWebApiPractice.@interface;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;

        public CartController(ICartServices _cartServices)
        {
            this.cartServices = _cartServices;
        }


        //get Cart by  userId from token
        // 
        [HttpGet]
        public IActionResult viewCart()
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          var cart =  cartServices.ViewCart(userId);
            if (cart == null)
                return Ok(new { message = "Cart Empty" });
            else
            {
                CartDto cartDto = cartServices.ConvertToDto(cart);

                return Ok(new { message = "Success", data = cartDto });
            }
        }
        [HttpPost]
        public IActionResult AddToCart(CartItemRequstDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart  =  cartServices.AddCartItem(userId, dto.ProductId, dto.quanity);
            if (cart == null)
                return BadRequest();
            else
            {
                List<CartItemDto> cartItemDtos = new List<CartItemDto>();
                foreach (var cartItem in cart.CartItems)
                {
                    productResponse productResponse = new productResponse
                    {
                        ID = cartItem.Product.id,
                        Description = cartItem.Product.Description,
                        CategoryId = cartItem.Product.CategoryId,
                        Name = cartItem.Product.Name,
                        Price = cartItem.Product.Price,
                        thunmailImage = cartItem.Product.Photos?.ElementAt(0)?.imageUrl
                    };
                    CartItemDto cartItemDto = new CartItemDto
                    {
                        id = cartItem.id,
                        price = cartItem.price,
                        quanity = cartItem.quanity,
                        Product = productResponse,
                        ProductId = cartItem.ProductId,
                    };
                    cartItemDtos.Add(cartItemDto);

                }
        
                CartDto cartDto = new CartDto
                {
                    Id = cart.Id,
                    CreatedAt = cart.CreatedAt,
                    Total = cart.Total,
                    UserID = cart.UserID,
                    cartItemDtos = cartItemDtos
                };

                return Ok(new { message = "Success", data = cartDto });
            }
        }

        [HttpDelete("cart-item/{CartItemId:int}")]
        public IActionResult deleteCartItem(int CartItemId)
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var cart =  cartServices.DeleteCartItem(CartItemId, userId);
            if (cart == null)
                return Ok(new { message = "Cart Empty" });
            else
            {
                CartDto cartDto = cartServices.ConvertToDto(cart);

                return Ok(new { message = "deleted", data = cartDto });
            }

        }
        [HttpDelete]
        public IActionResult deleteCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = cartServices.ClearCart(userId);
            return Ok(new { message = "deleted", data = cart });
        }

        [HttpPost("increase-qnty/{productId:int}")]
        public IActionResult IncreaseQuantity(int productId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cart cart =  cartServices.IncreaseQuantity(userId, productId);
            if (cart == null)
                return BadRequest();
            CartDto cartDto = cartServices.ConvertToDto(cart);

            return Ok(new { message = "updated", data = cartDto });

        }
        [HttpPost("decrease-qnty/{productId:int}")]
        public IActionResult decreaseQnty(int productId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cart cart = cartServices.DecreaseQuantity(userId, productId);
            if (cart == null)
                return BadRequest();
            CartDto cartDto = cartServices.ConvertToDto(cart);

            return Ok(new { message = "updated", data = cartDto });

        }



        [HttpPost("checkout")]
        public IActionResult UserCheckout([FromBody] Checkout checkoutDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var cart = cartServices.ViewCart(userId);

            if (cart == null)
                return BadRequest("Cart not found");

            if (!cart.CartItems.Any())
                return BadRequest("Cart is empty");

            Order newOrder = cartServices.checkOut(cart, checkoutDto.address, checkoutDto.postalcode);

            if (newOrder == null)
                return BadRequest("Checkout failed");

            return Ok((OrderDto)newOrder);
        }
    

        [HttpGet("orders")]
        public IActionResult getAllOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();



            List<OrderDto> allOrders = cartServices.getAllOrders(userId);

            if (allOrders == null)
                return BadRequest("failed");
           
            return Ok(new { orders = allOrders });
        }
    }

}
