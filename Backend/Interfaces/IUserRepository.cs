using Backend.Entities;

namespace Backend.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> RegisterUserAsync(User user);
    Task<User> PromoteUserAsync(User user);
}
