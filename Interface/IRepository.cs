using Locadora.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Interface
{
	public interface IRepository<TEntity> : IDisposable where TEntity : Entity
	{
		TEntity Adicionar(TEntity entity);
		TEntity Atualizar(TEntity entity);
		bool Salvar();
		TEntity ObterPorId(int id);
		List<TEntity> ObterTodos();
	}
}
