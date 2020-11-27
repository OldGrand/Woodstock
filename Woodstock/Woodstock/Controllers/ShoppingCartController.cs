using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.Infrastructure;
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
            var cartVMs = ReadUserCartVM().ToList();
            return View(cartVMs);
        }

        public IActionResult ChangeSelection(int watchId, bool isChecked)
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _cartService.UpdateSummary(userId, watchId, isChecked);

            return View();
        }

        public async Task<IActionResult> AddToCart(int watchId)
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.AddToCartAsync(userId, watchId);

            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        public IActionResult ChangeCount(int userId, int watchId, Operations operation)
        {
            var cartVMs = _cartService.ChangeCount(userId, watchId, operation)
                                      .Select(_ => _mapper.Map<ShoppingCartViewModel>(_))
                                      .ToList();

            return View(nameof(Items), cartVMs);
        }

        public IActionResult Remove(int cartId)
        {
            _cartService.DeleteFromCart(cartId);
            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        private IQueryable<ShoppingCartViewModel> ReadUserCartVM()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cartVMs = _cartService.ReadUserCart(userId)
                                          .Select(_ => _mapper.Map<ShoppingCartViewModel>(_));
            return cartVMs;
        }
    }
}