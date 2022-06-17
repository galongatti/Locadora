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

		public Filme Adicionar(Filme filme)
		{
			 _filmeRepository.Adicionar(filme);
			return filme;
		}

		public void AlterarDisponibidade(int id)
		{
			Filme filme = ObterPorId(id);
			filme.Disponivel = !filme.Disponivel;
			Atualizar(filme);
		}

		public Filme Atualizar(Filme filme)
		{
			 _filmeRepository.Atualizar(filme);
			return filme;
		}

		public List<Filme> BuscarTodosInativos()
		{
			return  _filmeRepository.BuscarTodosInativos();
		}

		public Filme ObterPorId(int id)
		{
			return  _filmeRepository.ObterPorId(id);
		}

		public List<Filme> ObterTodos()
		{
			return _filmeRepository.ObterTodos();
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
