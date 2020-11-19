using System.Threading.Tasks;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface IShopService
    {
        Task<PagedResultDTO<UserDTO>> GetItemsOnPage(int pageNum, int itemsOnPage);
    }
}
