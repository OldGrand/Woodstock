using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.ViewModels;
using System.Linq;
using Woodstock.BLL.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        //public IActionResult Index(int pageNum = 1, int itemsOnPage = 16)
        //{
        //    var pagedResult = (from watchDTO in _catalogService.ReadAll()
        //                       select new WatchViewModel
        //                       {
        //                           Title = watchDTO.Title,
        //                           Description = watchDTO.Description,
        //                           Diameter = watchDTO.Diameter,
        //                           Photo = watchDTO.Photo,
        //                           Price = watchDTO.Price
        //                       }).GetPaged(pageNum, itemsOnPage);
        //    return View(pagedResult);
        //}

        public IActionResult Index(FilteredWatchViewModel filteredVM, int pageNum=1, int itemsOnPage=16)
        {
            var pagedResult = (filteredVM.Filter switch
            {
                Filter.OrderByPriceAsc => _catalogService.ReadOrderedByAsc(),
                Filter.OrderByPriceDesc => _catalogService.ReadOrderedByPriceDesc(),
                Filter.Deafult => _catalogService.ReadAll(),
                _ => _catalogService.ReadAll()
            }).Select(_ => _mapper.Map<WatchViewModel>(_)).GetPaged(pageNum, itemsOnPage);
            filteredVM.PageResult = pagedResult;
            return View(filteredVM);
        }

        public IActionResult MensWatches(int pageNum = 1, int itemsOnPage = 16)
        {
            var pagedResult = (from watchDTO in _catalogService.ReadMen()
                               select new WatchViewModel
                               {
                                   Title = watchDTO.Title,
                                   Description = watchDTO.Description,
                                   Diameter = watchDTO.Diameter,
                                   Photo = watchDTO.Photo,
                                   Price = watchDTO.Price
                               }).GetPaged(pageNum, itemsOnPage);
            return View(nameof(Index), pagedResult);
        }
    }
}
