using Woodstock.BLL.Interfaces;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Abstract;
using Woodstock.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Woodstock.DAL;

namespace Woodstock.BLL.Services
{
    public class CatalogService : ApplicationContextService<Watch>, ICatalogService
    {
        public CatalogService(WoodstockDbContext context) : base(context) { }

        public IQueryable<WatchDTO> ReadAll()
        {
            return from watch in Set.AsNoTracking()
                   select new WatchDTO(watch.Diameter, watch.Description, watch.Price, watch.Photo, watch.Title, watch.Gender);
        }

        public IQueryable<WatchDTO> ReadMen()
        {
            return from watch in ReadAll()
                   where watch.Gender.Title == "Мужские"
                   select watch;
        }

        public IQueryable<WatchDTO> ReadWomen()
        {
            return from watch in ReadAll()
                   where watch.Gender.Title == "Женские"
                   select watch;
        }

        public IQueryable<WatchDTO> ReadOrderedByPriceDesc()
        {
            return from watch in ReadAll()
                   orderby watch.Price descending
                   select watch;
        }

        public IQueryable<WatchDTO> ReadOrderedByAsc()
        {
            return from watch in ReadAll()
                   orderby watch.Price ascending
                   select watch;
        }
    }
}
