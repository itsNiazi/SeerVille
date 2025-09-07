using System.ComponentModel.DataAnnotations;

namespace Backend.Helpers.QueryObjects;

// Needs further work for pagination/infinite scrolling?
public class PredictionQuery
{
    public Guid? TopicId { get; set; }
    public bool? IsResolved { get; set; } = false;

    [AllowedValues(["popular", "endingsoon", "new"], ErrorMessage = "SortBy must be popular, new, or endingSoon")]
    public string SortBy { get; set; } = "popular";

    [Range(1, 100)]
    public int PageSize { get; set; } = 9;
}
