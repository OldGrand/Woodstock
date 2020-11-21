using Woodstock.BLL.Interfaces;
using AutoMapper;
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
        public CatalogService(WoodstockDbContext context, IMapper mapper) : base(context, mapper) { }

        public IQueryable<WatchDTO> ReadAll()
        {
            return from watch in Set.AsNoTracking()
                   select new WatchDTO
                   {
                       Title = watch.Title,
                       Description = watch.Description,
                       Diameter = watch.Diameter,
                       Photo = watch.Photo,
                       Price = watch.Price,
                       GenderId = watch.GenderId
                   };
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
    }
}
