using Microsoft.AspNetCore.Mvc;

namespace TempusHiring.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return PartialView("_NotificationPartial", "Access Denied");
        }
    }
}
