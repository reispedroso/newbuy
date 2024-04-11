using Microsoft.EntityFrameworkCore;
using newbuy.App.Services;
using newbuy.Domain.Interfaces;
using newbuy.Domain.Models;
using newbuy.Infrastructure.Data;

namespace newbuy.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;
    private readonly DateTimeCorrection _timeCorrection = new();
    public async Task<Product> AddProductToDb(Product product)
    {
        if (product == null) { throw new Exception("Product is null"); }
        Product newProduct = new()
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            ProductTypeId = product.ProductTypeId,
            CreatedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow)
        };

        await _context.Product.AddAsync(newProduct);
        await _context.SaveChangesAsync();
        
        return newProduct;
    }

    public async Task<Product> GetProductById(Guid id)
    {
        if(id == Guid.Empty) { throw new Exception("Id is empty"); }

        Product productById = await _context.Product.FirstOrDefaultAsync(p => p.Id == id)
        ?? throw new Exception($"Product with id: {id} - not found");

        return productById;
    }

    public async Task<Product> GetProductByName(string name)
    {
        if(string.IsNullOrEmpty(name)) { throw new Exception("Name is empty"); }

        string nameLower = name.ToLowerInvariant();

        Product productByName = await _context.Product.Where(p => EF.Functions.Like(p.Name.ToLower(), nameLower)).FirstOrDefaultAsync()
                                ?? throw new Exception($"Product with name: {name} - not found");

        return productByName;
    }


    public async Task<List<Product>> GetAllProducts()
    {
        List<Product> products = await _context.Product.ToListAsync() ?? throw new Exception("No products found");

        return products;
    }

    public async  Task<List<Product>> GetProductsByTypeId(Guid typeId)
    {
        if(typeId == Guid.Empty) { throw new Exception("Type Id is empty"); }

        List<Product> productsByTypeId = await _context.Product.Where(p => p.ProductTypeId == typeId).ToListAsync()
        ?? throw new Exception($"Products with type id: {typeId} - not found");

        return productsByTypeId;
    }

    public async Task<List<Product>> GetProductsByTypeName(string typeName)
    {
        if(string.IsNullOrEmpty(typeName)) { throw new Exception("Type name is empty"); }

        string nameLower = typeName.ToLowerInvariant();

        List<Product> productsByNameType = await _context.Product.Where(p => EF.Functions.Like(typeName.ToLower(), nameLower)).ToListAsync()
        ?? throw new Exception($"Products with type name: {typeName} - not found");

        return productsByNameType;
    }

    public async Task<bool> UpdateProduct(Guid id, Product product)
    {
        if(product == null) { throw new Exception("Product is null"); }

        Product productToUpdate = await GetProductById(id);

        productToUpdate.Name = product.Name;
        productToUpdate.ProductTypeId = product.ProductTypeId;
        productToUpdate.UpdatedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow);

        _context.Product.Update(productToUpdate);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        if(id == Guid.Empty) { throw new Exception("Id is empty"); }

        Product productToDelete = await GetProductById(id);

        productToDelete.DeletedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow);

        await _context.SaveChangesAsync();

        return true;
    }
}