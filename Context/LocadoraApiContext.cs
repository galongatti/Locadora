using Locadora.Model;
using Locadora.ModelConfig;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Context
{
	public class LocadoraApiContext : DbContext
	{
        public DbSet<Cliente> Fornecedor { get; set; }
        public DbSet<Filme> Categoria { get; set; }
        public DbSet<Locacao> Lancamento { get; set; }
        public DbSet<LocacoesItens> LocacoesItens { get; set; }
        public LocadoraApiContext(DbContextOptions<LocadoraApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfiguration(new ClienteConfiguration());
            model.ApplyConfiguration(new FilmeConfiguration());
            model.ApplyConfiguration(new LocacaoConfiguration());
            model.ApplyConfiguration(new LocacaoItensConfiguration());


        }
    }
}
