using System.Collections.Generic;
using System.Linq;
using DL.Interfaces.DatabaseContext;
using DL.Interfaces.Repository;
using Poco.Entities;

namespace DL.Repositories.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
         private readonly IProjectDbContext _projDbContext;

         public UserProfileRepository(IProjectDbContext projectDbContext)
        {
            _projDbContext = projectDbContext;
        }

        public UserProfile GetUserProfileById(int id)
        {
            return _projDbContext
                       .UserProfile
                       .AsNoTracking()
                       .SingleOrDefault(up => up.Id == id);
        }

        public ICollection<UserProfile> GetAllUserProfile()
        {
            return _projDbContext
                        .UserProfile
                        .AsNoTracking()
                        .ToList();
        }
    }
}
