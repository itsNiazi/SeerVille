using Backend.Entities;

namespace Backend.Interfaces;

public interface IPredictionRepository
{
    Task<List<Prediction>> GetAllAsync();
    Task<Prediction?> GetByIdAsync(Guid id);
    Task<List<Prediction>> GetByTopicIdAsync(Guid id);
    Task<Prediction> CreateAsync(Prediction prediction);
    Task<Prediction> PatchAsync(Prediction patch);
    Task<Prediction> UpdateAsync(Prediction update);
    Task<Prediction?> DeleteAllAsync();
    Task<Prediction> DeleteAsync(Prediction prediction);
}
