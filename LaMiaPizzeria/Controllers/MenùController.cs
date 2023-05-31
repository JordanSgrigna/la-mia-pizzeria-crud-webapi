using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class MenùController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
