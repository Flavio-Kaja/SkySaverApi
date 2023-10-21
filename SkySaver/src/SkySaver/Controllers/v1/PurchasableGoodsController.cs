namespace SkySaver.Controllers.v1;

using SkySaver.Domain.PurchasableGoods.Features;
using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/purchasablegoods")]
[ApiVersion("1.0")]
public sealed class PurchasableGoodsController: ControllerBase
{
    private readonly IMediator _mediator;

    public PurchasableGoodsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new PurchasableGood record.
    /// </summary>
    [HttpPost(Name = "AddPurchasableGood")]
    public async Task<ActionResult<PurchasableGoodDto>> AddPurchasableGood([FromBody]PurchasableGoodForCreationDto purchasableGoodForCreation)
    {
        var command = new AddPurchasableGood.Command(purchasableGoodForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetPurchasableGood",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single PurchasableGood by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetPurchasableGood")]
    public async Task<ActionResult<PurchasableGoodDto>> GetPurchasableGood(Guid id)
    {
        var query = new GetPurchasableGood.Query(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all PurchasableGoods.
    /// </summary>
    [HttpGet(Name = "GetPurchasableGoods")]
    public async Task<IActionResult> GetPurchasableGoods([FromQuery] PurchasableGoodParametersDto purchasableGoodParametersDto)
    {
        var query = new GetPurchasableGoodList.Query(purchasableGoodParametersDto);
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
    /// Updates an entire existing PurchasableGood.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdatePurchasableGood")]
    public async Task<IActionResult> UpdatePurchasableGood(Guid id, PurchasableGoodForUpdateDto purchasableGood)
    {
        var command = new UpdatePurchasableGood.Command(id, purchasableGood);
        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Deletes an existing PurchasableGood record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeletePurchasableGood")]
    public async Task<ActionResult> DeletePurchasableGood(Guid id)
    {
        var command = new DeletePurchasableGood.Command(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
