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

        // Need to define Relationships and Constraints here
        modelBuilder.Entity<Prediction>()
       .HasOne(p => p.Creator)
       .WithMany()
       .HasForeignKey(p => p.CreatorId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.ResolvedByUser)
            .WithMany()
            .HasForeignKey(p => p.ResolvedBy)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.Topic)
            .WithMany()
            .HasForeignKey(p => p.TopicId);

        modelBuilder.Entity<PredictionVote>()
            .HasOne(pv => pv.User)
            .WithMany()
            .HasForeignKey(pv => pv.UserId);

        modelBuilder.Entity<PredictionVote>()
            .HasOne(pv => pv.Prediction)
            .WithMany(p => p.Votes)
            .HasForeignKey(pv => pv.PredictionId);
    }
}
