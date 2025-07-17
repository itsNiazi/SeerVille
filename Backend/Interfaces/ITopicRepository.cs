using Backend.Entities;

namespace Backend.Interfaces;

public interface ITopicRepository
{
    /// <summary>
    /// Retrieves all topics from the database.
    /// </summary>
    /// <returns>
    /// <para>A list of<c><see cref="Topic"/></c></para>
    /// </returns>
    Task<List<Topic>> GetAllAsync();

    /// <summary>
    /// Retrieves topic with provided unique identifier from the database.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="Topic"/></c> if the topic is found.</para>
    /// <para><c>null</c> if no matching topics with provided ID.</para>
    /// </returns>
    Task<Topic?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates new topic in the database.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="Topic"/></c></para>
    /// </returns>
    Task<Topic> CreateAsync(Topic topic);

    /// <summary>
    /// Deletes all topics from the database.
    /// </summary>
    Task<Topic?> DeleteAllAsync();

    /// <summary>
    /// Deletes topic in database with provided unique identifier.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="Topic"/></c></para>
    /// </returns>
    Task<Topic?> DeleteAsync(Guid id);

    /// <summary>
    /// Updates topic in database with provided modifications.
    /// </summary>
    /// <returns>
    /// <para>Modified<c><see cref="Topic"/></c>.</para>
    /// </returns>
    Task<Topic?> UpdateAsync(Topic topic);

    /// <summary>
    /// Checks whether topic with provided unique identifier exists in database.
    /// </summary>
    /// <returns>
    /// <para><c>boolean</c>.</para>
    /// </returns>
    Task<bool> CheckExists(Guid id);
}
