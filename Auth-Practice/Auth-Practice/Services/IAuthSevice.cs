using Auth_Practice.Entities;
using Auth_Practice.Entities.Models;

namespace Auth_Practice.Services;

public interface IAuthSevice
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
}