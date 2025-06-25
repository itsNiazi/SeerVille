namespace Backend.DTOs;

public class TopicDto
{
    public Guid TopicId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}

