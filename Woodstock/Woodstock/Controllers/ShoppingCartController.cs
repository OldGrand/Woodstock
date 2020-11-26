using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult Items()
        {
            var cartVMs = ReadUserCart().ToList();
            return View(cartVMs);
        }

        public async Task<IActionResult> AddToCart(int watchId)
        {//TODO дублируются товары
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.AddToCartAsync(userId, watchId);

            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        public IActionResult ChangeCount(int id, Operation operation)
        {
            var cartVMs = ReadUserCart().ToList();

            var changedItem = cartVMs.FirstOrDefault(_ => _.Id == id);
            var pagedResult = operation switch
            {
                Operation.Minus => changedItem.Count--,
                Operation.Plus => changedItem.Count++,
                _ => changedItem.Count,
            };

            return View(nameof(Items), cartVMs);
        }

        private IQueryable<ShoppingCartViewModel> ReadUserCart()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cartVMs = _cartService.ReadUserCart(userId)
                                          .Select(_ => _mapper.Map<ShoppingCartViewModel>(_));
            return cartVMs;
        }
    }
}