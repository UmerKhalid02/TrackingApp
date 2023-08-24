using AutoMapper;
using TrackingApp.Application.DataTransferObjects.OrderDTO;
using TrackingApp.Data.Entities.OrderEntity;

namespace TrackingApp.Web.Modules.Orders.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            OrderDetailsMapper();
            AddOrderMapper();
            UpdateOrderMapper();
        }

        private void OrderDetailsMapper()
        {
            CreateMap<Order, OrderResponseDTO>()
                .ForPath(dest => dest.Customer, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
        
        private void AddOrderMapper()
        {
            CreateMap<Order, AddOrderRequestDTO>().ReverseMap();
        }
        
        private void UpdateOrderMapper()
        {
            CreateMap<Order, UpdateOrderRequestDTO>().ReverseMap();
        }

    }
}
