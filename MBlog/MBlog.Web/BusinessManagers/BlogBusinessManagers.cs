using MBlog.Data.Model;
using MBlog.Service.Interfaces;
using MBlog.Web.Authorization;
using MBlog.Web.BusinessManagers.Interfaces;
using MBlog.Web.Models.BlogViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MBlog.Web.BusinessManagers
{
    public class BlogBusinessManagers : IBlogBusinessManagers
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironments;
        private readonly IAuthorizationService _authorizationService;

        public BlogBusinessManagers(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnvironments, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _blogService = blogService;
            _webHostEnvironments = webHostEnvironments;
            _authorizationService = authorizationService;
        }
        public async Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog blog = createBlogViewModel.Blog;

            blog.Createdon = DateTime.Now;
            blog.CreatedBy = await _userManager.GetUserAsync(claimsPrincipal);
            blog.UpdatedOn = DateTime.Now;
            blog.UpdatedBy = await _userManager.GetUserAsync(claimsPrincipal);


            blog = await _blogService.Add(blog);

            string webRootPath = _webHostEnvironments.WebRootPath;
            string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

            EnsureFolderExists(pathToImage);

            using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                await createBlogViewModel.HeaderImage.CopyToAsync(fileStream);
            }

            return blog;

        }

        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var blog = _blogService.GetBlog(editViewModel.Blog.Id);
            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await _authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);
            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            blog.IsPublished = editViewModel.Blog.IsPublished;
            blog.Content = editViewModel.Blog.Content;
            blog.Title = editViewModel.Blog.Title;
            blog.UpdatedOn = DateTime.Now;

            if (editViewModel.HeaderImage != null)
            {
                string webRootPath = _webHostEnvironments.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

                EnsureFolderExists(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await editViewModel.HeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Blog = await _blogService.Update(blog)
            };
        }



        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();
            var blogId = id.Value;

            var blog = _blogService.GetBlog(blogId);

            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await _authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);
            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            return new EditViewModel
            {
                Blog = blog
            };

        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }
        private void EnsureFolderExists(string path)
        {
            string directoryName = Path.GetDirectoryName(path);

            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}