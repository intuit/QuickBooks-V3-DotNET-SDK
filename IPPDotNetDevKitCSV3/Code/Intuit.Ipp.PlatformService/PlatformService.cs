////*********************************************************
// <copyright file="PlatformService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Intuit Platform services.</summary>
////*********************************************************
namespace Intuit.Ipp.PlatformService
{
    using System;
    using System.Xml;
    using System.Text;
    using  System.Collections.Generic;
    using Intuit.Ipp.PlatformService;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;

    /// <summary>
    /// PlatformService class
    /// </summary>
    public class PlatformService
    {
        /// <summary>
        /// Disconnect a user from QBO.
        /// </summary>

        /// <param name="consumerKey">OAuth consumerKey.</param>
        /// <param name="consumerSecret">OAuth consumerSecret.</param>
        /// <param name="accessToken">OAuth accessToken.</param>
        /// <param name="accessTokenSecret">OAuth accessTokenSecret.</param>
        /// <returns>void.</returns>
        public static void Disconnect(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
         {
             PlatformClient client = new PlatformClient(PlatformConfig.GetDisconnectUrl());
             string response = client.GetResponse(consumerKey, consumerSecret, accessToken, accessTokenSecret);
             ParseDisconnectResponse(response);
        }

        /// <summary>
        /// Parse DisconnectResponse
        /// </summary>
        /// <param name="response">Disconnect Response string</param>        
        /// <returns>void.</returns>
        public static void ParseDisconnectResponse(string response)
        {
            XmlDocument xmldoc = CoreHelper.ParseResponseIntoXml(response);

            var nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("api", "http://platform.intuit.com/api/v1");

            XmlNode node = xmldoc.SelectSingleNode("//api:ErrorCode", nsmgr);
            if (node != null && (!node.InnerText.Equals("0")))
            {
                throw GetPlatformException(xmldoc, nsmgr);
            }
        }
        /*
        public static void ParseReconnectResponse(string response)
        {
            XmlDocument xmldoc = CoreHelper.ParseResponseIntoXml(response);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("api", "http://platform.intuit.com/api/v1");

            XmlNode node = xmldoc.SelectSingleNode("//api:ErrorCode", nsmgr);
            if (node != null && (!node.InnerText.Equals("0")))
            {
                string errCode = node.InnerText;
                node = xmldoc.SelectSingleNode("//api:ErrorMessage", nsmgr);
                string errMsg = node.InnerText;
                node = xmldoc.SelectSingleNode("//api:ServerTime", nsmgr);
                string serverTime = node.InnerText;
                PlatformException execption = new PlatformException(errCode, errMsg, serverTime);
                throw execption;
            }
        }*/


        /// <summary>
        /// GetPlatformException
        /// </summary>  
        /// <param name="xmldoc">xmldoc</param>  
        /// <param name="nsmgr">nameSpaceManager</param>  
        /// <returns>PlatformException</returns>
        private static PlatformException GetPlatformException(XmlDocument xmldoc, XmlNamespaceManager nsmgr)
        {
            XmlNode node = xmldoc.SelectSingleNode("//api:ErrorCode", nsmgr);
            
                string errCode = node.InnerText;
                node = xmldoc.SelectSingleNode("//api:ErrorMessage", nsmgr);
                string errMsg = node.InnerText;
                node = xmldoc.SelectSingleNode("//api:ServerTime", nsmgr);
                string serverTime = node.InnerText;
                PlatformException execption = new PlatformException(errCode, errMsg, serverTime);
                return execption;
            
        }

        /// <summary>
        /// Reconnect
        /// </summary>  
        /// <param name="xmldoc">xmldoc</param>  
        /// <param name="nsmgr">nameSpaceManager</param>  
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> Reconnect(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
         {
             PlatformClient client = new PlatformClient(PlatformConfig.GetReconnectUrl());
             string response = client.GetResponse(consumerKey, consumerSecret, accessToken, accessTokenSecret);

             XmlDocument xmldoc = CoreHelper.ParseResponseIntoXml(response);
             XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
             nsmgr.AddNamespace("api", "http://platform.intuit.com/api/v1");

             XmlNode node = xmldoc.SelectSingleNode("//api:ErrorCode", nsmgr);
             if (node != null && (!node.InnerText.Equals("0")))
             {

                 throw GetPlatformException( xmldoc, nsmgr);
             }
             else
             {
                 Dictionary<string, string> oauthTokens = new Dictionary<string,string>();
                 
                 oauthTokens.Add( "OAuthToken", xmldoc.SelectSingleNode( "//api:OAuthToken",nsmgr).InnerText);
                 oauthTokens.Add("OAuthTokenSecret", xmldoc.SelectSingleNode("//api:OAuthTokenSecret",nsmgr).InnerText);
                 return oauthTokens;
             }
        }

        /// <summary>
        /// GetCurrentUser
        /// </summary>  
        /// <param name="consumerKey">OAuth consumerKey.</param>
        /// <param name="consumerSecret">OAuth consumerSecret.</param>
        /// <param name="accessToken">OAuth accessToken.</param>
        /// <param name="accessTokenSecret">OAuth accessTokenSecret.</param>
        /// <returns>Dictionary</returns>
        public static User GetCurrentUser(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
         {
            PlatformClient client = new PlatformClient(PlatformConfig.GetCurrentUserUrl());
             string response = client.GetResponse(consumerKey, consumerSecret, accessToken, accessTokenSecret);

             XmlDocument xmldoc = CoreHelper.ParseResponseIntoXml(response);
             XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
             nsmgr.AddNamespace("api", "http://platform.intuit.com/api/v1");

             XmlNode node = xmldoc.SelectSingleNode("//api:UserResponse", nsmgr);
             if (node == null)
             {
                 
                 throw GetPlatformException( xmldoc, nsmgr);
             }
             else
             {
                 User user = new User();
                 user.FirstName = node.SelectSingleNode("//api:FirstName",nsmgr).InnerText;
                 user.LastName = node.SelectSingleNode("//api:LastName",nsmgr).InnerText;
                 user.EmailAddress = node.SelectSingleNode("//api:EmailAddress",nsmgr).InnerText;
                 user.IsVerified = node.SelectSingleNode("//api:IsVerified",nsmgr).InnerText.Equals("true") ? true : false;
                 return user;
             }
        }
    }

}