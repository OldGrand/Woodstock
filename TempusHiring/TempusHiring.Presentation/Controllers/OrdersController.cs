using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempusHiring.BusinessLogic.Extensions;
using TempusHiring.BusinessLogic.Interfaces;
using TempusHiring.Presentation.Models.ViewModels;

namespace TempusHiring.Presentation.Controllers
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
