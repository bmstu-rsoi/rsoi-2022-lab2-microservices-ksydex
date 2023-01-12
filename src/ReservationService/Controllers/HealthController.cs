using Microsoft.AspNetCore.Mvc;

namespace ReservationService.Controllers;

[ApiController]
[Route("manage/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => Ok();
}