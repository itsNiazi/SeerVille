namespace Backend.DTOs;

/// <summary>
/// Represents topic object sent to the client.
/// </summary>
public class TopicDto
{
    public required Guid TopicId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string Icon { get; set; } = string.Empty;
}