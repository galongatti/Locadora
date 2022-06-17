using Locadora.Context;
using Locadora.Interface;
using Locadora.Logica;
using Locadora.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmeController : ControllerBase
	{
		private readonly IFilmeService _filmeService;

		public FilmeController(IFilmeService filmeService)
		{
			_filmeService = filmeService;
		}

		[HttpPost]
		public async Task<ActionResult<Filme>> CadastrarFilme(Filme filme)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto filme inválido"); }

				List<string> erros = _filmeService.ValidarDados(filme);

				if(erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Filme newFilme = await _filmeService.Adicionar(filme);
					return Ok(newFilme);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao cadastrar filme");
			}
		}

		[HttpPut]
		public async Task<ActionResult<Filme>> AtualizarFilme(Filme filme)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto filme inválido"); }

				List<string> erros = _filmeService.ValidarDados(filme);

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Filme FilmeExiste = await _filmeService.ObterPorId(filme.Id);

					if(FilmeExiste == null)
						return BadRequest("Filme não localizado");
					

					Filme newFilme = await _filmeService.Atualizar(filme);
					return Ok(newFilme);

				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao atualizar filme");
			}
		}

		[HttpGet("BuscarTodosFilmes")]
		public async Task<ActionResult<List<Filme>>> BuscarTodosFilmes()
		{
			try
			{
				return await _filmeService.ObterTodos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}

		[HttpGet("BuscarTodosFilmesInativos")]
		public async Task<ActionResult<List<Filme>>> BuscarTodosFilmesInativos()
		{
			try
			{
				return await _filmeService.BuscarTodosInativos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}

		[HttpGet("BuscarById/{id:int}")]
		public async Task<ActionResult<Filme>> BuscarFilmePorId(int id)
		{
			try
			{
				return await _filmeService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}


	}
}
