using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

/// <summary>
/// Represents object structure and format required from client upon registration.
/// </summary>
public class RegisterUserDto
{
    [Required]
    [StringLength(30, MinimumLength = 2)]
    [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Username cannot contain spaces or special characters.")]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }

    [Required]
    [StringLength(64, MinimumLength = 8)]
    public required string Password { get; set; }
}