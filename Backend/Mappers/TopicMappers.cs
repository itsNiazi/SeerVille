using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers;

public static class TopicMappers
{
    public static TopicDto ToTopicDto(this Topic topic)
    {
        return new TopicDto
        {
            TopicId = topic.TopicId,
            Name = topic.Name,
            Description = topic.Description
        };
    }

    public static Topic ToTopicEntity(this CreateTopicDto topic)
    {
        return new Topic
        {
            TopicId = Guid.NewGuid(),
            Name = topic.Name,
            Description = topic.Description
        };
    }
}

