using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
	public class GetProductResult
	{
		public string ProductName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
