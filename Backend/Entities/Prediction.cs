using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Prediction
{
    [Key]
    public Guid PredictionId { get; set; }
    public required Guid CreatorId { get; set; }
    public required Guid TopicId { get; set; }
    public required string PredictionName { get; set; }
    public required string PredictionRules { get; set; }
    public required DateTime PredictionDate { get; set; }
    public required DateTime ResolutionDate { get; set; }
    public required bool IsResolved { get; set; }
    public bool? IsCorrect { get; set; }
    public Guid? ResolvedBy { get; set; }
    public DateTime? ResolvedAt { get; set; }

    // Navigational hmm?
    [ForeignKey("CreatorId")]
    public User? Creator { get; set; }

    [ForeignKey("TopicId")]
    public Topic? Topic { get; set; }

    [ForeignKey("ResolvedBy")]
    public User? ResolvedByUser { get; set; }

    public ICollection<PredictionVote>? Votes { get; set; }
}
