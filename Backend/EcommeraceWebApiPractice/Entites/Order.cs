// Entities/Order.cs
using EcommeraceWebApiPractice.Data.Dto;

namespace EcommeraceWebApiPractice.Entites
{
    public class Order
    {
        public string OrderId { get; set; }
        public string userId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public DateTime date { set; get; }
        public List<OrderItem> OrderItems { get; set; } = new();

        public static implicit operator OrderDto(Order order) => new()
        {
            OrderId = order.OrderId,
            UserId = order.userId,
            Address = order.Address,
            date = order.date,
            PostalCode = order.PostalCode,
            OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price,
                ThumbnailImage = oi.Product?.Photos?.FirstOrDefault()?.imageUrl,
                ProductName = oi.Product.Name

            }).ToList() ?? new()
        };
    }
}