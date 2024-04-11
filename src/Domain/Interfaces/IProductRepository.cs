using newbuy.Domain.Models;

namespace newbuy.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> AddProductToDb(Product product);
    Task<Product> GetProductById(Guid id);
    Task<Product> GetProductByName(string name);

    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetProductsByTypeId(Guid typeId);
    Task<List<Product>> GetProductsByTypeName(string typeName);

    Task<bool> UpdateProduct(Guid id, Product product);
    Task<bool> DeleteProduct(Guid id);
    
}