using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IHomeViewModelService
    {
        Task<HomeViewModel> GetHomeViewModelServiceAsync(int? brandId, int? categoryId, int pageId);
    }
}
