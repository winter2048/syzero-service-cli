using AutoMapper;
using syzero.test.Core.Users;
using syzero.test.IApplication.Users.Dto;

namespace syzero.test.Application.MapProfile
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