using Backend.Entities;

namespace Backend.Interfaces;

public interface IPredictionVoteRepository
{
    Task<List<PredictionVote>> GetAllByIdAsync(string userId);
    Task<PredictionVote> VoteAsync(PredictionVote vote);
}
