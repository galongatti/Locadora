using Locadora.Interface;
using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Service
{
	public class LocacaoItemService : ILocacaoItemService
	{
		private readonly ILocacaoItemRepository _locacaoItemRepository;
		private readonly IFilmeService _filmeService;

		public LocacaoItemService(ILocacaoItemRepository locacaoItemRepository, IFilmeService filmeService)
		{
			_locacaoItemRepository = locacaoItemRepository;
			_filmeService = filmeService;
		}

		public async Task<LocacaoItem> Adicionar(LocacaoItem item)
		{
			item.Ativo = true;
			return await _locacaoItemRepository.Adicionar(item);
		}

		public async Task<LocacaoItem> Atualizar(LocacaoItem item)
		{
			return await _locacaoItemRepository.Atualizar(item);
		}

		public async Task<List<LocacaoItem>> BuscarPorIdLocacao(int id)
		{
			return await _locacaoItemRepository.BuscarPorIdLocacao(id);
		}

		public void InativarItens(int idLocacao)
		{
			List<LocacaoItem> itens = BuscarPorIdLocacao(idLocacao).Result;
			itens.ForEach(x =>
			{
				x.Ativo = false;
				_locacaoItemRepository.Atualizar(x);
				Filme filme = _filmeService.ObterPorId(x.IDFilme).Result;
				filme.Disponivel = true;
				_filmeService.Atualizar(filme);
			});
		}

		public async Task<LocacaoItem> ObterPorId(int id)
		{
			return await _locacaoItemRepository.ObterPorId(id);
		}

		public async Task<List<LocacaoItem>> ObterTodos()
		{
			return await _locacaoItemRepository.ObterTodos();
		}

		public List<string> ValidarDados(LocacaoItem item)
		{
			Filme filme = _filmeService.ObterPorId(item.IDFilme).Result;

			if (filme.Disponivel)
				return new List<string>() { string.Empty };
			else
				return new List<string>() { $"O filme {filme.Nome} não está disponivel para locação" };

		}
	}
}
