using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacaoService : IService<Locacao>
	{
		Task<List<Locacao>> ObterPorDocumentoCliente(string documento);
		Task<Locacao> DarBaixa(int id);
	}
}
