using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public IActionResult Index(int pageNum = 1, int itemsOnPage = 16)
        {
            var pagedResult =  (from watchDTO in _catalogService.ReadAll()
                                select new WatchViewModel
                                {
                                    Title = watchDTO.Title,
                                    Description = watchDTO.Description,
                                    Diameter = watchDTO.Diameter,
                                    Photo = watchDTO.Photo,
                                    Price = watchDTO.Price
                                }).GetPaged(pageNum, itemsOnPage);
            return View(pagedResult);
        }
    }
}
