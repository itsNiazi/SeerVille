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

    public UserService(TokenService tokenService, IUserRepository userRepo) //we need interface for tokenservice too?
    {
        _tokenService = tokenService;
        _userRepo = userRepo;
    }

    public async Task<List<BaseUserDto>> GetAllAsync()
    {
        var users = await _userRepo.GetAllAsync();
        var userDto = users.Select(x => x.ToBaseUserDto()).ToList();
        return userDto;
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

        userEntity.ToPromotedUserEntity(promoteDto); //unnecessary abstraction?

        var token = _tokenService.CreateToken(userEntity);

        var promotedUser = await _userRepo.PromoteUserAsync(userEntity);

        return promotedUser.ToUserDto(token);
    }
}
