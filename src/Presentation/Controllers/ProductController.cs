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
    
    
}