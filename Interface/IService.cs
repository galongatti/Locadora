using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IService<TEntity> where TEntity : Entity
	{
		Task<TEntity> Adicionar(TEntity entity);
		Task<TEntity> Atualizar(TEntity entity);
		Task<TEntity> ObterPorId(int id);
		Task<List<TEntity>> ObterTodos();
		List<string> ValidarDados(TEntity entity);
	}
}
