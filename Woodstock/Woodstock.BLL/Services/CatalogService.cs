using Woodstock.BLL.Interfaces;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Abstract;
using Woodstock.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Woodstock.DAL;
using Woodstock.BLL.Extensions;

namespace Woodstock.BLL.Services
{
    public class CatalogService : ApplicationContextService<Watch>, ICatalogService
    {
        public CatalogService(WoodstockDbContext context) : base(context) { }

        public IQueryable<WatchDTO> ReadAll()
        {
            return (from watch in Set
                    select new WatchDTO
                    {
                        Id = watch.Id,
                        Title = watch.Title,
                        Description = watch.Description,
                        Diameter = watch.Diameter,
                        Gender = watch.Gender,
                        Photo = watch.Photo,
                        Price = watch.Price
                    }).AsNoTracking();
        }

        public IQueryable<WatchDTO> ReadMen() =>
            ReadAll().ReadByGender("Мужские");

        public IQueryable<WatchDTO> ReadWomen() => 
            ReadAll().ReadByGender("Женские");

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
