using Locadora.Interface;
using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Service
{
	public class LocacoesService : ILocacoesService
	{
		private readonly ILocacoesRepository _locacoesRepository;
		private readonly IClienteService _clienteService;

		public LocacoesService(ILocacoesRepository locacoesRepository, IClienteService clienteService)
		{
			_locacoesRepository = locacoesRepository;
			_clienteService = clienteService;
		}

		public async Task<Locacao> Adicionar(Locacao locacao)
		{
			await _locacoesRepository.Adicionar(locacao);
			return locacao;
		}

		public async Task<Locacao> Atualizar(Locacao locacao)
		{
			await _locacoesRepository.Atualizar(locacao);
			return locacao;
		}

		public Task<Locacao> DarBaixa(int id)
		{
			Locacao locacao = ObterPorId(id).Result;
			locacao.Situacao = Situacao.FECHADO.ToString();
			return Atualizar(locacao);
		}

		public async Task<List<Locacao>> ObterPorDocumentoCliente(string documento)
		{
			return await _locacoesRepository.ObterPorDocumentoCliente(documento);
		}

		public async Task<Locacao> ObterPorId(int id)
		{
			return await _locacoesRepository.ObterPorId(id);
		}

		public async Task<List<Locacao>> ObterTodos()
		{
			return await _locacoesRepository.ObterTodos();
		}

		public List<string> ValidarDados(Locacao locacao)
		{
			List<string> listaErros = new List<string>();

			if (locacao.DiasAlocacao <= 0)
				listaErros.Add("O numero de dias para alocação deve ser igual ou superior a 1");

			if (locacao.IDCliente == default)
				listaErros.Add("Cliente inválido");

			if (string.IsNullOrEmpty(locacao.Situacao))
				listaErros.Add("O campo situação deve ser informado");

			else
			{
				Cliente cliente = _clienteService.ObterPorId(locacao.IDCliente).Result;

				if (cliente == null)
					listaErros.Add("O Cliente informado não existe");
				else
				{
					locacao.Cliente = cliente;
				}
			}


			return listaErros;
		}

	}
}
