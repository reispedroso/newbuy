using newbuy.App.Services;
using newbuy.Domain.Interfaces;
using newbuy.Domain.Models;
using newbuy.Infrastructure.Data;

namespace newbuy.Repositories;

public class ProductRepository(AppDbContext context) : IProductInterface
{
    private readonly AppDbContext _context = context;
    private readonly DateTimeCorrection timeCorrection = new();
    public async Task<Product> AddProductToDb(Product product)
    {
        if (product == null) { throw new Exception("Product is null"); }
        Product newProduct = new()
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            ProductTypeId = product.ProductTypeId,
            CreatedAt = timeCorrection.GetCorrectedDateTime(DateTime.UtcNow)
        };

        await _context.Product.AddAsync(newProduct);
        await _context.SaveChangesAsync();
        
        return newProduct;
    }

    
}