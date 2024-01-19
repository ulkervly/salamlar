using App.MVC;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.MVC.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

       
    }
}