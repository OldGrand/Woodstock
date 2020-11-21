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
            CreateMap<LoginBindingModel, UserDTO>()
                .ForMember(dst => dst.Email, src => src.MapFrom(_ => _.Email))
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<RegisterBindingModel, UserDTO>()
                .ForMember(dst => dst.Email, src => src.MapFrom(_ => _.Email))
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<ExternalRegisterBindingModel, UserDTO>()
                .ForMember(dst => dst.Email, src => src.MapFrom(_ => _.Email))
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<ResetPasswordDTO, ResetPasswordBindingModel>().ReverseMap();
            CreateMap<WatchDTO, WatchViewModel>().ReverseMap();
        }
    }
}
