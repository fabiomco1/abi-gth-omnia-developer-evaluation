using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateProduct : Profile
{
	/// <summary>
	/// Initializes the mappings for CreateUser feature
	/// </summary>
	public CreateProduct()
	{

		CreateMap<CreateProductRequest, CreateProductCommand>();
		CreateMap<CreateProductResult, CreateProductResponse>();

		CreateMap<CreateProductRequest, CreateProductCommand>();
		CreateMap<CreateProductResult, CreateProductResponse>();

		CreateMap<CreateProductCommand, Ambev.DeveloperEvaluation.Domain.Entities.Product>()
		  .ForMember(dest => dest.Id, opt => opt.Ignore()) // O ID geralmente é gerado automaticamente
		  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) 
		  .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

	}
}
