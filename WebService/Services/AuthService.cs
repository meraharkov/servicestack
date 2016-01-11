using System.Web.Security;
using Poco.Entities;
using ServiceStack;
using ServiceStack.Auth;

namespace WebService.Services
{
    public class AuthService2 : Service
    {
        public object Any(LoginRequest login)
        {
            login.UserName = "Maksym";
            login.Password = "Maks123$";

            using (var authService = ResolveService<AuthenticateService>())
            {
                AuthenticateResponse response = authService.Authenticate(new Authenticate
                {
                    provider = CredentialsAuthProvider.Name,
                    UserName = login.UserName,
                    Password = login.Password,
                    RememberMe = true,
                });

                // add ASP.NET auth cookie
                FormsAuthentication.SetAuthCookie(login.UserName, true);
                return response;
            }
        }

        public object Any(LogoutRequest logout)
        {
            AuthenticateResponse authResponse = null;
            logout.UserName = "Maksym";
            logout.Password = "Maks123$";

            using (var authService = ResolveService<AuthenticateService>())
            {
                authResponse = authService.Authenticate(new Authenticate
                {
                    UserName = logout.UserName,
                    Password = logout.Password,
                    provider = "logout"
                });
            }

            FormsAuthentication.SignOut();

            return authResponse;
        }
    }
}