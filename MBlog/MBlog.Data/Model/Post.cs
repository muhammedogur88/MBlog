using System;

namespace MBlog.Data.Model
{
    public class Post
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public string Content { get; set; }
        public Post Parent { get; set; }
        public ApplicationUser PostedBy { get; set; }
        public DateTime PostedOn { get; set; }

    }
}
