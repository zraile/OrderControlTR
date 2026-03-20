using FluentValidation;
using OrderControlTR.Application.DTOs.OrderItem;

namespace OrderControlTR.Application.Validators.OrderItem;

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.MenuItemId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
