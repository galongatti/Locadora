using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Logica
{
	public class FilmeService : IFilmeService
	{
		private readonly IFilmeRepository _filmeRepository;

		public FilmeService(IFilmeRepository filmeRepository)
		{
			_filmeRepository = filmeRepository;
		}

		public async Task<Filme> Adicionar(Filme filme)
		{
			await _filmeRepository.Adicionar(filme);
			return filme;
		}

		public async Task<Filme> Atualizar(Filme filme)
		{
			await _filmeRepository.Atualizar(filme);
			return filme;
		}

		public async Task<Filme> ObterPorId(int id)
		{
			return await _filmeRepository.ObterPorId(id);
		}

		public async Task<List<Filme>> ObterTodos()
		{
			return await _filmeRepository.ObterTodos();
		}

		public List<string> ValidarDados(Filme cliente)
		{
			List<string> listaErros = new List<string>();

			if (string.IsNullOrEmpty(cliente.Nome))
				listaErros.Add("O nome do filme deve ser preenchido");

			return listaErros;
		}
	}
}
