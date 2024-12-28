using System.Diagnostics;
using KuaforYonetim.Models;
using Microsoft.AspNetCore.Mvc;

namespace KuaforYonetim.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {

            return View();
        }
        public IActionResult About()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
     
}
