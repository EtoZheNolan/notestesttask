using FluentValidation;
using Notes.Application.DTOs.Requests;

namespace Notes.Api.Validators;

public class SignupRequestValidator : AbstractValidator<SignupRequestDto>
{
    public SignupRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(2).MaximumLength(12);
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MinimumLength(12);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(12);
        
        RuleFor(x => x.Password).NotEmpty()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[!@#$%^&*]").WithMessage("Password must contain at least one special character (!@#$%^&*).");
    }
}