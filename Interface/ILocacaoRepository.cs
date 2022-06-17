using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacaoRepository : IRepository<Locacao>
	{
		List<Locacao> ObterPorIdCliente(int cliente);
	}
}
