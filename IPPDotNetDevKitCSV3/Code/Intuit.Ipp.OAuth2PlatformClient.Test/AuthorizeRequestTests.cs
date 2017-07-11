// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.OAuth2PlatformClient;


namespace Intuit.Ipp.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class AuthorizeRequestTests
    {
        [TestMethod]
        public void Create_absolute_url_should_behave_as_expected()
        {
            var request = new AuthorizeRequest("http://server/authorize");

            var parmeters = new
            {
                foo = "foo",
                bar = "bar"
            };

            var url = request.Create(parmeters);
            Assert.AreEqual("http://server/authorize?foo=foo&bar=bar", url);
            
        }

        [TestMethod]
        public void Create_relative_url_should_behave_as_expected()
        {
            var request = new AuthorizeRequest(new Uri("/authorize", UriKind.Relative));

            var parmeters = new
            {
                foo = "foo",
                bar = "bar"
            };

            var url = request.Create(parmeters);
            Assert.AreEqual("/authorize?foo=foo&bar=bar", url);
         
        }
    }
}