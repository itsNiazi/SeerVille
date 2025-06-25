using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class User
{
    [Key]
    [Required]
    [MaxLength(50)]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(20)]
    public required string Username { get; set; }

    [Required]
    [MaxLength(20)]
    public required string PasswordHash { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    // hmm?
    public ICollection<Prediction>? PredictionsCreated { get; set; }
    public ICollection<Prediction>? PredictionsResolved { get; set; }
    public ICollection<PredictionVote>? Votes { get; set; }
}
