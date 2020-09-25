// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Intuit.Ipp.OAuth2PlatformClient.UnitTests
{
    [TestClass]
    public class AuthorizeResponseTests
    {
        [TestMethod]
        public void Error_Response_with_QueryString()
        {
            var url = "https://server/callback?error=foo";

            var response = new AuthorizeResponse(url);
            Assert.AreEqual(true,response.IsError);
            Assert.AreEqual("foo", response.Error);
        }

        [TestMethod]
        public void Error_Response_with_HashFragment()
        {
            var url = "https://server/callback#error=foo";

            var response = new AuthorizeResponse(url);

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual("foo", response.Error);
        }

        [TestMethod]
        public void Error_Response_with_QueryString_and_HashFragment()
        {
            var url = "https://server/callback?error=foo#_=_";

            var response = new AuthorizeResponse(url);

            Assert.AreEqual(true, response.IsError);
            Assert.AreEqual("foo", response.Error);
        }

        [TestMethod] 
        public void Code_Response_with_QueryString()
        {
            var url = "https://server/callback?state=security_token=138r5719ru3e1&url=https://server/callback&code=foo";

            var response = new AuthorizeResponse(url);


            Assert.AreEqual(false, response.IsError);
            Assert.AreEqual("foo", response.Code);

            Assert.AreEqual("security_token=138r5719ru3e1", response.Values["state"].ToString());
            Assert.AreEqual("security_token=138r5719ru3e1", response.TryGet("state").ToString());
           
        }


        //[TestMethod]
        //public void form_post_format_should_parse()
        //{
        //    var form = "id_token=foo&code=bar&scope=baz&session_state=quux";
        //    var response = new AuthorizeResponse(form);

        //    response.IsError.Should().BeFalse();
        //    response.IdentityToken.Should().Be("foo");
        //    response.Code.Should().Be("bar");
        //    response.Scope.Should().Be("baz");
        //    response.Values["session_state"].Should().Be("quux");
        //}
    }
}