using FinTrackPro.Application.Commands.Accounts;
using FluentValidation;

namespace FinTrackPro.Application.Validators.Accounts;

public sealed class UpdateAccountNameCommandValidator : AbstractValidator<UpdateAccountNameCommand>
{
    public UpdateAccountNameCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.AccountId)
            .NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
