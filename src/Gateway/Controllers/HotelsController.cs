using System.ComponentModel.DataAnnotations;
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
    public async Task<ActionResult<List<HotelDto>>> GetAll([FromQuery, Required] int page = 1, [FromQuery, Required] int size = 10)
        => Ok(await _reservationClientService.GetAllHotelsAsync(page, size));
    
    [HttpPost]
    public async Task<IActionResult> BookHotel(BookHotelDto model)
    {
        await _reservationClientService.BookHotelAsync(model.HotelUid, model.StartDate, model.EndDate);
        return Ok();
    }
}
