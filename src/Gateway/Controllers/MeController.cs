using Gateway.Constants;
using Gateway.Data.Dtos;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MeController : ControllerBase
{
    private readonly ReservationClientService _reservationClientService;


    public MeController()
    {
        _reservationClientService =
            new ReservationClientService(new LoyaltyClientService(), new PaymentClientService());
    }

    [HttpGet]
    public async Task<ActionResult<UserInfoDto>> Me([FromHeader(Name = HeaderConstants.UserName)] string userName)
        => Ok(await _reservationClientService.GetUserInfoAsync(userName));
}