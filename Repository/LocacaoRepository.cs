using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
	public class LocacaoRepository : Repository<Locacao>, ILocacaoRepository
	{
		public LocacaoRepository(LocadoraApiContext context) : base(context) { }

		public async Task<List<Locacao>> ObterPorIdCliente(int id)
		{
			List<Locacao> locacoes = await Db.Locacoes.Where(x => x.IDCliente == id).Include(x => x.Cliente).Include(x => x.Itens).ToListAsync();

			return locacoes;
		}

		public override async Task<Locacao> ObterPorId(int id) 
		{

			Locacao locacoes = await Db.Locacoes.Where(x => x.Id == id).Include(x => x.Cliente).Include(x => x.Itens).FirstOrDefaultAsync();

			return locacoes;

		}

		public override async Task<List<Locacao>> ObterTodos()
		{
			List<Locacao> locacoes = await Db.Locacoes.Include(x => x.Cliente).Include(x => x.Itens).ToListAsync();

			return locacoes;

		}
	}
}
