
using System.Collections.Generic;
using System.Configuration;

using BL.Services.Services;
using DL.Repositories.Repositories;
using DL.UnitOfWork;
using DL.UnitOfWork.Context;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Authentication.OAuth2;

using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using WebService.Services;


namespace WebService.Initialization
{
    public class ServiceStackAppHost : AppHostBase
    {
        public ServiceStackAppHost()
            : base("WebService", typeof(ProfileUserService).Assembly)
        { }

        public override void Configure(Funq.Container container)
        {
            var appSettings = new AppSettings();

            var successRedirectUrlFilter = ConfigurationManager.AppSettings["SuccessRedirectUrlFilter"];
            var authServerUrl = appSettings.GetString("oauth.auth0.OAuthServerUrl");

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),            //Use your own typed Custom UserSession type
                                new IAuthProvider[] {
                                    new CredentialsAuthProvider(),              //HTML Form post of UserName/Password credentials
                                    new TwitterAuthProvider(appSettings)
                                    {
                                        SuccessRedirectUrlFilter = (authProvider, url) => successRedirectUrlFilter
                                    },                                          //Sign-in with Twitter
                                    new FacebookAuthProvider(appSettings)
                                    {
                                         SuccessRedirectUrlFilter = (authProvider, url) => successRedirectUrlFilter
                                    },                                          //Sign-in with Facebook
                                    new DigestAuthProvider(appSettings),        //Sign-in with Digest Auth
                                    new BasicAuthProvider(),                    //Sign-in with Basic Auth
                                    new GoogleOAuth2Provider(appSettings),      //Sign-in with Google OpenId
                                    new LinkedInOAuth2Provider(appSettings),
                                    new VkAuthProvider(appSettings)
                                    {
                                        ApplicationId = "5205626",
                                        SecureKey = "Vjbd20LrrW1stdaWONty",
                                        SuccessRedirectUrlFilter = (authProvider, url) => successRedirectUrlFilter
                                    },
                                    new FourSquareOAuth2Provider(appSettings),
                                    new GithubAuthProvider(appSettings), 
                                    new Auth0Provider(appSettings, authServerUrl)
                                }));

            Plugins.Add(new SessionFeature());

            var allowDomainForCors = new List<string>(ConfigurationManager.AppSettings["allowDomainForCORS"].Split(new[] { ';' }));

            Plugins.Add(new RegistrationFeature());

            var cors = new CorsFeature(
                allowOriginWhitelist: allowDomainForCors,
                allowCredentials: true,
                allowedHeaders: "Content-Type, Allow, Authorization");
            Plugins.Add(cors);

            var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

            var dbConFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

            container.Register<IDbConnectionFactory>(dbConFactory);

            container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.InitSchema();
            CreateAdmine(authRepo);

            container.Register(new ProjectDbContext(connectionString));

            container.Register(c => new UnitOfWork(c.Resolve<ProjectDbContext>()));

            container.Register(c => new UserProfileRepository(c.Resolve<ProjectDbContext>()));

            container.Register(c => new UserProfileService(c.Resolve<UserProfileRepository>(), c.Resolve<UnitOfWork>()));


        }

        private void CreateAdmine(OrmLiteAuthRepository authRepo)
        {
            var admine = authRepo.GetUserAuthByUserName("Maksym");

            if (admine == null)
            {
                string hash;
                string salt;

                var adminEmail = ConfigurationManager.AppSettings["adminEmail"];
                var adminLastName = ConfigurationManager.AppSettings["adminLastName"];
                var adminName = ConfigurationManager.AppSettings["adminName"];
                var adminPassword = ConfigurationManager.AppSettings["adminPassword"];

                new SaltedHash().GetHashAndSaltString("password", out hash, out salt);
                authRepo.CreateUserAuth(new UserAuth
                {
                    Id = 1,
                    DisplayName = adminName,
                    Email = adminEmail,
                    UserName = adminName,
                    FirstName = adminName,
                    LastName = adminLastName,
                    PasswordHash = hash,
                    Salt = salt,
                    Roles = new List<string> { RoleNames.Admin },
                    // Permissions = new List<string> { "GetStatus" }
                }, adminPassword);
            }
        }

    }

}