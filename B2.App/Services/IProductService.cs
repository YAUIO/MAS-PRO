using B2.App.Dtos;
using TIN.Core.Dtos;

namespace B2.App.Services;

public interface IProductService
{
    Task<ProductDto> GetAllProductsAsync(PaginationDto dto);
}