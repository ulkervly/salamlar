using App.Business.Exceptions;
using App.Business.Services.Interfaces;
using App.Core.Entities;
using App.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.MVC.Areas.manage.Controllers
{
    //[Authorize]
    [Area("manage")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _memberService.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);

            }
            try
            {
                await _memberService.CreateAsync(member);
            }
            catch (MemberInvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            catch (Exception ex)
            {
                throw;
                
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var member =await _memberService.GetByIdAsync(x=>x.Id==id && !x.IsDeleted);
            if (member is null) return View();
            return View(member);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(Member member)
        {
           
            try
            {
                await _memberService.UpdateAsync(member);
            }
            catch(MemberInvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                throw;

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetByIdAsync(x => x.Id == id && !x.IsDeleted);
            if (member is null) return View();
            return View(member);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(Member member)
        {
            try
            {
                await _memberService.DeleteAsync(member.Id);
            }
           
            catch (Exception ex)
            {
                throw;

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
