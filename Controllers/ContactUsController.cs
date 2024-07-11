using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
