using B2.App.Dtos;
using B2.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromBody] PaginationDto dto)
    {
        var products = await service.GetAllProductsAsync(dto);
        return Ok(products);
    }
}