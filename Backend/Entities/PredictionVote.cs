
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class PredictionVote
{
    [Key]
    [Required]
    [MaxLength(50)]
    public Guid VoteId { get; set; }

    [Required]
    [MaxLength(20)]
    public required Prediction PredictionId { get; set; }

    [Required]
    [MaxLength(20)]
    public required User UserId { get; set; }

    [Required]
    public bool PredictedOutcome { get; set; }

    [Required]
    public DateTime VotedAt { get; set; }

    [ForeignKey("PredictionId")]
    public required Prediction Prediction { get; set; }

    [ForeignKey("UserId")]
    public required User User { get; set; }
}
