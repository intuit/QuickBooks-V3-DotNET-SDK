using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.LinqExtender;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;


namespace IDGOauthSample
{
    public partial class QBO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string realmId = HttpContext.Current.Session["realm"].ToString();
            //string accessToken = HttpContext.Current.Session["accessToken"].ToString();
            //string accessTokenSecret = HttpContext.Current.Session["accessTokenSecret"].ToString();
            //string consumerKey = HttpContext.Current.Session["consumerKey"].ToString();
            //string consumerSecret = HttpContext.Current.Session["consumerSecret"].ToString();

            //Oauth2
            string realmId = "1374329395";
            string accessToken = "eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..DauVaEJM8J-MA87xzLlAIw.mT7av2q-9PaCMNRoy7bkQPpjRNUozg9Ly_RBZByYDqK1O6lw8eb-YSkXJ_DDWq_IktrdCrOxmmKk8JNGJfwc3yMpoC6JIks1J2g-3fKHkZON0VJEE28RPa1ccd07w4GUmYOT3Tm27Ajt_JjQTIcw7fjfXjK-3KFWVzW9Uu69A71md_Ydqr9bJ4BNDven6jvY6iAnJkw5n-y2Q8aHGWAArEZSEdblERgl8d9ga7D_MxKbeiLo3a4Hr3FQ1_45RZv4WCIZ4N_dmJuqCuZzWxGa12cjXUbByaZBqUWr0J4I9clLTO8JEOHFbdBcL08aqBbnB-wM0PKAEMPgc_bpk112UMA73Ojm0xAohqFC-9_yXgRVtH4KTmU3Rb4xfVybzgixHCx_VomEj9zXMQyLBweZV37xg_lDJVTSR-nePChnh31_5OESvJINqXg4Z_2nkQaFDYLi-IRgzLZ9f_F0gWXeQAJ_0-OwD2V9pxQnWc9YK7KGnJcf50_HH5eb6MaHv1TGQ7AoWvNySh2r6VNdtY_Isrx6rEN9HBeYzwHLa8okyOA.INpWTX9lVsznYeKYBvzX-A";

            //string realmId = "123145851557297";
            //string accessToken = "qyprd2DbUviVJUw4wcIxAwWSZzF7yK2pEKy00OR5qQjkhqgV";
            //string accessTokenSecret = "TneCSQgvTshc3npAeiA0fljN2vYKLa8tognhyUyo";
            //string consumerKey = "qyprdPyI2zStZIVO1VilbbZvWy6E5q";
            //string consumerSecret = "n4vsVfMpU83eB6gH5G9vP6eIswKg91U0JLb5hs7G";


            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(accessToken);

            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            //serviceContext.IppConfiguration.BaseUrl.Qbo = "https://quickbooks.api.intuit.com/";
            //serviceContext.IppConfiguration.BaseUrl.Qbo = "https://qbonline-e2e.api.intuit.com/";
            serviceContext.IppConfiguration.Message.Request.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Xml;
            serviceContext.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Xml;
            //serviceContext.IppConfiguration.MinorVersion.Qbo = "6";
            //serviceContext.RequestId = "897kjhjjhkh9";
            //serviceContext.IppConfiguration.MinorVersion.Qbo = "6";
            DataService commonServiceQBO = new DataService(serviceContext);
            //QueryService<Item> inService1 = new QueryService<Item>(serviceContext);
            //Item In1 = inService1.ExecuteIdsQuery("SELECT * FROM Item").FirstOrDefault();
            //try
            //{
            //    Item a = new Item();
            //    a.Name = "Hours";
            //    a.IncomeAccountRef = new ReferenceType() { Value = "25" };

            //    a.Type = ItemTypeEnum.Service;
            //    a.TypeSpecified = true;
            //    var test = commonServiceQBO.Add<Item>(a);
            //}
            //catch (IdsException ex)
            //{
            //}

            //List<Account> accounts = commonServiceQBO.FindAll<Account>(new Account(), 1, 100).ToList<Account>();


            QueryService <Invoice> inService = new QueryService<Invoice>(serviceContext);
            var j = inService.ExecuteIdsQuery("SELECT * FROM Invoice").ToList<Invoice>();
            Invoice In = inService.ExecuteIdsQuery("SELECT * FROM Invoice where MetaData.LastUpdatedTime > '2016-01-06T16:32:23.0139357+00:00'  STARTPOSITION 1 MAXRESULTS 1000").FirstOrDefault();
            

        }
    }
}