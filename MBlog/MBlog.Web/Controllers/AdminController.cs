using Microsoft.AspNetCore.Mvc;

namespace MBlog.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
