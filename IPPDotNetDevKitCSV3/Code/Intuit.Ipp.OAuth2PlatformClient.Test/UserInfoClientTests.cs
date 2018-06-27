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
    public class UserInfoClientTests
    {
        const string Endpoint = "https://server/endpoint";

        [TestMethod]
        public async Task Valid_protocol_response_should_be_handled_correctly()
        {
            var binDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(binDir);
            DirectoryInfo dir = fileInfo.Directory.Parent.Parent;

            var document = File.ReadAllText(Path.Combine(dir.FullName, "Intuit.Ipp.OAuth2PlatformClient.Test\\Documents", "success_userinfo_response.json"));
            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new UserInfoClient(
                Endpoint,
                innerHttpMessageHandler: handler);

            var response = await client.GetAsync("token");


            Assert.AreEqual(false, response.IsError);
            Assert.AreEqual(ResponseErrorType.None, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.IsNotNull(response.Claims);

   
        }

     

        [TestMethod]
        public async Task Malformed_response_document_should_be_handled_correctly()
        {
            var document = "invalid";
            var handler = new NetworkHandler(document, HttpStatusCode.OK);

            var client = new UserInfoClient(
                Endpoint,
                innerHttpMessageHandler: handler);

            var response = await client.GetAsync("token");


            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Exception, response.ErrorType);
            Assert.AreEqual("invalid", response.Raw);
            Assert.IsNotNull(response.Exception);

 
        }

        [TestMethod]
        public async Task Exception_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(new Exception("exception"));

            var client = new UserInfoClient(
                Endpoint,
                innerHttpMessageHandler: handler);

            var response = await client.GetAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Exception, response.ErrorType);
            Assert.AreEqual("exception", response.Error);
            Assert.IsNotNull(response.Exception);


        }

        [TestMethod]
        public async Task Http_error_should_be_handled_correctly()
        {
            var handler = new NetworkHandler(HttpStatusCode.NotFound, "not found");

            var client = new UserInfoClient(
                Endpoint,
                innerHttpMessageHandler: handler);

            var response = await client.GetAsync("token");

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual(ResponseErrorType.Http, response.ErrorType);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.IsNotNull("not found",response.Error);
        
        }
    }
}
