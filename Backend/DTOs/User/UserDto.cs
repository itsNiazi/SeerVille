namespace Backend.DTOs;

/// <summary>
/// Represents user object sent to the client for actions requiring the 
/// access token, such as (register, login, etc.)
/// </summary>
public class UserDto
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string AccessToken { get; set; }
    public required DateTime CreatedAt { get; set; }
}