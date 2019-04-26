using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Security;
using Intuit.Ipp.Core;
using Intuit.Ipp.DataService;

using System.Net;
using System.Globalization;
using System.IO;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Data;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;
using Intuit.Ipp.Core.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intuit.Ipp.Test
{
    public class Initializer
    {

        static OAuth2Client oauthClient = null;
        public static Dictionary<string, string> tokenDict = new Dictionary<string, string>();
        public static string pathFile = "";
        static int counter = 0;

        static IConfigurationRoot builder;
        public Initializer(string path)
        {
            AuthorizationKeysQBO.tokenFilePath = path;
            builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(path, optional: true, reloadOnChange: true)
                 .Build();


            AuthorizationKeysQBO.accessTokenQBO= builder.GetSection("Oauth2Keys")["AccessToken"];

            AuthorizationKeysQBO.refreshTokenQBO = builder.GetSection("Oauth2Keys")["RefreshToken"];
            AuthorizationKeysQBO.clientIdQBO = builder.GetSection("Oauth2Keys")["ClientId"];
            AuthorizationKeysQBO.clientSecretQBO = builder.GetSection("Oauth2Keys")["ClientSecret"];
            AuthorizationKeysQBO.realmIdIAQBO = builder.GetSection("Oauth2Keys")["RealmId"];
            AuthorizationKeysQBO.redirectUrl = builder.GetSection("Oauth2Keys")["RedirectUrl"];
            AuthorizationKeysQBO.qboBaseUrl = builder.GetSection("Oauth2Keys")["QBOBaseUrl"];
            AuthorizationKeysQBO.appEnvironment = builder.GetSection("Oauth2Keys")["Environment"];

            counter++;


        }
        public Initializer() : this("Appsettings.json")
        {

        }


        static Initializer()
        { }


        private static void Initialize()
        {
            Initializer initializer = new Initializer();
        }


        internal static ServiceContext InitializeQBOServiceContextUsingoAuth()
        {
            if (counter==0)
                //if(tokenDict.Count == 0)
                Initialize();
            else
            {
                //Load the second json file
                string jsonFile = File.ReadAllText(AuthorizationKeysQBO.tokenFilePath);
                var jObj = JObject.Parse(jsonFile);
                AuthorizationKeysQBO.accessTokenQBO= jObj["Oauth2Keys"]["AccessToken"].ToString();
                AuthorizationKeysQBO.refreshTokenQBO = jObj["Oauth2Keys"]["RefreshToken"].ToString();
            }

            ServiceContext context = null;
            OAuth2RequestValidator reqValidator = null;
            //ValidateToken();
            //Initialize();
            ////Oauth1 validator
            // OAuthRequestValidator reqValidator = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, consumerKeySecretQBO);
            try
            {

                reqValidator = new OAuth2RequestValidator(AuthorizationKeysQBO.accessTokenQBO);
                context = new ServiceContext(AuthorizationKeysQBO.realmIdIAQBO, IntuitServicesType.QBO, reqValidator);
                context.IppConfiguration.MinorVersion.Qbo = "37";
                DataService.DataService service = new DataService.DataService(context);
                service.FindAll<Customer>(new Customer());

                //Add a dataservice call to check 401

                return context;
            }
            catch (IdsException ex)
            {
                if (ex.Message == "Unauthorized-401")
                {
                    oauthClient = new OAuth2Client(AuthorizationKeysQBO.clientIdQBO, AuthorizationKeysQBO.clientSecretQBO, AuthorizationKeysQBO.redirectUrl, AuthorizationKeysQBO.appEnvironment);
                    var tokenResp = oauthClient.RefreshTokenAsync(AuthorizationKeysQBO.refreshTokenQBO).Result;
                    if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
                    {
                        string jsonFile = File.ReadAllText(pathFile);
                        var jObj = JObject.Parse(jsonFile);
                        jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                        jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
                        //tokenDict["accessTokenQBO"] = jObj["Oauth2Keys"]["AccessToken"].ToString();
                        //tokenDict["refreshTokenQBO"] = jObj["Oauth2Keys"]["RefreshToken"].ToString();
                        string output = JsonConvert.SerializeObject(jObj, Formatting.Indented);
                        File.WriteAllText(pathFile, output);
                        //tokenDict.Clear();
                        InitializeQBOServiceContextUsingoAuth();
                        return context;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }

            }

        }

        //internal static ServiceContext InitializeQueryServiceContextUsingoAuth(bool isQBO)
        //{
        //    ValidateToken();
        //    Initialize();

        //    if (isQBO)
        //    {


        //        ////Oauth2 validator
        //        OAuth2RequestValidator reqValidator = new OAuth2RequestValidator(tokenDict["accessTokenQBO"]);
        //        ServiceContext context = new ServiceContext(tokenDict["realmIdIAQBO"], IntuitServicesType.QBO, reqValidator);
        //        context.IppConfiguration.MinorVersion.Qbo = "37";
        //        return context;

        //    }


        //}

        //internal static string ValidateToken(ServiceContext context=null)
        //{
        //    try
        //    {
        //        if (!tokenDict.ContainsKey("accessTokenQBO"))
        //        {
        //            OAuth2RequestValidator oAuthValidator = new OAuth2RequestValidator(tokenDict["accessTokenQBO"]);
        //            DataService.DataService service = new DataService.DataService(context);

        //        }
        //    }
        //    catch (IdsException ex)
        //    {
        //        if (ex.Message == "Unauthorized-401")
        //        {
        //            var tokenResp = oauthClient.RefreshTokenAsync(tokenDict["refreshTokenQBO"]).Result;
        //            if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
        //            {
        //                tokenDict["accessTokenQBO"] = tokenResp.AccessToken;
        //                tokenDict["refreshTokenQBO"] = tokenResp.RefreshToken;
        //                ValidateToken(context);
        //            }
        //            else
        //            {
        //                return null;
        //            }

        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    return tokenDict["accessTokenQBO"];
        //}


        internal static Customer CreateCustomer1()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust";
            newCustomer.FamilyName = "Cust";
            newCustomer.GivenName = "Cust";
            newCustomer.Balance = 1000;
            return newCustomer;
        }

        internal static Customer CreateCustomer2()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust";
            newCustomer.FamilyName = "Cust";
            newCustomer.GivenName = "Cust";
            newCustomer.Balance = 900;
            return newCustomer;
        }

        internal static Customer CreateCustomer3()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust";
            newCustomer.FamilyName = "Cust";
            newCustomer.GivenName = "Cust";
            newCustomer.Balance = 2000;
            return newCustomer;
        }

        internal static Customer CreateCustomer4()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust";
            newCustomer.FamilyName = "Cust";
            newCustomer.GivenName = "Custest";
            newCustomer.Balance = 2000;
            return newCustomer;
        }

        internal static Customer CreateCustomer5()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust111";
            newCustomer.FamilyName = "Cust";
            newCustomer.GivenName = "Custest";
            newCustomer.Balance = 2000;
            newCustomer.Active = true;
            return newCustomer;

        }

        internal static Customer CreateCustomer6()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust111";
            newCustomer.FamilyName = "A";
            newCustomer.GivenName = "Custest";
            newCustomer.Balance = 2000;
            newCustomer.Active = true;
            return newCustomer;

        }

        internal static Customer CreateCustomer7()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust111";
            newCustomer.FamilyName = "B";
            newCustomer.GivenName = "Custest";
            newCustomer.Balance = 2000;
            newCustomer.Active = true;
            return newCustomer;

        }

        internal static Customer CreateCustomer8()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Cust Cust Cust";
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "Cust111";
            newCustomer.FamilyName = "C";
            newCustomer.GivenName = "Custest";
            newCustomer.Balance = 2000;
            newCustomer.Active = true;
            return newCustomer;

        }

        internal static Customer AddCustomerHelper(ServiceContext context, Customer customer)
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            //Adding the Customer using Dataservice object
            Customer addedCustomer = service.Add<Customer>(customer);



            return addedCustomer;
        }




    }
}
