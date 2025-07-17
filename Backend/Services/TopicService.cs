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
        var topicDtoList = topics.Select(x => x.ToTopicDto()).ToList();
        return topicDtoList;
    }

    public async Task<TopicDto?> GetByIdAsync(Guid id)
    {
        var topic = await _topicRepo.GetByIdAsync(id);
        return topic == null ? null : topic.ToTopicDto();
    }

    public async Task<TopicDto> CreateAsync(CreateTopicDto topic)
    {
        var topicEntity = topic.ToTopicEntity();
        var createdTopic = await _topicRepo.CreateAsync(topicEntity);
        return createdTopic.ToTopicDto();
    }

    public async Task<Topic?> DeleteAllAsync()
    {
        return await _topicRepo.DeleteAllAsync();
    }

    public async Task<TopicDto?> DeleteByIdAsync(Guid id)
    {
        var deletedTopic = await _topicRepo.DeleteAsync(id);
        return deletedTopic == null ? null : deletedTopic.ToTopicDto();
    }

    public async Task<TopicDto?> UpdateAsync(Guid id, UpdateTopicDto update)
    {
        var topic = await _topicRepo.GetByIdAsync(id);

        if (topic == null)
        {
            return null;
        }

        topic.Name = update.Name;
        topic.Description = update.Description;

        await _topicRepo.UpdateAsync(topic);
        return topic.ToTopicDto();
    }
}
