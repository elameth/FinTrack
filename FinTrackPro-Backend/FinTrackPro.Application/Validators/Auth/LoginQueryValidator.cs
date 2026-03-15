using FinTrackPro.Application.Queries.Auth;
using FluentValidation;

namespace FinTrackPro.Application.Validators.Auth;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(query => query.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(query => query.Password)
            .NotEmpty();
    }
}
