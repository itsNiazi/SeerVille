using Backend.Data;
using Backend.DTOs.User;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<UserPredictionsDto>> GetUserPredictionsAsync(Guid userId)
    {
        return await _context.PredictionVotes
            .Where(v => v.UserId == userId)
            .Select(v => new UserPredictionsDto
            {
                PredictionId = v.Prediction.PredictionId,
                PredictionName = v.Prediction.PredictionName,
                PredictionRules = v.Prediction.PredictionRules,
                PredictionDate = v.Prediction.PredictionDate,
                ResolutionDate = v.Prediction.ResolutionDate,
                IsResolved = v.Prediction.IsResolved,
                IsCorrect = v.Prediction.IsCorrect,
                TopicName = v.Prediction.Topic.Name,
                UserVote = v.PredictedOutcome,
                VotedDate = v.VotedAt,
            })
            .OrderByDescending(v => v.VotedDate)
            //fix this dynamic later
            .Take(8)
            .ToListAsync();
    }


    public async Task<UserStatsDto> GetUserStatsAsync(Guid userId)
    {
        var stats = await _context.PredictionVotes
           .Where(v => v.UserId == userId)
           .Select(v => new
           {
               IsResolved = v.Prediction.IsResolved,
               IsCorrect = v.Prediction.IsResolved && v.PredictedOutcome == v.Prediction.IsCorrect
           })
           .GroupBy(_ => 1)
           .Select(g => new UserStatsDto
           {
               TotalVotes = g.Count(),
               ResolvedVotes = g.Count(x => x.IsResolved),
               CorrectVotes = g.Count(x => x.IsCorrect)
           })
           .FirstOrDefaultAsync();

        return stats ?? new UserStatsDto();
    }


    public async Task<List<UserTopicStatsDto>> GetUserTopTopicsAsync(Guid userId)
    {
        return await _context.PredictionVotes
        .Where(v => v.UserId == userId)
        .GroupBy(v => new { v.Prediction.TopicId, v.Prediction.Topic.Name, v.Prediction.Topic.Icon })
        .Select(g => new UserTopicStatsDto
        {
            TopicId = g.Key.TopicId,
            TopicName = g.Key.Name,
            TopicIcon = g.Key.Icon,
            TotalVotes = g.Count(),
            ResolvedVotes = g.Count(x => x.Prediction.IsResolved),
            CorrectVotes = g.Count(x => x.Prediction.IsResolved && x.PredictedOutcome == x.Prediction.IsCorrect)
        })
    .OrderByDescending(x => x.TotalVotes)
    .Take(3) // need to change it to dynamic when not lazy
    .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user == null ? null : user;
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> PromoteUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }


}
