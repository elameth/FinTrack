using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using FinTrackPro.Domain.Exceptions;
using MediatR;

namespace FinTrackPro.Application.Queries.Auth;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, AuthTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthTokenDto> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(query.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        return new AuthTokenDto(accessToken, refreshToken, _jwtTokenService.AccessTokenExpirationMinutes);
    }
}
