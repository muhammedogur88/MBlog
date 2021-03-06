using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MBlog.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Factory.StartNew(() => { return View(); });
        }
    }
}
