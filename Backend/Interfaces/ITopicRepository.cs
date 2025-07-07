using Backend.Entities;

namespace Backend.Interfaces;

public interface ITopicRepository
{
    Task<List<Topic>> GetAllAsync();
    Task<Topic?> GetByIdAsync(Guid id);
    Task<Topic> CreateAsync(Topic topic);
    Task<Topic?> DeleteAsync(Guid id);
    Task<Topic?> DeleteAllAsync();
    Task<Topic?> UpdateAsync(Topic topic);
    Task<bool> CheckExists(Guid id);
}
