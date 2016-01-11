using System.Data.Entity;
using Poco.Entities;

namespace DL.Interfaces.DatabaseContext
{
    public interface IProjectDbContext : IDbContext
    { 
        DbSet<UserProfile> UserProfile { get; set; }
    }
}
