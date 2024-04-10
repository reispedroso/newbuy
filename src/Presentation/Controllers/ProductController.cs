using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using newbuy.Domain.Models;
using newbuy.Domain.Interfaces;

namespace newbuy.Presentations.Controllers;

[Route("api/[controller]")]
[ApiController]public class ProductController(IProductInterface productRepository) : ControllerBase
{
    private readonly IProductInterface _productRepository = productRepository;

    [HttpPost("addproduct")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProduct(Product product)
    {
        try
        {
            Product newProduct = await _productRepository.AddProductToDb(product);
            return Ok(newProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("getproductbyid/{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            Product productById = await _productRepository.GetProductById(id);
            return Ok(productById);
        }   
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    [HttpGet("getproductbyname/{productName}")]
    public async Task<IActionResult> GetProductByName(string productName)
    {
        try
        {
            Product productByName = await _productRepository.GetProductByName(productName);
            return Ok(productByName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    [HttpGet("getallproducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            List<Product> allProducts = await _productRepository.GetAllProducts();
            return Ok(allProducts);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [HttpGet("getproductsbytypename/{typeName}")]
    public async Task<IActionResult> GetProductsByTypeName(string typeName)
    {
        try
        {
            List<Product> productsByTypeName = await _productRepository.GetProductsByTypeName(typeName);
            return Ok(productsByTypeName);
        }
        catch (Exception ex)    
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [HttpPut("updateproduct/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
    {
        try
        {
            var updateProduct = await _productRepository.UpdateProduct(id, product);
            return Ok(updateProduct);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    [HttpDelete("deleteproduct/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var toDeleteProduct = await _productRepository.DeleteProduct(id);
            return Ok(toDeleteProduct);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}