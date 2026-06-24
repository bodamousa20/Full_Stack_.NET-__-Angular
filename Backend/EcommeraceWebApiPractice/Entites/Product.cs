using EcommeraceWebApiPractice.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entites
{
    public class Product:BaseEnitity<int>
    {
        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public int CategoryId { set; get; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { set; get; }

        public virtual List<Photo> Photos { set; get; }
        public List<Favourite> Favourites { get; set; } 

    }
}
