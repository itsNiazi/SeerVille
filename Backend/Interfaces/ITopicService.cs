using Backend.DTOs;
using Backend.Entities;

namespace Backend.Interfaces;

public interface ITopicService
{
    Task<List<TopicDto>> GetAllAsync();
    Task<TopicDto?> GetByIdAsync(Guid id);
    Task<Topic> CreateAsync(CreateTopicDto topic);
    Task<TopicDto?> DeleteByIdAsync(Guid id);
    Task<Topic?> DeleteAllAsync(); //Not really architecturally needed, just for development purposes
    Task<TopicDto?> PatchAsync(Guid id, PatchTopicDto patch);
}
