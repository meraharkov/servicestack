using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

//using DL.DomainModels.Base;
//using DL.DomainModels.Entities;
using DL.Interfaces.DatabaseContext;
using DL.Interfaces.UoW;
using Poco.Base;
using Poco.Entities;

namespace DL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProjectDbContext _projDbContext;

        public UnitOfWork(IProjectDbContext projDbContext)
        {
            _projDbContext = projDbContext;
        } 


        public virtual TEntity Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            return GetDbSet<TEntity>().Add(entity);
        }

        public virtual ICollection<TEntity> Add<TEntity>(ICollection<TEntity> entities) where TEntity : class, IBaseEntity
        {
            return GetDbSet<TEntity>().AddRange(entities).ToList();
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            var dbContext = GetDbContext<TEntity>();

            var dbEntity = dbContext.Set<TEntity>().Single(t => t.Id == entity.Id);

            dbContext.Entry(dbEntity).CurrentValues.SetValues(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            var dbSet = GetDbSet<TEntity>();

            dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public virtual void Delete<TEntity>(ICollection<TEntity> entities) where TEntity : class, IBaseEntity
        {
            var dbSet = GetDbSet<TEntity>();

            foreach (var entity in entities)
            {
                dbSet.Attach(entity);
            }

            dbSet.RemoveRange(entities);
        }

        public virtual void SaveChanges()
        {
            _projDbContext.SaveChanges();
        }

        protected IDbContext GetDbContext<TEntity>()
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity))) return _projDbContext;

            throw new InvalidOperationException("The database context not found for entity " + typeof(TEntity).FullName);
        }

        protected DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IBaseEntity
        {
            return GetDbContext<TEntity>().Set<TEntity>();
        }
    }
}
