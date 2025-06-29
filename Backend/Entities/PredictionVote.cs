
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class PredictionVote
{
    [Key]
    public Guid VoteId { get; set; }
    public required Guid PredictionId { get; set; }
    public required Guid UserId { get; set; }
    public required bool PredictedOutcome { get; set; }
    public required DateTime VotedAt { get; set; }

    [ForeignKey("PredictionId")]
    public Prediction? Prediction { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
