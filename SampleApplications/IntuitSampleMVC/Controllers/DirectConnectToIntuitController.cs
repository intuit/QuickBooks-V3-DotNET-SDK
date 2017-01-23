using System.Configuration;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// Direct connect to Intuit Controller
    /// </summary>
    public class DirectConnectToIntuitController : Controller
    {
        /// <summary>
        /// OpenId Relying Party.
        /// </summary>
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        
        //
        // GET: /DirectConnectToIntuit/
        public ActionResult Index()
        {
            var openid_identifier = ConfigurationManager.AppSettings["openid_identifier"].ToString(); ;
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
                    Response.Redirect("/DirectConnectToIntuit");
                }

                // Stage 3: OpenID Provider sending assertion response, storing the response in Session object is only for demonstration purpose
                Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                FetchResponse fetch = response.GetExtension<FetchResponse>();
                if (fetch != null)
                {
                    Session["OpenIdResponse"] = "True";
                    Session["FriendlyEmail"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);// emailAddresses.Count > 0 ? emailAddresses[0] : null;
                    Session["FriendlyName"] = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);//fullNames.Count > 0 ? fullNames[0] : null;
                }
            }

            return View();
        }
    }
}
