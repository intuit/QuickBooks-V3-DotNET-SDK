using Intuit.Ipp.Core;
using Intuit.Ipp.Security;

namespace $safeprojectname$.utils
{
    /// <summary>
    /// Initializes the Request Validator and ServiceContext
    /// </summary>
    internal class Initializer
    {
        /// <summary>
        /// Helper method to initialize the OAuthValidator
        /// </summary>
        /// <param name="accessToken">Value for AccessToken</param>
        /// <param name="accessTokenSecret">Value for AccessTokenSecret</param>
        /// <param name="consumerKey">Value for ConsumerKey</param>
        /// <param name="consumerSecret">Value for ConsumerSecret</param>
        /// <returns>Object of OAuthRequestValidator</returns>
        internal static OAuthRequestValidator InitializeOAuthValidator(string accessToken, string accessTokenSecret, string consumerKey, string consumerSecret)
        {
            OAuthRequestValidator oauthValidator =
                new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
            return oauthValidator;
        }

        /// <summary>
        /// Helper method used to initialize the Service Context
        /// </summary>
        /// <param name="oauthValidator">Object of OAutheValidator</param>
        /// <param name="realmId">Value for RealmId</param>
        /// <param name="appToken">Value for AppToken</param>
        /// <param name="appDBId">Value for AppDBId</param>
        /// <param name="serviceType">Service Type Qbd or Qbo</param>
        /// <returns>Object of ServiceContext</returns>
        internal static ServiceContext InitializeServiceContext(OAuthRequestValidator oauthValidator, string realmId, string appToken, string appDBId, string serviceType)
        {
            ServiceContext context = null;
            IntuitServicesType intuitServiceType = IntuitServicesType.QBD;
            if (serviceType.Equals("QBO"))
            {
                intuitServiceType = IntuitServicesType.QBO;
            }
            else
            {
                intuitServiceType = IntuitServicesType.QBD;
            }

            context = new ServiceContext(realmId, intuitServiceType, oauthValidator);
            return context;
        }

        /// <summary>
        /// Initializes service context for Platform Services.
        /// </summary>
        /// <param name="oauthValidator">OAuth Request Validator.</param>
        /// <param name="appToken">Application Token.</param>
        /// <returns>Service Context.</returns>
        internal static ServiceContext InitializeServiceContext(OAuthRequestValidator oauthValidator,  string appToken)
        {
            ServiceContext context = ServiceContext.Create(appToken, "appDBID", oauthValidator);
            return context;
        }
    }
}
