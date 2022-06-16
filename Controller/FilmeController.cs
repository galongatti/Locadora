using Locadora.Context;
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
		private readonly LocadoraApiContext _context;

		public FilmeController(LocadoraApiContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Filme>> CadastrarFilme(Filme Filme)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest("Objeto filme inválido"); }

				LogicaFilme logica = new LogicaFilme(_context);
				List<string> erros = logica.ValidarDados(Filme);

				if(erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Filme newFilme = await logica.CadastrarFilme(Filme);
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

				LogicaFilme logica = new LogicaFilme(_context);
				List<string> erros = logica.ValidarDados(filme);

				if (erros.Count > 0)
				{
					return BadRequest(erros);
				}
				else
				{
					Filme FilmeExiste = await logica.BuscarFilmeId(filme.Id);
					if(FilmeExiste != null)
					{
						Filme newFilme = await logica.AtualizarFilme(filme);
						return Ok(newFilme);
					} 
					else
					{
						return BadRequest("Filme não localizado");
					}
					
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
				LogicaFilme logica = new LogicaFilme(_context);
				return await logica.BuscarTodosFilmes();
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
				LogicaFilme logica = new LogicaFilme(_context);
				return await logica.BuscarFilmeId(id);
			}
			catch (Exception)
			{

				return BadRequest("Erro ao buscar Filmes");
			}
		}


	}
}
