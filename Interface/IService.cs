using Locadora.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IService<TEntity> where TEntity : Entity
	{
		TEntity Adicionar(TEntity entity);
		TEntity Atualizar(TEntity entity);
		TEntity ObterPorId(int id);
		List<TEntity> ObterTodos();
		List<string> ValidarDados(TEntity entity);
	}
}
