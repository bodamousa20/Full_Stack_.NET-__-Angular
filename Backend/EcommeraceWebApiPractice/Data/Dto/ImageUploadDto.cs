using Microsoft.AspNetCore.Mvc;

namespace EcommeraceWebApiPractice.Data.Dto
{

    public class ImageUploadDto
    {
        [FromForm]
        public IFormFile File { get; set; }
    }
}
