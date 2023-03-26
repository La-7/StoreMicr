using System.Net;
using System.Reflection;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Coupon>> Get(string productName)
        {
            return Ok(await _repository.Get(productName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] Coupon coupon)
        {
            await _repository.Create(coupon);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] Coupon coupon)
        {
            await _repository.Update(coupon);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string productName)
        {
            await _repository.Delete(productName);
            return Ok();
        }
    }
}
