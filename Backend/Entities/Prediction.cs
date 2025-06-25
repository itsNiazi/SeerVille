using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Prediction
{
    [Key]
    [Required]
    [MaxLength(50)]
    public Guid PredictionId { get; set; }

    [Required]
    public required Guid CreatorId { get; set; }

    [Required]
    public required Guid TopicId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string PredictionName { get; set; }

    [Required]
    public DateTime PredictionDate { get; set; }

    [Required]
    public DateTime ResolutionDate { get; set; }

    public Boolean IsResolved { get; set; }

    public Boolean IsCorrect { get; set; }

    public Guid ResolvedBy { get; set; }

    public DateTime ResolvedAt { get; set; }

    [ForeignKey("CreatorId")]
    public required User Creator { get; set; }

    [ForeignKey("TopicId")]
    public required Topic Topic { get; set; }

    [ForeignKey("ResolvedBy")]
    public required User ResolvedByUser { get; set; }

    // hmm?
    public ICollection<PredictionVote>? Votes { get; set; }
}
