using AutoMapper;
using TrackingApp.Application.DataTransferObjects.OrderDTO;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Exceptions;
using TrackingApp.Application.Extensions;
using TrackingApp.Application.Helpers;
using TrackingApp.Application.Parameters;
using TrackingApp.Application.Wrappers;
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.IRepositories.IOrderRepository;
using TrackingApp.Data.IRepositories.IUserRepository;
using TrackingApp.Web.Extensions;

namespace TrackingApp.Web.Modules.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly AWS awsservice;
        public UserService(IMapper mapper, IUserRepository userRepository, IOrderRepository orderRepository, AWS aws)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            awsservice = aws;
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

        public async Task<Response<UserResponseDTO>> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var response = _mapper.Map<UserResponseDTO>(user);
            if (response != null)
                return new Response<UserResponseDTO>(true, response, GeneralMessages.RecordFetched);
            return new Response<UserResponseDTO>(false, null, GeneralMessages.RecordNotFound);
        }

        public async Task<Response<UserResponseDTO>> AddUser(AddUserRequestDTO request)
        {
            // Check if user with this username already exists
            var username = await _userRepository.UserWithUsernameExists(request.UserName);
            if(username)
                throw new ConflictException(GeneralMessages.UserWithUsernameExists);

            // Check if user with this email already exists
            if (!string.IsNullOrEmpty(request.Email)) {
                var email = await _userRepository.UserWithEmailExists(request.Email);
                
                if (email)
                    throw new ConflictException(GeneralMessages.UserWithEmailExists);
            }

            var newUser = _mapper.Map<User>(request);
            newUser.UserId = Guid.NewGuid();
            newUser.IsActive = true;
            newUser.CreatedAt = DateTime.Now;

            newUser.UserRole = new UserRole
            {
                RoleId = RolesKey.UserRoleId,
                UserId = newUser.UserId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            await _userRepository.AddUser(newUser);
            await _userRepository.SaveChanges();

            return new Response<UserResponseDTO>(true, null, GeneralMessages.RecordAdded);
        }

        public async Task<Response<UserResponseDTO>> UpdateUser(UpdateUserRequestDTO request, Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user == null)
                throw new KeyNotFoundException(GeneralMessages.UserNotFound);

            // Check if user with this username already exists
            if (!request.UserName.Equals(user.UserName))
            { 
                var username = await _userRepository.UserWithUsernameExists(request.UserName);
                if (username)
                    throw new ConflictException(GeneralMessages.UserWithUsernameExists);
            }

            // Check if user with this email already exists
            if (!string.IsNullOrEmpty(request.Email))
            {
                if (!string.IsNullOrEmpty(user.Email) && !request.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var email = await _userRepository.UserWithEmailExists(request.Email);

                    if (email)
                        throw new ConflictException(GeneralMessages.UserWithEmailExists);
                }
                else if(string.IsNullOrEmpty(user.Email))
                {
                    var email = await _userRepository.UserWithEmailExists(request.Email);

                    if (email)
                        throw new ConflictException(GeneralMessages.UserWithEmailExists);
                }
            }


            var updatedUser = _mapper.Map(request, user);
            updatedUser.UpdatedAt = DateTime.Now;
            
            await _userRepository.SaveChanges();

            var resposne = _mapper.Map<UserResponseDTO>(updatedUser);

            return new Response<UserResponseDTO>(true, resposne, GeneralMessages.RecordUpdated);
        }

        public async Task<Response<bool>> DeleteUser(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException(GeneralMessages.UserNotFound);

            // also delete user roles
            user.UserRole.IsActive = false;
            user.UserRole.UpdatedAt = DateTime.Now;
            user.UserRole.DeletedAt = DateTime.Now;

            user.UpdatedAt = DateTime.Now;
            user.DeletedAt = DateTime.Now;
            user.IsActive = false;

            await _userRepository.SaveChanges();
            return new Response<bool>(true, true, GeneralMessages.RecordDeleted);
        }

        public async Task<Response<List<OrderResponseDTO>>> GetAllUserActiveOrders(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException(GeneralMessages.UserNotFound);

            var orders = await _orderRepository.GetAllUserActiveOrders(userId);
            if(orders.Count == 0)
                return new Response<List<OrderResponseDTO>>(true, null, GeneralMessages.NoOrderPlaced);

            var response = _mapper.Map<List<OrderResponseDTO>>(orders);
            return new Response<List<OrderResponseDTO>>(true, response, GeneralMessages.RecordFetched);
        }

        public async Task<Response<OrderResponseDTO>> GetUserActiveOrderById(Guid userId, int orderId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException(GeneralMessages.UserNotFound);

            var order = await _orderRepository.GetUserActiveOrderById(userId, orderId);
            if(order == null)
                throw new KeyNotFoundException(GeneralMessages.OrderNotFound);

            var response = _mapper.Map<OrderResponseDTO>(order);
            return new Response<OrderResponseDTO>(true, response, GeneralMessages.RecordFetched);
        }

        public async Task<Response<bool>> UploadProfilePicture(Guid userId, FileUploadRequestDTO profilePic)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException(GeneralMessages.UserNotFound);

            // Check if profile pic already exists, then remove it first
            if(!string.IsNullOrEmpty(user.ProfilePicPath))
            {
                var isImageDeleted = await awsservice.DeleteImage(user.ProfilePicPath);
                if(!isImageDeleted)
                    throw new Exception(GeneralMessages.ImageError);
            }

            var file = profilePic.File;
            if (file != null && file.Length > 0)
            {
                string fileName = file.FileName;
                string timestamp = DateTime.Now.ToFileTime().ToString();
                string fileKey = timestamp + fileName;

                byte[] bytes = CommonHelper.ConvertToByteArray(file);
                if (bytes.Length > 0)
                {
                    await awsservice.UploadImage(bytes, fileKey);
                    user.ProfilePicPath = fileKey;
                    await _userRepository.SaveChanges();
                    return new Response<bool>(true, true, GeneralMessages.ProfilePicUploaded);
                }
                else
                {
                    throw new Exception(GeneralMessages.ImageError);
                }
            }
            throw new BadRequestException(GeneralMessages.InvalidFile);
        }
    }
}
