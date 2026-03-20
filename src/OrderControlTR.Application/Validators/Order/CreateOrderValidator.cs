using FluentValidation;
using OrderControlTR.Application.DTOs.Order;

namespace OrderControlTR.Application.Validators.Order;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.BranchId).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
    }
}
