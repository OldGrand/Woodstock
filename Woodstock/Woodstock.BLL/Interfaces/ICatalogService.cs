using System.Linq;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface ICatalogService
    {
        IQueryable<WatchDTO> ReadAll();
        IQueryable<WatchDTO> ReadMen();
        IQueryable<WatchDTO> ReadWomen();
        IQueryable<WatchDTO> ReadOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadOrderedByPriceAsc();
        IQueryable<WatchDTO> ReadMenOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadMenOrderedByPriceAsc();
        IQueryable<WatchDTO> ReadWomenOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadWomenOrderedByPriceAsc();
    }
}
