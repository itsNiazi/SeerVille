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
    }
}
