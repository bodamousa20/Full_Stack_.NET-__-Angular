using Ecom.Core.Entites;

namespace EcommeraceWebApiPractice.Entites
{
    public class Favourite
    {
        public int id { set; get; }
        public string userId { set; get; }
        public AccountUser user { set; get; }
        public List<Product> products { get; set; } = new List<Product>();
    }
}
