using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.ModelConfig
{
	public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
	{
		public void Configure(EntityTypeBuilder<Filme> builder)
		{
			builder.ToTable(nameof(Filme));
			builder.HasKey(b => b.Id);
			builder.Property(b => b.Nome).HasColumnType("VARCHAR(255)");
			builder.Property(b => b.Ativo).HasColumnType("BIT");
			builder.Property(b => b.Disponivel).HasColumnType("BIT");
	
		}
	}
}
