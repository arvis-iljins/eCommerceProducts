using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}
