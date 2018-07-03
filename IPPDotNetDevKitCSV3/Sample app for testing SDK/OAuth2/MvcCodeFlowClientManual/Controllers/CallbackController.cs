using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFlowClientManual.Controllers
{
    public class CallbackController : Controller
	{
        /// <summary>
        /// Code and realmid/company id recieved on Index page after redirect is complete from Authorization url
        /// </summary>
        public async Task<ActionResult> Index()
		{
            //Sync the state info and update if it is not the same
            var state = Request.QueryString["state"];
            if (state.Equals(AppController.auth2Client.CSRFToken, StringComparison.Ordinal))
            {
                ViewBag.State = state + " (valid)";
            }
            else
            {
                ViewBag.State = state + " (invalid)";
            }

            string code = Request.QueryString["code"] ?? "none";
            string realmId = Request.QueryString["realmId"] ?? "none";
            await GetAuthTokensAsync(code, realmId);

            ViewBag.Error = Request.QueryString["error"] ?? "none";

            return RedirectToAction("Tokens", "App");
        }

        /// <summary>
        /// Exchange Auth code with Auth Access and Refresh tokens and add them to Claim list
        /// </summary>
        private async Task GetAuthTokensAsync(string code, string realmId)
        {
            if (realmId != null)
            {
                Session["realmId"] = realmId;
            }

            Request.GetOwinContext().Authentication.SignOut("TempState");
            var tokenResponse = await AppController.auth2Client.GetBearerTokenAsync(code);

            var claims = new List<Claim>();

            if (Session["realmId"] != null)
            {
                claims.Add(new Claim("realmId", Session["realmId"].ToString()));
            }

            if (!string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
            {
                claims.Add(new Claim("access_token", tokenResponse.AccessToken));
                claims.Add(new Claim("access_token_expires_at", (DateTime.Now.AddSeconds(tokenResponse.AccessTokenExpiresIn)).ToString()));
            }

            if (!string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
            {
                claims.Add(new Claim("refresh_token", tokenResponse.RefreshToken));
                claims.Add(new Claim("refresh_token_expires_at", (DateTime.Now.AddSeconds(tokenResponse.RefreshTokenExpiresIn)).ToString()));
            }

            var id = new ClaimsIdentity(claims, "Cookies");
            Request.GetOwinContext().Authentication.SignIn(id);
        }
	}
}