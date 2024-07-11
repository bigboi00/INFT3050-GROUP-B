using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
