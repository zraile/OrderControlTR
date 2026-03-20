using FluentValidation.TestHelper;
using OrderControlTR.Application.DTOs.Auth;
using OrderControlTR.Application.Validators.Auth;

namespace OrderControlTR.Application.Tests.Validators;

public class RegisterRequestValidatorTests
{
    private readonly RegisterRequestValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_FirstName_IsEmpty()
    {
        var model = new RegisterRequestDto { FirstName = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_HaveError_When_Email_IsInvalid()
    {
        var model = new RegisterRequestDto { Email = "not-an-email" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_HaveError_When_Password_IsTooShort()
    {
        var model = new RegisterRequestDto { Password = "12345" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_NotHaveError_When_AllFieldsAreValid()
    {
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            UserName = "johndoe",
            Password = "password123"
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
