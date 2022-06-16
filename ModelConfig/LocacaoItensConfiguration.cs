using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.ModelConfig
{
	public class LocacaoItensConfiguration : IEntityTypeConfiguration<LocacoesItens>
	{
		public void Configure(EntityTypeBuilder<LocacoesItens> builder)
		{
			builder.ToTable(nameof(LocacoesItens));
			builder.HasKey(b => b.Id);
			builder.HasOne(b => b.Locacao).WithMany(b => b.Itens);
			builder.HasOne(b => b.Filme).WithMany(b => b.LocacoesItens);
		}
	}
}
