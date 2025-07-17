using Backend.DTOs;
using Backend.Entities;

namespace Backend.Interfaces;

public interface ITopicService
{
    /// <summary>
    /// Retrieves all topics from data layer.
    /// </summary>
    /// <returns>
    /// <para>A list of<c><see cref="TopicDto"/></c></para>
    /// </returns>
    Task<List<TopicDto>> GetAllAsync();

    /// <summary>
    /// Retrieves topic with provided unique identifier from the data layer.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="TopicDto"/></c> if the topic is found.</para>
    /// <para><c>null</c> if no matching topics with provided id.</para>
    /// </returns>
    Task<TopicDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates new topic in the system.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="TopicDto"/></c></para>
    /// </returns>
    Task<TopicDto> CreateAsync(CreateTopicDto topic);

    /// <summary>
    /// Deletes all topic from the system.
    /// </summary>
    Task<Topic?> DeleteAllAsync();

    /// <summary>
    /// Deletes topic with provided unique identifier.
    /// </summary>
    /// <returns>
    /// <para>Deleted<c><see cref="TopicDto"/></c> if the topic is found.</para>
    /// <para><c>null</c> if no matching topics with provided id.</para>
    /// </returns>
    Task<TopicDto?> DeleteByIdAsync(Guid id);

    /// <summary>
    /// Updates topic with provided 
    /// </summary>
    /// <returns>
    /// <para>Updated<c><see cref="TopicDto"/></c> if the topic is found.</para>
    /// <para><c>null</c> if no matching topics with provided id.</para>
    /// </returns>
    Task<TopicDto?> UpdateAsync(Guid id, UpdateTopicDto update);
}
