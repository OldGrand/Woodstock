using System.Linq;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Extensions;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Services
{
    public class ShoppigCartService : IShoppingCartService
    {
        private readonly WoodstockDbContext _context;

        public ShoppigCartService(WoodstockDbContext context) 
        {
            _context = context;
        }

        public IQueryable<ShoppingCartDTO> ReadUserCart(int userId)
        {
            return from cart in _context.ShoppingCarts
                   where cart.UserId == userId
                   select cart.ToDTO();
        }

        public async Task AddToCartAsync(int userId, int watchId)
        {
            var record = _context.ShoppingCarts.FirstOrDefault(_ => _.UserId == userId && _.WatchId == watchId);

            if (record is null)
            {
                var newCartRecord = new ShoppingCart
                {
                    Count = 1,
                    UserId = userId,
                    WatchId = watchId
                };
                _context.ShoppingCarts.Add(newCartRecord);
            }
            
            record.Count++;
            await _context.SaveChangesAsync();
        }
    }
}
