
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using System.Threading;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Extesnion class for IdentityModel
    /// </summary>
    public static class IdentityModelExtensions
    {
        
       

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="client"></param>
        ///// <param name="address"></param>
        ///// <param name="appEnvironment"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public static async Task<IdentityModel.Client.DiscoveryResponse> GetDiscoveryDocumentAsync(this HttpClient client, string address, string appEnvironment, CancellationToken cancellationToken = default)
        //{
        //    string discoveryUrl = "";
        //    if (appEnvironment == AppEnvironment.Production.ToString())
        //    {
        //        //discoveryUrl = OidcConstants.DicoveryUrl.Production;

        //    }
        //    else if (appEnvironment == AppEnvironment.E2E.ToString())
        //    {
        //        //discoveryUrl = OidcConstants.DicoveryUrl.E2E;
        //    }
        //    else
        //    {
        //        //discoveryUrl = OidcConstants.DicoveryUrl.Sandbox;
        //    }

        //    return await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = discoveryUrl }, cancellationToken);
        //}

        /// <summary>
        /// Get RealmId
        /// </summary>
        /// <param name="authorizeResponse"></param>
        /// <returns>string</returns>
        public static string GetRealmId(this AuthorizeResponse authorizeResponse)
        {
            string realmId = authorizeResponse.TryGet("realmId");
            if (realmId != null)
            {return realmId; }
            else
                return null;
        }

        /// <summary>
        /// Get Refresh Token expiry
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <returns>string</returns>
        public static string GetRefreshTokenExpiry(this TokenResponse tokenResponse)
        {
            string refreshTokenExpiry = tokenResponse.TryGet("x_refresh_token_expires_in");
            return refreshTokenExpiry;
        }

        //public static string GetIntuitTid(this Response response)
        //{
        //    var headers = response.Headers;
        //    IEnumerable<string> values;
        //    if (headers.TryGetValues("intuit_tid", out values))
        //    {
        //        string intuit_tid = values.First();
        //        return intuit_tid;
        //    }
        //    else {return "None" };
            
        //}

        


    }
}
