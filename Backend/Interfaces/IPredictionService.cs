using Backend.DTOs;
using Backend.DTOs.Prediction;

namespace Backend.Interfaces;

public interface IPredictionService
{
    Task<List<PredictionDto>> GetAllAsync();
    Task<PredictionDto?> GetByIdAsync(Guid id);
    // Task<PredictionDto?> GetByTopicAsync(string topic);
    Task<PredictionDto?> CreateAsync(string id, CreatePredictionDto createDto);
    // Task<PredictionDto?> UpdateByIdAsync(Guid id, UpdatePredictionDto updateDto);
    // Task<PredictionDto> DeleteAllAsync();
    // Task<PredictionDto?> DeleteByIdAsync(Guid id);
}
