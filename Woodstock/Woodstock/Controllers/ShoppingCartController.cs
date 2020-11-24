using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Woodstock.BLL.Interfaces;
using Woodstock.BLL.Pagination;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ISoppingCartService _cartService;
        private readonly IMapper _mapper;

        public ShoppingCartController(ISoppingCartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult ShoppingCart(int userId, int pageNum = 1, int itemOnPage = 5)
        {
            var pagedResult = _cartService.ReadUserCart(userId)
                                          .Select(_ => _mapper.Map<ShoppingCartViewModel>(_))
                                          .GetPaged(pageNum, itemOnPage);
            return View();
        }
    }
}