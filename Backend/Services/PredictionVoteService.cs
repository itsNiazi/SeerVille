using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;

namespace Backend.Services;

public class PredictionVoteService : IPredictionVoteService
{
    private readonly IPredictionVoteRepository _predictionVoteRepo;
    private readonly IPredictionRepository _predictionRepo;

    public PredictionVoteService(IPredictionVoteRepository predictionVoteRepo, IPredictionRepository predictionRepo)
    {
        _predictionVoteRepo = predictionVoteRepo;
        _predictionRepo = predictionRepo;
    }

    public async Task<List<PredictionVoteDto>> GetAllByIdAsync(string userId)
    {
        var votes = await _predictionVoteRepo.GetAllByIdAsync(userId);
        var votesDtoList = votes.Select(x => x.ToPredictionVoteDto()).ToList();
        return votesDtoList;
    }

    public async Task<PredictionVoteDto?> VoteAsync(string userId, CreatePredictionVoteDto voteDto)
    {
        var prediction = await _predictionRepo.GetByIdAsync(voteDto.PredictionId);
        if (prediction == null || prediction.IsResolved)
        {
            return null;
        }

        //Also check if the user has already voted?
        //Also shouldn't be able to vote if resolution date has passed || resolved.
        var voteEntity = voteDto.ToPredictionVoteEntity(userId);
        await _predictionVoteRepo.VoteAsync(voteEntity);
        return voteEntity.ToPredictionVoteDto();
    }
}
