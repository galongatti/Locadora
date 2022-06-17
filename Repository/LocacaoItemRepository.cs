using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
	public class LocacaoItemRepository : Repository<LocacaoItem>, ILocacaoItemRepository
	{
		public LocacaoItemRepository(LocadoraApiContext context) : base(context) { }

		public async Task<List<LocacaoItem>> BuscarPorIdLocacao(int id)
		{
			List<LocacaoItem> locacoes = await (from item in Db.LocacoesItens.AsNoTracking()
											join filme in Db.Filme.AsNoTracking()
											on item.IDFilme equals filme.Id
											where item.IDLocacao == id
											select new LocacaoItem
											{ 
												Filme = filme,
												Id = item.Id,
												IDFilme = item.IDFilme,
												IDLocacao = item.IDLocacao,
											}).ToListAsync();

			return locacoes;
		}

		public override async Task<LocacaoItem> ObterPorId(int id) 
		{

			LocacaoItem locacoes = await (from item in Db.LocacoesItens.AsNoTracking()
												join filme in Db.Filme.AsNoTracking()
												on item.IDFilme equals filme.Id
												where item.Id == id
												select new LocacaoItem
												{
													Filme = filme,
													Id = item.Id,
													IDFilme = item.IDFilme,
													IDLocacao = item.IDLocacao,
												}).FirstOrDefaultAsync();

			return locacoes;

		}

		public override async Task<List<LocacaoItem>> ObterTodos()
		{
			List<LocacaoItem> locacoesItens = await (from item in Db.LocacoesItens.AsNoTracking()
												join filme in Db.Filme.AsNoTracking()
												on item.IDFilme equals filme.Id
												select new LocacaoItem
												{
													Filme = filme,
													Id = item.Id,
													IDFilme = item.IDFilme,
													IDLocacao = item.IDLocacao,
												}).ToListAsync();

			return locacoesItens;
		}
	}
}
