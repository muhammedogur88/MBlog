using MBlog.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MBlog.Service.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
        Blog GetBlog(int blogId);
        IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser);
        Task<Blog> Update(Blog blog);
    }
}
