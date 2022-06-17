using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IClienteService : IService<Cliente>
	{
		public Task<Cliente> ObterClientePorDocumento(string documento);
		public Task<List<Cliente>> ObterTodosInativos();
	}
}
