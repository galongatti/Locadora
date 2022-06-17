using Locadora.Context;
using Locadora.Interface;
using Locadora.Logica;
using Locadora.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		/// <summary>
		/// Cadastra um filme
		/// </summary>
		[HttpPost]
		public ActionResult<Filme> CadastrarFilme(Filme filme)
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
					Filme newFilme = _filmeService.Adicionar(filme);
					return Ok(newFilme);
				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao cadastrar filme");
			}
		}

		/// <summary>
		/// Atualiza o registro do filme
		/// </summary>
		[HttpPut]
		public ActionResult<Filme> AtualizarFilme(Filme filme)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto filme inválido"); }

				List<string> erros =  _filmeService.ValidarDados(filme);

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Filme newFilme =  _filmeService.Atualizar(filme);
					return Ok(newFilme);

				}
			}
			catch (Exception)
			{
				return BadRequest("Erro ao atualizar filme");
			}
		}

		/// <summary>
		/// Busca todos os filmes ativos
		/// </summary>
		[HttpGet("BuscarTodosFilmes")]
		public ActionResult<List<Filme>> BuscarTodosFilmes()
		{
			try
			{
				return  _filmeService.ObterTodos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}

		/// <summary>
		/// Busca todos os filmes inativos
		/// </summary>
		[HttpGet("BuscarTodosFilmesInativos")]
		public ActionResult<List<Filme>> BuscarTodosFilmesInativos()
		{
			try
			{
				return  _filmeService.BuscarTodosInativos();
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}

		/// <summary>
		/// Busca um filme de acordo com o id
		/// </summary>
		[HttpGet("BuscarById/{id:int}")]
		public ActionResult<Filme> BuscarFilmePorId(int id)
		{
			try
			{
				return  _filmeService.ObterPorId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}

	}
}
