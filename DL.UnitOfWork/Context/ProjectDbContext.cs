using System.Data.Entity;
//using DL.DomainModels.Entities;
using DL.Interfaces.DatabaseContext;
using Poco.Entities;


namespace DL.UnitOfWork.Context
{
    public class ProjectDbContext : DbContext, IProjectDbContext
    {
       // public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public ProjectDbContext()
            : this("DatabaseConnectionString")
        {
        }

        public ProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
#if DEBUG
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
        }


    }
}

