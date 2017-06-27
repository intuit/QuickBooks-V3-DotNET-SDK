// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Intuit.OAuth2PlatformClient;
using System.Configuration;


namespace Intuit.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class DiscoveryClientTests
    {
        NetworkHandler _successHandler;
        string _endpoint = ConfigurationManager.AppSettings["DiscoveryUrlProduction"];

        public DiscoveryClientTests()
        {
            var discoFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents", "discovery.json");//Nimisha
            var document = File.ReadAllText(discoFileName);

            var jwksFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents", "discovery.json");//Nimisha
            var jwks = File.ReadAllText(jwksFileName);

            _successHandler = new NetworkHandler(request =>
            {
                if (request.RequestUri.AbsoluteUri.EndsWith("jwks"))
                {
                    return jwks;
                }

                return document;
            }, HttpStatusCode.OK);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void malformed_authority_url_should_throw()
        {
            string input = "https:something_weird_https://something_other";
            var client = new DiscoveryClient(input);
            
            //Assert.AreEqual(e.Message,("Malformed authority URL"));
            Assert.Fail();
        


    }

        [TestMethod]
        public void various_urls_should_normalize()
        {
            string input = "https://server:123/";
            var client = new DiscoveryClient(input);

            Assert.AreEqual(client.Url,"https://server:123/.well-known/openid_configuration");
            Assert.AreEqual(client.Authority,"https://server:123");
        }

        [TestMethod]
        public async Task Http_error_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.NotFound, "not found");

            var client = new DiscoveryClient(_endpoint, handler);
            var disco = await client.GetAsync();

            Assert.AreEqual(disco.IsError, true);
            Assert.AreEqual(disco.ErrorType,(ResponseErrorType.Http));
            Assert.AreEqual(disco.Error.StartsWith("Error connecting to"), true);
            Assert.AreEqual(disco.Error.EndsWith("not found"), true);
            Assert.AreEqual(disco.StatusCode,(HttpStatusCode.NotFound));
        }

        [TestMethod]
        public async Task Exception_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(new Exception("error"));

            var client = new DiscoveryClient(_endpoint, handler);
            var disco = await client.GetAsync();

            Assert.AreEqual(disco.IsError, true);
            Assert.AreEqual(disco.ErrorType, (ResponseErrorType.Exception));
            Assert.AreEqual(disco.Error.StartsWith("Error connecting to"), true);
            Assert.AreEqual(disco.Error.EndsWith("error"), true);
        }

        [TestMethod]
        public async Task TryGetValue_calls_should_behave_as_excected()
        {
            var client = new DiscoveryClient(_endpoint, _successHandler);
            var disco = await client.GetAsync();

            
            Assert.AreEqual(disco.IsError, false);
            Assert.IsNotNull(disco.TryGetValue(OidcConstants.Discovery.AuthorizationEndpoint));
            Assert.AreEqual(disco.TryGetString(OidcConstants.Discovery.AuthorizationEndpoint), "https://appcenter.intuit.com/connect/oauth2");
            Assert.IsNull(disco.TryGetValue("unknown"));
            Assert.IsNull(disco.TryGetString("unknown"));
        }

        [TestMethod]
        public async Task Strongly_typed_accessors_should_behave_as_expected()
        {
            var client = new DiscoveryClient(_endpoint, _successHandler);
            var disco = await client.GetAsync();

            Assert.AreEqual(disco.IsError, false);

            Assert.AreEqual(disco.TokenEndpoint, "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer");
            Assert.AreEqual(disco.AuthorizeEndpoint, "https://appcenter.intuit.com/connect/oauth2");
            Assert.AreEqual(disco.UserInfoEndpoint, "https://accounts.intuit.com/v1/openid_connect/userinfo");
            Assert.AreEqual(disco.RevocationEndpoint, "https://developer.api.intuit.com/v2/oauth2/tokens/revoke");
            Assert.AreEqual(disco.JwksUri, "https://oauth.platform.intuit.com/op/v1/jwks");



        }
    }
}