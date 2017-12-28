using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace SIENN.DbAccess.Repositories
{
	public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected GenericRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public virtual IEnumerable<TEntity> GetRange(int start, int count)
        {
            return _entities.Skip(start).Take(count).ToList();
        }

        public virtual IEnumerable<TEntity> GetRange(int start, int count, Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).Skip(start).Take(count).ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual int Count()
        {
            return _entities.Count();
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
			var e = _context.Entry(entity);
            e.State = EntityState.Modified;
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        protected DbSet<TEntity> _entities;
		protected DbContext _context;
    }
}
