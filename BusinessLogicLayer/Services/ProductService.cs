using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> GetProductAsync(int productId)
    {
        var product = await _productRepository.GetProduct(productId);
        return product is null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByConditionAsync(string? category)
    {
        var products = await _productRepository.GetProductsByCondition(p =>
            category == null || p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)
        );
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<int> AddProductAsync(CreateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        return await _productRepository.AddProduct(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        product.ProductId = productId;
        await _productRepository.UpdateProduct(product);
    }

    public async Task DeleteProductAsync(int productId)
    {
        await _productRepository.DeleteProduct(productId);
    }
}
