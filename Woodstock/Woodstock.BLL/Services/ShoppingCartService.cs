using System.Collections.Generic;
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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly WoodstockDbContext _context;

        public ShoppingCartService(WoodstockDbContext context) =>
            _context = context;

        private IQueryable<ShoppingCart> ReadAll(int userId)
        {
            return from cart in _context.ShoppingCarts
                   where cart.UserId == userId
                   select cart;
        }

        public OrderSummaryDTO UpdateSummry(int userId, int watchId)
        {
            var watchesPrice = (from cart in ReadAll(userId)
                                where cart.WatchId == watchId
                                select cart).Sum(_ => _.Watch.Price);
            var shippingPrice = 25;

            return new OrderSummaryDTO
            {
                Shipping = shippingPrice,
                SubTotal = watchesPrice,
                Total = shippingPrice + watchesPrice,
            };
        }

        public IQueryable<ShoppingCartDTO> ReadUserCart(int userId)
        {
            return from cart in ReadAll(userId)
                   select cart.ToDTO();
        }

        public void DeleteFromCart(int cartId)
        {
            var shoppingCart = _context.ShoppingCarts.Find(cartId);

            if (shoppingCart is not null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ShoppingCartDTO> ChangeCount(int userId, int watchId, Operations operation)
        {
            var shoppingCarts = ReadAll(userId).ToList();

            var changedItem = shoppingCarts.FirstOrDefault(_ => _.WatchId == watchId);
            changedItem.Count = operation switch
            {
                Operations.Minus => changedItem.Count - 1,
                Operations.Plus => changedItem.Count + 1,
                _ => changedItem.Count
            };

            _context.SaveChanges();
            return shoppingCarts.Select(_ => _.ToDTO());
        }

        public async Task AddToCartAsync(int userId, int watchId)
        {
            var record = _context.ShoppingCarts.FirstOrDefault(_ => _.UserId == userId && _.WatchId == watchId);

            if (record is null)
            {
                record = new ShoppingCart
                {
                    UserId = userId,
                    WatchId = watchId
                };
                _context.ShoppingCarts.Add(record);
            }
            
            record.Count++;
            await _context.SaveChangesAsync();
        }
    }
}
