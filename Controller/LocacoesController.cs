using Locadora.Interface;
using Locadora.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocacoesController : ControllerBase
	{
		private readonly ILocacaoService _locacaoService;
		private readonly ILocacaoItemService _locacaoItemService;

		public LocacoesController(ILocacaoService locacaoService, ILocacaoItemService locacaoItemService)
		{
			_locacaoService = locacaoService;
			_locacaoItemService = locacaoItemService;
		}

		/// <summary>
		/// Realiza uma locação
		/// </summary>
		[HttpPost]
		public ActionResult<Locacao> CadastrarLocacao(Locacao locacao) 
		{
			try 
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto locação inválido"); }

				List<string> erros =  _locacaoService.ValidarDados(locacao);
				List<string> errosItens = new List<string>();
				locacao.Itens.ForEach( x =>
				{
					List<string> erro =  _locacaoItemService.ValidarDados(x);
					if (!string.IsNullOrEmpty(erro[0]))
						errosItens.AddRange(erro);
				});

				if(erros.Count > 0 || errosItens.Count > 0)
				{
					var objErro =
					new
					{
						ErroPedido = erros,
						ErroItens = errosItens
					};

					return BadRequest(objErro);
				}
				else
				{
					 _locacaoService.Adicionar(locacao);

				
					return Ok(locacao);
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Erro ao cadastrar Locação");
			}

		}

		/// <summary>
		/// Atualiza a locação passada por parametro
		/// </summary>
		[HttpPut]
		public ActionResult<Locacao> AtualizarLocacao(Locacao locacao)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto locação inválido"); }

				Locacao locacaoExiste =  _locacaoService.ObterPorId(locacao.Id);

				if (locacaoExiste == null)
					return BadRequest("Locação não localizada");

				List<string> erros =  _locacaoService.ValidarDados(locacao);
				List<string> errosItens = new List<string>();
				List<LocacaoItem> itensAux = locacao.Itens;

				itensAux.ForEach( x =>
				{
					List<string> erro =  _locacaoItemService.ValidarAtualizacao(x);
					if (!string.IsNullOrEmpty(erro[0]))
						errosItens.AddRange(erro);
				});

				if (erros.Count > 0 || errosItens.Count > 0)
				{
					var objErro =
					new
					{
						ErroPedido = erros,
						ErroItens = errosItens
					};

					return BadRequest(objErro);
				}
				else
				{
					 _locacaoItemService.InativarItens(locacao.Id);

					Locacao newLocacao =  _locacaoService.Atualizar(locacao);
					
					return Ok(newLocacao);
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Erro ao atualizar locação");
			}

		}


		/// <summary>
		/// Faz a baixa da locação
		/// </summary>
		[HttpPut("DarBaixa/{id:int}")]
		public ActionResult<Locacao> DarBaixa(int id)
		{
			try
			{
				Locacao locacaoExiste =  _locacaoService.ObterPorId(id);

				if (locacaoExiste == null)
					return BadRequest("Locação não localizada");

				return  _locacaoService.DarBaixa(id);

			}
			catch (Exception)
			{
				return BadRequest("Erro ao dar baixa na locação");
			}
		}

		/// <summary>
		/// Busca todas as locações e entidades relacionadas, como cliente e itens 
		/// </summary>
		[HttpGet("BuscarTodasLocacoes")]
		public ActionResult<List<Locacao>> BuscarTodasLocacoes()
		{
			try
			{
				return  _locacaoService.ObterTodos();
			}
			catch (Exception ex)
			{

				return BadRequest("Erro ao buscar as Locações");
			}
		}

		/// <summary>
		/// Busca uma locação de acordo com o id passado, junto com as entidades relacionadas, como cliente e itens 
		/// </summary>
		[HttpGet("BuscarById/{id:int}")]
		public ActionResult<Locacao> BuscarLocacaoItemPorId(int id)
		{
			try
			{
				return  _locacaoService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar a Locação");
			}
		}

		/// <summary>
		/// Busca todas locações de um cliente de acordo com seu documento passado, junto com as entidades relacionadas, como cliente e itens 
		/// </summary>
		[HttpGet("BuscarByDocumento/{documento}")]
		public ActionResult<List<Locacao>> BuscarByLocacao(string documento)
		{
			try
			{
				return  _locacaoService.ObterPorDocumentoCliente(documento);
			}
			catch (Exception)
			{
				return BadRequest("Erro ao buscar a Locação");
			}
		}
	}
}
