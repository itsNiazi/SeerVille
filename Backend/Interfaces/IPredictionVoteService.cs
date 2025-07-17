using Backend.DTOs;

namespace Backend.Interfaces;

public interface IPredictionVoteService
{
    Task<List<PredictionVoteDto>> GetAllByIdAsync(string userId);
    Task<PredictionVoteDto?> VoteAsync(string userId, CreatePredictionVoteDto voteDto);
}
