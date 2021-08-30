using MBlog.Data;
using MBlog.Data.Model;
using MBlog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.Service
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _db;

        public BlogService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Blog GetBlog(int blogId)
        {
            return _db.Blogs.FirstOrDefault(blog => blog.Id == blogId);
        }

        public IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser)
        {
            return _db.Blogs
                .Include(blog => blog.CreatedBy)
                .Include(blog => blog.UpdatedBy)
                .Include(blog => blog.Posts)
                .Where(blog => blog.CreatedBy == applicationUser);
        }

        public async Task<Blog> Add(Blog blog)
        {
            _db.Add(blog);
            await _db.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> Update(Blog blog)
        {
            _db.Update(blog);
            await _db.SaveChangesAsync();
            return blog;
        }
    }
}
