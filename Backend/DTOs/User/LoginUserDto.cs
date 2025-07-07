using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.User;

/// <summary>
/// Represents the object structure and format required from the client upon login.
/// </summary>
public class LoginUserDto
{
    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }

    [Required]
    [StringLength(64, MinimumLength = 8)]
    public required string Password { get; set; }
}