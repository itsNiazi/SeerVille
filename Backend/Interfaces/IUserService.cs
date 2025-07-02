using Backend.DTOs;
using Backend.DTOs.User;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<UserDto?> LoginUserAsync(LoginUserDto loginDto);
    Task<UserDto?> RegisterUserAsync(RegisterUserDto registerDto);
    // ChangePassword
    // DeleteUser
    // ToModerator
    // ToAdmin
}
