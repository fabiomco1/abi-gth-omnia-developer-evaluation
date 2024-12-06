using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleProfile : Profile
{
	public CancelSaleProfile()
	{
		CreateMap<string, Application.Sales.CancelSale.CancelSaleCommand>()
			.ConstructUsing(saleNumber => new Application.Sales.CancelSale.CancelSaleCommand(saleNumber));

		CreateMap<CancelSaleResult, CancelSaleResponse>()
			.ForMember(dest => dest.CancelledAt, opt => opt.MapFrom(src => src.CancelledAt));
	}
}
