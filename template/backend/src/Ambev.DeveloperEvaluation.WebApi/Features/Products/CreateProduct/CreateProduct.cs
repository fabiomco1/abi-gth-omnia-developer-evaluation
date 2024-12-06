using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProduct : Profile
{
	public CreateProduct()
	{
		CreateMap<CreateProductRequest, CreateProductCommand>();
		CreateMap<CreateProductResult, CreateProductResponse>();

		CreateMap<CreateProductCommand, Ambev.DeveloperEvaluation.Domain.Entities.Product>()
		  .ForMember(dest => dest.Id, opt => opt.Ignore()) // O ID geralmente é gerado automaticamente
		  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => (DateTime?)DateTime.UtcNow))
		  .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => (DateTime?)DateTime.UtcNow));

		CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.Product, CreateProductResult>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));
	}
}
