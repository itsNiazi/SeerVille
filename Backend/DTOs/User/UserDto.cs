using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class UserDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Role { get; set; }

    [Required]
    public required string Token { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
}
