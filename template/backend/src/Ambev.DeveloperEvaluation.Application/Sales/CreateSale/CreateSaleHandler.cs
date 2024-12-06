using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;

	public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository)
	{
		_saleRepository = saleRepository;
		_mapper = mapper;
		_productRepository = productRepository;
	}

	public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
	{
		var sale = _mapper.Map<Sale>(command);

		if (sale.Id == Guid.Empty)
		{
			sale.Id = Guid.NewGuid();  
		}
		var totalItems = sale.SalesProducts.Count();

		decimal totalSaleAmount = 0;
		
		foreach (var sp in sale.SalesProducts)
		{
			// Busca o produto no repositório
			var product = await _productRepository.GetByIdAsync(sp.ProductId, cancellationToken);

			sp.SaleNumber = sale.Id;

			if (product == null)
			{
				throw new InvalidOperationException($"Product with ID {sp.ProductId} not found");
			}

			// Calcula o TotalItemAmount para o produto
			sp.TotalItemAmount = sp.Quantity * product.Price;
			
			//Calcula o desconto
			if (totalItems >=4)
			{
				if (totalItems <=10)
					sp.Discount = sp.TotalItemAmount * 0.1m;
				else  sp.Discount = sp.TotalItemAmount * 0.2m; 
			}
			
			// Contabiliza o valor total do pedido
			totalSaleAmount = totalSaleAmount + (sp.TotalItemAmount- sp.Discount);
		}
		
		sale.TotalItems = totalItems;
		sale.TotalSaleAmount= totalSaleAmount;
		sale.CreatedAt = DateTime.UtcNow;

		var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
		var result = _mapper.Map<CreateSaleResult>(createdSale);
	return result;
	}
}