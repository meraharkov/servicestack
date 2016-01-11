using System.Collections.Generic;
using Poco.Base;

namespace DL.Interfaces.UoW
{
    public  interface IUnitOfWork
    {
        TEntity Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        ICollection<TEntity> Add<TEntity>(ICollection<TEntity> entities) where TEntity : class, IBaseEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        void Delete<TEntity>(ICollection<TEntity> entities) where TEntity : class, IBaseEntity;
        void SaveChanges();
    }
}
