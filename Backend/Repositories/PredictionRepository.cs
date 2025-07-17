using Backend.Data;
using Backend.Entities;
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

    public async Task<List<Prediction>> GetAllAsync()
    {
        return await _context.Predictions.ToListAsync();
    }

    public async Task<Prediction?> GetByIdAsync(Guid id)
    {
        return await _context.Predictions.FindAsync(id);
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
