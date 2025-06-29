using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreatePredictionDto
{
    [Required]
    public Guid CreatorId { get; set; }

    [Required]
    public required Guid TopicId { get; set; }

    [Required]
    public required string PredictionName { get; set; }

    [Required]
    public required DateTime ResolutionDate { get; set; }
}
