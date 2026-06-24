using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Entites;

namespace EcommeraceWebApiPractice.Data.Dto
{
    public class CartDto
    {
        public int Id { set; get; }
        public string UserID { set; get; }
        public decimal Total { set; get; }
        public DateTime CreatedAt { set; get; }
        public List<CartItemDto>cartItemDtos { set; get; }


    }

    public class CartItemDto
    {
        public int id { set; get; }
        public int ProductId { get; set; }
        public int quanity { set; get; }
        public decimal price { set; get; }

        public productResponse Product { set; get; }
    }
}
