using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

/// <summary>
/// Represents topic object structure and format required from client for updating topic.
/// </summary>
public class UpdateTopicDto
{
    [Required]
    [StringLength(30, MinimumLength = 2)]
    [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Name cannot contain spaces or special characters.")]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }
}