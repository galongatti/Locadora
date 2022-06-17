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

		public List<Locacao> ObterPorIdCliente(int id)
		{
			List<Locacao> locacoes = Db.Locacoes.Where(x => x.IDCliente == id).Include(x => x.Cliente).Include(x => x.Itens).ToList();

			return locacoes;
		}

		public override Locacao ObterPorId(int id) 
		{

			Locacao locacoes = Db.Locacoes.Where(x => x.Id == id).Include(x => x.Cliente).Include(x => x.Itens).FirstOrDefault();

			return locacoes;

		}

		public override List<Locacao> ObterTodos()
		{
			List<Locacao> locacoes = Db.Locacoes.Include(x => x.Cliente).Include(x => x.Itens).ToList();

			return locacoes;

		}
	}
}
