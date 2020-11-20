using System.Threading.Tasks;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedResultDTO<WatchDTO>> GetItemsOnPage(int pageNum, int itemsOnPage);
    }
}
