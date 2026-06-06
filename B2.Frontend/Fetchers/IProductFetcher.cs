using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public interface IProductFetcher
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(PaginationDto dto);
}