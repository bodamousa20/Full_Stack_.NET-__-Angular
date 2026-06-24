using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Entites;

namespace EcommeraceWebApiPractice.Data.Dto
{
    public class FavouriteResponseDto
    {
        public int id { set; get; }
        public string userId { set; get; }
        public List<productResponse> products { get; set; } 

    }
}
