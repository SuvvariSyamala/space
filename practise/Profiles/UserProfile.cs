using AutoMapper;
using practise.DTO;
using practise.Entitys;

namespace practise.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

        }


    }
}
