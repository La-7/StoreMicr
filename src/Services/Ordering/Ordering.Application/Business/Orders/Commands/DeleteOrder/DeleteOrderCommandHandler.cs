using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Business.Orders.Commands.UpdateOrder;
using Ordering.Application.Exceptions;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Business.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public long Id { get; }

        public DeleteOrderCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(command.Id) ??
                        throw new NotFoundExceptions(nameof(Order), command.Id);
            await _orderRepository.Delete(order);
            _logger.LogInformation($"Order {order.Id} is successfully removed");
        }
    }
}
