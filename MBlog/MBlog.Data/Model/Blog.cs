using System;
using System.Collections.Generic;

namespace MBlog.Data.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime Createdon { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
