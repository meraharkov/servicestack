using System;
using ServiceStack;

namespace Poco.Entities
{
    public class UserProfile : BaseEntity
    {
        public DateTime Birthday { get; set; }

        public bool IsMale { get; set; }

        public string Language { get; set; }

        public string UserId { get; set; }

       // public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/userProfile")]
     [Authenticate]
    public class UserProfileRequest : IReturn<UserProfile>
    {
        public int Id { get; set; }
    }
}
