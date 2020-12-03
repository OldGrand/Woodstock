using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;
using System.Linq;
using Woodstock.BLL.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index(FilteredWatchViewModel filteredVM, int pageNum = 1)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadOrderedByPriceDesc(),
                Filter.SortByNoveltyAsc => _catalogService.ReadOrderedByNoveltyAsc(),
                Filter.SortByNoveltyDesc => _catalogService.ReadOrderedByNoveltyDesc(),
                _ => _catalogService.ReadAll()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);
            filteredVM.PageResult = pagedResult;
            filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

            return View(filteredVM);
        }

        public IActionResult MensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadMenOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadMenOrderedByPriceDesc(),
                Filter.SortByNoveltyAsc => _catalogService.ReadMenOrderedByNoveltyAsc(),
                Filter.SortByNoveltyDesc => _catalogService.ReadMenOrderedByNoveltyDesc(),
                _ => _catalogService.ReadMen()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);
            filteredVM.PageResult = pagedResult;
            filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

            return View(nameof(Index), filteredVM);
        }

        public IActionResult WomensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadWomenOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadWomenOrderedByPriceDesc(),
                Filter.SortByNoveltyAsc => _catalogService.ReadWomenOrderedByNoveltyAsc(),
                Filter.SortByNoveltyDesc => _catalogService.ReadWomenOrderedByNoveltyDesc(),
                _ => _catalogService.ReadWomen()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);
            filteredVM.PageResult = pagedResult;
            filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

            return View(nameof(Index), filteredVM);
        }
    }
}