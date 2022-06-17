using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacaoItemService : IService<LocacaoItem>
	{
		List<LocacaoItem> BuscarPorIdLocacao(int id);
		bool InativarItens(int idLocacao);
		List<string> ValidarAtualizacao(LocacaoItem item);


	}
}
