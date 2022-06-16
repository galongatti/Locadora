using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacoesRepository : IRepository<Locacao>
	{
		Task<List<Locacao>> ObterPorDocumentoCliente(string documento);
	}
}
