using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Ambev.DeveloperEvaluation.Common.Security
{
	public interface IProduct
	{
		public string Id { get; }
		public string ProductName { get; }
	}
}
