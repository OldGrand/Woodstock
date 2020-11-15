using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Woodstock.BLL.DTOs;
using Woodstock.DAL.Entities;
using Woodstock.PL.Models.BindingModels;

namespace Woodstock.PL.Profiles
{
    public class ProfileBLL : Profile
    {
        public ProfileBLL()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<UserDTO, User>()
                .ForMember(dst => dst.Email, src => src.MapFrom(_ => _.Email))
                .ForMember(dst => dst.UserName, src => src.MapFrom(_ => _.Email))
                .ReverseMap();

            CreateMap<IdentityResult, IdentityResultDTO>().ReverseMap();
            CreateMap<IdentityError, IdentityErrorDTO>().ReverseMap();
            CreateMap<SignInResult, SignInResultDTO>().ReverseMap();
        }
    }
}
