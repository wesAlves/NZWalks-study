using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _dbContext;

    public RegionsController(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = _dbContext.Regions.ToList();
        return Ok(regions);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        //Only could be used with primary key
        // var region = _dbContext.Regions.Find(id);

        var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
        
        if (region is null)
        {
            return NotFound();
        }

        return Ok(region);
    }

    // [HttpPost]
    // public async Task<IActionResult> AddRegion
}