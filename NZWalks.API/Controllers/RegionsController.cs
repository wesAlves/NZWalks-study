using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
        //Get data from Database - Domain models;
        var regionsDomain = _dbContext.Regions.ToList();

        //Map Domain models to DTOs;
        var regionsDto = new List<RegionDTO>();
        foreach (var region in regionsDomain)
        {
            regionsDto.Add(new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                ImageUrl = region.ImageUrl
            });
        }

        //Return the DTO
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        //Only could be used with primary key
        // var region = _dbContext.Regions.Find(id);

        var regionDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (regionDomain is null)
        {
            return NotFound();
        }

        var regionDto = new RegionDTO
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            ImageUrl = regionDomain.ImageUrl
        };

        return Ok(regionDto);
    }

    [HttpPost]
    public IActionResult CreateRegion([FromBody] CreateRegionDto createRegionDto)
    {
        var regionDomain = new Region()
        {
            Code = createRegionDto.Code,
            Name = createRegionDto.Name,
            ImageUrl = createRegionDto.ImageUrl
        };

        _dbContext.Regions.Add(regionDomain);
        _dbContext.SaveChanges();
        
        //Map the Domain model back to the DTO
        var regionDto = new RegionDTO
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            ImageUrl = regionDomain.ImageUrl
        };

        //Here we should return 201 to acheive that we whant to pass the location from the resource
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }
}