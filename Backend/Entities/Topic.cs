using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class Topic
{
    [Key]
    public Guid TopicId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    // Navigational hmm?
    public ICollection<Prediction>? Predictions { get; set; }
}
