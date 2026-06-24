using Ecom.Core.Entites;

namespace EcommeraceWebApiPractice
{
    public interface ICategory
    {

        Task<List<Category>> GetAllAsync();
        Task<Category?> GetById(int id);
        Task<Category> AddAsync(Category entity);
        Task<Category?> UpdateAsync(int id, Category entity);
        Task<bool> DeleteAsync(int id);

    }
}
