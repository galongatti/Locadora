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

		public async Task<List<Locacao>> ObterPorDocumentoCliente(string documento)
		{
			List<Locacao> locacoes = await (from locacao in Db.Locacoes.AsNoTracking()
											join cliente in Db.Cliente.AsNoTracking()
											on locacao.IDCliente equals cliente.Id
											join itens in Db.LocacoesItens.AsNoTracking()
											on locacao.Id equals itens.Id into itensAux
											where cliente.Documento == documento && locacao.Situacao != Status.INATIVO 
											select new Locacao
											{ 
												Cliente = cliente,
												DataAlocacao = locacao.DataAlocacao,
												DataParaDevolucao = locacao.DataParaDevolucao,
												DiasAlocacao = locacao.DiasAlocacao,
												Id = locacao.Id,
												IDCliente = cliente.Id,
												Situacao = locacao.Situacao,
												Itens = itensAux.Where(x => x.Ativo == true).ToList()
											}).ToListAsync();

			return locacoes;
		}

		public override async Task<Locacao> ObterPorId(int id) 
		{

			Locacao locacaoObj = await (from locacao in Db.Locacoes.AsNoTracking()
											join cliente in Db.Cliente.AsNoTracking()
											on locacao.Cliente.Id equals cliente.Id
											join itens in Db.LocacoesItens.AsNoTracking()
											on locacao.Id equals itens.Id into itensAux
											where locacao.Id == id
											select new Locacao
											{
												Cliente = cliente,
												DataAlocacao = locacao.DataAlocacao,
												DataParaDevolucao = locacao.DataParaDevolucao,
												DiasAlocacao = locacao.DiasAlocacao,
												Id = locacao.Id,
												IDCliente = cliente.Id,
												Situacao = locacao.Situacao,
												Itens = itensAux.Where(x => x.Ativo == true).ToList()
											}).FirstOrDefaultAsync();

			return locacaoObj;

		}

		public override async Task<List<Locacao>> ObterTodos()
		{
			List<Locacao> locacoes = await (from locacao in Db.Locacoes.AsNoTracking()
											join cliente in Db.Cliente.AsNoTracking()
											on locacao.IDCliente equals cliente.Id
											join itens in Db.LocacoesItens.AsNoTracking()
											on locacao.Id equals itens.Id into itensAux
											where locacao.Situacao != Status.INATIVO
											select new Locacao
											{
												Cliente = cliente,
												DataAlocacao = locacao.DataAlocacao,
												DataParaDevolucao = locacao.DataParaDevolucao,
												DiasAlocacao = locacao.DiasAlocacao,
												Id = locacao.Id,
												IDCliente = cliente.Id,
												Situacao = locacao.Situacao,
												Itens = itensAux.Where(x => x.Ativo == true).ToList()
											}).ToListAsync();

			return locacoes;

		}
	}
}
