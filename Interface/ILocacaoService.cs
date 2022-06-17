using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacaoService : IService<Locacao>
	{
		List<Locacao> ObterPorDocumentoCliente(string documento);
		Locacao DarBaixa(int id);
		List<Locacao> AlimentarObservacao(List<Locacao> lista);
		Locacao AlimentarObservacao(Locacao lista);
	}
}
