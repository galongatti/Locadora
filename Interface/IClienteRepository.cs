using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IClienteRepository : IRepository<Cliente>
	{
		public Task<Cliente> ObterClientePorDocumento(string documento);
	}
}
