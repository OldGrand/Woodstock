using AutoMapper;
using Woodstock.BLL.DTOs;
using Woodstock.PL.Models.BindingModels;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Profiles
{
    public class ProfilePL : Profile
    {
        public ProfilePL()
        {
            CreateMap<ResetPasswordDTO, ResetPasswordBindingModel>().ReverseMap();
            CreateMap<WatchDTO, WatchViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<ShoppingCartDTO, ShoppingCartViewModel>().ReverseMap();
        }
    }
}
