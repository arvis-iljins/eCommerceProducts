using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock must be 0 or greater.");
    }
}
