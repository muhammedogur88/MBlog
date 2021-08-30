using MBlog.Data;
using MBlog.Data.Model;
using MBlog.Service;
using MBlog.Service.Interfaces;
using MBlog.Web.Authorization;
using MBlog.Web.BusinessManagers;
using MBlog.Web.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Net;

namespace MBlog.Web.Configuration
{
    public static class AppServices
    {
        public static void AddDefaultServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogBusinessManagers, BlogBusinessManagers>();
            services.AddScoped<IAdminBusinessManagers, AdminBusinessManagers>();

            services.AddScoped<IBlogService, BlogService>();

        }

        public static void AddCustomAuthorizaiton(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, BlogAuthorizationHandler>();
        }
    }
}
