using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;
namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sale : BaseEntity, ISale
{
	string ISale.Id => Id.ToString();
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public Decimal TotalSaleAmount { get; set; }
	public int TotalItems { get; set; }
	public string Branch { get; set; } = string.Empty;
	public Boolean Cancelled { get; set; } 
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? CancelledAt { get; set; }
	public List<SaleProduct> SalesProducts { get; set; } = new List<SaleProduct>();

	public Sale()
	{
		CreatedAt = DateTime.UtcNow;
	}
}