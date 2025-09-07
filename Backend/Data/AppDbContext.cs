using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<PredictionVote> PredictionVotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // USERS
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.UserId);

            entity.Property(u => u.UserId).HasColumnName("user_id");
            entity.Property(u => u.Username).HasColumnName("username");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
            entity.Property(u => u.Role).HasColumnName("role");
            entity.Property(u => u.CreatedAt).HasColumnName("created_at");
            entity.Property(u => u.AvatarPath).HasColumnName("avatar_path");

            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });

        // TOPICS
        modelBuilder.Entity<Topic>(entity =>
        {
            entity.ToTable("Topics");

            entity.HasKey(t => t.TopicId);

            entity.Property(t => t.TopicId).HasColumnName("topic_id");
            entity.Property(t => t.Name).HasColumnName("name");
            entity.Property(t => t.Description).HasColumnName("description");

            entity.HasIndex(t => t.Name).IsUnique();
        });

        // PREDICTIONS
        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.ToTable("Predictions");

            entity.HasKey(p => p.PredictionId);

            entity.Property(p => p.PredictionId).HasColumnName("prediction_id");
            entity.Property(p => p.CreatorId).HasColumnName("creator_id");
            entity.Property(p => p.TopicId).HasColumnName("topic_id");
            entity.Property(p => p.PredictionName).HasColumnName("prediction_name");
            entity.Property(p => p.PredictionRules).HasColumnName("prediction_rules");
            entity.Property(p => p.PredictionDate).HasColumnName("prediction_date");
            entity.Property(p => p.ResolutionDate).HasColumnName("resolution_date");
            entity.Property(p => p.IsResolved).HasColumnName("is_resolved");
            entity.Property(p => p.IsCorrect).HasColumnName("is_correct");
            entity.Property(p => p.ResolvedBy).HasColumnName("resolved_by");
            entity.Property(p => p.ResolvedAt).HasColumnName("resolved_at");

            // Relationships
            entity.HasOne(p => p.Creator)
                  .WithMany(u => u.PredictionsCreated)
                  .HasForeignKey(p => p.CreatorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Topic)
                  .WithMany(t => t.Predictions)
                  .HasForeignKey(p => p.TopicId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.ResolvedByUser)
                  .WithMany(u => u.PredictionsResolved)
                  .HasForeignKey(p => p.ResolvedBy)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // PREDICTION VOTES
        modelBuilder.Entity<PredictionVote>(entity =>
        {
            entity.ToTable("PredictionVotes");

            entity.HasKey(v => v.VoteId);

            entity.Property(v => v.VoteId).HasColumnName("vote_id");
            entity.Property(v => v.PredictionId).HasColumnName("prediction_id");
            entity.Property(v => v.UserId).HasColumnName("user_id");
            entity.Property(v => v.PredictedOutcome).HasColumnName("predicted_outcome");
            entity.Property(v => v.VotedAt).HasColumnName("voted_at");

            entity.HasOne(v => v.Prediction)
                  .WithMany(p => p.Votes)
                  .HasForeignKey(v => v.PredictionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(v => v.User)
                  .WithMany(u => u.Votes)
                  .HasForeignKey(v => v.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(v => new { v.PredictionId, v.UserId }).IsUnique();
        });
    }

}
