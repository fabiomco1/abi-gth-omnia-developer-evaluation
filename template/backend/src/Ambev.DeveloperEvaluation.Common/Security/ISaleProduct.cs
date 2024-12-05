using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Ambev.DeveloperEvaluation.Common.Security
{
	public interface ISaleProduct
	{
		public string Id { get; }
	//	public string SaleNumber { get; }
		public string ProductId { get; }
	}
}
