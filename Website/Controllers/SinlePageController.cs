using System.Web.Mvc;
using Poco.Entities;
using ServiceStack;
using ServiceStack.Auth;


namespace Website.Controllers
{
    public class SinlePageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Twitter()
        {
            var responce = MvcApplication.ServiceClient.Get("http://localhost:10050/auth/twitter");

            return Redirect(responce.ResponseUri.ToString());
        }

        public void Login( )
        {
            MvcApplication.ServiceClient.Get<AuthenticateResponse>("/authenticate?username=Maksym&password=Maks123$");
        }

        [HttpPost]
        public void LogOut()
        {
            MvcApplication.ServiceClient.Get<AuthenticateResponse>("/auth/logout");
        }

        [HttpPost]
        public JsonResult GetUserProfile()
        {
        
            var client = new JsonServiceClient("http://localhost:10050");
            
            var authResponse = client.Post(new Authenticate
            {
                provider = CredentialsAuthProvider.Name, //= credentials
                UserName = "Maksym",
                Password = "Maks123$",
                RememberMe = true,
            });

            UserProfile userProfile = client.Post(new UserProfileRequest());

            return Json(userProfile);
        }


        [HttpPost]
        public JsonResult GetUserProfileStatic()
        { 
            var authResponseStatic = MvcApplication.ServiceClient.Post(new Authenticate
            {
                provider = CredentialsAuthProvider.Name, //= credentials
                UserName = "Maksym",
                Password = "Maks123$",
                RememberMe = true,


            });

            UserProfile userProfile = MvcApplication.ServiceClient.Post(new UserProfileRequest());

            return Json(userProfile);
        }
    }
}