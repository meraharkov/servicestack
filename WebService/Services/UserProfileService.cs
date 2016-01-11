using System;
using BL.Services.Services;
using Poco.Entities;
using ServiceStack;

namespace WebService.Services
{
  public class ProfileUserService : Service
    {
        private readonly UserProfileService _userProfileService;

        public ProfileUserService(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public object Any(UserProfileRequest request)
        {
            if (request.Id == 0)
            {
                return new UserProfile()
                {
                    Birthday = DateTime.Now,
                    IsMale = true,
                    Language = "En",
                    UserId = "831",
                };
            }

            var profileUsers = _userProfileService.GetUserProfileById(request.Id);

            return profileUsers;
        }
    }
}