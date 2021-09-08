using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _data;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _data = _context.Set<T>();
        }
            
        public async Task Create(T entity)
        {
            await _data.AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            await Task.Run(() => _data.Remove(entity)) ;
        }

        public async Task<T> Find(Expression<Func<T, bool>> expression,
                                  Func<IQueryable<T>, IIncludableQueryable<T, Object>> includes = null)
        {
            IQueryable<T> query = _data;
            if (includes !=null )
            {              
                query = includes(query);
            }
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<ICollection<T>> FindAll(Expression<Func<T, bool>> expression = null, 
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                  Func<IQueryable<T>, IIncludableQueryable<T, Object>> includes = null)
        {
            IQueryable<T> query = _data;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null )
            {            
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);

            }
            return await query.ToListAsync();
        }

        public async Task<bool> IsExists(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _data;
            return await query.AnyAsync(expression);
        }               

        public async Task Update(T entity)
        {
            await Task.Run(() => _data.Update(entity));
        }
    }
}
