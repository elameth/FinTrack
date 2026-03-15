namespace FinTrackPro.Application.DTOs;

public record AuthTokenDto(string AccessToken, string RefreshToken, int ExpiresInMinutes);
