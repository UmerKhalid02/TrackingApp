using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Parameters;
using TrackingApp.Application.Wrappers;

namespace TrackingApp.Web.Modules.Users
{
    public interface IUserService
    {
        Task<Response<PaginationResponseModel>> GetAllUsers(UserPageParamter request);
        Task<Response<UserResponseDTO>> GetUserById(Guid userId);
        Task<Response<UserResponseDTO>> AddUser(AddUserRequestDTO request);
    }
}
