using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.ModelConfig
{
	public class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
	{
		public void Configure(EntityTypeBuilder<Locacao> builder)
		{
			builder.ToTable(nameof(Locacao));
			builder.HasKey(b => b.Id);
			builder.HasOne(b => b.Cliente).WithMany(b => b.Locacoes);
			builder.Property(b => b.Situacao).HasColumnType("VARCHAR(20)");
			builder.Property(b => b.DataAlocacao).HasColumnType("DATETIME");
			builder.Property(b => b.DataParaDevolucao).HasColumnType("DATETIME");
			builder.Property(b => b.DiasAlocacao).HasColumnType("INT");
		}
	}
}
