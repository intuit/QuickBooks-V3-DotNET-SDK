

////*********************************************************
// <copyright file="WebhooksService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains WebhooksService Class for verifying Webhooks Payload and Deserializing </summary>
////*********************************************************

namespace Intuit.Ipp.WebhooksService
{

    
    using Intuit.Ipp.Core;
    using System;
    using Intuit.Ipp.Core.Configuration;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;
    using System.Text;
    using System.Threading;
    using Newtonsoft.Json;
    using System.Security.Cryptography;
    using System.Configuration;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;
    using System.IO;


    /// <summary>
    /// WebhooksService class
    /// </summary>
    public class WebhooksService : IWebhooksService
    {

        /// <summary>
        /// Gets or sets the Webhooks verifier Token.
        /// </summary>
        internal string verifier { get; set; }


        #region Private Members
        private string verifierToken;
        #endregion


        #region Public Members
        /// <summary>
        /// Gets or sets the Ipp configuration.
        /// </summary>
        public IppConfiguration IppConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the Verifier Token configuration.
        /// </summary>
        public string VerifierToken
        {
            get
            {
                return this.GetVerfierToken();
            }
            set
            {
                this.verifierToken = value;
            }

        }
        #endregion


        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        public WebhooksService()
        {
            
            this.verifierToken = this.GetVerfierToken();
        }
        #endregion

        /// <summary>
        /// Verifies Webhooks payload against the Header's signature
        /// </summary>
        /// <returns>Returns a WebhooksEvent object.</returns>
        public bool VerifyPayload(string intuitHeaderSignature, string payload)
        {
            //Get intuitHeader
            string hmacHeaderSignature = intuitHeaderSignature;

            //Get Webhooks verifier token          
            this.verifier = this.VerifierToken;


            if (hmacHeaderSignature == null)
            {
                return false;
            }
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(verifier);
                var dataBytes = Encoding.UTF8.GetBytes(payload);

                //use the SHA256Managed Class to compute the hash
                var hmac = new HMACSHA256(keyBytes);
                var hmacBytes = hmac.ComputeHash(dataBytes);

                //Get payload signature value. 
                
                var payloadSignature = Convert.ToBase64String(hmacBytes);//Payload value
                

                //Compare webhooks response payload's signature with the signature passed in the header of the post webhooks request from Intuit. If they match, the call is verified.
                if ((string)hmacHeaderSignature == payloadSignature)
                {
                    

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch(ValidationException)
            {
                return false;

            }



            
        }




        /// <summary>
        /// Executes a Deserialization operation for Webhooks Events payload
        /// </summary>        
        /// <returns>Returns a WebhooksEvent object.</returns>
        public WebhooksEvent GetWebooksEvents(string payload)
        {
            WebhooksEvent webhooksEvent = JsonConvert.DeserializeObject<WebhooksEvent>(payload);
            return webhooksEvent;
        }



        /// <summary>
        /// Get Verifier Token value from config
        /// </summary>
        /// <returns>Returns string verifier token object.</returns>
        private string GetVerfierToken()
        {
            this.IppConfiguration = new LocalConfigReader().ReadConfiguration();
            this.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called GetVerifierToken method.");
            string verifierToken = this.IppConfiguration.VerifierToken.Value;

            return verifierToken;


        }


    }
}
