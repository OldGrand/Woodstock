using AutoMapper;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.DTOs.Account;
using Woodstock.PL.Models.AccountViewModels;
using Woodstock.PL.Models.ViewModels;

namespace Woodstock.PL.Profiles
{
    public class ProfilePL : Profile
    {
        public ProfilePL()
        {
            CreateMap<ResetPasswordDTO, ResetPasswordViewModel>().ReverseMap();
            CreateMap<ExternalRegisterViewModel, UserDTO>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<LoginViewModel, UserDTO>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<RegisterViewModel, UserDTO>()
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
