using System.ComponentModel.DataAnnotations;
using Gateway.Constants;
using Gateway.Data.Dtos;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoyaltyController : ControllerBase
{
    private readonly LoyaltyClientService _loyaltyClientService;

    public LoyaltyController()
    {
        _loyaltyClientService = new LoyaltyClientService();
    }

    [HttpGet]
    public async Task<ActionResult<LoyaltyDto>> Get([FromHeader(Name = HeaderConstants.UserName)] string userName)
        => Ok(await _loyaltyClientService.GetAsync(userName));
}