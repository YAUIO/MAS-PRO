using B2.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Controllers;

[ApiController]
[Route("locations")]
public class LocationController(ILocationService service) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserLocations(Guid userId)
    {
        var locations = await service.GetAllUserLocationsAsync(userId);
        return Ok(locations);
    }
    
    [HttpGet("{sellerId:guid}")]
    public async Task<IActionResult> GetSellerLocations(Guid sellerId)
    {
        var locations = await service.GetAllSellerLocationsAsync(sellerId);
        return Ok(locations);
    }
}