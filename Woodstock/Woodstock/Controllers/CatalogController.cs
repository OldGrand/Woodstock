using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogService shopService, IMapper mapper)
        {
            _catalogService = shopService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNum = 1, int itemsOnPage = 16)
        {
            var pagedResultDTO = await _catalogService.GetItemsOnPage(pageNum, itemsOnPage);
            var padegResultVM = _mapper.Map<PagedResultViewModel<WatchViewModel>>(pagedResultDTO);
            return View(padegResultVM);
        }
    }
}
