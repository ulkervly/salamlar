
using App.Core.Entities;
using App.Core.Repositories;
using App.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<int> Commitasync()
        {
          return await _context.SaveChangesAsync();
        }

        public async Task Createasync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public  IQueryable<T> GetAll(params string[]? includes)
        {
            var query=_getQuery(includes);
            return  query;

        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression, params string[]? includes)
        {
            var query=_getQuery(includes);
            return expression is not null ? query.Where(expression) : query;
        }

        public async Task<T> GetSingleByIdAsync(Expression<Func<T, bool>>? expression, params string[]? includes)
        {
           var query=_getQuery(includes);
            return expression is null ? await query.Where(expression).FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }

        private IQueryable<T> _getQuery(params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes!=null && includes.Length>0)
            {
               
                foreach (var item in includes)
                {
                    query=query.Include(item);

                }
            }
            return query;

        }
    }
}
