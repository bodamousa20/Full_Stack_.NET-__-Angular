using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Entites;

namespace EcommeraceWebApiPractice.@interface
{
    public interface ICartServices
    {
        public cart ViewCart(string userId);
        public cart AddCartItem(string userId, int productId, int quantity);
        public cart DeleteCartItem(int cartItemId, string userId);
        public cart ClearCart(string userId);
        public CartDto ConvertToDto(cart cart);
        public cart IncreaseQuantity(string userId, int cartItemId);
        public cart DecreaseQuantity(string userId, int cartItemId);
        public Order checkOut(cart cart, string address, string postalCode);
        public OrderDto ConvertToOrderDto(Order order);
        List<OrderDto> getAllOrders(string userId);
    }
}