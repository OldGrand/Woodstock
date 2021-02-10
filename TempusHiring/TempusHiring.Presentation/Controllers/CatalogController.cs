using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TempusHiring.BusinessLogic.Interfaces;
using TempusHiring.BusinessLogic.Pagination;
using TempusHiring.Presentation.Models.ViewModels;

namespace TempusHiring.Presentation.Controllers
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
            if (filteredVM.PriceRange is null)
            {
                var priceRangeDTO = _catalogService.GetWatchesPriceRange();
                filteredVM.PriceRange = _mapper.Map<PriceRangeViewModel>(priceRangeDTO);
            }
            else
            {
                _catalogService.ChangePriceRange(filteredVM.PriceRange.StartPrice, filteredVM.PriceRange.EndPrice);
            }

            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadOrderedByPriceAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadOrderedByPriceDesc(),
                Filter.SortByNoveltyAsc => _catalogService.ReadOrderedByNoveltyAsc(),
                Filter.SortByNoveltyDesc => _catalogService.ReadOrderedByNoveltyDesc(),
                Filter.SortByPopularityAsc => _catalogService.ReadOrderedByPopularityAsc(),
                Filter.SortByPopularityDesc => _catalogService.ReadOrderedByPopularityDesc(),
                _ => _catalogService.ReadAll()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);

            filteredVM.PageResult = pagedResult;
            filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

            return View(filteredVM);
        }

        //public IActionResult MensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        //{
        //    if (!string.IsNullOrEmpty(filteredVM.PriceRange.StartPrice) || !string.IsNullOrEmpty(filteredVM.PriceRange.EndPrice))
        //        _catalogService.ChangePriceRange(filteredVM.PriceRange.StartPrice, filteredVM.PriceRange.EndPrice);

        //    var pagedResult = (filteredVM.Filter switch
        //    {
        //        Filter.OrderByPriceAsc => _catalogService.ReadMenOrderedByPriceAsc(),
        //        Filter.OrderByPriceDesc => _catalogService.ReadMenOrderedByPriceDesc(),
        //        Filter.SortByNoveltyAsc => _catalogService.ReadMenOrderedByNoveltyAsc(),
        //        Filter.SortByNoveltyDesc => _catalogService.ReadMenOrderedByNoveltyDesc(),
        //        Filter.SortByPopularityAsc => _catalogService.ReadMenOrderedByPopularityAsc(),
        //        Filter.SortByPopularityDesc => _catalogService.ReadMenOrderedByPopularityDesc(),
        //        _ => _catalogService.ReadMen()
        //    }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);

        //    filteredVM.PageResult = pagedResult;
        //    filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

        //    return View(nameof(Index), filteredVM);
        //}

        //public IActionResult WomensWatches(FilteredWatchViewModel filteredVM, int pageNum = 1)
        //{
        //    if (!string.IsNullOrEmpty(filteredVM.PriceRange.StartPrice) || !string.IsNullOrEmpty(filteredVM.PriceRange.EndPrice))
        //        _catalogService.ChangePriceRange(filteredVM.PriceRange.StartPrice, filteredVM.PriceRange.EndPrice);

        //    var pagedResult = (filteredVM.Filter switch
        //    {
        //        Filter.OrderByPriceAsc => _catalogService.ReadWomenOrderedByPriceAsc(),
        //        Filter.OrderByPriceDesc => _catalogService.ReadWomenOrderedByPriceDesc(),
        //        Filter.SortByNoveltyAsc => _catalogService.ReadWomenOrderedByNoveltyAsc(),
        //        Filter.SortByNoveltyDesc => _catalogService.ReadWomenOrderedByNoveltyDesc(),
        //        Filter.SortByPopularityAsc => _catalogService.ReadWomenOrderedByPopularityAsc(),
        //        Filter.SortByPopularityDesc => _catalogService.ReadWomenOrderedByPopularityDesc(),
        //        _ => _catalogService.ReadWomen()
        //    }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, filteredVM.ItemsOnPage);

        //    filteredVM.PageResult = pagedResult;
        //    filteredVM.ItemsOnPageVM = new SelectList(new[] { 12, 24, 36 });

        //    return View(nameof(Index), filteredVM);
        //}

        public IActionResult GetPriceRange() => Json(_catalogService.GetWatchesPriceRange());
    }
}