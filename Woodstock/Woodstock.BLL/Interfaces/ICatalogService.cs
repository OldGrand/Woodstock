using System.Linq;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface ICatalogService
    {
        IQueryable<WatchDTO> ReadAll();
    }
}
