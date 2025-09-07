using Backend.DTOs;
using Backend.Entities;
using Backend.Helpers.QueryObjects;

namespace Backend.Interfaces;

public interface IPredictionRepository
{
    Task<List<PredictionSummaryDto>> GetAllAsync(Guid userId, PredictionQuery query);
    Task<Prediction?> GetByIdAsync(Guid id);
    Task<List<Prediction>> GetByTopicIdAsync(Guid id);
    Task<Prediction> CreateAsync(Prediction prediction);
    Task<Prediction> PatchAsync(Prediction patch);
    Task<Prediction> UpdateAsync(Prediction update);
    Task<Prediction?> DeleteAllAsync();
    Task<Prediction> DeleteAsync(Prediction prediction);
}
