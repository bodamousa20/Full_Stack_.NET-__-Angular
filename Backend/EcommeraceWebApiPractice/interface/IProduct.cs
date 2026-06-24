using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Data.Dto;

namespace EcommeraceWebApiPractice
{
    public interface IProduct
    {
        Task<IReadOnlyList<Product>> GetAllAsync(filterDto filter,int pageNumber);
        Task<Product?> GetById(int id);
        Task<Product> AddAsync(Product entity);
        Task<Product?> UpdateAsync(int id ,Product entity);
        Task<bool> DeleteAsync(int id);
        int countProduct();

    }
}
