using B2.Data.Models;

namespace B2.Data.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
}