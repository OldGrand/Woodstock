using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        public ShopController(IShopService shopService, IMapper mapper)
        {
            _shopService = shopService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNum = 1, int itemsOnPage = 16)
        {
            var pagedResultDTO = await _shopService.GetItemsOnPage(pageNum, itemsOnPage);
            var padegResultVM = _mapper.Map<PagedResultViewModel<WatchViewModel>>(pagedResultDTO);
            return View(padegResultVM);
        }
    }
}
