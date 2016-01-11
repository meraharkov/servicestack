using System.Collections.Generic;
using Poco.Entities;

namespace BL.Interfaces.Services
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfileById(int id);

        ICollection<UserProfile> GetAllUserProfiles();
        
        UserProfile CreateUserProfile(UserProfile model);

        UserProfile UpdateUserProfile(UserProfile model);

        void DeleteUserProfile(int id);
    }
}
