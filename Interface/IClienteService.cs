using Locadora.Model;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IClienteService : IService<Cliente>
	{
		public Task<Cliente> ObterClientePorDocumento(string documento);
	}
}
