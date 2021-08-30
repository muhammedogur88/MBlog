using MBlog.Web.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MBlog.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManagers _adminBusinessManagers;

        public AdminController(IAdminBusinessManagers adminBusinessManagers)
        {
            _adminBusinessManagers = adminBusinessManagers;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _adminBusinessManagers.GetAdminDashBoard(User));
        }
    }
}
