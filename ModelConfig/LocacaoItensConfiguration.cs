using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.ModelConfig
{
	public class LocacaoItensConfiguration : IEntityTypeConfiguration<LocacaoItem>
	{
		public void Configure(EntityTypeBuilder<LocacaoItem> builder)
		{
			builder.ToTable(nameof(LocacaoItem));
			builder.HasKey(b => b.Id);
			builder.HasOne(b => b.Locacao).WithMany(b => b.Itens).HasForeignKey(b => b.IDLocacao);
			builder.HasOne(b => b.Filme).WithMany(b => b.LocacoesItens).HasForeignKey(b => b.IDFilme);
		}
	}
}
