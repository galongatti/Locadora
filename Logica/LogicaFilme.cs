using Locadora.Context;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Logica
{
	public class LogicaFilme
	{
		private readonly LocadoraApiContext _context;
		private readonly DbSet<Filme> _dbSet;
		public LogicaFilme(LocadoraApiContext context)
		{
			_context = context;
			_dbSet = context.Filme;
		}

		public List<string> ValidarDados(Filme cliente)
		{
			List<string> listaErros = new List<string>();

			if (string.IsNullOrEmpty(cliente.Nome))
				listaErros.Add("O nome do filme deve ser preenchido");

			return listaErros;
		}

		public async Task<Filme> CadastrarFilme(Filme filme)
		{
			_context.Filme.Add(filme);
			await _context.SaveChangesAsync();
			return filme;
		}

		public async Task<Filme> AtualizarFilme(Filme filme)
		{
			_context.Filme.Update(filme);
			await _context.SaveChangesAsync();
			return filme;
		}

		public async Task<Filme> BuscarFilmeId(int id)
		{
			Filme filme = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
			return filme;
		}

		public async Task<List<Filme>> BuscarTodosFilmes()
		{
			List<Filme> filmes = await _dbSet.AsNoTracking().ToListAsync();
			return filmes;
		}

	}
}
