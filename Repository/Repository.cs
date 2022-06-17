using Locadora.Context;
using Locadora.Interface;
using Locadora.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public virtual TEntity Adicionar(TEntity entity)
		{
			entity.DataInclusao = DateTime.Now;

			DbSet.Add(entity);
			Salvar();
			return entity;
		}

		public virtual TEntity Atualizar(TEntity entity)
		{
		    Db.ChangeTracker.Clear();
			entity.DataInclusao = DateTime.Now;
			entity.DataAlteracao = DateTime.Now;

			DbSet.Update(entity);
			Salvar();
			return entity;
		}

		public virtual TEntity ObterPorId(int id)
		{
			TEntity obj = DbSet.Find(id);
			return obj;
		}

		public virtual List<TEntity> ObterTodos()
		{
			List<TEntity> obj = DbSet.ToList();
			return obj;
		}

		public bool Salvar()
		{
			return Db.SaveChanges() > 0;
		}

		public void Dispose()
		{
			Db?.Dispose();
		}
	}
}
