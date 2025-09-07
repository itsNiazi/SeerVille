using Backend.DTOs;
using Backend.DTOs.Prediction;
using Backend.Entities;
using Backend.Helpers.QueryObjects;

namespace Backend.Interfaces;

public interface IPredictionService
{
    Task<List<PredictionSummaryDto>> GetAllAsync(Guid userId, PredictionQuery query);
    Task<PredictionDto?> GetByIdAsync(Guid id);
    Task<List<PredictionDto>?> GetByTopicIdAsync(Guid id);
    Task<PredictionDto?> CreateAsync(string id, CreatePredictionDto createDto);
    Task<PredictionDto?> UpdateByIdAsync(Guid id, UpdatePredictionDto updateDto);
    Task<PredictionDto?> PatchByIdAsync(Guid id, string resolverId, PatchPredictionDto patchDto);
    Task<Prediction?> DeleteAllAsync();
    Task<PredictionDto?> DeleteByIdAsync(Guid id);
}
