namespace Auth_Practice.Entities.Models;

public class RefreshTokenRequestDto
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}