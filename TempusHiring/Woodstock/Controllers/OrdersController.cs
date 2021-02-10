using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Woodstock.BLL.Extensions;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public IActionResult Items()
        {
            var userId = User.GetId();
            var orderDTOs = _orderService.GetOrders(userId).ToList();
            var orderVMs = _mapper.Map<IEnumerable<OrderViewModel>>(orderDTOs);
            return View(orderVMs);
        }

        public IActionResult CreateOrder()
        {
            var userId = User.GetId();
            _orderService.AddItemsToOrder(userId);

            return RedirectToAction(nameof(Items), "Orders");
        }
    }
}
