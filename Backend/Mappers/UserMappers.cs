using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public static User ToUserEntity(this CreateUserDto user)
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Username = user.Username,
            PasswordHash = user.Password, // + hashing function logic
            Email = user.Email,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
