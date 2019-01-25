using System.Configuration;
using Intuit.Ipp.Security;

namespace Intuit.Ipp.Core.Test
{
    internal class Initializer
    {

        internal static ServiceContext InitializeServiceContextQbo()
        {
            string accessToken = ConfigurationManager.AppSettings["AccessTokenQBO"];
            string accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecretQBO"];
            string consumerKey = ConfigurationManager.AppSettings["ConsumerKeyQBO"];
            string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecretQBO"];
            string realmId = ConfigurationManager.AppSettings["realmIAQBO"];
            //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("bearertoken");
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            return serviceContext;
        }

       
    }
}
