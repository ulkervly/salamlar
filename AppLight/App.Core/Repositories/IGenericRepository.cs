using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }
        Task Createasync(T entity);
        void Delete(T entity);
        Task<int> Commitasync();
        IQueryable<T> GetAll(params string[]? includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression, params string[]? includes);
        Task<T> GetSingleByIdAsync(Expression<Func<T, bool>>? expression, params string[]? includes);
    }
}
