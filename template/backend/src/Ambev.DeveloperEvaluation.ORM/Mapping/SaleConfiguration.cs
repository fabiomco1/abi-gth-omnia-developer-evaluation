using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
	public void Configure(EntityTypeBuilder<Sale> builder)
	{
		builder.ToTable("Sales");

		builder.HasKey(u => u.Id);
		builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

		builder.Property(u => u.Customer).IsRequired().HasMaxLength(50);
	}
}
