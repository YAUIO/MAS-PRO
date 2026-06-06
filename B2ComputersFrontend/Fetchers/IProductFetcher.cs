using B2.App.Dtos;
using TIN.Core.Dtos;

namespace B2ComputersFrontend.Fetchers;

public interface IProductFetcher
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(PaginationDto dto);
}