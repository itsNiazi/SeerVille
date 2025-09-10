using Backend.DTOs;
using Backend.DTOs.User;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Mappers;

namespace Backend.Services;

public class UserService : IUserService
{
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepo;

    public UserService(TokenService tokenService, IUserRepository userRepo)
    {
        _tokenService = tokenService;
        _userRepo = userRepo;
    }

    public async Task<List<BaseUserDto>> GetAllAsync()
    {
        var users = await _userRepo.GetAllAsync();
        var userDtoList = users.Select(x => x.ToBaseUserDto()).ToList();
        return userDtoList;
    }


    public async Task<List<UserPredictionsDto>> GetUserPredictionsAsync(Guid userId)
    {
        return await _userRepo.GetUserPredictionsAsync(userId);
    }


    public async Task<UserStatsDto> GetUserStatsAsync(Guid userId)
    {
        return await _userRepo.GetUserStatsAsync(userId);
    }


    public async Task<List<UserTopicStatsDto>> GetUserTopTopicsAsync(Guid userId)
    {
        return await _userRepo.GetUserTopTopicsAsync(userId);
    }


    public async Task<BaseUserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        return user == null ? null : user.ToBaseUserDto();
    }


    public async Task<UserDto?> LoginUserAsync(LoginUserDto loginDto)
    {
        var userEntity = await _userRepo.GetByEmailAsync(loginDto.Email);
        if (userEntity == null)
        {
            return null;
        }

        var isPassword = PasswordHasher.VerifyPassword(loginDto.Password, userEntity.PasswordHash);
        if (!isPassword)
        {
            return null;
        }

        var token = _tokenService.CreateToken(userEntity);
        return userEntity.ToUserDto(token);
    }


    public async Task<UserDto?> RegisterUserAsync(RegisterUserDto registerDto)
    {
        var emailExists = await _userRepo.GetByEmailAsync(registerDto.Email);
        if (emailExists != null)
        {
            return null;
        }

        var hashedPassword = PasswordHasher.HashPassword(registerDto.Password);
        var userEntity = registerDto.ToNewUserEntity(hashedPassword);

        var token = _tokenService.CreateToken(userEntity);
        await _userRepo.RegisterUserAsync(userEntity);
        return userEntity.ToUserDto(token);
    }


    public async Task<UserDto?> PromoteUserAsync(Guid id, PromoteUserDto promoteDto)
    {
        var userEntity = await _userRepo.GetByIdAsync(id);
        if (userEntity == null || userEntity.Role == promoteDto.Role)
        {
            return null;
        }

        userEntity.Role = promoteDto.Role;

        var token = _tokenService.CreateToken(userEntity);

        var promotedUser = await _userRepo.PromoteUserAsync(userEntity);

        return promotedUser.ToUserDto(token);
    }
}
