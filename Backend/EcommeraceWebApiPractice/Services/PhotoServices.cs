using Azure.Core;
using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;

namespace EcommeraceWebApiPractice.Services
{
    public class PhotoServices : IPhotoService
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env; // ← gives us wwwroot path

        public PhotoServices(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }


        public Task<bool> DeleteAsync(int photoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Photo?> UploadAsync(int productId, IFormFile file)
        {
            Product ?product =  context.products.Find(productId);
            if (product == null)
                return null;
            // 2 - generate unique file name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";

            // 3 - build the folder path
            string folderPath = Path.Combine(env.WebRootPath, "images", "products");

            // 4 - create folder if not exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 5 - save the file physically
            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 6 - save URL to database
            Photo photo = new Photo
            {   
                fileExtention = Path.GetExtension(file.FileName),
                fileName = file.FileName,
                fileSize = file.Length,
                imageUrl = $"images/products/{fileName}",
                ProductId = productId
            };

            await context.photos.AddAsync(photo);
            await context.SaveChangesAsync();

            return photo;
        }
    }
}
