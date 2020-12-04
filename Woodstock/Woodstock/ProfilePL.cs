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
            CreateMap<ExternalRegisterBindingModel, UserDTO>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();
            CreateMap<LoginBindingModel, UserDTO>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();
            CreateMap<RegisterBindingModel, UserDTO>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();
            CreateMap<WatchDTO, WatchViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<ShoppingCartDTO, ShoppingCartViewModel>().ReverseMap();
            CreateMap<OrderSummaryDTO, OrderSummaryViewModel>().ReverseMap();
            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            CreateMap<OrderWatchLinkDTO, OrderWatchLinkViewModel>().ReverseMap();
            CreateMap<PriceRangeDTO, PriceRangeViewModel>().ReverseMap();
        }
    }
}
