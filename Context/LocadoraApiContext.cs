using Locadora.Model;
using Locadora.ModelConfig;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Context
{
	public class LocadoraApiContext : DbContext
	{
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<LocacaoItem> LocacoesItens { get; set; }
        public LocadoraApiContext(DbContextOptions<LocadoraApiContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfiguration(new ClienteConfiguration());
            model.ApplyConfiguration(new FilmeConfiguration());
            model.ApplyConfiguration(new LocacaoConfiguration());
            model.ApplyConfiguration(new LocacaoItensConfiguration());


        }
    }
}
