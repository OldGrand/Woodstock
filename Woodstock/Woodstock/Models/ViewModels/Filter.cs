using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.ViewModels
{
    public enum Filter
    {
        [Display(Name = "По умолчанию")]
        Deafult,
        
        [Display(Name = "По популярности")]
        SortByPopularity,

        [Display(Name = "Новизна : самые старые")]
        SortByNoveltyAsc,

        [Display(Name = "Новизна : самые новые")]
        SortByNoveltyDesc,

        [Display(Name = "Цена : по возрастанию")]
        OrderByPriceAsc,

        [Display(Name = "Цена : по убыванию")]
        OrderByPriceDesc
    }
}
