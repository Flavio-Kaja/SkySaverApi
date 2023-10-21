namespace SkySaver.Controllers.v1;

using SkySaver.Domain.ScavengerHunts.Features;
using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/scavengerhunts")]
[ApiVersion("1.0")]
public sealed class ScavengerHuntsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ScavengerHuntsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new ScavengerHunt record.
    /// </summary>
    [HttpPost(Name = "AddScavengerHunt")]
    public async Task<ActionResult<ScavengerHuntDto>> AddScavengerHunt([FromBody]ScavengerHuntForCreationDto scavengerHuntForCreation)
    {
        var command = new AddScavengerHunt.Command(scavengerHuntForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetScavengerHunt",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single ScavengerHunt by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetScavengerHunt")]
    public async Task<ActionResult<ScavengerHuntDto>> GetScavengerHunt(Guid id)
    {
        var query = new GetScavengerHunt.Query(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all ScavengerHunts.
    /// </summary>
    [HttpGet(Name = "GetScavengerHunts")]
    public async Task<IActionResult> GetScavengerHunts([FromQuery] ScavengerHuntParametersDto scavengerHuntParametersDto)
    {
        var query = new GetScavengerHuntList.Query(scavengerHuntParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing ScavengerHunt.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateScavengerHunt")]
    public async Task<IActionResult> UpdateScavengerHunt(Guid id, ScavengerHuntForUpdateDto scavengerHunt)
    {
        var command = new UpdateScavengerHunt.Command(id, scavengerHunt);
        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Deletes an existing ScavengerHunt record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteScavengerHunt")]
    public async Task<ActionResult> DeleteScavengerHunt(Guid id)
    {
        var command = new DeleteScavengerHunt.Command(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
