using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Commands.Auth;

public record RegisterUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<AuthTokenDto>;
