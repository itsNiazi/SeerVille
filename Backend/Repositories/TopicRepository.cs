using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly AppDbContext _context;

    public TopicRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Topic>> GetAllAsync()
    {

        return await _context.Topics.ToListAsync();
    }


    public async Task<Topic?> GetByIdAsync(Guid id)
    {
        return await _context.Topics.FindAsync(id);
    }


    public async Task<Topic> CreateAsync(Topic topicEntity)
    {
        _context.Topics.Add(topicEntity);
        await _context.SaveChangesAsync();
        return topicEntity;
    }


    public async Task<Topic?> DeleteAsync(Guid id)
    {
        var topic = await GetByIdAsync(id);
        if (topic == null) return null;

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();
        return topic;
    }


    public async Task<Topic?> DeleteAllAsync()
    {
        var topics = await _context.Topics.ToListAsync();
        _context.Topics.RemoveRange(topics);
        await _context.SaveChangesAsync();
        return null;
    }


    public async Task<Topic?> UpdateAsync(Topic topic)
    {
        _context.Topics.Update(topic);
        await _context.SaveChangesAsync();
        return topic;
    }


    public async Task<bool> CheckExists(Guid id)
    {
        return await _context.Topics.AnyAsync(x => x.TopicId == id);
    }

}
