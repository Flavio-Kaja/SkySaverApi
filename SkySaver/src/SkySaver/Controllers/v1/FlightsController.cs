namespace SkySaver.Controllers.v1;

using SkySaver.Domain.Flights.Features;
using SkySaver.Domain.Flights.Dtos;
using SkySaver.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/flights")]
[ApiVersion("1.0")]
public sealed class FlightsController: ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Flight record.
    /// </summary>
    [HttpPost(Name = "AddFlight")]
    public async Task<ActionResult<FlightDto>> AddFlight([FromBody]FlightForCreationDto flightForCreation)
    {
        var command = new AddFlight.Command(flightForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetFlight",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Flight by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetFlight")]
    public async Task<ActionResult<FlightDto>> GetFlight(Guid id)
    {
        var query = new GetFlight.Query(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Flights.
    /// </summary>
    [HttpGet(Name = "GetFlights")]
    public async Task<IActionResult> GetFlights([FromQuery] FlightParametersDto flightParametersDto)
    {
        var query = new GetFlightList.Query(flightParametersDto);
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
    /// Updates an entire existing Flight.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateFlight")]
    public async Task<IActionResult> UpdateFlight(Guid id, FlightForUpdateDto flight)
    {
        var command = new UpdateFlight.Command(id, flight);
        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Flight record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteFlight")]
    public async Task<ActionResult> DeleteFlight(Guid id)
    {
        var command = new DeleteFlight.Command(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
