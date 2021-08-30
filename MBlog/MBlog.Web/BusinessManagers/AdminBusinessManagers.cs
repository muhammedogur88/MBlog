using MBlog.Data.Model;
using MBlog.Service.Interfaces;
using MBlog.Web.BusinessManagers.Interfaces;
using MBlog.Web.Models.AdminViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MBlog.Web.BusinessManagers
{
    public class AdminBusinessManagers : IAdminBusinessManagers
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;

        public AdminBusinessManagers(UserManager<ApplicationUser> userManager, IBlogService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }
        public async Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);

            return new IndexViewModel
            {
                Blogs = _blogService.GetBlogs(applicationUser)
            };
        }
    }
}

