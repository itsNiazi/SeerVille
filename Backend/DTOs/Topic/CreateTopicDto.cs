using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreateTopicDto
{
    //REGEX to prevent Special character & trailing white space?
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }
}
