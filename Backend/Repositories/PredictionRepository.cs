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

    // public Task<Prediction?> UpdateByIdAsync(Prediction updateDto)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Prediction> DeleteAllAsync()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Prediction?> DeleteByIdAsync(Guid id)
    // {
    //     throw new NotImplementedException();
    // }
}
