namespace SkySaver.Controllers.v1;

using SkySaver.Domain.UserPurchases.Features;
using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/userpurchases")]
[ApiVersion("1.0")]
public sealed class UserPurchasesController: ControllerBase
{
    private readonly IMediator _mediator;

    public UserPurchasesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new UserPurchase record.
    /// </summary>
    [HttpPost(Name = "AddUserPurchase")]
    public async Task<ActionResult<UserPurchaseDto>> AddUserPurchase([FromBody]UserPurchaseForCreationDto userPurchaseForCreation)
    {
        var command = new AddUserPurchase.Command(userPurchaseForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetUserPurchase",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single UserPurchase by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetUserPurchase")]
    public async Task<ActionResult<UserPurchaseDto>> GetUserPurchase(Guid id)
    {
        var query = new GetUserPurchase.Query(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all UserPurchases.
    /// </summary>
    [HttpGet(Name = "GetUserPurchases")]
    public async Task<IActionResult> GetUserPurchases([FromQuery] UserPurchaseParametersDto userPurchaseParametersDto)
    {
        var query = new GetUserPurchaseList.Query(userPurchaseParametersDto);
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
    /// Updates an entire existing UserPurchase.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateUserPurchase")]
    public async Task<IActionResult> UpdateUserPurchase(Guid id, UserPurchaseForUpdateDto userPurchase)
    {
        var command = new UpdateUserPurchase.Command(id, userPurchase);
        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Deletes an existing UserPurchase record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteUserPurchase")]
    public async Task<ActionResult> DeleteUserPurchase(Guid id)
    {
        var command = new DeleteUserPurchase.Command(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
