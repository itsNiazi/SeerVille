using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Helpers.QueryObjects;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class PredictionRepository : IPredictionRepository
{
    public readonly AppDbContext _context;

    public PredictionRepository(AppDbContext context)
    {
        _context = context;
    }

    // URL ENCODED here, frontend or both??!??
    public async Task<List<PredictionSummaryDto>> GetAllAsync(Guid userId, PredictionQuery query)
    {
        var predictions = _context.Predictions.AsQueryable();

        if (query.TopicId.HasValue)
        {
            predictions = predictions.Where(p => p.TopicId == query.TopicId.Value);
        }

        if (query.IsResolved.HasValue)
        {
            predictions = predictions.Where(p => p.IsResolved == query.IsResolved.Value);
        }

        var projected = predictions.Select(p => new PredictionSummaryDto
        {
            PredictionId = p.PredictionId,
            PredictionName = p.PredictionName,
            PredictionRules = p.PredictionRules,
            CreatorId = p.CreatorId,
            TopicId = p.TopicId,
            PredictionDate = p.PredictionDate,
            ResolutionDate = p.ResolutionDate,
            IsResolved = p.IsResolved,
            IsCorrect = p.IsCorrect,
            ResolvedAt = p.ResolvedAt,
            YesVotes = p.Votes.Count(v => v.PredictedOutcome),
            NoVotes = p.Votes.Count(v => !v.PredictedOutcome),
            TotalVotes = p.Votes.Count(),
            PredictedOutcome = p.Votes.Where(v => v.UserId == userId).Select(v => (bool?)v.PredictedOutcome).FirstOrDefault()
        });

        projected = query.SortBy switch
        {
            "endingsoon" => projected.OrderBy(p => p.ResolutionDate),
            "new" => projected.OrderByDescending(p => p.PredictionDate),
            "popular" => projected.OrderByDescending(p => p.TotalVotes),
            _ => projected.OrderByDescending(p => p.PredictionDate) // default
        };

        projected = projected.Take(query.PageSize);

        return await projected.ToListAsync();
    }

    public async Task<Prediction?> GetByIdAsync(Guid id)
    {
        return await _context.Predictions.FindAsync(id);
    }

    public async Task<List<Prediction>> GetByTopicIdAsync(Guid id)
    {
        return await _context.Predictions.Where(x => x.TopicId == id).ToListAsync();
    }

    public async Task<Prediction> CreateAsync(Prediction prediction)
    {
        _context.Predictions.Add(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }

    public async Task<Prediction> PatchAsync(Prediction patch)
    {
        _context.Predictions.Update(patch);
        await _context.SaveChangesAsync();
        return patch;
    }

    public async Task<Prediction> UpdateAsync(Prediction update)
    {
        _context.Predictions.Update(update);
        await _context.SaveChangesAsync();
        return update;
    }

    public async Task<Prediction?> DeleteAllAsync()
    {
        var predictions = await _context.Predictions.ToListAsync();
        _context.Predictions.RemoveRange(predictions);
        await _context.SaveChangesAsync();
        return null;
    }

    public async Task<Prediction> DeleteAsync(Prediction prediction)
    {
        _context.Predictions.Remove(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }
}
