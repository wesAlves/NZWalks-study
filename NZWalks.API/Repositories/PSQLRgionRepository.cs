using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        
        return regionDomain;
    }

    public async Task<Region> CreateRegionAsync(Region region)
    {
        _dbContext.Regions.Add(region);
        await _dbContext.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
    {
        var existRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existRegion == null)
            return null;
        
        //Map DTO to Domain model
        existRegion.Code = region.Code;
        existRegion.Name = region.Name;
        existRegion.ImageUrl = region.ImageUrl;

        await _dbContext.SaveChangesAsync();
        
        return existRegion;
    }

    public async Task<Region?> DeleteRegionAsync(Guid id)
    {
        var existRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (existRegion == null)
            return null;

        _dbContext.Regions.Remove(existRegion);
        await _dbContext.SaveChangesAsync();

        return existRegion;
    }
}