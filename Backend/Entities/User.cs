using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class User
{
    [Key]
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
    public required DateTime CreatedAt { get; set; }

    // Navigational hmm?
    public ICollection<Prediction>? PredictionsCreated { get; set; }
    public ICollection<Prediction>? PredictionsResolved { get; set; }
    public ICollection<PredictionVote>? Votes { get; set; }
}
