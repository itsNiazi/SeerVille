using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers;

public static class UserMappers
{
    public static BaseUserDto ToBaseUserDto(this User user)
    {
        return new BaseUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    public static UserDto ToUserDto(this User user, string token)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            AccessToken = token,
            CreatedAt = user.CreatedAt
        };
    }

    public static User ToNewUserEntity(this RegisterUserDto user, string hashedPassword)
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Username = user.Username,
            Email = user.Email,
            PasswordHash = hashedPassword,
            Role = "user",
            CreatedAt = DateTime.UtcNow,
        };
    }
}
