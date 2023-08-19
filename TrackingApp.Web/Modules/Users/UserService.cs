﻿using AutoMapper;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Extensions;
using TrackingApp.Application.Parameters;
using TrackingApp.Application.Wrappers;
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.IRepositories.IUserRepository;

namespace TrackingApp.Web.Modules.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Response<PaginationResponseModel>> GetAllUsers(UserPageParamter request)
        {
            var response = await _userRepository.GetAllUsers();

            var query = response.AsQueryable();
            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(a => a.UserName.ToLower().Contains(request.UserName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(a => a.Email.ToLower().Contains(request.Email.ToLower()));
            }

            request.OrderBy ??= PaginationOrder.UserName;
            request.OrderType ??= PaginationOrder.Descending;

            if (request.PageNumber == 0 && request.PageSize == 0)
            {
                request.PageNumber = 1;
                request.PageSize = query.Count();
            }
            else 
            {
                request.PageNumber = request.PageNumber == 0 ? 1 : request.PageNumber;
                request.PageSize = request.PageSize == 0 ? 10 : request.PageSize;
            }

            var responseQuery = _mapper.Map<List<UserResponseDTO>>(query.ToList());

            var result = UserPagedList<UserResponseDTO>.CreateAsync(responseQuery.AsQueryable(), request);

            var Response = new PaginationResponseModel
            {
                Pagination = new Pagination
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages < 1 ? 1 : result.TotalPages,
                    TotalCount = result.TotalCount,
                },
                Items = result.ToList(),
            };

            return new Response<PaginationResponseModel>(true, Response, GeneralMessages.RecordFetched);
        }

        public async Task<Response<UserResponseDTO>> AddUser(AddUserRequestDTO request)
        {
            var newUser = _mapper.Map<User>(request);
            newUser.IsActive = true;
            newUser.CreatedAt = DateTime.Now;

            await _userRepository.AddUser(newUser);
            await _userRepository.SaveChanges();

            return new Response<UserResponseDTO>(true, null, GeneralMessages.RecordAdded);
        }

        public async Task<Response<UserResponseDTO>> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var response = _mapper.Map<UserResponseDTO>(user);
            if(response != null)
                return new Response<UserResponseDTO>(true, response, GeneralMessages.RecordFetched);
            return new Response<UserResponseDTO>(false, null, GeneralMessages.RecordNotFound);
        }
    }
}
