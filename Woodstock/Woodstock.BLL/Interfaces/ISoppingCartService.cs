using System.Linq;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface ISoppingCartService
    {
        IQueryable<ShoppingCartDTO> ReadAll();
        IQueryable<ShoppingCartDTO> ReadUserCart(int id);
    }
}
