using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Security;
using Intuit.Ipp.Core;
using System.Configuration;
using System.Net;
using System.Globalization;
using System.IO;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Data;

namespace Intuit.Ipp.Test
{
    public class Initializer
    {
        private static string accessTokenQBO = string.Empty;
        private static string refreshTokenQBO = string.Empty;
        private static string clientIdQBO = string.Empty;
        private static string clientSecretQBO = string.Empty;
        private static string realmIdIAQBO = string.Empty;

        private static string accessTokenSecretQBO = string.Empty;
        private static string consumerKeyQBO = string.Empty;
        private static string consumerKeySecretQBO = string.Empty;

        private static void Initialize()
        {

            //Oauth1 tokens
            accessTokenQBO = ConfigurationManager.AppSettings["accessTokenQBO"];
            accessTokenSecretQBO = ConfigurationManager.AppSettings["accessTokenSecretQBO"];
            consumerKeyQBO = ConfigurationManager.AppSettings["consumerKeyQBO"];
            consumerKeySecretQBO = ConfigurationManager.AppSettings["consumerSecretQBO"];
            realmIdIAQBO = ConfigurationManager.AppSettings["realmIdIAQBO"];

            //////Ouath2 tokens
            //accessTokenQBO = ConfigurationManager.AppSettings["accessTokenQBO"];
            //refreshTokenQBO = ConfigurationManager.AppSettings["refreshTokenQBO"];
            //realmIdIAQBO = ConfigurationManager.AppSettings["realmIdIAQBO"];
            //clientIdQBO = ConfigurationManager.AppSettings["clientIdKeyQBO"];
            //clientSecretQBO = ConfigurationManager.AppSettings["clientSecretQBO"];



        }


        internal static ServiceContext InitializeQBOServiceContextUsingoAuth()
        {
            Initialize();
            ////Oauth1 validator
            OAuthRequestValidator reqValidator = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, consumerKeySecretQBO);

            ////Oauth2 validator
            //OAuth2RequestValidator reqValidator = new OAuth2RequestValidator(accessTokenQBO);
            ServiceContext context = new ServiceContext(realmIdIAQBO, IntuitServicesType.QBO, reqValidator);
            context.IppConfiguration.MinorVersion.Qbo = "12";
            return context;
        }

       

        internal static ServiceContext InitializeQueryServiceContextUsingoAuth(bool isQBO)
        {
            Initialize();
            ServiceContext context = null;
            if (isQBO)
            {
                ////Oauth1 validator
                OAuthRequestValidator reqValidator = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, consumerKeySecretQBO);


                ////Oauth2 validator
                //OAuth2RequestValidator reqValidator = new OAuth2RequestValidator(accessTokenQBO);
                context = new ServiceContext(realmIdIAQBO, IntuitServicesType.QBO, reqValidator);
            }
            
            return context;
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
