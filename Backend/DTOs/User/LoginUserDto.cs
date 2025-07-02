using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.User;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

}
