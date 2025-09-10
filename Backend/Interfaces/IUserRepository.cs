using Backend.DTOs.User;
using Backend.Entities;

namespace Backend.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves all users from database.
    /// </summary>
    /// <returns><para>A list of<c><see cref="User"/></c></para></returns>
    Task<List<User>> GetAllAsync();

    Task<List<UserPredictionsDto>> GetUserPredictionsAsync(Guid userId);

    Task<UserStatsDto> GetUserStatsAsync(Guid userId);

    Task<List<UserTopicStatsDto>> GetUserTopTopicsAsync(Guid userId);

    /// <summary>
    /// Retrieves user with provided unique identifier from the database.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="User"/></c> if the user is found.</para>
    /// </returns>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves user with provided email from the database.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="User"/></c> if the email is found.</para>
    /// <para><c>null</c> if email is not found.</para>
    /// </returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Registers a new user to the database.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="User"/></c> if user is successfully registered.</para>
    /// </returns>
    Task<User> RegisterUserAsync(User user);

    /// <summary>
    /// Modifies user's role with the provided role.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="User"/></c> if user role is successfully modified.</para>
    /// </returns>
    Task<User> PromoteUserAsync(User user);
}
