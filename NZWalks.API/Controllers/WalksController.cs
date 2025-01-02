using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IWalkRepository _repository;

    public WalksController(IWalkRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWalk([FromBody] CreateWalkDto createWalkDto)
    {
        //Map DTO to domain model
        var walkDomainModel = new Walk
        {
            Name = createWalkDto.Name,
            Description = createWalkDto.Description,
            LengthInKm = createWalkDto.LengthInKm,
            WalkImageUrl = createWalkDto.WalkImageUrl,
            DifficultyId = createWalkDto.DifficultyId,
            RegionId = createWalkDto.RegionId
        };

        await _repository.CreateAsync(walkDomainModel);

        //Map domain model to DTO
        var walkDto = new WalkDTO
        {
            Name = createWalkDto.Name,
            Description = createWalkDto.Description,
            LengthInKm = createWalkDto.LengthInKm,
            WalkImageUrl = createWalkDto.WalkImageUrl,
            DifficultyId = createWalkDto.DifficultyId,
            RegionId = createWalkDto.RegionId
        };

        return Ok(walkDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWalks()
    {
        var walks = await _repository.GetAllAsync();

        var walksDTO = new List<Walk>();

        foreach (var walk in walks)
        {
            walksDTO.Add(walk);
        }


        return Ok(walksDTO);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomain = _repository.GetByIdAsync(id);

        if (walkDomain is null)
            return NotFound();

        //TODO: MAP TO A DTO
        return Ok(walkDomain);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] WalkDTO walkDto)
    {
        var walkDomain = _repository.UpdateAsync(id, new Walk()
        {
            Name = walkDto.Name,
            Description = walkDto.Description,
            LengthInKm = walkDto.LengthInKm
        });

        //TODO: MAP TO A DTO
        return Ok(walkDomain);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
    {
        var walkDomain = _repository.DeleteAsync(id);

        //TODO: MAP TO A DTO
        return Ok(walkDomain);
    }
}