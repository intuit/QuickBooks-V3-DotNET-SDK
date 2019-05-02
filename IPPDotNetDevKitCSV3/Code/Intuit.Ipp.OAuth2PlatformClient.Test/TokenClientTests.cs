// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Intuit.Ipp.OAuth2PlatformClient;


namespace Intuit.Ipp.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class TokenClientTests
    {
        const string Endpoint = "https://server/token";
        
        [TestMethod]
        public async Task Valid_protocol_response_should_be_handled_correctly()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "success_token_response.json"));
            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");

            Assert.AreEqual(false, response.IsError);
            Assert.AreEqual(ResponseErrorType.None, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.AreEqual(3600, response.AccessTokenExpiresIn);
            Assert.AreEqual(15552000, response.RefreshTokenExpiresIn);
            Assert.AreEqual("access_token", response.AccessToken);
            Assert.AreEqual("refresh_token", response.RefreshToken);
            Assert.AreEqual("id_token", response.IdentityToken);  
        }

        [TestMethod]
        public async Task Valid_protocol_error_should_be_handled_correctly()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "failure_token_response.json"));
            var handler = new NetworkHandler(document, HttpStatusCode.BadRequest);

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Protocol, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.HttpStatusCode);
            Assert.AreEqual("error", response.Error);
            Assert.AreEqual("error_description", response.ErrorDescription); 
        }

        [TestMethod]
        public async Task Malformed_response_document_should_be_handled_correctly()
        {
            var document = "invalid";
            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");


            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Exception, response.ErrorType);
            Assert.AreEqual("invalid", response.Raw);
            //Assert.AreEqual("error", response.Error);
            Assert.IsNotNull(response.Exception);
        }

        [TestMethod]
        public async Task Exception_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.InternalServerError,"exception");

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Http, response.ErrorType);     
            Assert.AreEqual("exception", response.Error);
            Assert.IsNotNull(response.Error); 
        }

        [TestMethod]
        public async Task Http_error_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.NotFound, "not found");

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Http, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.AreEqual("not found", response.Error); 
        }

        [TestMethod]
        public async Task Setting_basic_authentication_style_should_send_basic_authentication_header()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "success_token_response.json"));
            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new TokenClient(
                Endpoint,
                "clientid",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");
            var request = handler.Request;

            Assert.IsNotNull(request.Headers.Authorization);
            Assert.AreEqual("Basic",request.Headers.Authorization.Scheme.ToString());
            Assert.AreEqual(Convert.ToBase64String(Encoding.UTF8.GetBytes("clientid:clientsecret")), request.Headers.Authorization.Parameter);
        }

        [TestMethod]
        public async Task Setting_authentication_style_to_basic_explicitly_should_send_header()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "success_token_response.json"));

            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new TokenClient(
                Endpoint,
                innerHttpMessageHandler: handler);

            client.ClientId = "clientid";
            client.ClientSecret = "clientsecret";
            client.AuthenticationStyle = AuthenticationStyle.OAuth2;

            var response = await client.RequestTokenFromCodeAsync("123", "https://server/callback");

            var request = handler.Request;

            Assert.IsNotNull(request.Headers.Authorization);
            Assert.AreEqual("Basic", request.Headers.Authorization.Scheme.ToString());
            Assert.AreEqual(Convert.ToBase64String(Encoding.UTF8.GetBytes("clientid:clientsecret")), request.Headers.Authorization.Parameter);
        }
    }
}