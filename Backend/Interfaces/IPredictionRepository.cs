using Backend.Entities;

namespace Backend.Interfaces;

public interface IPredictionRepository
{
    Task<List<Prediction>> GetAllAsync();
    Task<Prediction?> GetByIdAsync(Guid id);
    // Task<Prediction?> GetByTopicAsync(string topic);
    Task<Prediction> CreateAsync(Prediction prediction);
    // Task<Prediction?> UpdateByIdAsync(Prediction updateDto);
    // Task<Prediction> DeleteAllAsync();
    // Task<Prediction?> DeleteByIdAsync(Guid id);
}
