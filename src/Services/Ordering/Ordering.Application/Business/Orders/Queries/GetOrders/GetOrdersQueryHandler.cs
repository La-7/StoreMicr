using AutoMapper;
using MediatR;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Application.Models.Responses;

namespace Ordering.Application.Business.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrdersQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserName(request.UserName);
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
