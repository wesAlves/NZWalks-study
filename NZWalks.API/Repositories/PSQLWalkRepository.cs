using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class PSQLWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext _context;

    public PSQLWalkRepository(NZWalksDbContext context)
    {
        _context = context;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();

        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _context.Walks.ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        var walkExists = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkExists is null)
            return null;

        return walkExists;
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var walkExists = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkExists is null)
            return null;

        walkExists.Name = walk.Name;
        walkExists.Description = walk.Description;
        walkExists.LengthInKm = walk.LengthInKm;
        walkExists.WalkImageUrl = walk.WalkImageUrl;
        walkExists.DifficultyId = walk.DifficultyId;
        walkExists.RegionId = walk.RegionId;

        return walkExists;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walkExists = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkExists is null)
            return null;

        _context.Walks.Remove(walkExists);
        await _context.SaveChangesAsync();

        return walkExists;
    }
}