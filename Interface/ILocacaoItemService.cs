using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface ILocacaoItemService : IService<LocacaoItem>
	{
		Task<List<LocacaoItem>> BuscarPorIdLocacao(int id);
		void InativarItens(int idLocacao);

	}
}
