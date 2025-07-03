namespace Backend.DTOs;

// should change to PUT and requiring all relevant fields
public class PatchTopicDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
