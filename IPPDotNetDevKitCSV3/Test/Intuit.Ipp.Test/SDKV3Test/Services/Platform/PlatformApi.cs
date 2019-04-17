using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Threading;
using Intuit.Ipp.QueryFilter;

using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.Platform
{
	[TestClass][Ignore]
	public class PlatformApi
	{
		ServiceContext qboContextOAuth = null;
        string ValidAccessToken = "qyprdjjV5yb5YlzBGBo3fOesZ8WG3POL83PFg77UFkrVRat6";
        string ValidAccessTokenSecret = "BhL29aQD7k1HyJyFXWP8HrDzBNnVa8YtuZsxtX4l";
        string ValidConsumerKey = "qyprd68jMGebyWgiNFT411r4KnhmB9";
        string ValidConsumerSecret = "j2QGgFRv5yYMXB5lVXUI0NBPlHAmz0drjANG1KeX";

        string InvalidAccessToken = "lvprdS0vcMp3lHlNirwpsWrsHnmjh0pLbDpK6WCXGtySTx61";
        string InvalidAccessTokenSecret = "wAhzfEhOCjtWBoza2xAPG2Ey1BV599lLd1y92ytl";

        string realmIAQBO = "123145693359857";


		[TestInitialize]
		public void MyTestInitializer()
		{
            // OAuthRequestValidator oAuthRequestValidatorQbo = new OAuthRequestValidator(ValidAccessToken, ValidAccessTokenSecret,ValidConsumerKey, ValidConsumerSecret);
            OAuth2RequestValidator oAuthRequestValidatorQbo = new OAuth2RequestValidator(ValidAccessToken);
            qboContextOAuth = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, oAuthRequestValidatorQbo);
        }




        #region PlatformDisconnect

		public void PlatformDisconnectValidOauth(String oauthtoken,String oauthtokensecret)
		{
           //try
           //{
           //     PlatformService.PlatformService.Disconnect(ValidConsumerKey, ValidConsumerSecret,
           //          oauthtoken, oauthtokensecret);
           //}
           //catch (PlatformException pex)
           //{
           //    Console.WriteLine("PlatformDisconnect throw PlatformException errCode:" + pex.ErrorCode + " errMsg:" + pex.ErrorMessage + " serverTime:" + pex.ServerTime);
           //     Assert.Fail();
           //}

            //OAuthRequestValidator ioAuthRequestValidatorQbo = new OAuthRequestValidator(oauthtoken, oauthtokensecret, ValidConsumerKey, ValidConsumerSecret);
            OAuth2RequestValidator ioAuthRequestValidatorQbo = new OAuth2RequestValidator(ValidAccessToken);
            ServiceContext iqboContextOAuth = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, ioAuthRequestValidatorQbo);
           Customer customer = QBOHelper.CreateCustomer(iqboContextOAuth);
           try
           {
               Customer added = Helper.Add<Customer>(iqboContextOAuth, customer);
           }
           catch (InvalidTokenException e)
           {
               Assert.AreEqual("Unauthorized-401", e.Message);
               return;
           }
           Assert.Fail();
        }


        [TestMethod]
		public void PlatformDisconnectInvalidOauth()
		{
            //try
            //{
            //    PlatformService.PlatformService.Disconnect(ValidConsumerKey, ValidConsumerSecret,
            //        InvalidAccessToken, InvalidAccessTokenSecret);
            //}
            //catch (PlatformException pex)
            //{
            //    Assert.AreEqual("270", pex.ErrorCode);
            //    Assert.AreEqual("OAuth Token rejected", pex.ErrorMessage);
            //    Assert.IsNotNull(pex.ServerTime);
            //    return;
            //}
            Assert.Fail();
		}

        #endregion

        #region Reconnect

        //Steps to enable this test
        // 1. Go to https://appcenter.intuit.com/Playground/OAuth/IA/ generate Access token which expiration date without 30 dats from current date
        // 2. Remove Ignore
        [TestMethod]
        [Ignore]
        public void PlatformReconnectValidOauth()
        {
            //try
            //{
            //    String oauthtoken = null; ;
            //    String oauthtokensecret = null;

            //    Dictionary<string, string> oauthTokens = PlatformService.PlatformService.Reconnect(ValidConsumerKey, ValidConsumerSecret,
            //         "lvprdkyvvpOSwdI8ufHlYb1IpWp8RpjAv8lZ7KK0H9jiVsFo", "gQBx9lR3F4Iwm42ir3n3zxIM75KxI7wuiC5o7oKr");
            //    if (oauthTokens.ContainsKey("OAuthToken"))
            //    {
            //        oauthtoken = oauthTokens["OAuthToken"];
            //        Assert.IsNotNull(oauthtoken);
            //    }

            //    // See whether Dictionary contains this string.
            //    if (oauthTokens.ContainsKey("OAuthTokenSecret"))
            //    {
            //        oauthtokensecret = oauthTokens["OAuthTokenSecret"];
            //        Assert.IsNotNull(oauthtokensecret);
            //    }
            //    PlatformDisconnectValidOauth(oauthtoken,oauthtokensecret);
            //}
            //catch (PlatformException pex)
            //{
            //    Console.WriteLine("PlatformReconnect throw exception. Error Code" +pex.ErrorCode + ", Error Message:"+pex.ErrorMessage);
            //    Assert.Fail();
            //}

        }

        //Steps To enable this test
        // 1. Generate a New Access Token which expiration date more than 30 days from current date
        // 2. Remove Ignore
        [TestMethod]
        [Ignore]
        public void PlatformReconnectValidOauth212()
        {
            //try
            //{

            //    Dictionary<string, string> oauthTokens = PlatformService.PlatformService.Reconnect(ValidConsumerKey, ValidConsumerSecret,
            //         "lvprdYXHs7Xc95g70D5UFX9mWSShDhepkCWvr95tb19SUIPD", "zdy5xPim4viFTNuBTu0c2IqJCRhJUSXTuNr3fXoR");
            //}
            //catch (PlatformException pex)
            //{
            //    Assert.AreEqual("212", pex.ErrorCode);
            //    Assert.AreEqual("Token Refresh Window Out of Bounds", pex.ErrorMessage);
            //    Assert.IsNotNull(pex.ServerTime);
            //    return;
            //}
            Assert.Fail();
           
        }

        [TestMethod]
        public void PlatformReconnectInvalidOauth270()
        {
            //try
            //{
            //    PlatformService.PlatformService.Reconnect(ValidConsumerKey, ValidConsumerSecret,
            //        InvalidAccessToken, InvalidAccessTokenSecret);
            //}
            //catch (PlatformException pex)
            //{
            //    Assert.AreEqual("270", pex.ErrorCode);
            //    Assert.AreEqual("OAuth Token rejected", pex.ErrorMessage);
            //    Assert.IsNotNull(pex.ServerTime);
            //    return;
            //}
            Assert.Fail();
        }


		#endregion

        #region getCurrentUser

        [TestMethod]
        [Ignore]
        public void PlatformGetCurrentUserWithValidOauth()
        {
            //Intuit.Ipp.PlatformService.User myuser=PlatformService.PlatformService.GetCurrentUser(ValidConsumerKey, ValidConsumerSecret,
            //        ValidAccessToken,ValidAccessTokenSecret);
            //Assert.AreEqual("yelena", myuser.FirstName);
            //Assert.AreEqual("gartsman", myuser.LastName);
            //Assert.AreEqual("yelenastage@intuit.com", myuser.EmailAddress);
            //Assert.IsTrue(myuser.IsVerified);
        }

        [TestMethod]
        public void PlatformGetCurrentUserWithInvalidOauth()
        {
            //try
            //{
            //    Intuit.Ipp.PlatformService.User myuser = PlatformService.PlatformService.GetCurrentUser(ValidConsumerKey, ValidConsumerSecret,
            //            InvalidAccessToken, InvalidAccessTokenSecret);
            //}
            //catch (PlatformException pex)
            //{
            //    Assert.AreEqual("22", pex.ErrorCode);
            //    Assert.AreEqual("This API requires Authorization.", pex.ErrorMessage);
            //    Assert.IsNotNull(pex.ServerTime);
            //    return;
            //}
            Assert.Fail();
        }

        //Change Intuit.Ipp.PlatformService.PlatformConfig.currentUserUrl URL and run this test
        [Ignore]
        [TestMethod]
        public void PlatformGetCurrentUserWithConenctionError()
        {
            //try
            //{
            //    Intuit.Ipp.PlatformService.User myuser = PlatformService.PlatformService.GetCurrentUser(ValidConsumerKey, ValidConsumerSecret,
            //        ValidAccessToken, ValidAccessTokenSecret);
            //}
            //catch (PlatformException pex)
            //{
            //    Console.Write(pex.ErrorCode + pex.ErrorMessage);
            //    Assert.AreEqual("ConnectFailure", pex.ErrorCode);
            //    Assert.AreEqual("Unable to connect to the remote server", pex.ErrorMessage);
            //    return;
            //}
            Assert.Fail();
        }



        #endregion

     

    }
}
