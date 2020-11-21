using AutoMapper;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Pagination;
using Woodstock.DAL.Entities;

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

            CreateMap<Watch, WatchDTO>().ReverseMap();
        }
    }
}
