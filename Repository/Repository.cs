using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Repository
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
	{

		protected readonly LocadoraApiContext Db;
		protected readonly DbSet<TEntity> DbSet;

		protected Repository(LocadoraApiContext db)
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public virtual async Task<TEntity> Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
			await Salvar();
			return entity;
		}

		public virtual async Task<TEntity> Atualizar(TEntity entity)
		{
			DbSet.Update(entity);
			await Salvar();
			return entity;
		}

		public virtual async Task<TEntity> ObterPorId(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task<List<TEntity>> ObterTodos()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<bool> Salvar()
		{
			return await Db.SaveChangesAsync() > 0;
		}

		public void Dispose()
		{
			Db?.Dispose();
		}
	}
}
