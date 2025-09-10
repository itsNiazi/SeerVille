namespace Backend.DTOs.User;

public class UserPredictionsDto
{
    public Guid PredictionId { get; set; }
    public string PredictionName { get; set; }
    public string PredictionRules { get; set; }
    public DateTime PredictionDate { get; set; }
    public DateTime ResolutionDate { get; set; }
    public DateTime VotedDate { get; set; }
    public bool IsResolved { get; set; }
    public bool? IsCorrect { get; set; }

    public string TopicName { get; set; } = string.Empty;

    public bool UserVote { get; set; }
}
