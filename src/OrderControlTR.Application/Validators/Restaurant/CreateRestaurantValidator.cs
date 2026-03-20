using FluentValidation;
using OrderControlTR.Application.DTOs.Restaurant;

namespace OrderControlTR.Application.Validators.Restaurant;

public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
    }
}
