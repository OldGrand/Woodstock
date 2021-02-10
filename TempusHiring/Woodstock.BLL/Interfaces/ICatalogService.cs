using System.Linq;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface ICatalogService
    {
        PriceRangeDTO GetWatchesPriceRange();
        void ChangePriceRange(decimal start, decimal end);


        IQueryable<WatchDTO> ReadAll();
        IQueryable<WatchDTO> ReadOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadOrderedByPriceAsc();
        IQueryable<WatchDTO> ReadOrderedByNoveltyDesc();
        IQueryable<WatchDTO> ReadOrderedByNoveltyAsc();
        IQueryable<WatchDTO> ReadOrderedByPopularityDesc();
        IQueryable<WatchDTO> ReadOrderedByPopularityAsc();


        IQueryable<WatchDTO> ReadMen();
        IQueryable<WatchDTO> ReadMenOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadMenOrderedByPriceAsc();
        IQueryable<WatchDTO> ReadMenOrderedByNoveltyDesc();
        IQueryable<WatchDTO> ReadMenOrderedByNoveltyAsc();
        IQueryable<WatchDTO> ReadMenOrderedByPopularityDesc();
        IQueryable<WatchDTO> ReadMenOrderedByPopularityAsc();

        IQueryable<WatchDTO> ReadWomen();
        IQueryable<WatchDTO> ReadWomenOrderedByPriceDesc();
        IQueryable<WatchDTO> ReadWomenOrderedByPriceAsc();
        IQueryable<WatchDTO> ReadWomenOrderedByNoveltyDesc();
        IQueryable<WatchDTO> ReadWomenOrderedByNoveltyAsc();
        IQueryable<WatchDTO> ReadWomenOrderedByPopularityDesc();
        IQueryable<WatchDTO> ReadWomenOrderedByPopularityAsc();
    }
}
