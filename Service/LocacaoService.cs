using Locadora.Interface;
using Locadora.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Service
{
	public class LocacaoService : ILocacaoService
	{
		private readonly ILocacaoRepository _locacoesRepository;
		private readonly IClienteService _clienteService;

		public LocacaoService(ILocacaoRepository locacoesRepository, IClienteService clienteService)
		{
			_locacoesRepository = locacoesRepository;
			_clienteService = clienteService;
		}

		public async Task<Locacao> Adicionar(Locacao locacao)
		{
			locacao.Situacao = Status.ABERTO.ToString();
			locacao.DataParaDevolucao = locacao.DataAlocacao.AddDays(locacao.DiasAlocacao).Date;
			await _locacoesRepository.Adicionar(locacao);
			return locacao;
		}

		public List<Locacao> AlimentarObservacao(List<Locacao> lista)
		{
			lista.ForEach(x =>
			{
				AlimentarObservacao(x);
			});

			return lista;
		}

		public Locacao AlimentarObservacao(Locacao x)
		{
			if (x.Situacao == Status.ABERTO.ToString())
			{
				if (x.DataParaDevolucao.Date < DateTime.Now.Date)
					x.ObservacaoSituacao = "LOCAÇÃO EM ATRASO";
			}

			return x;
		}

		public async Task<Locacao> Atualizar(Locacao locacao)
		{
			locacao.Situacao = Status.ABERTO.ToString();
			await _locacoesRepository.Atualizar(locacao);
			return locacao;
		}

		public Task<Locacao> DarBaixa(int id)
		{
			Locacao locacao = ObterPorId(id).Result;
			locacao.Situacao = Status.CONCLUIDO.ToString();
			return Atualizar(locacao);
		}

		public async Task<List<Locacao>> ObterPorDocumentoCliente(string documento)
		{
			Cliente cliente = _clienteService.ObterClientePorDocumento(documento).Result;
			List<Locacao> locacao = await _locacoesRepository.ObterPorIdCliente(cliente.Id);
			AlimentarObservacao(locacao);
			return locacao;
		}

		public async Task<Locacao> ObterPorId(int id)
		{
			Locacao locacao = await _locacoesRepository.ObterPorId(id);
			AlimentarObservacao(locacao);
			return locacao;
		}

		public async Task<List<Locacao>> ObterTodos()
		{
			List<Locacao> locacoes = await _locacoesRepository.ObterTodos();
			AlimentarObservacao(locacoes);
			List<Locacao> newObjlocacoes = AlimentarObservacao(locacoes);

			return newObjlocacoes;
		}

		public List<string> ValidarDados(Locacao locacao)
		{
			List<string> listaErros = new List<string>();

			if (locacao.DiasAlocacao <= 0)
				listaErros.Add("O numero de dias para locação deve ser igual ou superior a 1");

			if (locacao.IDCliente == default)
				listaErros.Add("Cliente inválido");

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
