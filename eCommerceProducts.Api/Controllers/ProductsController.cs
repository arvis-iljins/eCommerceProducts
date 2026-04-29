using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProducts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
        [FromQuery] string? category
    )
    {
        var products = await _productService.GetProductsByConditionAsync(category);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProduct([FromBody] CreateProductRequest request)
    {
        var newId = await _productService.AddProductAsync(request);
        return CreatedAtAction(nameof(GetProduct), new { id = newId }, newId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
    {
        await _productService.UpdateProductAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
