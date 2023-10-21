namespace SkySaver.Controllers.v1;

using SkySaver.Domain.Streaks.Features;
using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/streaks")]
[ApiVersion("1.0")]
public sealed class StreaksController: ControllerBase
{
    private readonly IMediator _mediator;

    public StreaksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Streak record.
    /// </summary>
    [HttpPost(Name = "AddStreak")]
    public async Task<ActionResult<StreakDto>> AddStreak([FromBody]StreakForCreationDto streakForCreation)
    {
        var command = new AddStreak.Command(streakForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetStreak",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Streak by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetStreak")]
    public async Task<ActionResult<StreakDto>> GetStreak(Guid id)
    {
        var query = new GetStreak.Query(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Streaks.
    /// </summary>
    [HttpGet(Name = "GetStreaks")]
    public async Task<IActionResult> GetStreaks([FromQuery] StreakParametersDto streakParametersDto)
    {
        var query = new GetStreakList.Query(streakParametersDto);
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
    /// Updates an entire existing Streak.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateStreak")]
    public async Task<IActionResult> UpdateStreak(Guid id, StreakForUpdateDto streak)
    {
        var command = new UpdateStreak.Command(id, streak);
        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Streak record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteStreak")]
    public async Task<ActionResult> DeleteStreak(Guid id)
    {
        var command = new DeleteStreak.Command(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
