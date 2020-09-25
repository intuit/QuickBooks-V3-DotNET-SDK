////*********************************************************
// <copyright file="ServiceContext.cs" company="Intuit">
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
 *******************************************************************************/
// <summary>This file contains SdkException.</summary>
// <summary>This file contains Service Context.</summary>
////*********************************************************

namespace Intuit.Ipp.Core
{
    using System;
    using System.Globalization;
    using Configuration;
    using Diagnostics;
    using Exception;
    using Security;
    using System.Collections.Generic;

#if NETCORE
    using Microsoft.Extensions.Configuration;
#endif


    /// <summary>
    /// This Enumeration specifies which Intuit service to connect to.
    /// </summary>
    public enum IntuitServicesType
    {


        /// <summary>
        /// QuickBooks Online Data through IDS.
        /// </summary>
        QBO,

        /// <summary>
        /// Intuit Platform services.
        /// </summary>
        IPS,

        /// <summary>
        /// None service type.
        /// </summary>
        None
    }

    /// <summary>
    /// Intuit Partner Platform Service Context.
    /// </summary>
    public class ServiceContext
    {
        #region Private Members

        /// <summary>
        /// The Realm Id.
        /// </summary>
        private string realmId;

        /// <summary>
        /// Intuit Service Type(QBO).
        /// </summary>
        private IntuitServicesType serviceType;

        /// <summary>
        /// Base Uri for IDS Service Call.
        /// </summary>
        private string baseserviceURL;



        /// <summary>
        /// Application Token.
        /// </summary>
        private string appToken;



        /// <summary>
        /// this flag indicates if static create methods of this class has been invoked.
        /// </summary>
        private bool isCreateMethod;

        /// <summary>
        /// Temporary storage for serialization and compression values for request and reponse.
        /// </summary>
        private Message messageValues;

        /// <summary>
        /// include param to be passed to services.  Responsible for getting additional data or special handling of request.
        /// </summary>
        private List<String> include = new List<String>();

        /// <summary>
        /// requestId param to be passed to services.  Responsible for identifying each request by a unique identifier.
        /// </summary>
        private string requestId;


        #endregion

        #region Constructors

        #region Intuit Anywhere and Federated

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="realmId">The realm id.</param>
        /// <param name="serviceType">Service Type - QBO/QB.</param>
        /// <param name="requestValidator">The request validate.</param>
        /// <param name="configReader">The config reader, if <see langword="null"/>, <see cref="JsonFileConfigurationProvider" /> will be used</param>
        /// <returns>Returns ServiceContext object.</returns>
        /// <exception cref="Intuit.Ipp.Exception.IdsException">If arguments are null or empty.</exception>
        /// <exception cref="Intuit.Ipp.Exception.InvalidRealmException">If realm id is invalid.</exception>
        /// <exception cref="Intuit.Ipp.Exception.InvalidTokenException">If the token is invalid.</exception>
        public ServiceContext(string realmId, IntuitServicesType serviceType, IRequestValidator requestValidator = null, IConfigurationProvider configReader = null)
        {
            IppConfiguration = (configReader ?? new JsonFileConfigurationProvider()).GetConfiguration();

            // Validate Parameters
            if (string.IsNullOrWhiteSpace(realmId))
            {
                IdsExceptionManager.HandleException(new InvalidRealmException(Properties.Resources.ParameterNotNullEmptyMessage, new ArgumentException(Properties.Resources.ParameterNotNullMessage, Properties.Resources.RealmIdParameterName)));
            }

            this.realmId = realmId;
            this.serviceType = serviceType;
            if (requestValidator != null)
            {
                IppConfiguration.Security = requestValidator;
            }

            if (IppConfiguration.Security == null)
            {
                throw new InvalidTokenException("Atleast one security mechanism has to be specified for the SDK to work. Either use config file or use constructor to specify the authentication type.");
            }

            baseserviceURL = GetBaseURL();
            isCreateMethod = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="appToken">Application Token.</param>
        /// <param name="realmId">The realm id.</param>
        /// <param name="serviceType">Service Type - QBO.</param>
        /// <param name="requestValidator">The request validate.</param>
        /// <returns>Returns ServiceContext object.</returns>
        /// <exception cref="Intuit.Ipp.Exception.IdsException">If arguments are null or empty.</exception>
        /// <exception cref="Intuit.Ipp.Exception.InvalidRealmException">If realm id is invalid.</exception>
        /// <exception cref="Intuit.Ipp.Exception.InvalidTokenException">If the token is invalid.</exception>
        [Obsolete("This constructor is deprecated as appToken is not supported in OAuth2. Please use the other constuctor")]
        public ServiceContext(string appToken, string realmId, IntuitServicesType serviceType, IRequestValidator requestValidator = null)
            : this()
        {
            // Validate Parameters
            if (string.IsNullOrWhiteSpace(appToken))
            {
                IdsExceptionManager.HandleException(new IdsException(Properties.Resources.ParameterNotNullMessage, new ArgumentNullException(Properties.Resources.AppTokenParameterName, Properties.Resources.ParameterNotNullMessage)));
            }

            if (string.IsNullOrWhiteSpace(realmId))
            {
                IdsExceptionManager.HandleException(new InvalidRealmException(Properties.Resources.ParameterNotNullEmptyMessage, new ArgumentException(Properties.Resources.ParameterNotNullMessage, Properties.Resources.RealmIdParameterName)));
            }

            this.realmId = realmId;
            this.serviceType = serviceType;
            this.appToken = appToken;
            
            if (requestValidator != null)
            {
                IppConfiguration.Security = requestValidator;
            }

            if (IppConfiguration.Security == null)
            {
                throw new InvalidTokenException("Atleast one security mechanism has to be specified for the SDK to work. Either use config file or use constructor to specify the authenticatio type.");
            }

            baseserviceURL = GetBaseURL();
            isCreateMethod = false;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceContext"/> class from being created.
        /// </summary>
        private ServiceContext() { }

        #endregion



        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Ipp configuration.
        /// </summary>
        public IppConfiguration IppConfiguration { get; set; }

        /// <summary>
        /// Gets Base Uri for IDS Service Calls.
        /// </summary>
        public string BaseUrl
        {
            get { return GetBaseURL(); }
        }

        /// <summary>
        /// Gets Realm/Company Id.
        /// </summary>
        public string RealmId
        {
            get { return realmId; }
        }

        /// <summary>
        /// Gets or Sets the Application Token.
        /// </summary>
        [Obsolete("This property is deprecated as appToken is not supported in OAuth2")]
        public string AppToken
        {
            get { return appToken; }
        }




        /// <summary>
        /// Gets Intuit Service Type.
        /// </summary>
        public IntuitServicesType ServiceType
        {
            get
            {
                return serviceType;
            }
        }


        /// <summary>
        /// Gets Intuit Include Type.
        /// </summary>
        public List<String> Include
        {
            get
            {
                return include;
            }
            set
            {
                include = value;
            }
        }

        /// <summary>
        /// Gets Intuit MinorVersion Type.
        /// </summary>
        public string MinorVersion
        {
            get
            {
                return GetMinorVersion();
            }
        }

        /// <summary>
        /// Gets Unique requestId for the API call.
        /// </summary>
        public string RequestId {
            get
            {
                if (requestId ==null)
                {
                    requestId = Guid.NewGuid().ToString("N");
                }
                return requestId;
            }
            set
            {
                requestId = value;
            }
        }

        /// <summary>
        /// timeout param to be passed to services.  To setup the ReadWriteTimeout property in HttpWebRequest.
        /// It is only for sync web requests. If not set, the default timeout will be used.
        /// </summary>
        public Nullable<int> Timeout { get; set; }

        #endregion



        #region Public Methods

        /// <summary>
        /// Populates the values of the service context like realmId, service type to the Data Services being targetted.
        /// </summary>
        public void UseDataServices()
        {
            if (isCreateMethod)
            {
                serviceType = IntuitServicesType.QBO;

                // Get the base Uri for the new service type
                baseserviceURL = GetBaseURL();
                RevertConfiguration();
            }
        }

        /// <summary>
        /// Populates the values of the service context like service type and base url to Platform Services.
        /// </summary>
        public void UsePlatformServices()
        {
            serviceType = IntuitServicesType.IPS;
            baseserviceURL = GetBaseURL();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the default configuration values for IPS operations.
        /// </summary>
        private void SetDefaultConfigurationForIPS()
        {
            messageValues = new Message
            {
                Request = new Request
                {
                    CompressionFormat = IppConfiguration.Message.Request.CompressionFormat,
                    SerializationFormat = IppConfiguration.Message.Request.SerializationFormat
                },
                Response = new Response
                {
                    CompressionFormat = IppConfiguration.Message.Response.CompressionFormat,
                    SerializationFormat = IppConfiguration.Message.Response.SerializationFormat
                }
            };

            // Set the serviceContext IPP configuration to what IPS is accepting.
            IppConfiguration.Message.Request.SerializationFormat = SerializationFormat.Xml;
            IppConfiguration.Message.Request.CompressionFormat = CompressionFormat.None;
            IppConfiguration.Message.Response.SerializationFormat = SerializationFormat.Xml;
            IppConfiguration.Message.Response.CompressionFormat = CompressionFormat.None;
        }

        /// <summary>
        /// Reverts the ipp configuration to the original values.
        /// </summary>
        private void RevertConfiguration()
        {
            if (messageValues != null && messageValues.Request != null && messageValues.Response != null)
            {
                IppConfiguration.Message.Request.SerializationFormat = messageValues.Request.SerializationFormat;
                IppConfiguration.Message.Request.CompressionFormat = messageValues.Request.CompressionFormat;
                IppConfiguration.Message.Response.SerializationFormat = messageValues.Response.SerializationFormat;
                IppConfiguration.Message.Response.CompressionFormat = messageValues.Response.CompressionFormat;
            }
        }

        /// <summary>
        /// Gets the base Uri for a QBO user.
        /// </summary>
        /// <returns>Returns the base Uri endpoint for a user.</returns>
        private string GetBaseURL()
        {
            IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called GetBaseURL method.");
            string baseurl = string.Empty;


            if (serviceType == IntuitServicesType.QBO)
            {
                baseurl = IppConfiguration.BaseUrl.Qbo;

                if (string.IsNullOrEmpty(baseurl))
                {
                    baseurl = Utility.CoreConstants.QBO_BASEURL;
                }
                else
                {
                    if (!baseurl.EndsWith("/"))
                    {
                        baseurl = baseurl + "/";
                    }
                }


                IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, string.Format(CultureInfo.InvariantCulture, "BaseUrl set for QBO Service Type: {0}.", baseurl));
            }
            else if (serviceType == IntuitServicesType.IPS)
            {
                baseurl = IppConfiguration.BaseUrl.Ips;
                if (string.IsNullOrEmpty(baseurl))
                {
                    baseurl = Utility.CoreConstants.IPS_BASEURL;
                }

                IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, string.Format(CultureInfo.InvariantCulture, "BaseUrl set for Intuit Platform Service Type: {0}.", baseurl));
            }

            return baseurl;
        }

        /// <summary>
        /// Gets the minorVersion for a QBO call
        /// </summary>
        /// <returns>Returns the minorVersion</returns>
        private string GetMinorVersion()
        {
            IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called GetMinorVersion method.");
            string minorversion = null;

            minorversion = IppConfiguration.MinorVersion.Qbo;
            IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, string.Format(CultureInfo.InvariantCulture, "MinorVersion set for QBO Service Type: {0}.", minorversion));


            return minorversion;
        }



        #endregion
    }
}
