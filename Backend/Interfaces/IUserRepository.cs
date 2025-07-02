using Backend.Entities;

namespace Backend.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> RegisterUserAsync(User user);
}
