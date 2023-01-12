using Gateway.Constants;
using Gateway.Data.Dtos;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly ReservationClientService _reservationClientService;

    public HotelsController()
    {
        _reservationClientService = new ReservationClientService("http://reservation_service:80");
    }

    [HttpGet]
    public async Task<ActionResult<List<HotelDto>>> GetAll([FromQuery] int page, [FromQuery] int size)
        => Ok(await _reservationClientService.GetAllHotelsAsync(page, size));
}
