using System.Collections.Generic;
using Poco.Entities;


namespace DL.Interfaces.Repository
{
    public interface IUserProfileRepository
    {
        UserProfile GetUserProfileById(int id);

        ICollection<UserProfile> GetAllUserProfile();
    }
}
