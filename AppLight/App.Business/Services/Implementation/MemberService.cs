using App.Business.Exceptions;
using App.Business.Extensions;
using App.Business.Services.Interfaces;
using App.Core.Entities;
using App.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IMemeberRepository _memeberRepository;
        private readonly IWebHostEnvironment _env;

        public MemberService(IMemeberRepository memeberRepository,IWebHostEnvironment env)
        {
            _memeberRepository = memeberRepository;
            _env = env;
        }
        public async Task CreateAsync(Member member)
        {
            if (member==null)
            {
                throw new NullReferenceException();

            }
            if (member.ImageFile.ContentType!="image/jpeg" && member.ImageFile.ContentType != "image/png")
            {
                throw new MemberInvalidImage("ImageFile","Format must be Jpg or png");
            }
            if (member.ImageFile.Length>2097474)
            {
                throw new MemberInvalidImage("ImageFile","Size must lower 2 mb");
            }
            member.ImageUrl = member.ImageFile.Savefile(_env.WebRootPath,"uploads/memebers");
            
            member.CreatedDate = DateTime.UtcNow;
            member.UpdatedDate = DateTime.UtcNow;
            await _memeberRepository.Createasync(member);
            await _memeberRepository.Commitasync();
        }

        public async Task DeleteAsync(int Id)
        {
            if (Id < 0) throw new NullReferenceException();
            var member = await _memeberRepository.GetSingleByIdAsync(x=>x.Id==Id && !x.IsDeleted);
            if (member is null)
            {
                throw new NullReferenceException();
            }
             _memeberRepository.Delete(member);
            await _memeberRepository.Commitasync();
        }

        public async Task<List<Member>> GetAllAsync(Expression<Func<Member, bool>> expression = null, params string[]? includes)
        {
            return await _memeberRepository.GetAllWhere(expression,includes).ToListAsync();
        }

        public async Task<Member> GetByIdAsync(Expression<Func<Member, bool>> expression = null, params string[]? includes)
        {
            return await _memeberRepository.GetSingleByIdAsync(expression, includes);
        }

        public async Task UpdateAsync(Member member)
        {
            var exmember = await _memeberRepository.GetSingleByIdAsync(x=>x.Id==x.Id && !x.IsDeleted);
            if (exmember is null)
            {
                throw new Exception();
            }
            
            if (member.ImageFile.ContentType != "image/jpeg" && member.ImageFile.ContentType != "image/png")
            {
                throw new MemberInvalidImage("ImageFile", "Format must be jpeg or png");
            }
            if (member.ImageFile.Length > 2097474)
            {
                throw new MemberInvalidImage("ImageFile", "Size must lower 2 mb");
            }
            FileManger.DeleteFile(_env.WebRootPath,"uploads/memebers", exmember.ImageUrl);
            exmember.ImageUrl = member.ImageFile.Savefile(_env.WebRootPath, "uploads/memebers");

            exmember.Name = member.Name;
            exmember.Prof=member.Prof;
            
            exmember.UpdatedDate = DateTime.UtcNow;

            await _memeberRepository.Commitasync();
        }
    }
}
