using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
