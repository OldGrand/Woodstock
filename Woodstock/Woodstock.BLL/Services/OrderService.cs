using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly WoodstockDbContext _context;

        public OrderService(WoodstockDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(int userId)
        {
            var order = new Order
            {
                UserId = userId,
                PaymentMethod = "Наличные",
                OrderDate = DateTime.Now,
                User = _context.Users.Find(userId),
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public void AddItemsToOrder(int userId)
        {
            var order = CreateOrder(userId);


        }
    }
}
