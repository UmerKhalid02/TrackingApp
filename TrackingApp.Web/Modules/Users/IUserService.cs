﻿using TrackingApp.Application.DataTransferObjects.OrderDTO;
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
        Task<Response<UserResponseDTO>> UpdateUser(UpdateUserRequestDTO request, Guid userId);
        Task<Response<bool>> DeleteUser(Guid userId);
        Task<Response<List<OrderResponseDTO>>> GetAllUserActiveOrders(Guid userId);
        Task<Response<OrderResponseDTO>> GetUserActiveOrderById(Guid userId, int orderId);
        Task<Response<bool>> UploadProfilePicture(Guid userId, FileUploadRequestDTO profilePic);
    }
}
