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
		private readonly ILocacoesService _locacaoService;

		public LocacoesController(ILocacoesService locacaoService)
		{
			_locacaoService = locacaoService;
		}

		[HttpPost]
		public async Task<ActionResult<Locacao>> CadastrarLocacao(Locacao locacao) 
		{
			try 
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto locação inválido"); }

				List<string> erros = _locacaoService.ValidarDados(locacao);

				if(erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Locacao newLocacao = await _locacaoService.Adicionar(locacao);
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

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Locacao newLocacao = await _locacaoService.Adicionar(locacao);
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
		public async Task<ActionResult<Locacao>> BuscarFilmePorId(int id)
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
	}
}
