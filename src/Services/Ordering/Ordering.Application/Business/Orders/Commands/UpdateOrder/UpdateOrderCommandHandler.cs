using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Exceptions;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Application.Models.Requests;
using Ordering.Domain.Entities;

namespace Ordering.Application.Business.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public OrderUpdateRequest Request { get; }

        public UpdateOrderCommand(OrderUpdateRequest request)
        {
            Request = request;
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, 
            ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(command.Request.Id) ??
                        throw new NotFoundExceptions(nameof(Order), command.Request.Id);

            _mapper.Map(command, order, typeof(OrderUpdateRequest), typeof(Order));
            await _orderRepository.Update(order);
            _logger.LogInformation($"Order {order.Id} is successfully updated");
        }
    }
}
