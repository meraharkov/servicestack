using System;
using System.Collections.Generic;
using BL.Interfaces.Services;
using DL.Interfaces.Repository;
using DL.Interfaces.UoW;
using Poco.Entities;

namespace BL.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUserProfileRepository userProfileRepository,
                                             IUnitOfWork unitOfWork)
        {
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public UserProfile GetUserProfileById(int id)
        {
          var userProfile = _userProfileRepository.GetUserProfileById(id);


          if (userProfile == null)
              throw new Exception("UserProfile not found");

            return userProfile;
        }

        public ICollection<UserProfile> GetAllUserProfiles()
        {
            var userProfiles = _userProfileRepository.GetAllUserProfile();

            return userProfiles;
        }

        public UserProfile CreateUserProfile(UserProfile model)
        {
            _unitOfWork.Add(model);

            _unitOfWork.SaveChanges();

            return model;
        }

        public UserProfile UpdateUserProfile(UserProfile model)
        {
            _unitOfWork.Update(model);

            _unitOfWork.SaveChanges();

            return model;
        }

        public void DeleteUserProfile(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);

            _unitOfWork.Delete(userProfile);

            _unitOfWork.SaveChanges();
        }
    }
}
