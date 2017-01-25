////*********************************************************
// <copyright file="PlatformClient.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/// <summary>This file contains PlatformClient for platform calls </summary>
////*********************************************************

namespace Intuit.Ipp.PlatformService {
using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using DevDefined.OAuth.Consumer;
    using DevDefined.OAuth.Framework;

    /// <summary>
    /// PlatformClient class
    /// </summary>
    public class PlatformClient
{
    private string url;
	public PlatformClient( string url)
	{
        this.url = url;
	}

    public string GetResponse(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
    {
        try
        {
            HttpWebRequest httpWebRequest = WebRequest.Create(this.url) as HttpWebRequest;
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(httpWebRequest, consumerKey, consumerSecret, accessToken, accessTokenSecret));
            UTF8Encoding encoding = new UTF8Encoding();
            HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            Stream data = httpWebResponse.GetResponseStream();

            return new StreamReader(data).ReadToEnd();
        }
        catch (WebException webException)
        {
            throw new PlatformException( webException.Status.ToString(), webException.Message, "");
        }
    }

    static string GetDevDefinedOAuthHeader(HttpWebRequest webRequest, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
    {

        OAuthConsumerContext consumerContext = new OAuthConsumerContext
        {
            ConsumerKey = consumerKey,
            ConsumerSecret = consumerSecret,
            SignatureMethod = SignatureMethod.HmacSha1,
            UseHeaderForOAuthParameters = true

        };

        consumerContext.UseHeaderForOAuthParameters = true;

        //URIs not used - we already have Oauth tokens
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
        consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
        consumerRequest = consumerRequest.SignWithToken();
        return consumerRequest.Context.GenerateOAuthParametersForHeader();
    }

}
}