using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Interfaces.Infrastructure;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Application.Models;
using Ordering.Application.Models.Requests;
using Ordering.Domain.Entities;

namespace Ordering.Application.Business.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand: IRequest<long>
    {
        public OrderCreateRequest Request { get; }

        public CreateOrderCommand(OrderCreateRequest request)
        {
            Request = request;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, 
            IEmailService emailService, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<long> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(command.Request);
            var newOrder = await _orderRepository.Add(order);

            _logger.LogInformation($"{newOrder.Id} order is created");

            await SendEmail(newOrder);

            return newOrder.Id;
        }

        private async Task SendEmail(Order order)
        {
            var email = new Email
                { To = "theyneedr@gmail.com", Body = $"Order was created", Subject = "New order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due an email service error: {ex.Message}");
            }
        }
    }
}
