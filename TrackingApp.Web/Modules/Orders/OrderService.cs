﻿using AutoMapper;
using TrackingApp.Application.DataTransferObjects.OrderDTO;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Exceptions;
using TrackingApp.Application.Extensions;
using TrackingApp.Application.Helpers;
using TrackingApp.Application.Parameters;
using TrackingApp.Application.Wrappers;
using TrackingApp.Data.Entities.OrderEntity;
using TrackingApp.Data.IRepositories.IOrderRepository;
using TrackingApp.Data.IRepositories.IUserRepository;
using TrackingApp.Web.Extensions;

namespace TrackingApp.Web.Modules.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly AWS _awsservice;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IUserRepository userService, AWS aws, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userService;
            _awsservice = aws;
            _mapper = mapper;
        }

        public async Task<Response<PaginationResponseModel>> GetAllActiveOrders(OrderPageParamter request)
        {
            var response = await _orderRepository.GetAllActiveOrders();

            var query = response.AsQueryable();
            if (!string.IsNullOrEmpty(request.OrderName))
            {
                query = query.Where(a => a.OrderName.ToLower().Contains(request.OrderName.ToLower()));
            }

            if (request.Quantity != null && request.Quantity != 0)
            {
                query = query.Where(a => a.Quantity == request.Quantity);
            }

            if (!string.IsNullOrEmpty(request.OrderStatus))
            {
                query = query.Where(a => a.OrderStatus.ToLower().Contains(request.OrderStatus.ToLower()));
            }

            request.OrderBy ??= PaginationOrder.OrderName;
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

            var responseQuery = _mapper.Map<List<OrderResponseDTO>>(query.ToList());

            var result = OrderPagedList<OrderResponseDTO>.CreateAsync(responseQuery.AsQueryable(), request);

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

        public async Task<Response<PaginationResponseModel>> GetAllCompletedOrders(OrderPageParamter request)
        {
            var response = await _orderRepository.GetAllCompletedOrders();

            var query = response.AsQueryable();
            if (!string.IsNullOrEmpty(request.OrderName))
            {
                query = query.Where(a => a.OrderName.ToLower().Contains(request.OrderName.ToLower()));
            }

            if (request.Quantity != null && request.Quantity != 0)
            {
                query = query.Where(a => a.Quantity == request.Quantity);
            }

            if (!string.IsNullOrEmpty(request.OrderStatus))
            {
                query = query.Where(a => a.OrderStatus.ToLower().Contains(request.OrderStatus.ToLower()));
            }

            request.OrderBy ??= PaginationOrder.OrderName;
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

            var responseQuery = _mapper.Map<List<OrderResponseDTO>>(query.ToList());

            var result = OrderPagedList<OrderResponseDTO>.CreateAsync(responseQuery.AsQueryable(), request);

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

        public async Task<Response<OrderResponseDTO>> GetActiveOrderById(int orderId)
        {
            var order = await _orderRepository.GetActiveOrderById(orderId);

            if (order == null)
                throw new BadRequestException(GeneralMessages.InvalidOrderId);

            var response = _mapper.Map<OrderResponseDTO>(order);
            return new Response<OrderResponseDTO>(true, response, GeneralMessages.RecordFetched);
        }

        public async Task<Response<OrderResponseDTO>> AddOrder(AddOrderRequestDTO request)
        {
            var user = await _userRepository.GetUserByUsername(request.UserName);
            if (user == null)
                throw new BadRequestException(GeneralMessages.InvalidUserName);

            if (request.OrderDate < DateTime.Today)
                throw new BadRequestException(GeneralMessages.InvalidOrderDate);

            if (request.OrderDate > request.DeliveryDate)
                throw new BadRequestException(GeneralMessages.InvalidDates);

            var order = _mapper.Map<Order>(request);
            order.UserId = user.UserId;
            order.User = user;
            order.OrderStatus = EOrderStatus.DYING.GetOrderStatusStringValue();

            // store order images
            if (request.OrderImage != null)
            {
                await this.UploadImage(order, request.OrderImage, OrderImageTypes.ORDERIMAGE);
            }
            if (request.StitchingImage != null)
            {
                await this.UploadImage(order, request.StitchingImage, OrderImageTypes.STITCHINGIMAGE);
            }
            
            order.IsActive = true;
            order.CreatedAt = DateTime.UtcNow;

            await _orderRepository.AddOrder(order);
            await _orderRepository.SaveChanges();

            return new Response<OrderResponseDTO>(true, null, GeneralMessages.RecordAdded);
        }

        public async Task<Response<OrderResponseDTO>> UpdateOrder(int orderId, UpdateOrderRequestDTO request)
        {
            var order = await _orderRepository.GetActiveOrderById(orderId);
            if (order == null)
                throw new BadRequestException(GeneralMessages.InvalidOrderId);

            var user = await _userRepository.GetUserByUsername(request.UserName);
            if (user == null)
                throw new BadRequestException(GeneralMessages.InvalidUserName);

            if (request.OrderDate < DateTime.Today)
                throw new BadRequestException(GeneralMessages.InvalidOrderDate);

            if (request.OrderDate > request.DeliveryDate)
                throw new BadRequestException(GeneralMessages.InvalidDates);

            _mapper.Map(request, order);
            order.UserId = user.UserId;
            order.User = user;

            // store or replace order images
            if (request.OrderImage != null)
            {
                if (string.IsNullOrEmpty(order.OrderImagePath))
                    await this.UploadImage(order, request.OrderImage, OrderImageTypes.ORDERIMAGE);

                else if (!string.IsNullOrEmpty(order.OrderImagePath) && !order.OrderImagePath.Contains(request.OrderImage.FileName))
                {
                    await _awsservice.DeleteImage(order.OrderImagePath);
                    await this.UploadImage(order, request.OrderImage, OrderImageTypes.ORDERIMAGE);
                }
            }
            if (request.StitchingImage != null)
            {
                if(string.IsNullOrEmpty(order.StitchingImagePath))
                    await this.UploadImage(order, request.StitchingImage, OrderImageTypes.STITCHINGIMAGE);

                else if (!string.IsNullOrEmpty(order.StitchingImagePath) && !order.StitchingImagePath.Contains(request.StitchingImage.FileName))
                {
                    await _awsservice.DeleteImage(order.StitchingImagePath);
                    await this.UploadImage(order, request.StitchingImage, OrderImageTypes.STITCHINGIMAGE);
                }
            }

            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.SaveChanges();
            var response = _mapper.Map<OrderResponseDTO>(order);

            return new Response<OrderResponseDTO>(true, response, GeneralMessages.RecordUpdated);
        }

        public async Task<Response<OrderResponseDTO>> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _orderRepository.GetActiveOrderById(orderId);
            if (order == null)
                throw new BadRequestException(GeneralMessages.InvalidOrderId);

            if (string.IsNullOrEmpty(status) || EOrderStatusExtensions.OrderStatusIsInvalid(status))
                throw new BadRequestException(GeneralMessages.InvalidOrderStatus);

            order.OrderStatus = status.ToUpper();
            order.IsActive = false;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.SaveChanges();

            var response = _mapper.Map<OrderResponseDTO>(order);
            return new Response<OrderResponseDTO>(true, response, GeneralMessages.RecordUpdated);
        }

        public async Task<Response<bool>> DeleteOrder(int orderId)
        {
            var order = await _orderRepository.GetActiveOrderById(orderId);
            if (order == null)
                throw new BadRequestException(GeneralMessages.InvalidOrderId);

            order.IsActive = false;
            order.UpdatedAt = DateTime.UtcNow;
            order.DeletedAt = DateTime.UtcNow;

            await _orderRepository.SaveChanges();

            return new Response<bool>(true, true, GeneralMessages.RecordDeleted);
        }

        private async Task<Response<bool>> UploadImage(Order order, IFormFile file, string type)
        {
            if (file.Length > 0)
            { 
                string fileName = file.FileName;
                string timestamp = DateTime.Now.ToFileTime().ToString();
                string fileKey = timestamp + fileName;

                byte[] bytes = CommonHelper.ConvertToByteArray(file);
                if (bytes.Length > 0)
                {
                    await _awsservice.UploadImage(bytes, fileKey);

                    if (type.Equals(OrderImageTypes.ORDERIMAGE)) {
                        order.OrderImagePath = fileKey;
                    }
                    else {
                        order.StitchingImagePath = fileKey;
                    }
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
