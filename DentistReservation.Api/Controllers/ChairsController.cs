using DentistReservation.Application.Chairs.Queries;
using DentistReservation.Infrastructure.Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentistReservation.Api.Controllers;

[ApiController]
[Route("chairs")]
public class ChairsController : ControllerBase
{
    private readonly ISender _sender;

    public ChairsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetChair(
        [FromRoute] Guid id
    )
    {
        var request = new GetChairQuery(id);
        var response = await _sender.Send(request);

        if (response.HasError)
            return BadRequest(response.Error);
        
        return Ok(response.Value);
    }
    
    [HttpGet]
    public async Task<ActionResult> ListChairs(
        [FromQuery] Pagination pagination
    )
    {
        var request = new ListChairQuery(pagination.PageIndex, pagination.PageSize);
        var response = await _sender.Send(request);
        return Ok(response);
    }
}