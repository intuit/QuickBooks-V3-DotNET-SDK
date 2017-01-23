#region Using

using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#endregion

/// <summary>
/// Static Class having OpenId related Implementation
/// </summary>
public static class OpenID
{
    /// <summary>
    /// Redirects user to login page if he is not already authenticated
    /// </summary>
    /// <param name="opendIdProviderUrl">OpenId Provider Url.</param>
    /// <param name="identity">Identity value.</param>
    /// <param name="claimedId">Claim identifier.</param>
    /// <param name="requiredParameters">Required Parameters.</param>
    /// <param name="optionalParameters">Optional Parameters.</param>
    /// <returns>True if login was successful.</returns>
    public static bool Login(string opendIdProviderUrl, string identity, string claimedId, string requiredParameters, string optionalParameters)
    {
        try
        {
            if (!string.IsNullOrEmpty(opendIdProviderUrl))
            {
                string redirectUrl = CreateRedirectUrl(requiredParameters, optionalParameters, claimedId, identity);
                OpenIdData data = new OpenIdData(identity);
                HttpContext.Current.Session["openid"] = data;
                HttpContext.Current.Response.Redirect(opendIdProviderUrl + redirectUrl, true);
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

        return false;
    }

    /// <summary>
    /// Authenticates the request from the OpenID provider.
    /// </summary>
    public static OpenIdData Authenticate()
    {
        OpenIdData data = (OpenIdData)HttpContext.Current.Session["openid"];

        // Make sure the client has been through the Login method
        if (data == null)
            return new OpenIdData(string.Empty);

        NameValueCollection query = HttpContext.Current.Request.QueryString;

        // Make sure the incoming request's identity matches the one stored in session
        //if (query["openid.claimed_id"] != data.Identity)
        //  return data;

        data = new OpenIdData(string.Empty, query["openid.mode"] == "id_res");
        NameValueCollection nameValueColl = new NameValueCollection();
        foreach (string name in query.Keys)
        {
            nameValueColl.Add(name.Replace("openid.sreg.", string.Empty), query[name]);
        }

        data = new OpenIdData(string.Empty, query["openid.mode"] == "id_res", nameValueColl);
        HttpContext.Current.Session.Remove("openid");
        return data;
    }

    /// <summary>
    /// Checks Wheter request is OpenID or not
    /// </summary>
    public static bool IsOpenIdRequest
    {
        get
        {
            // All OpenID request must use the GET method
            if (!HttpContext.Current.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                return false;

            return HttpContext.Current.Request.QueryString["openid.mode"] != null;
        }
    }

    #region Private methods

    private static readonly Regex REGEX_LINK = new Regex(@"<link[^>]*/?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex REGEX_HREF = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Create the URL to Intuit's Open ID provider end point
    /// </summary>
    /// <param name="requiredParameters"></param>
    /// <param name="optionalParameters"></param>
    /// <param name="claimedId"></param>
    /// <param name="identity"></param>
    /// <returns></returns>
    private static string CreateRedirectUrl(string requiredParameters, string optionalParameters, string claimedId, string identity)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("?openid.ns=" + HttpUtility.UrlEncode("http://specs.openid.net/auth/2.0"));
        sb.Append("&openid.mode=checkid_setup");
        sb.Append("&openid.claimed_id=" + HttpUtility.UrlEncode(claimedId));
        sb.Append("&openid.identity=" + HttpUtility.UrlEncode(identity));
        sb.Append("&openid.assoc_handle=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
        sb.Append("&openid.return_to=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
        sb.Append("&openid.realm=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
        return sb.ToString();
    }

    #endregion
}

/// <summary>
/// The data store used for keeping state between OpenID requests.
/// </summary>
public class OpenIdData
{
    /// <summary>
    /// Initializes a new instance of the OpenIdData class.
    /// </summary>
    /// <param name="identity">Identity value.</param>
    public OpenIdData(string identity)
    {
        this.Identity = identity;
        this.Parameters = new NameValueCollection();
    }

    /// <summary>
    /// Initializes a new instance of the OpenIdData class.
    /// </summary>
    /// <param name="identity">Identity value.</param>
    /// <param name="isSuccess">Is Success.</param>
    public OpenIdData(string identity, bool isSuccess)
    {
        this.Identity = identity;
        this.IsSuccess = isSuccess;
        this.Parameters = new NameValueCollection();
    }

    /// <summary>
    /// Initializes a new instance of the OpenIdData class.
    /// </summary>
    /// <param name="identity">Identity value.</param>
    /// <param name="isSuccess">Is Success.</param>
    /// <param name="nameValueColl">Name Value collection.</param>
    public OpenIdData(string identity, bool isSuccess, NameValueCollection nameValueColl)
    {
        this.Identity = identity;
        this.IsSuccess = isSuccess;
        this.Parameters = nameValueColl;
    }

    /// <summary>
    /// Gets value indicating whether the operation is success.
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets the Identity value.
    /// </summary>
    public string Identity { get; private set; }

    /// <summary>
    /// Gets the Name Value collection.
    /// </summary>
    public NameValueCollection Parameters { get; private set; }
}