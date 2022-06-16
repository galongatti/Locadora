using Locadora.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IRepository<TEntity> : IDisposable where TEntity : Entity
	{
		Task<TEntity> Adicionar(TEntity entity);
		Task<TEntity> Atualizar(TEntity entity);
		Task<bool> Salvar();
		Task<TEntity> ObterPorId(int id);
		Task<List<TEntity>> ObterTodos();
	}
}
