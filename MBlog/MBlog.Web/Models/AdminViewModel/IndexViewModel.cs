using MBlog.Data.Model;
using System.Collections.Generic;

namespace MBlog.Web.Models.AdminViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
