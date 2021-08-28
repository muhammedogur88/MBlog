using Microsoft.AspNetCore.Mvc;

namespace MBlog.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(); 
        }
    }
}
