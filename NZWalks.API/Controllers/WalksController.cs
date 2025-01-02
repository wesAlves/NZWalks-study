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
}