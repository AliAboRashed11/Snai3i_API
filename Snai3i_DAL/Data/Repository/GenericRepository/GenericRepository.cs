using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SnaiiDBContext Context;

        protected readonly DbSet<T> table;
        public GenericRepository(SnaiiDBContext _context)
        {
            Context = _context;
            table = _context.Set<T>();
        }

        // generic get all async
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> perdicate, string? Includeword)
        {

            IQueryable<T> query = table;
            if (perdicate != null)
            {
                query = query.Where(perdicate);
            }
            if (Includeword != null)
            {
                //split =>  , هيجيلك كلمه او اكتر من كلمه هتفصل بنهم ب 
                //context.tool.include("Size, Revie") فا انا بقوله ممكن يجيلك كلمه واحده او اكتر من كلمه فا لما يجيلك اكتر من كلمه اعمل ما بنهم الفصله دى وخد كل كلمه لوحدها
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }


            return await table.AsNoTracking().ToListAsync();
        }

        // generic getbyid async 

        public async Task<T?> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }// generic insert async 

        public async Task InsertAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        // generic update async 

        public async Task UpdateAsync(T entity)
        {
            table.Update(entity);
        }

        // generic delete async 
        public async Task DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                table.Remove(entity);
            }
        }

        // generic saveasync 
        public async Task saveAsync()
        {
            await Context.SaveChangesAsync();
        }

    }
}
