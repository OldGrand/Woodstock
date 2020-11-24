using Woodstock.BLL.Abstract;
using Woodstock.DAL;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Services
{
    public class ShoppigCartService : ApplicationContextService<ShoppingCart>
    {
        public ShoppigCartService(WoodstockDbContext context) : base(context) { }
    }
}
