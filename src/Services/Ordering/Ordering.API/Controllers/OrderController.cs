using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Business.Orders.Commands.CreateOrder;
using Ordering.Application.Business.Orders.Commands.DeleteOrder;
using Ordering.Application.Business.Orders.Commands.UpdateOrder;
using Ordering.Application.Business.Orders.Queries.GetOrders;
using Ordering.Application.Models.Requests;
using Ordering.Application.Models.Responses;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetOrdersQuery(userName), cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<long>> Create([FromBody] OrderCreateRequest request,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new CreateOrderCommand(request), cancellationToken);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] OrderUpdateRequest request, 
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateOrderCommand(request), cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
