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

		[HttpPost]
		public async Task<ActionResult<Locacao>> CadastrarLocacao(Locacao locacao) 
		{
			try 
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto locação inválido"); }

				List<string> erros = _locacaoService.ValidarDados(locacao);
				List<string> errosItens = new List<string>();
				locacao.Itens.ForEach(x =>
				{
					List<string> erro = _locacaoItemService.ValidarDados(x);
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
					Locacao newLocacao = await _locacaoService.Adicionar(locacao);

					List<LocacaoItem> newItens = new List<LocacaoItem>();
					locacao.Itens.ForEach(async x =>
					{
						LocacaoItem item = await _locacaoItemService.Adicionar(x);
						newItens.Add(item);
					});

					newLocacao.Itens = newItens;
					return Ok(newLocacao);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao cadastrar filme");
			}

		}

		[HttpPut]
		public async Task<ActionResult<Locacao>> AtualizarLocacao(Locacao locacao)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto locação inválido"); }

				Locacao locacaoExiste = await _locacaoService.ObterPorId(locacao.Id);

				if (locacaoExiste == null)
					return BadRequest("Locação não localizada");

				List<string> erros = _locacaoService.ValidarDados(locacao);
				List<string> errosItens = new List<string>();
				locacao.Itens.ForEach(x =>
				{
					List<string> erro = _locacaoItemService.ValidarDados(x);
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

					Locacao newLocacao = await _locacaoService.Atualizar(locacao);
					List<LocacaoItem> newItens = new List<LocacaoItem>();
					locacao.Itens.ForEach(async x =>
					{
						LocacaoItem item = await _locacaoItemService.Adicionar(x);
						newItens.Add(item);
					});

					newLocacao.Itens = newItens;

					return Ok(newLocacao);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao atualizar locação");
			}

		}

		[HttpPut("DarBaixa/{id:int}")]
		public async Task<ActionResult<Locacao>> DarBaixa(int id)
		{
			try
			{
				Locacao locacaoExiste = await _locacaoService.ObterPorId(id);

				if (locacaoExiste == null)
					return BadRequest("Locação não localizada");

				return await _locacaoService.DarBaixa(id);

			}
			catch (Exception)
			{
				return BadRequest("Erro ao dar baixa na locação");
			}
		}

		[HttpGet("BuscarTodasLocacoes")]
		public async Task<ActionResult<List<Locacao>>> BuscarTodasLocacoes()
		{
			try
			{
				return await _locacaoService.ObterTodos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar as Locações");
			}
		}

		[HttpGet("BuscarById/{id:int}")]
		public async Task<ActionResult<Locacao>> BuscarLocacaoItemPorId(int id)
		{
			try
			{
				return await _locacaoService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar a Locação");
			}
		}

		[HttpGet("BuscarByDocumento/{id:int}")]
		public async Task<ActionResult<List<Locacao>>> BuscarByLocacao(string documento)
		{
			try
			{
				return await _locacaoService.ObterPorDocumentoCliente(documento);
			}
			catch (Exception)
			{
				return BadRequest("Erro ao buscar a Locação");
			}
		}
	}
}
