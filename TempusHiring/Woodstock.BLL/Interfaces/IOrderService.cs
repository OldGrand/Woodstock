using System.Linq;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddItemsToOrder(int userId);
        IQueryable<OrderDTO> GetOrders(int userId);
    }
}
