using Microsoft.EntityFrameworkCore;
using System.Linq;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Extensions;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL;

namespace Woodstock.BLL.Services
{
    public class ShoppigCartService : ISoppingCartService
    {
        private readonly WoodstockDbContext _context;

        public ShoppigCartService(WoodstockDbContext context) 
        {
            _context = context;
        }

        public IQueryable<ShoppingCartDTO> ReadAll() =>
            (from shoppingCart in _context.ShoppingCarts select shoppingCart.ToDTO()).AsNoTracking();

        public IQueryable<ShoppingCartDTO> ReadUserCart(int id)
        {
            return from cart in ReadAll()
                   where cart.Id == id
                   select cart;
        }
    }
}
