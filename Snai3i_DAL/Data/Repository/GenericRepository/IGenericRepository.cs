using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null);

        public Task<T?> GetByIdAsync(object id);

        public Task InsertAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(object id);

        public Task saveAsync();
    }
}
