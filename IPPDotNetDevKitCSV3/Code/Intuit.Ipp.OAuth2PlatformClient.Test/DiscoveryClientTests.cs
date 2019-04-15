// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Configuration;
using Intuit.Ipp.OAuth2PlatformClient.Helpers;

namespace Intuit.Ipp.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class DiscoveryClientTests
    {
        NetworkHandler _successHandler;
        string _endpoint = ConfigurationManager.AppSettings["DiscoveryUrlProduction"];

        public DiscoveryClientTests()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "discovery.json"));

            var jwks = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "discovery.json"));

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
        public void DiscoveryClientTests2()
        {
            DiscoveryClient clientTest = new DiscoveryClient(AppEnvironment.Sandbox);
            Assert.AreEqual("https://developer.api.intuit.com/.well-known/openid_sandbox_configuration", clientTest.Url);

            DiscoveryClient clientTest2 = new DiscoveryClient(AppEnvironment.Production);
            Assert.AreEqual("https://developer.api.intuit.com/.well-known/openid_configuration", clientTest2.Url);

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

        [TestMethod]
        public void Http_error_should_be_handled_correctly_Get()
        {
            var handler = new NetworkHandler(HttpStatusCode.NotFound, "not found");
            var client = new DiscoveryClient(_endpoint, handler);
            var disco = client.Get();

            Assert.AreEqual(disco.IsError, true);
            Assert.AreEqual(disco.ErrorType, (ResponseErrorType.Http));
            Assert.AreEqual(disco.Error.StartsWith("Error connecting to"), true);
            Assert.AreEqual(disco.Error.EndsWith("not found"), true);
            Assert.AreEqual(disco.StatusCode, (HttpStatusCode.NotFound));
        }

        [TestMethod]
        public void Exception_should_be_handled_correctly_Get()
        {
            var handler = new NetworkHandler(new Exception("error"));
            var client = new DiscoveryClient(_endpoint, handler);
            var disco = client.Get();

            Assert.AreEqual(disco.IsError, true);
            Assert.AreEqual(disco.ErrorType, (ResponseErrorType.Exception));
            Assert.AreEqual(disco.Error.StartsWith("Error connecting to"), true);
            Assert.AreEqual(disco.Error.EndsWith("errors occurred."), true);
        }

        [TestMethod]
        public void TryGetValue_calls_should_behave_as_excected_Get()
        {
            var client = new DiscoveryClient(_endpoint, _successHandler);
            var disco = client.Get();

            Assert.AreEqual(disco.IsError, false);
            Assert.IsNotNull(disco.TryGetValue(OidcConstants.Discovery.AuthorizationEndpoint));
            Assert.AreEqual(disco.TryGetString(OidcConstants.Discovery.AuthorizationEndpoint), "https://appcenter.intuit.com/connect/oauth2");
            Assert.IsNull(disco.TryGetValue("unknown"));
            Assert.IsNull(disco.TryGetString("unknown"));
        }

        [TestMethod]
        public void Strongly_typed_accessors_should_behave_as_expected_Get()
        {
            var client = new DiscoveryClient(_endpoint, _successHandler);
            var disco = client.Get();

            Assert.AreEqual(disco.IsError, false);
            Assert.AreEqual(disco.TokenEndpoint, "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer");
            Assert.AreEqual(disco.AuthorizeEndpoint, "https://appcenter.intuit.com/connect/oauth2");
            Assert.AreEqual(disco.UserInfoEndpoint, "https://accounts.intuit.com/v1/openid_connect/userinfo");
            Assert.AreEqual(disco.RevocationEndpoint, "https://developer.api.intuit.com/v2/oauth2/tokens/revoke");
            Assert.AreEqual(disco.JwksUri, "https://oauth.platform.intuit.com/op/v1/jwks");
        }
    }
}