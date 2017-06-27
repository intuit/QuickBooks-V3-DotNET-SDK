# OAuth2-Dotnet-master
OAuth2 Web Forms Sample app for Dotnet
The Intuit Developer team has written this OAuth 2.0 Sample App in Node.JS to provide working examples of OAuth 2.0 concepts, and how to integrate with Intuit endpoints.

**Getting Started**

Before beginning, it may be helpful to have a basic understanding of OAuth 2.0 concepts. There are plenty of tutorials and guides to get started with OAuth 2.0. Check out the docs on https://developer.intuit.com/

**PreRequisites**

Visual Studio 2015
ngrok


**Setup**
Clone this repository/Download the sample app.

**Configuring your app**

All configuration for this app is located in web.config. Locate and open this file.

We will need to update 4 items:

clientId
clientSecret
redirectUri
logPath

First 3 values must match exactly with what is listed in your app settings on developer.intuit.com. If you haven't already created an app, you may do so there. Please read on for important notes about client credentials, scopes, and redirect urls.
logPath should be the location of a physical path on your disk.


**Client Credentials**

Once you have created an app on Intuit's Developer Portal, you can find your credentials (Client ID and Client Secret) under the "Keys" tab. You will also find a section to enter your Redirect URI here.

**Redirect URI**
You'll have to set a Redirect URI in both 'web.config' and the Developer Portal ("Keys" section). With this app, the typical value would be http://localhost:52626/TestOAuth2_WithSDK.aspx, unless you host this sample app in a different way (if you were testing HTTPS, for example or changing the port).

**Scopes**

Use the scopes as shown in the sample app or docs for different flows.

It is important to ensure that the scopes your are requesting match the scopes allowed on the Developer Portal. For this sample app to work by default, your app on Developer Portal must support both Accounting and Payment scopes. If you'd like to support Accounting only, simply remove thecom.intuit.quickbooks.payment scope from web.config.

**Run your app!**

After setting up both Developer Portal and your web.config(setup Log Path too), run the sample app. Check logs on the path you have already configured in the web.config to get details of how the flow worked.


All flows should work. The sample app supports the following flows:

**Sign In With Intuit** - this flow requests OpenID only scopes. Feel free to change the scopes being requested in web.config. After authorizing (or if the account you are using has already been authorized for this app), the redirect URL will parse the JWT ID token, and make an API call to the user information endpoint.

**Connect To QuickBooks** - this flow requests non-OpenID scopes. You will be able to make a QuickBooks API sample call (using the OAuth2 token) on the /connected landing page.

**Get App Now (Openid)** - this flow requests both OpenID and non-OpenID scopes. It simulates the request that would come once a user clicks "Get App Now" on the apps.com website, after you publish your app.

