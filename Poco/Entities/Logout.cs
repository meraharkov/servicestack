 
using ServiceStack;

namespace Poco.Entities
{ 
    public class Logout
    {
        public AuthenticateResponse ResponseStatus { get; set; }
    }
     
    [Route("/LogoutUrl")]
    public class LogoutRequest : IReturn<Logout>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        //  public string redirect = null
    }
}
