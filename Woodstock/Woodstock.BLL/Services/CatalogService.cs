using Woodstock.BLL.Interfaces;
using Woodstock.BLL.DTOs;
using System.Linq;
using Woodstock.DAL;
using Woodstock.BLL.Extensions;
using Infrastructure.Enums;
using Woodstock.DAL.Entities;

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
            (start: _context.Watches.Min(_ => _.Price), end: _context.Watches.Max(_ => _.Price));

        public IQueryable<Watch> ReadAllEntities() =>
            from watch in _context.Watches
            where watch.CountInStock > 0
            select watch;

        public IQueryable<WatchDTO> ReadAll() =>
            from watch in _context.Watches
            where watch.CountInStock > 0
            select watch.ToDTO();

        public IQueryable<WatchDTO> ReadMen() =>
            ReadAllEntities().ReadByGender(Gender.Man);

        public IQueryable<WatchDTO> ReadWomen() =>
            ReadAllEntities().ReadByGender(Gender.Woman);

        public IQueryable<WatchDTO> ReadOrderedByPriceDesc() =>
            ReadAllEntities().OrderByDescending(_ => _.Price).Select(_ => _.ToDTO());

        public IQueryable<WatchDTO> ReadOrderedByPriceAsc() =>
            ReadAllEntities().OrderBy(_ => _.Price).Select(_ => _.ToDTO());

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
