using Ecom.Core.Entites;

namespace EcommeraceWebApiPractice
{
    
        public interface IPhotoService
        {
            Task<Photo?> UploadAsync(int productId, IFormFile file);
            Task<bool> DeleteAsync(int photoId);
        }
    
}
