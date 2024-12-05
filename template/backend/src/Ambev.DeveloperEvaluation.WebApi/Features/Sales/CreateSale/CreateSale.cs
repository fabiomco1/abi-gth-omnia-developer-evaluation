using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.SalesProducts.CreateSaleProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesProducts.CreateSaleProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateSale : Profile
{
	public CreateSale()
	{
		// Mapeamento de CreateSaleRequest para CreateSaleCommand
		CreateMap<CreateSaleRequest, CreateSaleCommand>()
			.ForMember(dest => dest.SalesProducts, opt => opt.MapFrom(src => src.SalesProducts));

		// Mapeamento de CreateSaleProductRequest para CreateSaleProductCommand
		CreateMap<CreateSaleProductRequest, CreateSaleProductCommand>()
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => Guid.Parse(src.ProductId))); // Certifique-se de tratar o ProductId

		// Mapeamento de CreateSaleCommand para Sale
		CreateMap<CreateSaleCommand, Sale>()
			.ForMember(dest => dest.SalesProducts, opt => opt.MapFrom(src => src.SalesProducts)); // Mapeando a lista de produtos

		// Mapeamento de CreateSaleProductCommand para SaleProduct
		CreateMap<CreateSaleProductCommand, SaleProduct>()
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId)) // Mapeamento de ProductId para SaleProduct
			.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
			.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

		CreateMap<Sale, CreateSaleResult>();

		CreateMap<CreateSaleRequest, CreateSaleCommand>();
		CreateMap<CreateSaleResult, CreateSaleResponse>();



	}
}
