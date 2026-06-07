using B2.App.Dtos;
using B2.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        await service.CreateOrderAsync(dto);
        return Created();
    }
}