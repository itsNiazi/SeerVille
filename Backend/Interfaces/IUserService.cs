using Backend.DTOs;
using Backend.DTOs.User;

namespace Backend.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Retrieves all users from data layer.
    /// </summary>
    /// <returns>A list of users in the system conforming to BaseUserDto.</returns>
    Task<List<BaseUserDto>> GetAllAsync();

    /// <summary>
    /// Retrieves user with provided unique identifier from the data layer.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="BaseUserDto"/></c> if the user is found.</para>
    /// <para><c>null</c> if no matching user with provided id.</para>
    /// </returns>
    Task<BaseUserDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Authenticates user with provided email and password.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="UserDto"/></c> if user is successfully authenticated.</para>
    /// <para><c>null</c> if email or password incorrect.</para>
    /// </returns>
    Task<UserDto?> LoginUserAsync(LoginUserDto loginDto);

    /// <summary>
    /// Registers a new user to the system.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="UserDto"/></c> if user is successfully registered.</para>
    /// <para><c>null</c> if email already exists.</para>
    /// </returns>
    Task<UserDto?> RegisterUserAsync(RegisterUserDto registerDto);

    /// <summary>
    /// Promotes/Demotes user with the provided role.
    /// </summary>
    /// <returns>
    /// <para><c><see cref="UserDto"/></c> if user role is successfully modified.</para>
    /// <para><c>null</c> if user not found or role already has provided role.</para>
    /// </returns>
    Task<UserDto?> PromoteUserAsync(Guid id, PromoteUserDto promoteDto);

    // ChangePassword
    // DeleteUser
}
