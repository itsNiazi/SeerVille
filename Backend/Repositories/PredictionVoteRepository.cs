using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class PredictionVoteRepository : IPredictionVoteRepository
{
    public readonly AppDbContext _context;
    public PredictionVoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PredictionVote>> GetAllByIdAsync(string userId)
    {
        return await _context.PredictionVotes
        .Where(x => x.UserId == Guid.Parse(userId))
        .ToListAsync();
        //     var votes = await _context.PredictionVotes
        // .Where(v => v.UserId == Guid.Parse(userId))
        // .Include(v => v.Prediction)
        // .Include(v => v.User)
        // .ToListAsync();
        //     return votes;
    }

    public async Task<PredictionVote> VoteAsync(PredictionVote vote)
    {
        _context.PredictionVotes.Add(vote);
        await _context.SaveChangesAsync();
        return vote;
    }
}
