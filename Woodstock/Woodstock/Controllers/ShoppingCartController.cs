using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Woodstock.BLL.Extensions;
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
            var userId = User.GetId();

            var cartWrapper = new CartWrapperViewModel
            {
                OrderSummary = _mapper.Map<OrderSummaryViewModel>(_cartService.GetSummary(userId)),
                ShoppingCarts = ReadUserCartVM(userId).ToList()
            };

            return View(cartWrapper);
        }

        public IActionResult ChangeSelection(int watchId, string test, bool isChecked)
        {
            var userId = User.GetId();
            _cartService.UpdateSummary(userId, watchId, isChecked);

            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        public IActionResult Buy()
        {
            return RedirectToAction("CreateOrder", "Orders");
        }

        public async Task<IActionResult> AddToCart(int watchId)
        {
            var userId = User.GetId();
            await _cartService.AddToCartAsync(userId, watchId);

            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        public IActionResult ChangeCount(int watchId, Operations operation)
        {
            var userId = User.GetId();
            _cartService.ChangeCount(userId, watchId, operation);

            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        public IActionResult Remove(int cartId)
        {
            _cartService.DeleteFromCart(cartId);
            return RedirectToAction(nameof(Items), "ShoppingCart");
        }

        private IQueryable<ShoppingCartViewModel> ReadUserCartVM(int userId)
        {
            var cartVMs = _cartService.ReadUserCart(userId)
                                          .Select(_ => _mapper.Map<ShoppingCartViewModel>(_));
            return cartVMs;
        }
    }
}