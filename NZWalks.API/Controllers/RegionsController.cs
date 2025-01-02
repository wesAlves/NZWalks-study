using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;

    public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
    {
        _dbContext = dbContext;
        _regionRepository = regionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //Get data from Database - Domain models;
        var regionsDomain = await _regionRepository.GetAllAsync();

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
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        //Only could be used with primary key
        // var region = _dbContext.Regions.Find(id);

        var regionDomain = await _regionRepository.GetByIdAsync(id);

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
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto createRegionDto)
    {
        var regionDomain = new Region()
        {
            Code = createRegionDto.Code,
            Name = createRegionDto.Name,
            ImageUrl = createRegionDto.ImageUrl
        };

        await _regionRepository.CreateRegionAsync(regionDomain);

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

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
    {
        var regionDomainModel = new Region
        {
            Code = updateRegionDto.Code,
            Name = updateRegionDto.Name,
            ImageUrl = updateRegionDto.ImageUrl
        };

        await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

        //Convert Domain model to DTO
        var regionDto = new RegionDTO
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            ImageUrl = regionDomainModel.ImageUrl
        };

        return Ok(regionDto);
    }


    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {

        var regionDomainModel = await _regionRepository.DeleteRegionAsync(id);

        if (regionDomainModel is null)
            return NotFound();

        //Convert Domain model to DTO
        var regionDto = new RegionDTO
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            ImageUrl = regionDomainModel.ImageUrl
        };
        
        return Ok(regionDto);
    }
}