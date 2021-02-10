using System.ComponentModel.DataAnnotations;

namespace TempusHiring.Presentation.Models.ViewModels
{
    public enum Filter
    {
        [Display(Name = "По умолчанию")]
        Deafult,
        
        [Display(Name = "Самые продоваемые")]
        SortByPopularityDesc,

        [Display(Name = "Наименее продоваемые")]
        SortByPopularityAsc,

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
