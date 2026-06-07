using B2.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Controllers;

[ApiController]
[Route("locations")]
public class LocationController(ILocationService service, IConfiguration cfg) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserLocations()
    {
        var locations = await service.GetAllUserLocationsAsync(Guid.Parse(cfg["userId"]!));
        return Ok(locations);
    }
    
    [HttpGet("{sellerId:guid}")]
    public async Task<IActionResult> GetSellerLocations(Guid sellerId)
    {
        var locations = await service.GetAllSellerLocationsAsync(sellerId);
        return Ok(locations);
    }
}