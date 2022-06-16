using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.ModelConfig
{
	public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	{
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
			builder.ToTable(nameof(Cliente));
			builder.HasKey(b => b.Id);
			builder.Property(b => b.Nome).HasColumnType("VARCHAR(255)");
			builder.Property(b => b.Documento).HasColumnType("VARCHAR(20)");
			builder.Property(b => b.Ativo).HasColumnType("BIT");
		}
	}
}
