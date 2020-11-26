using System.Linq;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Extensions;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL;
using Woodstock.DAL.Entities;
using Woodstock.Infrastructure;

namespace Woodstock.BLL.Services
{
    public class ShoppigCartService : IShoppingCartService
    {
        private readonly WoodstockDbContext _context;

        public ShoppigCartService(WoodstockDbContext context) =>
            _context = context;

        public IQueryable<ShoppingCartDTO> ReadUserCart(int userId)
        {
            return from cart in _context.ShoppingCarts
                   where cart.UserId == userId
                   select cart.ToDTO();
        }

        public IQueryable<ShoppingCartDTO> ChangeCount(int userId, Operations operation)
        {
            var cartDTOs = ReadUserCart(userId);

            var changedItem = cartDTOs.FirstOrDefault(_ => _.Id == userId);
            changedItem.Count = operation switch
            {
                Operations.Minus => changedItem.Count - 1,
                Operations.Plus => changedItem.Count + 1,
                _ => changedItem.Count
            };

            _context.SaveChanges();
            return cartDTOs;
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
