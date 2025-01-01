using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class PSQLRgionRepository : IRegionRepository
{
    private readonly NZWalksDbContext _dbContext;

    public PSQLRgionRepository(NZWalksDbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }
    
    public async Task<List<Region>> GetAllAsync()
    {
        var regionsDomain = await _dbContext.Regions.ToListAsync();

        return regionsDomain;
    }
}