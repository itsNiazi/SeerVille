using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreateTopicDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }
}
