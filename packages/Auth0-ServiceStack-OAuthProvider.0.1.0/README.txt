Auth0-ServiceStack-OAuthProvider | Usage
========================================

Open Web.config file and set the Auth0 settings:

<!-- Auth0 Settings -->
<add key="oauth.auth0.AppId" value="YOUR CLIENT ID" />
<add key="oauth.auth0.AppSecret" value="YOUR CLIENT SECRET" />
<add key="oauth.auth0.OAuthServerUrl" value="YOUR NAMESPACE: https://{tenant}.auth0.com" />

and add the following to your App_Start\AppHost.cs file under the ConfigureAuth method:

var appSettings = new AppSettings();

// Default route: /auth/{provider}
Plugins.Add(new AuthFeature(
	() => new Auth0UserSession(),
	new IAuthProvider[] {
		new Auth0Provider(appSettings, appSettings.GetString("oauth.auth0.OAuthServerUrl"))
	}));