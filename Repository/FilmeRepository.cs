using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Locadora.Repository
{
	public class FilmeRepository : Repository<Filme>, IFilmeRepository
	{
		public FilmeRepository(LocadoraApiContext context) : base(context) { }

		public  List<Filme> BuscarTodosInativos()
		{
			List<Filme> filmes = (from c in Db.Filme.AsNoTracking()
										where c.Ativo == false 
										select c).ToList();
			return filmes;
		}

		public override List<Filme> ObterTodos()
		{
			List<Filme> filmes =  (from c in Db.Filme.AsNoTracking()
										   where c.Ativo == true
										   select c).ToList();
			return filmes;
		}
	}
}
