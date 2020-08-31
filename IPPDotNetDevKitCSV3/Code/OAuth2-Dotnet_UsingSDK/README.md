[![Sample Banner](views/Sample.png)][ss1]

# OAuth2-Dotnet-UsingSDK
OAuth2 Web Forms Sample app for Dotnet
The Intuit Developer team has written this OAuth 2.0 Sample App in .Net(C#) to provide working examples of OAuth 2.0 concepts, and how to integrate with Intuit endpoints.

## Getting Started

Before beginning, it may be helpful to have a basic understanding of OAuth 2.0 concepts. There are plenty of tutorials and guides to get started with OAuth 2.0. Check out the docs on https://developer.intuit.com/

### PreRequisites

.Net Framework 4.6.1

Microsoft.Net.Compilers 2.10.0

## Setup
Clone this repository/Download the sample app.

## Configuring your app

All configuration for this app is located in web.config. Locate and open this file.

We will need to update 5 items:

1. clientId
2. clientSecret
3. redirectUri
4. appEnvironment (sandbox/production)
5. logPath

First 3 values must match exactly with what is listed in your app settings on developer.intuit.com. If you haven't already created an app, you may do so there. Please read on for important notes about client credentials, scopes, and redirect urls.
logPath should be the location of a physical path on your disk.


### Client Credentials

Once you have created an app on Intuit's Developer Portal, you can find your credentials (Client ID and Client Secret) under the "Keys" tab. You will also find a section to enter your Redirect URI here.

### Redirect URI
You'll have to set a Redirect URI in both 'web.config' and the Developer Portal ("Keys" section). With this app, the typical value would be http://localhost:59785/Default.aspx, unless you host this sample app in a different way (if you were testing HTTPS, for example or changing the port).


## Run your app!

After setting up both Developer Portal and your web.config(setup Log Path too), run the sample app. Check logs on the path you have already configured in the web.config to get details of how the flow worked.

All flows should work, please note the scope in each of these flows. The sample app supports the following flows:

### Sign In With Intuit 
This flow requests OpenID only scopes. After authorizing (or if the account you are using has already been authorized for this app), the redirect URL will parse the JWT ID token, and make an API call to the user information endpoint.

### Connect To QuickBooks 
This flow requests OAuth scopes. You will be able to make a QuickBooks API sample call (using the OAuth2 token) on the /connected landing page.

### Get App Now 
This flow requests both OpenID and OAuth scopes. It simulates the request that would come once a user clicks "Get App Now" on the apps.com website, after you publish your app.

#### Note: This app uses new OAuth2Client. If you want to refer the other way to use standalone OAuth2 clients, download v1.0 from Release tab

[ss1]: https://help.developer.intuit.com/s/samplefeedback?cid=9010&repoName=OAuth2-Dotnet-UsingSDK
