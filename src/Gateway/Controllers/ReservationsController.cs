using Gateway.Constants;
using Gateway.Data.Dtos;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationClientService _reservationClientService;

    public ReservationsController()
    {
        _reservationClientService =
            new ReservationClientService(new LoyaltyClientService(), new PaymentClientService());
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDto>>> GetAll(
        [FromHeader(Name = HeaderConstants.UserName)]
        string userName, [FromQuery] int page = 1,
        [FromQuery] int size = 10)
        => Ok((await _reservationClientService.GetAllReservationsAsync(page, size, userName))!.Items);

    [HttpGet("{uId}")]
    public async Task<ActionResult<ReservationDto>> GetByUId(string uId)
        => Ok(await _reservationClientService.GetReservationByUidAsync(uId));


    [HttpPost]
    public async Task<ActionResult<ReservationBookDto>> Book(BookHotelDto model,
        [FromHeader(Name = HeaderConstants.UserName)]
        string userName)
        => Ok(await _reservationClientService.BookHotelAsync(model.HotelUid, model.StartDate, model.EndDate, userName));


    [HttpDelete("{uId}")]
    public async Task<ActionResult<ReservationDto>> Book(string uId,
        [FromHeader(Name = HeaderConstants.UserName)]
        string userName)
    {
        await _reservationClientService.CancelBookHotelAsync(uId, userName);
        return NoContent();
    }
}