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

namespace Intuit.Ipp.Test
{
    public class Initializer
    {
       
        static OAuth2Client oauthClient = null;
        public static Dictionary<string, string> tokenDict = new Dictionary<string, string>();
       
        IConfigurationRoot builder;
        public Initializer(string path)
        {
            IppConfiguration ippConfig = new IppConfiguration();
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();


            tokenDict.Add("accessTokenQBO", builder.GetSection("Oauth2Keys")["AccessToken"]);
            tokenDict.Add("refreshTokenQBO", builder.GetSection("Oauth2Keys")["RefreshToken"]);
            tokenDict.Add("clientIdQBO", builder.GetSection("Oauth2Keys")["ClientId"]);
            tokenDict.Add("clientSecretQBO", builder.GetSection("Oauth2Keys")["ClientSecret"]);
            tokenDict.Add("realmIdIAQBO", builder.GetSection("Oauth2Keys")["RealmId"]);
            tokenDict.Add("redirectUrl", builder.GetSection("Oauth2Keys")["RedirectUrl"]);
            tokenDict.Add("qboBaseUrl", builder.GetSection("Oauth2Keys")["QBOBaseUrl"])
                

        }
        public Initializer() : this("Appsettings.json")
        {
        }





        //private static void Initialize()
        //{

        //    //Oauth1 tokens
        //    //accessTokenQBO = ConfigurationManager.AppSettings["accessTokenQBO"];
        //    //refreshTokenQBO  = ConfigurationManager.AppSettings["refreshTokenQBO"];
        //    //tokenDict.Add("accessToken", accessTokenQBO);
        //    //tokenDict.Add("refreshToken", refreshTokenQBO);
        //    //////Ouath2 tokens
        //    //accessTokenQBO = ConfigurationManager.AppSettings["accessTokenQBO"];
        //    //refreshTokenQBO = ConfigurationManager.AppSettings["refreshTokenQBO"];
        //    //realmIdIAQBO = ConfigurationManager.AppSettings["realmIdIAQBO"];
        //    //clientIdQBO = ConfigurationManager.AppSettings["clientIdKeyQBO"];
        //    //clientSecretQBO = ConfigurationManager.AppSettings["clientSecretQBO"];



        //}


        internal static ServiceContext InitializeQBOServiceContextUsingoAuth()
        {
            //ValidateToken();
            //Initialize();
            ////Oauth1 validator
            // OAuthRequestValidator reqValidator = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, consumerKeySecretQBO);
            try
            {
                ////Oauth2 validator
                OAuth2RequestValidator reqValidator = new OAuth2RequestValidator(tokenDict["accessTokenQBO"]);
                ServiceContext context = new ServiceContext(tokenDict["realmIdIAQBO"], IntuitServicesType.QBO, reqValidator);
                context.IppConfiguration.MinorVersion.Qbo = "37";
                DataService service = new DataService(context);
                //Add a dataservice call to check 401
                return context;
            }
            catch (IdsException ex)
            {
                if (ex.Message == "Unauthorized-401")
                {
                    var tokenResp = oauthClient.RefreshTokenAsync(tokenDict["refreshTokenQBO"]).Result;
                    if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
                    {
                        tokenDict["accessTokenQBO"] = tokenResp.AccessToken;
                        tokenDict["refreshTokenQBO"] = tokenResp.RefreshToken;
                        //?=InitializeQBOServiceContextUsingoAuth();
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

        //internal static string ValidateToken()
        //{
        //    try
        //    {
        //        if (!tokenDict.ContainsKey("accessToken"))
        //        {
        //            OAuth2RequestValidator oAuthValidator = new OAuth2RequestValidator(tokenDict["accessToken"]);
        //            DataService service = new DataService(context);

        //        }
        //    }
        //    catch (IdsException ex)
        //    {
        //        if (ex.Message == "Unauthorized-401")
        //        {
        //            var tokenResp = oauthClient.RefreshTokenAsync(tokenDict["refreshToken"]).Result;
        //            if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
        //            {
        //                tokenDict["accessToken"] = tokenResp.AccessToken;
        //                tokenDict["refreshToken"] = tokenResp.RefreshToken;
        //                ValidateToken();
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
        //    return tokenDict["accessToken"];
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
