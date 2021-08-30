using MBlog.Web.Models.AdminViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MBlog.Web.BusinessManagers.Interfaces
{
    public interface IAdminBusinessManagers
    {
        Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal);
    }
}
