using Backend.DTOs;
using Backend.DTOs.Prediction;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Repositories;

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

    public async Task<PredictionDto?> CreateAsync(string id, CreatePredictionDto createDto)
    {
        var topic = await _topicrepo.GetByIdAsync(createDto.TopicId); //Should it be joins or topic + predcition repo
        if (topic == null)
        {
            return null;
        }

        var predictionEntity = createDto.ToPredictionEntity(id);
        var createdPrediction = await _predictionRepo.CreateAsync(predictionEntity);
        return createdPrediction.ToPredictionDto();
    }

    // public Task<PredictionDto?> UpdateByIdAsync(Guid id, UpdatePredictionDto updateDto)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<PredictionDto> DeleteAllAsync()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<PredictionDto?> DeleteByIdAsync(Guid id)
    // {
    //     throw new NotImplementedException();
    // }
}
