using System;
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

        public OrderSummaryDTO GetSummary(int userId)
        {
            var watches = ReadAll(userId);
            var totalPrice = watches.Sum(_ => _.Watch.Price * _.Count);
            var shippingPrice = 25;

            return new OrderSummaryDTO
            {
                Shipping = shippingPrice,
                SubTotal = totalPrice,
                Total = shippingPrice + totalPrice,
                Count = watches.Sum(_ => _.Count)
            };
        }

        public void MoveCartToOrder(int userId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var checkedWatches = ReadAll(userId).Where(_ => _.IsChecked);

                    var order = new Order
                    {
                        UserId = userId,
                        PaymentMethod = "qwer",
                        Count = checkedWatches.Sum(_ => _.Count),
                        TotalPrice = checkedWatches.Sum(_ => _.Count * _.Watch.Price),
                        OrderDate = DateTime.Now,
                        User = _context.Users.Find(userId),
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public OrderSummaryDTO UpdateSummary(int userId, int watchId, bool isChecked)
        {
            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(_ => _.UserId == userId && _.WatchId == watchId);

            if (shoppingCart is not null)
            {
                shoppingCart.IsChecked = isChecked;
                _context.SaveChanges();
            }

            return GetSummary(userId);
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

        public void ChangeCount(int userId, int watchId, Operations operation)
        {
            var changedItem = _context.ShoppingCarts.FirstOrDefault(_ => _.UserId == userId && _.WatchId == watchId);
            changedItem.Count = operation switch
            {
                Operations.Minus => changedItem.Count - 1,
                Operations.Plus => changedItem.Count + 1,
                _ => changedItem.Count
            };

            _context.SaveChanges();
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
