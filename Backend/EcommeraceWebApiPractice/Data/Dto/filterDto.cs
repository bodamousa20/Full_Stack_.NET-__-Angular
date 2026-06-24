namespace EcommeraceWebApiPractice.Data.Dto
{
    public class filterDto
    {       public string? query { get; set; }
            public int? CategoryId { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public bool? Sort { get; set; } 
    }
}
