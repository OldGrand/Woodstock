using Woodstock.BLL.Interfaces;
using Woodstock.BLL.DTOs;
using System.Linq;
using Woodstock.DAL;
using Woodstock.BLL.Extensions;
using Infrastructure.Enums;
using System.Text.RegularExpressions;

namespace Woodstock.BLL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly WoodstockDbContext _context;
        private decimal _start;
        private decimal _end;

        public CatalogService(WoodstockDbContext context)
        {
            _context = context;
            _start = _context.Watches.Min(_ => _.Price);
            _end = _context.Watches.Max(_ => _.Price);
        }

        public (decimal start, decimal end) GetWatchesPriceRange() =>
            (_start, _end);

        public void ChangePriceRange(string start, string end)
        {
            _start = decimal.Parse( Regex.Replace(start, @"\s*", string.Empty));
            _end = decimal.Parse(Regex.Replace(end, @"\s*", string.Empty));
        }

        public IQueryable<WatchDTO> ReadAll() =>
            from watch in _context.Watches
            where watch.CountInStock > 0 && (watch.Price >= _start && watch.Price <= _end)
            select new WatchDTO
            {
                Id = watch.Id,
                Title = watch.Title,
                Description = watch.Description,
                Diameter = watch.Diameter,
                Gender = watch.Gender,
                Photo = watch.Photo,
                Price = watch.Price,
                CountInStock = watch.CountInStock,
                SaledCount = watch.SaledCount,
            };
        public IQueryable<WatchDTO> ReadOrderedByPriceDesc() =>
            ReadAll().OrderByDescending(_ => _.Price);
        public IQueryable<WatchDTO> ReadOrderedByPriceAsc() =>
            ReadAll().OrderBy(_ => _.Price);
        public IQueryable<WatchDTO> ReadOrderedByNoveltyDesc() =>
            ReadAll().OrderByDescending(_ => _.Id);
        public IQueryable<WatchDTO> ReadOrderedByNoveltyAsc() =>
            ReadAll().OrderBy(_ => _.Id);
        public IQueryable<WatchDTO> ReadOrderedByPopularityDesc() =>
            ReadAll().OrderByDescending(_ => _.SaledCount);
        public IQueryable<WatchDTO> ReadOrderedByPopularityAsc() =>
            ReadAll().OrderBy(_ => _.SaledCount);


        public IQueryable<WatchDTO> ReadMen() =>
            ReadAll().ReadByGender(Gender.Man);
        public IQueryable<WatchDTO> ReadMenOrderedByPriceDesc() =>
            ReadMen().OrderByDescending(_ => _.Price);
        public IQueryable<WatchDTO> ReadMenOrderedByPriceAsc() =>
            ReadMen().OrderBy(_ => _.Price);
        public IQueryable<WatchDTO> ReadMenOrderedByNoveltyDesc() =>
            ReadMen().OrderByDescending(_ => _.Id);
        public IQueryable<WatchDTO> ReadMenOrderedByNoveltyAsc() =>
            ReadMen().OrderBy(_ => _.Id);
        public IQueryable<WatchDTO> ReadMenOrderedByPopularityDesc() =>
            ReadMen().OrderByDescending(_ => _.SaledCount);
        public IQueryable<WatchDTO> ReadMenOrderedByPopularityAsc() =>
            ReadMen().OrderBy(_ => _.SaledCount);


        public IQueryable<WatchDTO> ReadWomen() =>
            ReadAll().ReadByGender(Gender.Woman);
        public IQueryable<WatchDTO> ReadWomenOrderedByPriceDesc() =>
            ReadWomen().OrderByDescending(_ => _.Price);
        public IQueryable<WatchDTO> ReadWomenOrderedByPriceAsc() =>
            ReadWomen().OrderBy(_ => _.Price);
        public IQueryable<WatchDTO> ReadWomenOrderedByNoveltyDesc() =>
            ReadWomen().OrderByDescending(_ => _.Id);
        public IQueryable<WatchDTO> ReadWomenOrderedByNoveltyAsc() =>
            ReadWomen().OrderBy(_ => _.Id);
        public IQueryable<WatchDTO> ReadWomenOrderedByPopularityDesc() =>
            ReadWomen().OrderByDescending(_ => _.SaledCount);
        public IQueryable<WatchDTO> ReadWomenOrderedByPopularityAsc() =>
            ReadWomen().OrderBy(_ => _.SaledCount);
    }
}
