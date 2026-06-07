using B2.App.Dtos;

namespace B2.App.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync(PaginationDto dto);
}