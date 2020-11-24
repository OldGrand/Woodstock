using Microsoft.EntityFrameworkCore;
using System.Linq;
using Woodstock.BLL.Abstract;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Extensions;
using Woodstock.DAL;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Services
{
    public class ShoppigCartService : ApplicationContextService<ShoppingCart>
    {
        public ShoppigCartService(WoodstockDbContext context) : base(context) { }

        public IQueryable<ShoppingCartDTO> ReadAll() =>
            (from shoppingCart in Set select shoppingCart.ToDTO()).AsNoTracking();
    }
}
