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
        var regions = new List<Region>
        {
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Auckland Region",
                Code = "AKL"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Wellington Region",
                Code = "WLG"
            }
        };

        return Ok(regions);
    }
}