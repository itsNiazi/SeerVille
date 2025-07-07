using System.ComponentModel.DataAnnotations;
using Backend.Helpers;

namespace Backend.DTOs.User;

/// <summary>
/// Represents object structure and format required from client for role change.
/// </summary>
public class PromoteUserDto
{
    [Required]
    [AllowedValues([Roles.Admin, Roles.Moderator, Roles.User], ErrorMessage = "Invalid role value.")]
    public required string Role { get; set; } //need to change to a "role" type
}