using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService _services)
        {
            this.photoService = _services;
        }

        [HttpPost("{id:int}")]
        [Consumes("multipart/form-data")]  
        public async Task<IActionResult> UploadImage(int id ,[FromForm] ImageUploadDto request)
        {
            fileValidation(request.File);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Photo? photo = await photoService.UploadAsync(id, request.File);
            if (photo == null)
                return BadRequest("The Product is Not found");

            return Ok(new { message = "Uploaded Successfully", data = photo.imageUrl });




        }


        private void fileValidation(IFormFile file)
        {
            var allowedMethod  = new string[] { ".jpg", ".jpeg", ".png" };
           if(! allowedMethod.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("extention", "The file Extention is not allowed ,please try another File ");
            }
            if (file.Length > 10485760)
                ModelState.AddModelError("File-size", "The File exceeded the allowed size 10MB");

        }

 

    }
}
