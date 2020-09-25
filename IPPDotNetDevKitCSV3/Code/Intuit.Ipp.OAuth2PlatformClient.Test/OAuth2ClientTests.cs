using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Intuit.Ipp.OAuth2PlatformClient.Test.Common;
using Intuit.Ipp.Core;

namespace Intuit.Ipp.OAuth2PlatformClient.Tests
{
    [TestClass()]
    public class OAuth2ClientTests
    {
        //static string clientId = ConfigurationManager.AppSettings["ClientId"];
        //static string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        //static string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];
        //static string appEnv = ConfigurationManager.AppSettings["AppEnvironment"];
        //OAuth2Client client = new OAuth2Client(clientId, clientSecret, redirectUrl, appEnv);

        [TestMethod()]
        public void GetAuthorizationURLTest()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService.DataService service = new DataService.DataService(context);

            OAuth2Client client = new OAuth2Client(AuthorizationKeysQBO.clientIdQBO,AuthorizationKeysQBO.clientSecretQBO,AuthorizationKeysQBO.redirectUrl,AuthorizationKeysQBO.appEnvironment);
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            scopes.Add(OidcScopes.Payment);
            string csrfToken = CryptoRandom.CreateUniqueId(); 
            string actual = client.GetAuthorizationURL(scopes, csrfToken);

            string expected = string.Format("https://appcenter.intuit.com/connect/oauth2?client_id={0}&response_type=code&scope=com.intuit.quickbooks.accounting%20com.intuit.quickbooks.payment&redirect_uri={1}&state={2}", client.ClientID, Uri.EscapeDataString(client.RedirectURI), client.CSRFToken);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAuthorizationURLTest1()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService.DataService service = new DataService.DataService(context);
            
            OAuth2Client client = new OAuth2Client(AuthorizationKeysQBO.clientIdQBO, AuthorizationKeysQBO.clientSecretQBO, AuthorizationKeysQBO.redirectUrl, AuthorizationKeysQBO.appEnvironment);

            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            scopes.Add(OidcScopes.Payment);
            string actual = client.GetAuthorizationURL(scopes);

            string expected = string.Format("https://appcenter.intuit.com/connect/oauth2?client_id={0}&response_type=code&scope=com.intuit.quickbooks.accounting%20com.intuit.quickbooks.payment&redirect_uri={1}&state={2}", client.ClientID, Uri.EscapeDataString(client.RedirectURI), client.CSRFToken);

            Assert.AreEqual(expected, actual);
        }

    }
}