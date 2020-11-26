using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;
using Woodstock.Infrastructure;

namespace Woodstock.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        IQueryable<ShoppingCartDTO> ReadUserCart(int id);
        Task AddToCartAsync(int userId, int watchId);
        IEnumerable<ShoppingCartDTO> ChangeCount(int id, Operations operation);
    }
}
