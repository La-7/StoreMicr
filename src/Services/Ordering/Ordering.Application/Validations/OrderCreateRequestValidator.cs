using FluentValidation;
using Ordering.Application.Models.Requests;

namespace Ordering.Application.Validations
{
    public class OrderCreateRequestValidator : AbstractValidator<OrderCreateRequest>
    {
        public OrderCreateRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required")
                .MaximumLength(50).WithMessage("User name must be exceed 50 characters");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("Total price is required")
                .GreaterThan(0).WithMessage("Total price should be greater than 0");
        }
    }
}
