using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EcommeraceWebApiPractice.Entites
{
    public class cart
    {
        public int Id { set; get; }
        public string UserID { set; get; }
        public decimal Total { set; get; }
        public DateTime CreatedAt { set; get; }

        public AccountUser User { set; get; }
        public List<cartItem> CartItems { set; get; }
    }

    public class cartItem
    {
        public int id { set; get; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int quanity { set; get; }
        public decimal price { set; get; }

        public Product Product { set; get; }
        public cart Cart { set; get; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderId { get; set; }  // FK to Order
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}

