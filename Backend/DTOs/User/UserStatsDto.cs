
namespace Backend.DTOs.User;

public class UserStatsDto
{
    public int TotalVotes { get; set; }
    public int ResolvedVotes { get; set; }
    public int CorrectVotes { get; set; }
    public double Accuracy => ResolvedVotes == 0 ? 0 : (double)CorrectVotes / ResolvedVotes;
}

public class UserTopicStatsDto
{
    public Guid TopicId { get; set; }
    public string TopicName { get; set; }
    public string TopicIcon { get; set; }
    public int TotalVotes { get; set; }
    public int ResolvedVotes { get; set; }
    public int CorrectVotes { get; set; }
    public double Accuracy => ResolvedVotes == 0 ? 0 : (double)CorrectVotes / ResolvedVotes;
}
