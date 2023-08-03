using FluentValidation;
using Notes.Application.DTOs.Requests;

namespace Notes.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        const string isRequired = "is required!";

        RuleFor(x => x.Username).NotEmpty().WithMessage(x => $"{nameof(x.Username)} {isRequired}");
        RuleFor(x => x.Password).NotEmpty().WithMessage(x => $"{nameof(x.Password)} {isRequired}");
    }
}