using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class TopicDto
{
    [Required]
    public Guid TopicId { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }
}

