using B2.App.Dtos;
using B2.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpGet("{pageSize:int}/{page:int}")]
    public async Task<IActionResult> GetAllProducts(int pageSize, int page)
    {
        var dto = new PaginationDto()
        {
            Page = page,
            PageSize = pageSize,
        };
        var products = await service.GetAllProductsAsync(dto);
        return Ok(products);
    }
}