using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;
using System.Linq;
using Woodstock.BLL.Pagination;

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

        public IActionResult Index(int pageNum = 1, int itemsOnPage = 12)
        {
            var pagedResult = _catalogService.ReadAll()
                .Select(_ => _mapper.Map<WatchViewModel>(_))
                .GetPaged(pageNum, itemsOnPage);
            var filteredVM = new FilteredWatchViewModel
            {
                PageResult = pagedResult,
                Filter = Filter.Deafult
            };
            return View(filteredVM);
        }

        [HttpPost]
        public IActionResult Index(FilteredWatchViewModel filteredVM, int pageNum = 1, int itemsOnPage = 12)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadOrderedByPriceDesc(),
                _ => _catalogService.ReadAll()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, itemsOnPage);
            filteredVM.PageResult = pagedResult;
            return View(filteredVM);
        }

        public IActionResult MensWatches(int pageNum = 1, int itemsOnPage = 12)
        {
            var pagedResult = _catalogService.ReadMen()
                .Select(_ => _mapper.Map<WatchViewModel>(_))
                .GetPaged(pageNum, itemsOnPage);
            var filteredVM = new FilteredWatchViewModel
            {
                PageResult = pagedResult,
                Filter = Filter.Deafult
            };
            return View(filteredVM);
        }

        [HttpPost]
        public IActionResult MensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadMenOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadMenOrderedByPriceDesc(),
                _ => _catalogService.ReadMen()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);
            filteredVM.PageResult = pagedResult;
            return View(filteredVM);
        }

        public IActionResult WomensWatches(int pageNum = 1, int itemsOnPage = 12)
        {
            var pagedResult = _catalogService.ReadWomen()
                .Select(_ => _mapper.Map<WatchViewModel>(_))
                .GetPaged(pageNum, itemsOnPage);
            var filteredVM = new FilteredWatchViewModel
            {
                PageResult = pagedResult,
                Filter = Filter.Deafult
            };
            return View(filteredVM);
        }

        [HttpPost]
        public IActionResult WomensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadMenOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadMenOrderedByPriceDesc(),
                _ => _catalogService.ReadMen()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);
            filteredVM.PageResult = pagedResult;
            return View(filteredVM);
        }
    }
}
