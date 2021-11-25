using AutoMapper;
using Template1.Template2.Core.Users;
using Template1.Template2.IApplication.Users.Dto;

namespace Template1.Template2.Application.MapProfile
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();

            
        }
    }
}