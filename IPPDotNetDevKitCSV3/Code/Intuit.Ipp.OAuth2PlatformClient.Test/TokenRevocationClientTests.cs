// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Intuit.Ipp.OAuth2PlatformClient;


namespace Intuit.Ipp.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class TokenRevocationClientTests
    {
        const string Endpoint = "https://server/endoint";

        [TestMethod]
        public async Task Valid_protocol_response_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.OK, "ok");

            var client = new TokenRevocationClient(
                Endpoint,
                "client",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RevokeAccessTokenAsync("token");

            Assert.AreEqual(false,response.IsError);
            Assert.AreEqual(ResponseErrorType.None, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);
        }

        [TestMethod]
        public async Task Valid_protocol_error_should_be_handled_correctly()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "failure_token_revocation_response.json"));
            var handler = new NetworkHandler(document, HttpStatusCode.BadRequest);

            var client = new TokenRevocationClient(
                 Endpoint,
                 "client",
                 "clientsecret",
                 innerHttpMessageHandler: handler);

            var response = await client.RevokeAccessTokenAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Protocol, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.HttpStatusCode);
            Assert.AreEqual("error", response.Error); 
        }

        [TestMethod]
        public async Task Malformed_response_document_should_be_handled_correctly()
        {
            var document = "invalid";
            var handler = new NetworkHandler(document, HttpStatusCode.BadRequest);

            var client = new TokenRevocationClient(
                Endpoint,
                "client",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RevokeAccessTokenAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Exception, response.ErrorType);
            Assert.AreEqual("invalid", response.Raw);
            Assert.IsNotNull(response.Exception);
        }

        [TestMethod]
        public async Task Exception_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(new Exception("exception"));

            var client = new TokenRevocationClient(
                Endpoint,
                "client",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RevokeAccessTokenAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Exception, response.ErrorType);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual("exception", response.Error);
        }

        [TestMethod]
        public async Task Http_error_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.NotFound, "not found");

            var client = new TokenRevocationClient(
                Endpoint,
                "client",
                "clientsecret",
                innerHttpMessageHandler: handler);

            var response = await client.RevokeAccessTokenAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Http, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.AreEqual("not found", response.Error);
        }
    }
}
