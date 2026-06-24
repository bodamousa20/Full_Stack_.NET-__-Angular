using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;

namespace EcommeraceWebApiPractice.Services
{
    public class CategoryServices : ICategory
    {
        private readonly AppDbContext context;

        public CategoryServices(AppDbContext context)
        {
            this.context = context;
        }

 
        public async Task<Category> AddAsync(Category entity)
        {
           context.categories.Add(entity);
           await context.SaveChangesAsync();
           return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
          var category =  context.categories.FirstOrDefault(p => p.id == id);
            if (category == null)
                return false;
            context.categories.Remove(category);
            await context.SaveChangesAsync();

            return true;

        }

        public async Task<List<Category>> GetAllAsync()
        {
           return context.categories.ToList();
        }

        public async Task<Category?> GetById(int id)
        {
            var category = context.categories.Find(id);
            if (category == null)
                return null;
                return category;

        }

        public async Task<Category?> UpdateAsync(int id, Category entity)
        {
            var category = context.categories.Find(id);
            if (category == null)
                return null;

            category.name = entity.name;
            category.description = entity.description;
            await context.SaveChangesAsync();
            return category;
        }


    }
}
