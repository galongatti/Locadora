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

		public LocacaoItem Adicionar(LocacaoItem item)
		{
			Filme filme = _filmeService.ObterPorId(item.IDFilme);
			filme.Disponivel = false;
			_filmeService.Atualizar(filme);
			item.Ativo = true;
			return _locacaoItemRepository.Adicionar(item);
		}

		public LocacaoItem Atualizar(LocacaoItem item)
		{
			return _locacaoItemRepository.Atualizar(item);
		}

		public List<LocacaoItem> BuscarPorIdLocacao(int id)
		{
			return _locacaoItemRepository.BuscarPorIdLocacao(id);
		}

		public bool InativarItens(int idLocacao)
		{
			List<LocacaoItem> itens = BuscarPorIdLocacao(idLocacao);
			itens.ForEach(x => 
			{
				x.Ativo = false;
				_locacaoItemRepository.Atualizar(x);
				_filmeService.AlterarDisponibidade(x.IDFilme);
				
			});

			return true;
		}

		public LocacaoItem ObterPorId(int id)
		{
			return _locacaoItemRepository.ObterPorId(id);
		}

		public List<LocacaoItem> ObterTodos()
		{
			return _locacaoItemRepository.ObterTodos();
		}

		public List<string> ValidarAtualizacao(LocacaoItem item)
		{
			List<LocacaoItem> list = BuscarPorIdLocacao(item.IDLocacao);
			if(!list.Exists(x => x.IDFilme == item.IDFilme))
			{
				Filme filme = _filmeService.ObterPorId(item.IDFilme);
				if(!filme.Disponivel)
					return new List<string>() { $"O filme {filme.Nome} não está disponivel para locação" };
			}

			return new List<string>() { string.Empty };

		}

		public List<string> ValidarDados(LocacaoItem item)
		{
			Filme filme = _filmeService.ObterPorId(item.IDFilme);

			if (filme.Disponivel)
				return new List<string>() { string.Empty };
			else
				return new List<string>() { $"O filme {filme.Nome} não está disponivel para locação" };

		}
	}
}
