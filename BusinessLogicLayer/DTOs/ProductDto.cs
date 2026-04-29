namespace BusinessLogicLayer.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}
