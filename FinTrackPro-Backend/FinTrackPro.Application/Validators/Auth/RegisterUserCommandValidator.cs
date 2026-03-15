using FinTrackPro.Application.Commands.Auth;
using FluentValidation;

namespace FinTrackPro.Application.Validators.Auth;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(255)
            .EmailAddress();

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100);

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
    }
}
