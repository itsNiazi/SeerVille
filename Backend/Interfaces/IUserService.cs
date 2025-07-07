using Backend.DTOs;
using Backend.DTOs.User;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<List<BaseUserDto>> GetAllAsync();
    Task<BaseUserDto?> GetByIdAsync(Guid id);
    Task<UserDto?> LoginUserAsync(LoginUserDto loginDto);
    Task<UserDto?> RegisterUserAsync(RegisterUserDto registerDto);
    Task<UserDto?> PromoteUserAsync(Guid id, PromoteUserDto promoteDto);
    // ChangePassword
    // DeleteUser
}
