using AutoMapper;
using Ordering.Application.Models.Requests;
using Ordering.Application.Models.Responses;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderUpdateRequest, Order>();
        }
    }
}
