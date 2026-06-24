using Ecom.Core.Entites;

namespace EcommeraceWebApiPractice.Data.Dto
{
    public class productRequestDto
    {

        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public int CategoryId { set; get; }

    }
    public class productDetailsResponse
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public int CategoryId { set; get; }

        public List<PhotoDto> Photos { set; get; }

    }
    public class productResponse
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public int CategoryId { set; get; }

        public string thunmailImage { set; get; }
    }

}
