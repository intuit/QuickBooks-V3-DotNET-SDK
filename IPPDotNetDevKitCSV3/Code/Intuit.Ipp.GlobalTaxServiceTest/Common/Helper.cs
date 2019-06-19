using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Intuit.Ipp.Core;
using Intuit.Ipp.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using Intuit.Ipp.DataService;
using Newtonsoft.Json;
using Intuit.Ipp.OAuth2PlatformClient;
using Newtonsoft.Json.Linq;
//using Intuit.Ipp.LinqExtender;


namespace Intuit.Ipp.GlobalTaxService.Test.Common
{
    /// <summary>
    /// Summary description for Helper.
    /// </summary>

    public class Helper
    {
        internal static ServiceContext GetNewTokens_ServiceContext()
        {


            FileInfo fileinfo = new FileInfo(AuthorizationKeysQBO.tokenFilePath);
            string jsonFile = File.ReadAllText(fileinfo.FullName);
            var jObj = JObject.Parse(jsonFile);

            var oauth2Client = new OAuth2Client(AuthorizationKeysQBO.clientIdQBO, AuthorizationKeysQBO.clientSecretQBO, AuthorizationKeysQBO.redirectUrl, AuthorizationKeysQBO.appEnvironment);
            try
            {
                var tokenResp = oauth2Client.RefreshTokenAsync(AuthorizationKeysQBO.refreshTokenQBO).Result;
                jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
            }
            catch (IdsException ex)
            {

                if (jObj["Oauth2Keys"]["RefreshToken"] != null)
                {
                    var tokenResp = oauth2Client.RefreshTokenAsync(jObj["Oauth2Keys"]["RefreshToken"].ToString()).Result;
                    jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                    jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
                }
                else
                {
                    throw;
                }
            }


            string output = JsonConvert.SerializeObject(jObj, Formatting.Indented);
            File.WriteAllText(fileinfo.FullName, output);
            //tokenDict.Clear();
            var serviceContext = Initializer.InitializeServiceContextQbo();
            return serviceContext;



        }

       
    }
}
