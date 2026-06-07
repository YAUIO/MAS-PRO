using B2.App.Dtos;
using B2.Data.Repositories;

namespace B2.App.Services;

public class ProductService(IProductRepository repo) : IProductService
{
    public async Task<List<ProductDto>> GetAllProductsAsync(PaginationDto dto)
    {
        var products = await repo.GetAllProductsAsync();
        return [.. products.Select(p => p.ToDto())];
    }
}