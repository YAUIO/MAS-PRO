using B2.App.Dtos;

namespace B2ComputersFrontend.Fetchers;

public interface IProductFetcher
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(PaginationDto dto);
}