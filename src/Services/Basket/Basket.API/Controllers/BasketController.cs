using System.Net;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpsServices;
using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, IMapper mapper,
            IPublishEndpoint publishEndpoint, DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Get(string userName)
        {
            return Ok(await _repository.Get(userName) ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Update([FromBody] ShoppingCart basket)
        {
            foreach (var cartItem in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(cartItem.Name);
                cartItem.Price -= coupon.Amount;
            }

            return Ok(await _repository.Update(basket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string userName)
        {
            await _repository.Delete(userName);
            return Ok();
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BasketCheckout([FromBody] BasketCheckoutModel basketCheckout)
        {
            var basket = await _repository.Get(basketCheckout.UserName);
            if (basket is null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            await _repository.Delete(basket.UserName);
            return Accepted();
        }
    }
}
