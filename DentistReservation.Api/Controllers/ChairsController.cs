using DentistReservation.Application.Reservations.Commands;

namespace DentistReservation.Api.Controllers;

[ApiController]
[Route("chairs")]
public class ChairsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetChair(
        [FromRoute] Guid id
    )
    {
        var request = new GetChairQuery(id);
        var response = await sender.Send(request);

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
        var response = await sender.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateChair(
        [FromBody] ModifyReservationModel modifyReservationModel
    )
    {
        var request = new CreateChairCommand(
            modifyReservationModel.Description,
            modifyReservationModel.Number,
            modifyReservationModel.StartHour,
            modifyReservationModel.StartMinute,
            modifyReservationModel.EndHour,
            modifyReservationModel.EndMinute,
            modifyReservationModel.AverageDuration,
            modifyReservationModel.AverageSetupInMinutes
        );

        var response = await sender.Send(request);

        if (response.HasError)
            return BadRequest(response.Error);

        return Created("", response.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateChair(
        [FromRoute] Guid id,
        [FromBody] ModifyReservationModel modifyReservationModel
    )
    {
        var request = new UpdateChairCommand(
            id,
            modifyReservationModel.Description,
            modifyReservationModel.Number,
            modifyReservationModel.StartHour,
            modifyReservationModel.StartMinute,
            modifyReservationModel.EndHour,
            modifyReservationModel.EndMinute,
            modifyReservationModel.AverageDuration,
            modifyReservationModel.AverageSetupInMinutes
        );

        var response = await sender.Send(request);

        if (response.HasError)
            return BadRequest(response.Error);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteChair(
        [FromRoute] Guid id
    )
    {
        var request = new DeleteChairCommand(id);
        var response = await sender.Send(request);

        if (response.HasError)
            return BadRequest(response.Error);

        return NoContent();
    }

    [HttpPost("auto-allocate-chair-reservation")]
    public async Task<ActionResult> AutoAllocateChairReservation()
    {
        var request = new AutoCreateReservationCommand();
        var response = await sender.Send(request);

        if (response.HasError)
            return BadRequest(response.Error);

        return Created("", response.Value);
    }
}