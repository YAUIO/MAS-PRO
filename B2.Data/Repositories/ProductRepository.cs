using B2.Data.DbContext;
using B2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.Repositories;

public class ProductRepository(B2DbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await context.Products.ToListAsync();
    }
}