using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace DL.Interfaces.DatabaseContext
{
    public interface IDbContext
    {
        DbChangeTracker ChangeTracker { get; }

        DbContextConfiguration Configuration { get; }

        Database Database { get; }

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Type GetType();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet Set(Type entityType);
    }
}
