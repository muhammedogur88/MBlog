using MBlog.Data.Model;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MBlog.Web.Models.BlogViewModel
{
    public class CreateViewModel
    {
        [Required, Display(Name ="Header Image")]
        public IFormFile HeaderImage { get; set; }
        public Blog Blog { get; set; }
    }
}
