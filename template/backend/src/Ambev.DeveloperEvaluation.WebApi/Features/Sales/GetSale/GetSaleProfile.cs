using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
public class GetSaleProfile : Profile
{
	public GetSaleProfile()
	{
		CreateMap<string, Application.Sales.GetSale.GetSaleCommand>()
			.ConstructUsing(saleNumber => new Application.Sales.GetSale.GetSaleCommand(saleNumber));

		CreateMap<GetSaleResult, GetSaleResponse>()
			.ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
			.ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
			.ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalSaleAmount))
			.ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
			.ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
			.ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber));
	}
}
