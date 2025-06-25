namespace Backend.DTOs;

public class CreatePredictionDto
{
    public Guid CreatorId { get; set; }
    public Guid TopicId { get; set; }
    public required string PredictionName { get; set; }
    public DateTime ResolutionDate { get; set; }
}
