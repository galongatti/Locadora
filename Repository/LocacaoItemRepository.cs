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

		public List<LocacaoItem> BuscarPorIdLocacao(int id)
		{
			List<LocacaoItem> locacoes = (from item in Db.LocacoesItens.AsNoTracking()
											join filme in Db.Filme.AsNoTracking()
											on item.IDFilme equals filme.Id
											where item.IDLocacao == id && item.Ativo == true
											select new LocacaoItem
											{ 
												Filme = filme,
												Id = item.Id,
												IDFilme = item.IDFilme,
												IDLocacao = item.IDLocacao,
											}).ToList();

			return locacoes;
		}

		public override LocacaoItem ObterPorId(int id) 
		{

			LocacaoItem locacoes = (from item in Db.LocacoesItens.AsNoTracking()
												join filme in Db.Filme.AsNoTracking()
												on item.IDFilme equals filme.Id
												where item.Id == id
												select new LocacaoItem
												{
													Filme = filme,
													Id = item.Id,
													IDFilme = item.IDFilme,
													IDLocacao = item.IDLocacao,
												}).FirstOrDefault();

			return locacoes;

		}

		public override List<LocacaoItem> ObterTodos()
		{
			List<LocacaoItem> locacoesItens = (from item in Db.LocacoesItens.AsNoTracking()
												join filme in Db.Filme.AsNoTracking()
												on item.IDFilme equals filme.Id
												select new LocacaoItem
												{
													Filme = filme,
													Id = item.Id,
													IDFilme = item.IDFilme,
													IDLocacao = item.IDLocacao,
												}).ToList();

			return locacoesItens;
		}
	}
}
