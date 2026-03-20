using OrderControlTR.Application.DTOs.Auth;

namespace OrderControlTR.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<UserDto> GetCurrentUserAsync(int userId);
}
