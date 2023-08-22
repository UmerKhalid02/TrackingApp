using AutoMapper;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
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
            UpdateUserMapper();
        }

        private void AddUserMapper()
        {
            CreateMap<User, AddUserRequestDTO>().ReverseMap();

            // map register request to user
            CreateMap<AddUserRequestDTO, RegisterRequestModel>().ReverseMap();
        }

        private void UserDetailsMapper()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
        }

        private void UpdateUserMapper()
        {
            CreateMap<User, UpdateUserRequestDTO>().ReverseMap();
        }
    }
}
