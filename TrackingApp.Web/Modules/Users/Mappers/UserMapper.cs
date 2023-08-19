using AutoMapper;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Data.Entities.UserEntity;

namespace TrackingApp.Web.Modules.Users.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            AddUserMapper();
            UserDetailsMapper();
        }

        private void AddUserMapper()
        {
            CreateMap<User, AddUserRequestDTO>().ReverseMap();
        }

        private void UserDetailsMapper()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
        }
    }
}
