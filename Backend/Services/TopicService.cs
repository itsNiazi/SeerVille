using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Mappers;

namespace Backend.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepo;

    public TopicService(ITopicRepository topicRepository)
    {
        _topicRepo = topicRepository;
    }

    public async Task<List<TopicDto>> GetAllAsync()
    {
        var topics = await _topicRepo.GetAllAsync();
        var topicDto = topics.Select(x => x.ToTopicDto()).ToList();
        return topicDto;
    }

    public async Task<TopicDto?> GetByIdAsync(Guid id)
    {
        var topic = await _topicRepo.GetByIdAsync(id);
        return topic == null ? null : topic.ToTopicDto();
    }

    public async Task<Topic> CreateAsync(CreateTopicDto topic)
    {
        var topicEntity = topic.ToTopicEntity();
        var created = await _topicRepo.CreateAsync(topicEntity);
        return created;  //hmmm...
    }

    public async Task<Topic?> DeleteAllAsync()
    {
        return await _topicRepo.DeleteAllAsync();
    }

    public async Task<TopicDto?> DeleteByIdAsync(Guid id)
    {
        var deleted = await _topicRepo.DeleteAsync(id);
        return deleted == null ? null : deleted.ToTopicDto();
    }

    public async Task<TopicDto?> UpdateAsync(Guid id, UpdateTopicDto patch)
    {
        var topic = await _topicRepo.GetByIdAsync(id);
        if (topic == null) return null;

        if (!string.IsNullOrWhiteSpace(patch.Name))
            topic.Name = patch.Name.Trim();

        if (!string.IsNullOrWhiteSpace(patch.Description))
            topic.Description = patch.Description.Trim();

        await _topicRepo.UpdateAsync(topic);
        return topic.ToTopicDto();
    }
}
