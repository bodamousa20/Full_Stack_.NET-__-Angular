using Ecom.Core;
using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;
using EcommeraceWebApiPractice.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace EcommeraceWebApiPractice.Services
{
    public class ProductServices : IProduct
    {
        private readonly AppDbContext context;

        public ProductServices(AppDbContext _context)
        {
            this.context = _context;
        }

        public async Task<Product?> AddAsync(Product entity)
        {
            //check for that catId
            bool isExist = context.categories.Any(p=>p.id == entity.CategoryId);
            if (!isExist)
                return null;
           await context.products.AddAsync(entity);
           await context.SaveChangesAsync();
            return entity;
        }

        public int countProduct()
        {
            return context.products.AsNoTracking().Count();
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var product =  context.products.Find(id);
            if (product == null)
                return false;
             context.products.Remove(product);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(filterDto filter,int pageNumber)
        {
            //applying filter
            var products = context.products.Include(p => p.Photos).AsQueryable();
            if (filter.query != null && filter.query != "")
                products = products.Where(p => p.Name.Contains(filter.query));

            if (filter.CategoryId.HasValue)
                products = products.Where(p => p.CategoryId == filter.CategoryId);

            if (filter.MinPrice.HasValue)
                products = products.Where(p => p.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= filter.MaxPrice);

            if (filter.Sort.HasValue  && filter.Sort == true)
                products = products.OrderBy(p => p.Price);
            else
                products = products.OrderByDescending(p => p.Price);

            int pageSize = 15;
            int Skip = (pageNumber - 1) * pageSize;


            //apply Paginination
            return products.Skip(Skip).Take(pageSize).ToList();
        }

        public async Task<Product?> GetById(int id)
        {
           return await context.products.Include(p => p.Photos)
                                    .FirstOrDefaultAsync(p=>p.id == id);
        }

        public async Task<Product?> UpdateAsync(int id,Product entity)
        {   if(context.categories.Find(entity.CategoryId)== null)
            {
                return null;
            }
            var existingProduct = context.products.Find(id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = entity.Name;
            existingProduct.Description = entity.Description;
            existingProduct.Price = entity.Price;
            existingProduct.CategoryId = entity.CategoryId;
            await context.SaveChangesAsync();
          return await context.products
        .Include(p => p.Photos)
        .FirstOrDefaultAsync(p => p.id == id);
        }



    }
}
