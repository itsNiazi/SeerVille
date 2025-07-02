using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Topic
{
    [Key]
    [Column("topic_id")]
    public Guid TopicId { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public required string Description { get; set; }

    // Navigational hmm?
    public ICollection<Prediction>? Predictions { get; set; }
}
