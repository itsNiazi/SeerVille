using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class Topic
{
    [Key]
    [Required]
    [MaxLength(50)]
    public Guid TopicId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    // hmm?
    public ICollection<Prediction>? Predictions { get; set; }
}
