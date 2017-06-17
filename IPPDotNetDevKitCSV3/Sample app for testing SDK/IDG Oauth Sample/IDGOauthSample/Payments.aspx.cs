using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDGOauthSample
{
    public partial class Payments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string consumerKey = Session["consumerKey"].ToString();
            //string consumerSecret = Session["consumerSecret"].ToString();
            //string realmId = Session["realm"].ToString();
            //string accessToken = Session["accessToken"].ToString();
            //string accessTokenSecret = Session["accessTokenSecret"].ToString();

            string body = "{\"amount\": \"10.55\",\"token\": \"5205b47f-8d95-4b72-9510-51207312f621\",\"currency\": \"USD\"}";
            //string responseXML = ExecutePaymentsCharge(consumerKey, consumerSecret, accessToken, accessTokenSecret, realmId, body);
            string responseXML = ExecutePaymentsCharge("qyprdvwMVV5B09tVwoMZDB2fWko0po", "jW0t7v5qCSz4hy30peUr0o10xUxzI5bcUpaXhGe7", "qyprd8oN66ggnl2ODMqJf8VnD3L2IGA4vT2yyogJRmp7a87n", "GErvQSlESxvSGwFIaDpDIyefkeefTXIatTY3kFYc", "193514346765932", body);
        }



        public static string ExecutePaymentsCharge(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string realmId, string body)
        {
            string uri = string.Format("https://sandbox.api.intuit.com/quickbooks/v4/payments/charges");
            HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
            httpWebRequest.Headers.Add("Request-Id", "iuy87y79t3861796t87r6sy");
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(consumerKey, consumerSecret, accessToken, accessTokenSecret, httpWebRequest, body));
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] content = encoding.GetBytes(body);
            using (var stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(content, 0, content.Length);
            }
            HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            using (Stream data = httpWebResponse.GetResponseStream())
            {
                //return XML response
                return new StreamReader(data).ReadToEnd();
            }
        }

        private static string GetDevDefinedOAuthHeader(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, HttpWebRequest webRequest, string requestBody)
        {

            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                SignatureMethod = SignatureMethod.HmacSha1,
                ConsumerSecret = consumerSecret,
                UseHeaderForOAuthParameters = true
            };

            //We already have OAuth tokens, so OAuth URIs below are not used - set to example.com
            OAuthSession oSession = new OAuthSession(consumerContext, "https://www.example.com",
                                    "https://www.example.com",
                                    "https://www.example.com");

            oSession.AccessToken = new TokenBase
            {
                Token = accessToken,
                ConsumerKey = consumerKey,
                TokenSecret = accessTokenSecret
            };

            IConsumerRequest consumerRequest = oSession.Request();
            consumerRequest = ConsumerRequestExtensions.ForMethod(consumerRequest, webRequest.Method);
            if (requestBody != null)
            {
                consumerRequest = consumerRequest.Post().WithRawContentType(webRequest.ContentType).WithRawContent(System.Text.Encoding.ASCII.GetBytes(requestBody));
            }
            consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
            consumerRequest = consumerRequest.SignWithToken();
            return consumerRequest.Context.GenerateOAuthParametersForHeader();
        }
    }
}