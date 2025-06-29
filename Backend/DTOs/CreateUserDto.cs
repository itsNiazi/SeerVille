using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreateUserDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string Email { get; set; }
}
