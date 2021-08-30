using MBlog.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MBlog.Web.Authorization
{
    public class BlogAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Blog>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Blog resource)
        {
            var applicationUser = await _userManager.GetUserAsync(context.User);

            if (requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name || applicationUser == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }
        }
    }
}
