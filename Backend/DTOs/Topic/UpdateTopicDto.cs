using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

/// <summary>
/// Represents topic object structure and format required from client for updating topic.
/// </summary>
public class UpdateTopicDto
{
    [StringLength(30, MinimumLength = 2)]
    [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Name cannot contain spaces or special characters.")]
    public string? Name { get; set; }

    public string? Description { get; set; }
}