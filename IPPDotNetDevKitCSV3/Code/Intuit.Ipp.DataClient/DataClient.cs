
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Intuit.Ipp.Client
{
    public class DataClient
    {
        private readonly Func<HttpMessageInvoker> _client;
        private readonly TokenClientOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClient"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">client</exception>
        public DataClient(HttpMessageInvoker client, TokenClientOptions options)
            : this(() => client, options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClient"/> class.
        /// </summary>
        /// <param name="client">The client func.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">client</exception>
        public DataClient(Func<HttpMessageInvoker> client, TokenClientOptions options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? new TokenClientOptions();
        }

        /// <summary>
        /// Sets request parameters from the options.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="parameters">The parameters.</param>
        internal void ApplyRequestParameters(TokenRequest request, IDictionary<string, string> parameters)
        {
            request.Address = _options.Address;
            request.ClientId = _options.ClientId;
            request.ClientSecret = _options.ClientSecret;
            request.ClientAssertion = _options.ClientAssertion;
            request.ClientCredentialStyle = _options.ClientCredentialStyle;
            request.AuthorizationHeaderStyle = _options.AuthorizationHeaderStyle;
            request.Parameters = _options.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.Parameters.Add(parameter);
                }
            }
        }


        #region AddAsynchronous-New

        /// <summary>
        /// Adds an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        //public Task<T> AddAsynchronous<T>(T entity, CancellationToken cancellationToken = default(CancellationToken)) where T : IEntity
        //{
        //    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add.");

        //    // Validate parameter
        //    if (!ServicesHelper.IsTypeNull(entity))
        //    {
        //        IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
        //        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
        //        IdsExceptionManager.HandleException(exception);
        //    }

        //    string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

        //    // Builds resource Uri
        //    string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

        //    // Creates request parameters
        //    RequestParameters parameters;
        //    if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
        //    {
        //        parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
        //    }
        //    else
        //    {
        //        parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
        //    }

        //    // Prepares request
        //    HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

        //    string response = string.Empty;
        //    try
        //    {
        //        // gets response
        //        response = this.restHandler.GetResponse(request);
        //    }
        //    catch (IdsException ex)
        //    {
        //        IdsExceptionManager.HandleException(ex);
        //    }

        //    CoreHelper.CheckNullResponseAndThrowException(response);

        //    // de serialize object
        //    IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
        //    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
        //    return (T)(restResponse.AnyIntuitObject as IEntity);
        //}

        #endregion
    }
}
