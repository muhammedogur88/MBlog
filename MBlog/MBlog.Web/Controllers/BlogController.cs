using MBlog.Web.BusinessManagers.Interfaces;
using MBlog.Web.Models.BlogViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MBlog.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManagers _businessBlogManager;

        public BlogController(IBlogBusinessManagers businessBlogManager)
        {
            _businessBlogManager = businessBlogManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createBlogViewModel)
        {
            await _businessBlogManager.CreateBlog(createBlogViewModel, User);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await _businessBlogManager.GetEditViewModel(id, User);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            var action = await _businessBlogManager.UpdateBlog(editViewModel, User);

            if (action.Result is null)
            {
                return RedirectToAction("Edit", new { editViewModel.Blog.Id });
            }
            return action.Result;
        }
    }
}
