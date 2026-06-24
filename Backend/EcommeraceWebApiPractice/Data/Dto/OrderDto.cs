// Data/Dto/OrderDto.cs
namespace EcommeraceWebApiPractice.Data.Dto
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public string UserId { set; get; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public DateTime date { set; get; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string ThumbnailImage { get; set; } 
    }
}