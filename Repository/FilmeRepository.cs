using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;

namespace Locadora.Repository
{
	public class FilmeRepository : Repository<Filme>, IFilmeRepository
	{
		public FilmeRepository(LocadoraApiContext context) : base(context) { }
	}
}
