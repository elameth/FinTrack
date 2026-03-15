using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Queries.Auth;

public record LoginQuery(string Email, string Password) : IRequest<AuthTokenDto>;
