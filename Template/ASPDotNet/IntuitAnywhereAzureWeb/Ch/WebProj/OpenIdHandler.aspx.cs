using System;
using System.Xml;
using System.Configuration;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;


namespace $safeprojectname$
{
    /// <summary>
    /// This flow enables single sign on (SSO) between this app and the Intuit App Center.
    /// This feature offers two key benefits:  First, the user only has to sign in once, 
    /// instead of having to sign in to both this app and Intuit App Center.  
    /// Second, this app does not need to implement a customized solution for user authentication 
    /// because it relies on Intuit's OpenID service.
    /// the following occurs during the sign in process:
    /// 1.The user initiates the sign in process by going to your app and clicking the Sign in with Intuit button.
    /// 2.The Intuit sign in window appears, where the user enters the Intuit user ID (email) and password.
    /// 3.this app sends an authentication request to the Intuit OpenID service.
    /// 4.This page verifies the authentication response it receives from the Intuit OpenID service and stores
    /// user information inside the session object.
    /// </summary>
    public partial class OpenIdHandler : System.Web.UI.Page
    {
        /// <summary>
        /// OpenId Relying Party
        /// </summary>
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

        /// <summary>
        /// Action Results for Index, uses DotNetOpenAuth for creating OpenId Request with Intuit
        /// and handling response recieved. 
        /// </summary>
        /// <param name="sender">Sender of th event.</param>
        /// <param name="e">Event Args.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var openid_identifier = ConfigurationManager.AppSettings["openid_identifier"];
            var returnUrl = "OpenIdHandler.aspx";
            var response = openid.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(openid_identifier, out id))
                {
                    try
                    {
                        IAuthenticationRequest request = openid.CreateRequest(openid_identifier);
                        FetchRequest fetch = new FetchRequest();
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Email));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.FullName));
                        request.AddExtension(fetch);
                        request.RedirectToProvider();
                    }
                    catch (ProtocolException ex)
                    {
                       throw ex;
                    }
                }
            }
            else
            {
                if (response.FriendlyIdentifierForDisplay == null)
                {
                    Response.Redirect("/OpenIdHandler.aspx");
                }

                // Stage 3: OpenID Provider sending assertion response
                Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                FetchResponse fetch = response.GetExtension<FetchResponse>();
                if (fetch != null)
                {
                    Session["OpenIdResponse"] = "True";
                    Session["FriendlyEmail"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);// emailAddresses.Count > 0 ? emailAddresses[0] : null;
                    Session["FriendlyName"] = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);//fullNames.Count > 0 ? fullNames[0] : null;

                    //get the Oauth Access token for the user from OauthAccessTokenStorage.xml
                    OauthAccessTokenStorageHelper.GetOauthAccessTokenForUser(Session["FriendlyEmail"].ToString(),Page);
                }

                string query = Request.Url.Query;
                if (!string.IsNullOrWhiteSpace(query) && query.ToLower().Contains("disconnect=true"))
                {
                    Session["accessToken"] = "dummyAccessToken";
                    Session["accessTokenSecret"] = "dummyAccessTokenSecret";
                    Session["Flag"] = true;
                    Response.Redirect("CleanupOnDisconnect.aspx");
                }

                if (!string.IsNullOrEmpty(returnUrl) )
                {
                    Response.Redirect("default.aspx");
                }
             }
        }
    }
}