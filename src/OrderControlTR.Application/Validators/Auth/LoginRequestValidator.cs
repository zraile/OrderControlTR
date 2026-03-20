using FluentValidation;
using OrderControlTR.Application.DTOs.Auth;

namespace OrderControlTR.Application.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.UserName) || !string.IsNullOrEmpty(x.Email))
            .WithMessage("Either UserName or Email must be provided.");
        RuleFor(x => x.Password).NotEmpty();
    }
}
