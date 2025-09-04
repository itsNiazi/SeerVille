using Backend.DTOs;
using Backend.DTOs.Prediction;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;

namespace Backend.Services;

public class PredictionService : IPredictionService
{
    private readonly IPredictionRepository _predictionRepo;
    private readonly ITopicRepository _topicrepo;

    public PredictionService(IPredictionRepository predictionRepo, ITopicRepository topicRepo)
    {
        _predictionRepo = predictionRepo;
        _topicrepo = topicRepo;
    }

    public async Task<List<PredictionVoteCountDto>> GetAllWithVotesAsync()
    {
        var predictions = await _predictionRepo.GetAllWithVotesAsync();
        return predictions;
    }

    public async Task<List<PredictionDto>> GetAllAsync()
    {
        var predictions = await _predictionRepo.GetAllAsync();
        var predictionDto = predictions.Select(x => x.ToPredictionDto()).ToList();
        return predictionDto;
    }

    public async Task<PredictionDto?> GetByIdAsync(Guid id)
    {
        var prediction = await _predictionRepo.GetByIdAsync(id);
        if (prediction == null)
        {
            return null;
        }
        return prediction.ToPredictionDto();
    }

    public async Task<List<PredictionDto>?> GetByTopicIdAsync(Guid id)
    {
        var predictions = await _predictionRepo.GetByTopicIdAsync(id);
        if (predictions == null || predictions.Count <= 0)
        {
            return null;
        }
        var predictionDto = predictions.Select(x => x.ToPredictionDto()).ToList();
        return predictionDto;
    }

    public async Task<PredictionDto?> CreateAsync(string id, CreatePredictionDto createDto)
    {
        var topicExists = await _topicrepo.CheckExists(createDto.TopicId);
        if (!topicExists)
        {
            return null;
        }

        var predictionEntity = createDto.ToPredictionEntity(id);
        var createdPrediction = await _predictionRepo.CreateAsync(predictionEntity);
        return createdPrediction.ToPredictionDto();
    }

    public async Task<PredictionDto?> UpdateByIdAsync(Guid id, UpdatePredictionDto updateDto)
    {
        var prediction = await _predictionRepo.GetByIdAsync(id);
        if (prediction == null)
        {
            return null;
        }

        // if (prediction.IsResolved)
        // {
        //     return null;
        // }

        prediction.PredictionName = updateDto.PredictionName;
        prediction.ResolutionDate = updateDto.ResolutionDate;

        var updatedPrediction = await _predictionRepo.UpdateAsync(prediction);
        return updatedPrediction.ToPredictionDto();
    }

    public async Task<PredictionDto?> PatchByIdAsync(Guid id, string resolverId, PatchPredictionDto patchDto)
    {
        var prediction = await _predictionRepo.GetByIdAsync(id);
        if (prediction == null)
        {
            return null;
        }

        prediction.IsResolved = patchDto.IsResolved;
        prediction.IsCorrect = patchDto.IsCorrect;
        prediction.ResolvedBy = Guid.Parse(resolverId);
        prediction.ResolvedAt = DateTime.UtcNow;

        var patchedPrediction = await _predictionRepo.PatchAsync(prediction);

        return patchedPrediction.ToPredictionDto();
    }

    public async Task<Prediction?> DeleteAllAsync()
    {
        // Also should not be able to delete predictions that are resolved?
        return await _predictionRepo.DeleteAllAsync();
    }

    public async Task<PredictionDto?> DeleteByIdAsync(Guid id)
    {
        // Also should not be able to delete topics that are resolved ?
        var prediction = await _predictionRepo.GetByIdAsync(id);
        if (prediction == null)
        {
            return null;
        }

        var deletedPrediction = await _predictionRepo.DeleteAsync(prediction);
        return deletedPrediction.ToPredictionDto();
    }
}
