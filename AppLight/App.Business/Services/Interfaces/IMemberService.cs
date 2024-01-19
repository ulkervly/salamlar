using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Interfaces
{
    public interface IMemberService
    {
        Task CreateAsync(Member member);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Member member);
        Task <List<Member>> GetAllAsync(Expression<Func<Member,bool>> expression=null,params string[]? includes);
        Task<Member> GetByIdAsync(Expression<Func<Member, bool>> expression = null, params string[]? includes);
    }
}
