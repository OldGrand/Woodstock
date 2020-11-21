using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.ViewModels
{
    public enum Filter
    {
        [Display(Name = "По умолчанию")]
        Deafult,
        
        [Display(Name = "По популярности")]
        SortByPopularity,

        [Display(Name = "По новизне")]
        SortByNewest,

        [Display(Name = "Цена : по возрастанию")]
        OrderByPriceAsc,

        [Display(Name = "Цена : по убыванию")]
        OrderByPriceDesc
    }
}
