using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public class ProductFetcher(Fetcher fetcher) : IProductFetcher
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(PaginationDto dto)
    {
        return await fetcher.GetAtPath<List<ProductDto>>($"products/{dto.PageSize}/{dto.Page}");
    }
}