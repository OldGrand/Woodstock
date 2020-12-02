using Woodstock.BLL.Interfaces;
using Woodstock.BLL.DTOs;
using System.Linq;
using Woodstock.DAL;
using Woodstock.BLL.Extensions;
using Infrastructure.Enums;

namespace Woodstock.BLL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly WoodstockDbContext _context;

        public CatalogService(WoodstockDbContext context)
        {
            _context = context;
        }

        public (decimal start, decimal end) GetWatchesPriceRange() =>
            (_context.Watches.Min(_ => _.Price), _context.Watches.Max(_ => _.Price));

        public IQueryable<WatchDTO> ReadAll() =>
            from watch in _context.Watches
            where watch.CountInStock > 0
            select new WatchDTO
            {
                Id = watch.Id,
                Title = watch.Title,
                Description = watch.Description,
                Diameter = watch.Diameter,
                Gender = watch.Gender,
                Photo = watch.Photo,
                Price = watch.Price,
                CountInStock = watch.CountInStock
            };

        public IQueryable<WatchDTO> ReadMen() =>
            ReadAll().ReadByGender(Gender.Man);

        public IQueryable<WatchDTO> ReadWomen() =>
            ReadAll().ReadByGender(Gender.Woman);

        public IQueryable<WatchDTO> ReadOrderedByPriceDesc() =>
            ReadAll().OrderByDescending(_ => _.Price);

        public IQueryable<WatchDTO> ReadOrderedByPriceAsc() =>
            ReadAll().OrderBy(_ => _.Price);

        public IQueryable<WatchDTO> ReadMenOrderedByPriceDesc() =>
            ReadMen().OrderByDescending(_ => _.Price);

        public IQueryable<WatchDTO> ReadMenOrderedByPriceAsc() =>
            ReadMen().OrderBy(_ => _.Price);

        public IQueryable<WatchDTO> ReadWomenOrderedByPriceDesc() =>
            ReadWomen().OrderByDescending(_ => _.Price);

        public IQueryable<WatchDTO> ReadWomenOrderedByPriceAsc() =>
            ReadWomen().OrderBy(_ => _.Price);
    }
}
