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
}