using System.Linq;
using System.Threading.Tasks;
using TempusHiring.BusinessLogic.DTOs;
using TempusHiring.Common;

namespace TempusHiring.BusinessLogic.Interfaces
{
    public interface IShoppingCartService
    {
        IQueryable<ShoppingCartDTO> ReadUserCart(int id);
        Task AddToCartAsync(int userId, int watchId);
        void ChangeCount(int userId, int watchId, Operations operation);
        void DeleteFromCart(int cartId);
        void UpdateSelection(int userId, int watchId, bool isChecked);
        OrderSummaryDTO GetSummary(int userId);
    }
}
