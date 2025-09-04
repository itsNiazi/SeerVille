namespace Backend.DTOs;

/// <summary>
/// Represents the base user object sent to the client, without overexposing details.
/// </summary>
public class BaseUserDto
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string AvatarPath { get; set; }
}