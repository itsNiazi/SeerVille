using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class User
{
    [Key]
    [Column("user_id")]
    public required Guid UserId { get; set; }
    [Column("username")]
    public required string Username { get; set; }
    [Column("email")]
    public required string Email { get; set; }
    [Column("password_hash")]
    public required string PasswordHash { get; set; }
    [Column("role")]
    public required string Role { get; set; }
    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    // Navigational hmm?
    public ICollection<Prediction>? PredictionsCreated { get; set; }
    public ICollection<Prediction>? PredictionsResolved { get; set; }
    public ICollection<PredictionVote>? Votes { get; set; }
}
