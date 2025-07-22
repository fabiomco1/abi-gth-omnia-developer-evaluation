using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

public class ListSalesQueryHandler : IRequestHandler<ListSalesQuery, List<GetSaleResult>>
{
	private readonly ISaleRepository _repository;
	private readonly IMapper _mapper;

	public ListSalesQueryHandler(ISaleRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<List<GetSaleResult>> Handle(ListSalesQuery request, CancellationToken cancellationToken)
	{
		var sales = await _repository.GetAllAsync(cancellationToken);
		return _mapper.Map<List<GetSaleResult>>(sales);
	}
}
