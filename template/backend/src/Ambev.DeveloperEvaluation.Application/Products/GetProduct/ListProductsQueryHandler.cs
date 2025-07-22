using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, List<ProductDto>>
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;

	public ListProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
	{
		_productRepository = productRepository;
		_mapper = mapper;
	}

	public async Task<List<ProductDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
	{
		var products = await _productRepository.GetAllAsync(cancellationToken);
		return _mapper.Map<List<ProductDto>>(products);
	}
}