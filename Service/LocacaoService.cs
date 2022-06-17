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
		private readonly IFilmeService _filmeService;

		public LocacaoService(ILocacaoRepository locacoesRepository, IClienteService clienteService, IFilmeService filmeService)
		{
			_locacoesRepository = locacoesRepository;
			_clienteService = clienteService;
			_filmeService = filmeService;
		}

		public Locacao Adicionar(Locacao locacao)
		{
			locacao.Situacao = Status.ABERTO.ToString();
			locacao.DataParaDevolucao = locacao.DataAlocacao.AddDays(locacao.DiasAlocacao).Date;
			locacao.Itens.ForEach(x =>
			{
				x.DataInclusao = DateTime.Now;
				x.DataAlteracao = DateTime.Now;
			});

			_locacoesRepository.Adicionar(locacao);

			locacao.Itens.ForEach(x =>
			{
				_filmeService.AlterarDisponibidade(x.IDFilme);
			});

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

		public Locacao Atualizar(Locacao locacao)
		{
			locacao.Situacao = Status.ABERTO.ToString();
			locacao.DataParaDevolucao = locacao.DataAlocacao.AddDays(locacao.DiasAlocacao).Date;
			locacao.Itens.ForEach(x =>
			{
				x.DataInclusao = DateTime.Now;
				x.DataAlteracao = DateTime.Now;
			});

			_locacoesRepository.Atualizar(locacao);
			locacao.Itens.ForEach(x =>
			{
				_filmeService.AlterarDisponibidade(x.IDFilme);
			});
			return locacao;
		}

		public Locacao DarBaixa(int id)
		{
			Locacao locacao = ObterPorId(id);
			locacao.Situacao = Status.CONCLUIDO.ToString();
			_locacoesRepository.Atualizar(locacao);
			List<LocacaoItem> itens = locacao.Itens;
			itens.RemoveAll(x => x.Ativo == false);
			itens.ForEach(x =>
			{
				_filmeService.AlterarDisponibidade(x.IDFilme);
			});

			return locacao;
		}

		public List<Locacao> ObterPorDocumentoCliente(string documento)
		{
			Cliente cliente = _clienteService.ObterClientePorDocumento(documento);
			List<Locacao> locacao = _locacoesRepository.ObterPorIdCliente(cliente.Id);
			AlimentarObservacao(locacao);
			return locacao;
		}

		public Locacao ObterPorId(int id)
		{
			Locacao locacao = _locacoesRepository.ObterPorId(id);
			AlimentarObservacao(locacao);
			return locacao;
		}

		public List<Locacao> ObterTodos()
		{
			List<Locacao> locacoes = _locacoesRepository.ObterTodos();
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
				Cliente cliente = _clienteService.ObterPorId(locacao.IDCliente);

				if (cliente == null)
					listaErros.Add("O Cliente informado não existe");
			}


			return listaErros;
		}

		
	}
}
