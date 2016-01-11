using ServiceStack;

namespace Poco.Entities
{

    public class Login 
    {
        public AuthenticateResponse ResponseStatus { get; set; }
    }


    [Route("/LoginUrl")]
    public class LoginRequest : IReturn<Login>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        //  public string redirect = null
    }

}
