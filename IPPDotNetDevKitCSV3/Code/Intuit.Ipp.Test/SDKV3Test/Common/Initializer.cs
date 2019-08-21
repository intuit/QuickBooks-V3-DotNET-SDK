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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //AuthorizationKeysQBO.tokenFilePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))), "TokenStore.json");

            AuthorizationKeysQBO.tokenFilePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TokenStore.json");

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(path, optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

           
            AuthorizationKeysQBO.accessTokenQBO= builder.GetSection("Oauth2Keys")["AccessToken"];

            AuthorizationKeysQBO.refreshTokenQBO = builder.GetSection("Oauth2Keys")["RefreshToken"];
            AuthorizationKeysQBO.clientIdQBO = builder.GetSection("Oauth2Keys")["ClientId"];
            AuthorizationKeysQBO.clientSecretQBO = builder.GetSection("Oauth2Keys")["ClientSecret"];
            AuthorizationKeysQBO.realmIdIAQBO = builder.GetSection("Oauth2Keys")["RealmId"];
            AuthorizationKeysQBO.redirectUrl = builder.GetSection("Oauth2Keys")["RedirectUrl"];
            AuthorizationKeysQBO.qboBaseUrl = builder.GetSection("Oauth2Keys")["QBOBaseUrl"];
            AuthorizationKeysQBO.appEnvironment = builder.GetSection("Oauth2Keys")["Environment"];
            FileInfo fileinfo = new FileInfo(AuthorizationKeysQBO.tokenFilePath);
            string jsonFile = File.ReadAllText(fileinfo.FullName);
            var jObj = JObject.Parse(jsonFile);
            jObj["Oauth2Keys"]["AccessToken"] = AuthorizationKeysQBO.accessTokenQBO;
            jObj["Oauth2Keys"]["RefreshToken"] = AuthorizationKeysQBO.refreshTokenQBO;

            string output = JsonConvert.SerializeObject(jObj, Formatting.Indented);
            File.WriteAllText(fileinfo.FullName, output);

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
                FileInfo fileinfo = new FileInfo(AuthorizationKeysQBO.tokenFilePath);
                string jsonFile = File.ReadAllText(fileinfo.FullName);
                var jObj = JObject.Parse(jsonFile);
                AuthorizationKeysQBO.accessTokenQBO= jObj["Oauth2Keys"]["AccessToken"].ToString();
                AuthorizationKeysQBO.refreshTokenQBO = jObj["Oauth2Keys"]["RefreshToken"].ToString();
            }

            ServiceContext context = null;
            OAuth2RequestValidator reqValidator = null;
            try
            {

                reqValidator = new OAuth2RequestValidator(AuthorizationKeysQBO.accessTokenQBO);
                context = new ServiceContext(AuthorizationKeysQBO.realmIdIAQBO, IntuitServicesType.QBO, reqValidator);
                context.IppConfiguration.MinorVersion.Qbo = "37";
                DataService.DataService service = new DataService.DataService(context);
                var compinfo= service.FindAll<CompanyInfo>(new CompanyInfo());

                //Add a dataservice call to check 401

                return context;
            }
            catch (IdsException ex)
            {
                if (ex.Message == "Unauthorized-401")
                {
                    //oauthClient = new OAuth2Client(AuthorizationKeysQBO.clientIdQBO, AuthorizationKeysQBO.clientSecretQBO, AuthorizationKeysQBO.redirectUrl, AuthorizationKeysQBO.appEnvironment);
                    //var tokenResp = oauthClient.RefreshTokenAsync(AuthorizationKeysQBO.refreshTokenQBO).Result;
                    //if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
                    //{
                    //    FileInfo fileinfo = new FileInfo(AuthorizationKeysQBO.tokenFilePath);
                    //    string jsonFile = File.ReadAllText(fileinfo.FullName);
                    //    var jObj = JObject.Parse(jsonFile);
                    //    jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                    //    jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;

                    //    string output = JsonConvert.SerializeObject(jObj, Formatting.Indented);
                    //    File.WriteAllText(fileinfo.FullName, output);

                    var serviceContext = Helper.GetNewTokens_ServiceContext();
                    return serviceContext;

                }
                else
                {
                    throw ex;
                }
            }

        }

        

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
