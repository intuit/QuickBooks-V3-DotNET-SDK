////******************************************************************************************************************************
// <copyright file="IntuitRetryPolicy.cs" company="Intuit">
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
// <summary>This file contains retry mechanism for unreliable actions and transient conditions.</summary>
////******************************************************************************************************************************

namespace Intuit.Ipp.Retry
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Threading;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Core;
    using System.IO;

    /// <summary>
    /// Defines a delegate that will be invoked whenever a retry condition is encountered.
    /// </summary>
    /// <param name="retryCount">The current retry attempt count.</param>
    /// <param name="lastException">The exception which caused the retry conditions to occur.</param>
    /// <param name="delay">The delay delay which indicates how long the current thread will be suspended for before the next iteration will be invoked.</param>
    /// <returns>Returns a delegate that will be invoked whenever to retry should be attempt.</returns>
    internal delegate bool ShouldRetry(int retryCount, Exception lastException, out TimeSpan delay);

    /// <summary>
    ///  Provides the retry mechanism for unreliable actions and transient conditions.
    /// </summary>
    public class IntuitRetryPolicy
    {

        /// <summary>
        /// The Service Context.
        /// </summary>
        private ServiceContext context;


        /// <summary>
        /// Delegate that will be invoked whenever a retry condition is encountered.
        /// </summary>
        private ShouldRetry shouldRetry;

        /// <summary>
        /// The Retry Count.
        /// </summary>
        private int retryCount;

        /// <summary>
        /// The Retry Interval.
        /// </summary>
        private TimeSpan retryInterval;

        /// <summary>
        /// The initial interval value that will apply for the first retry.
        /// </summary>
        private TimeSpan initialInterval;

        /// <summary>
        /// The incremental time value that will be used for calculating the progressive delay between retries.
        /// </summary>
        private TimeSpan increment;

        /// <summary>
        /// The minimum back-off time.
        /// </summary>
        private TimeSpan minBackOff;

        /// <summary>
        /// The maximum back-off time.
        /// </summary>
        private TimeSpan maxBackOff;

        /// <summary>
        /// The value which will be used to calculate a random delta in the exponential delay between retries.
        /// </summary>
        private TimeSpan deltaBackOff;

        /// <summary>
        /// Prevents a default instance of the <see cref="IntuitRetryPolicy"/> class from being created.
        /// </summary>
        private IntuitRetryPolicy()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="context">The service context.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The time interval between retries.</param>
        public IntuitRetryPolicy(ServiceContext context, int retryCount, TimeSpan retryInterval) : this()
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(retryInterval.Ticks, "retryInterval");

            this.retryCount = retryCount;
            this.retryInterval = retryInterval;
            this.shouldRetry = this.GetShouldFixedRetry();
            this.context = context;
        }
  

        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The time interval between retries.</param>
        public IntuitRetryPolicy(int retryCount, TimeSpan retryInterval)
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(retryInterval.Ticks, "retryInterval");

            this.retryCount = retryCount;
            this.retryInterval = retryInterval;
            this.shouldRetry = this.GetShouldFixedRetry();
        }

       
        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="context">The service context.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used for calculating the progressive delay between retries.</param>
        public IntuitRetryPolicy(ServiceContext context, int retryCount, TimeSpan initialInterval, TimeSpan increment) : this()
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(initialInterval.Ticks, "initialInterval");
            IntuitRetryHelper.ArgumentNotNegativeValue(increment.Ticks, "increment");

            this.retryCount = retryCount;
            this.initialInterval = initialInterval;
            this.increment = increment;
            this.shouldRetry = this.GetShouldIncrementalRetry();
            this.context = context;
        }

   

        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used for calculating the progressive delay between retries.</param>
        public IntuitRetryPolicy(int retryCount, TimeSpan initialInterval, TimeSpan increment)
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(initialInterval.Ticks, "initialInterval");
            IntuitRetryHelper.ArgumentNotNegativeValue(increment.Ticks, "increment");

            this.retryCount = retryCount;
            this.initialInterval = initialInterval;
            this.increment = increment;
            this.shouldRetry = this.GetShouldIncrementalRetry();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="context">The service context.</param>
        /// <param name="retryCount">The maximum number of retry attempts.</param>
        /// <param name="minBackoff">The minimum back-off time</param>
        /// <param name="maxBackoff">The maximum back-off time.</param>
        /// <param name="deltaBackoff">The value which will be used to calculate a random delta in the exponential delay between retries.</param>
        public IntuitRetryPolicy(ServiceContext context, int retryCount, TimeSpan minBackoff, TimeSpan maxBackoff, TimeSpan deltaBackoff) : this()
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(minBackoff.Ticks, "minBackoff");
            IntuitRetryHelper.ArgumentNotNegativeValue(maxBackoff.Ticks, "maxBackoff");
            IntuitRetryHelper.ArgumentNotNegativeValue(deltaBackoff.Ticks, "deltaBackoff");
            IntuitRetryHelper.ArgumentNotGreaterThan(minBackoff.TotalMilliseconds, maxBackoff.TotalMilliseconds, "minBackoff");

            this.retryCount = retryCount;
            this.minBackOff = minBackoff;
            this.maxBackOff = maxBackoff;
            this.deltaBackOff = deltaBackoff;
            this.shouldRetry = this.GetShouldExponentialBackOffRetry();
            this.context = context;
        }

       


        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryPolicy"/> class.
        /// </summary>
        /// <param name="retryCount">The maximum number of retry attempts.</param>
        /// <param name="minBackoff">The minimum back-off time</param>
        /// <param name="maxBackoff">The maximum back-off time.</param>
        /// <param name="deltaBackoff">The value which will be used to calculate a random delta in the exponential delay between retries.</param>
        public IntuitRetryPolicy(int retryCount, TimeSpan minBackoff, TimeSpan maxBackoff, TimeSpan deltaBackoff)
        {
            IntuitRetryHelper.ArgumentNotNegativeValue(retryCount, "retryCount");
            IntuitRetryHelper.ArgumentNotNegativeValue(minBackoff.Ticks, "minBackoff");
            IntuitRetryHelper.ArgumentNotNegativeValue(maxBackoff.Ticks, "maxBackoff");
            IntuitRetryHelper.ArgumentNotNegativeValue(deltaBackoff.Ticks, "deltaBackoff");
            IntuitRetryHelper.ArgumentNotGreaterThan(minBackoff.TotalMilliseconds, maxBackoff.TotalMilliseconds, "minBackoff");

            this.retryCount = retryCount;
            this.minBackOff = minBackoff;
            this.maxBackOff = maxBackoff;
            this.deltaBackOff = deltaBackoff;
            this.shouldRetry = this.GetShouldExponentialBackOffRetry();
        }

        /// <summary>
        /// An instance of a callback delegate that will be invoked whenever a retry condition is encountered.
        /// </summary>
        public event EventHandler<IntuitRetryingEventArgs> Retrying;

        /// <summary>
        /// Gets or sets the extended exception retry strategy.
        /// </summary>
        /// <value>
        /// The extended exception retry strategy.
        /// </value>
        public IExtendedRetry ExtendedRetryException { get; set; }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <param name="action">A delegate representing the executable action which doesn't return any results.</param>
        public void ExecuteAction(Action action)
        {
            IntuitRetryHelper.IsArgumentNull(action, "action");

            this.ExecuteAction(() => { action(); return default(object); });
        }

        /// <summary>
        /// Repetitively executes the specified asynchronous action while it satisfies the current retry policy.
        /// </summary>
        /// <param name="beginAction">The begin method of the async pattern.</param>
        /// <param name="endAction">The end method of the async pattern.</param>
        /// <param name="successHandler">The action to perform when the async operation is done.</param>
        /// <param name="faultHandler">The fault handler delegate that will be triggered if the operation cannot be successfully invoked despite retry attempts.</param>
        public void ExecuteAction(Action<AsyncCallback> beginAction, Action<IAsyncResult> endAction, Action successHandler, Action<Exception> faultHandler)
        {
            IntuitRetryHelper.IsArgumentNull(beginAction, "beginAction");
            IntuitRetryHelper.IsArgumentNull(endAction, "endAction");
            IntuitRetryHelper.IsArgumentNull(successHandler, "successHandler");
            IntuitRetryHelper.IsArgumentNull(faultHandler, "faultHandler");

            this.ExecuteAction<object>(
                beginAction,
                ar => { endAction(ar); return null; },
                _ => successHandler(),
                faultHandler);
        }

        /// <summary>
        /// Repetitively executes the specified asynchronous action while it satisfies the current retry policy.
        /// </summary>
        /// <typeparam name="TResult">The type of the object returned by the async operation.</typeparam>
        /// <param name="beginAction">The begin method of the async pattern.</param>
        /// <param name="endAction">The end method of the async pattern.</param>
        /// <param name="successHandler">The action to perform when the async operation is done.</param>
        /// <param name="faultHandler">The fault handler delegate that will be triggered if the operation cannot be successfully invoked despite retry attempts.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Needs to catch all exceptions to test them.")]
        public void ExecuteAction<TResult>(Action<AsyncCallback> beginAction, Func<IAsyncResult, TResult> endAction, Action<TResult> successHandler, Action<Exception> faultHandler)
        {
            IntuitRetryHelper.IsArgumentNull(beginAction, "beginAction");
            IntuitRetryHelper.IsArgumentNull(endAction, "endAction");
            IntuitRetryHelper.IsArgumentNull(successHandler, "successHandler");
            IntuitRetryHelper.IsArgumentNull(faultHandler, "faultHandler");

            int retryCount = 0;
            AsyncCallback endInvoke = null;
            Func<Action, bool> executeWithRetry = null;

            // Configure a custom callback delegate that invokes the end operation and the success handler if the operation succeedes
            endInvoke =
                ar =>
                {
                    var result = default(TResult);

                    if (executeWithRetry(() => result = endAction(ar)))
                    {
                        successHandler(result);
                    }
                };

            // Utility delegate to invoke an action and implement the core retry logic
            // If the action succeeds (i.e. does not throw an exception) it returns true.
            // If the action throws, it analizes it for retries. If a retry is required, it restarts the async operation; otherwise, it invokes the fault handler.
            executeWithRetry =
                a =>
                {
                    try
                    {
                        // Invoke the callback delegate which can throw an exception if the main async operation has completed with a fault.
                        a();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Capture the original exception for analysis.
                        var lastError = ex;

                        // Handling of RetryLimitExceededException needs to be done separately. This exception type indicates the application's intent to exit from the retry loop.
                        if (lastError is RetryExceededException)
                        {
                            if (lastError.InnerException != null)
                            {
                                faultHandler(lastError.InnerException);
                                return false;
                            }
                            else
                            {
                                faultHandler(lastError);
                                return false;
                            }
                        }
                        else
                        {
                            var delay = TimeSpan.Zero;

                            // Check if we should continue retrying on this exception. If not, invoke the fault handler so that user code can take control.
                            if (!IsTransient(lastError) || ((this.ExtendedRetryException != null) && this.ExtendedRetryException.IsRetryException(ex)))
                            {
                                faultHandler(lastError);
                                return false;
                            }
                            else
                            {
                                if (delay.TotalMilliseconds < 0)
                                {
                                    delay = TimeSpan.Zero;
                                }

                                retryCount = retryCount + 1;
                                if (!this.shouldRetry(retryCount, lastError, out delay))
                                {
                                    WebException webException = ex as WebException;


                                    
                                    string errorString = string.Empty;
                                    if (webException != null)
                                    {

                                        // If not null then check the response property of the webException object.
                                        if (webException.Response != null)
                                    {
                                        // There is a response from the Ids server. Cast it to HttpWebResponse.
                                        HttpWebResponse errorResponse = (HttpWebResponse)webException.Response;

                                        // Get the status code description of the error response.
                                        string statusCodeDescription = errorResponse.StatusCode.ToString();

                                        // Get the status code of the error response.
                                        int statusCode = (int)errorResponse.StatusCode;


                                        ICompressor responseCompressor = CoreHelper.GetCompressor(this.context, false);
                                        if (!string.IsNullOrWhiteSpace(errorResponse.ContentEncoding) && responseCompressor != null)
                                        {
                                            using (var responseStream = errorResponse.GetResponseStream()) //Check for decompressing
                                            {
                                                using (var decompressedStream = responseCompressor.Decompress(responseStream))
                                                {
                                                    // Get the response stream.
                                                    StreamReader reader = new StreamReader(decompressedStream);
                                                    //StreamReader reader = new StreamReader(responseStream);

                                                    // Read the Stream
                                                    errorString = reader.ReadToEnd();
                                                    // Close reader
                                                    reader.Close();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            using (Stream responseStream = errorResponse.GetResponseStream())
                                            {
                                                // Get the response stream.
                                                StreamReader reader = new StreamReader(responseStream);

                                                // Read the Stream
                                                errorString = reader.ReadToEnd();
                                                // Close reader
                                                reader.Close();
                                            }
                                        }

                                        // Log the error string to disk.
                                        CoreHelper.GetRequestLogging(this.context).LogPlatformRequests(errorString, false);
                                    }
                                }

                                    Core.Rest.FaultHandler fault = new Core.Rest.FaultHandler(this.context);
                                    IdsException idsException = fault.ParseErrorResponseAndPrepareException(errorString);



                                    if (idsException != null)
                                    {
                                        faultHandler(new RetryExceededException(webException.Message, webException.Status.ToString(), webException.Source, idsException));
                                        return false;
                                    }
                                    else if(webException != null)
                                    {
                                        faultHandler(new RetryExceededException(webException.Message, webException.Status.ToString(), webException.Source, webException));
                                        return false;
                                    }

                                    faultHandler(new RetryExceededException(ex.Message, ex));
                                    return false;
                                }

                                // Notify the respective subscribers about this exception.
                                this.OnRetrying(retryCount, lastError, delay);

                                // Sleep for the defined interval before repetitively executing the main async operation.
                                if (retryCount > 2 && delay > TimeSpan.Zero)
                                {
                                    Thread.Sleep(delay);
                                }

                                executeWithRetry(() => beginAction(endInvoke));
                            }
                        }

                        return false;
                    }
                };

            // Invoke the the main async operation for the first time which should return control to the caller immediately.
            executeWithRetry(() => beginAction(endInvoke));
        }

        /// <summary>
        /// Notifies the subscribers whenever a retry condition is encountered.
        /// </summary>
        /// <param name="retryCount">The current retry attempt count.</param>
        /// <param name="lastError">The exception which caused the retry conditions to occur.</param>
        /// <param name="delay">The delay indicating how long the current thread will be suspended for before the next iteration will be invoked.</param>
        protected virtual void OnRetrying(int retryCount, System.Exception lastError, TimeSpan delay)
        {
            if (this.Retrying != null)
            {
                this.Retrying(this, new IntuitRetryingEventArgs(retryCount, delay, lastError));
            }
        }

        /// <summary>
        /// Checks whether parameter ex is transient exception or not.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>Returns whether transient exception or not.</returns>
        private static bool CheckIsTransient(System.Exception ex)
        {
            var webException = ex as WebException;

            if ((webException != null) && (webException.Response != null))
            {
                HttpWebResponse errorResponse = (HttpWebResponse)webException.Response;
                int statusCode = (int)errorResponse.StatusCode;
                if (statusCode == 404 || statusCode == 400)
                {
                    return false;
                }
            }

            if (webException != null &&
                (webException.Status == WebExceptionStatus.ProtocolError || webException.Status == WebExceptionStatus.ConnectionClosed ||
                webException.Status == WebExceptionStatus.ConnectFailure || webException.Status == WebExceptionStatus.Timeout))
            {
                return true;
            }

            if (ex is TimeoutException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>True if the specified exception is considered as transient, otherwise false.</returns>
        private static bool IsTransient(System.Exception ex)
        {
            return ex != null && (CheckIsTransient(ex) || (ex.InnerException != null && CheckIsTransient(ex.InnerException)));
        }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from the executable action.</typeparam>
        /// <param name="func">A delegate representing the executable action which returns the result of type R.</param>
        /// <returns>The result from the action.</returns>
        private TResult ExecuteAction<TResult>(Func<TResult> func)
        {
            IntuitRetryHelper.IsArgumentNull(func, "func");

            int retryCount = 1;
            TimeSpan delay = TimeSpan.Zero;
            Exception lastError;

            while (true)
            {
                lastError = null;

                try
                {
                    return func();
                }
                catch (RetryExceededException retryExceededException)
                {
                    // The user code can throw a RetryLimitExceededException to force the exit from the retry loop.
                    // The RetryLimitExceeded exception can have an inner exception attached to it. This is the exception
                    // which we will have to throw up the stack so that callers can handle it.
                    if (retryExceededException.InnerException != null)
                    {
                        throw retryExceededException.InnerException;
                    }
                    else
                    {
                        return default(TResult);
                    }
                }
                catch (Exception ex)
                {
                    lastError = ex;

                    if (!IsTransient(lastError) || ((this.ExtendedRetryException != null) && this.ExtendedRetryException.IsRetryException(ex)))
                    {
                        throw;
                    }

                    if (!this.shouldRetry(retryCount++, lastError, out delay))
                    {
                        WebException webException = ex as WebException;

                      
                        string errorString = string.Empty;

                    if (webException != null)
                    {
                        // If not null then check the response property of the webException object.
                        if (webException.Response != null)
                        {
                            // There is a response from the Ids server. Cast it to HttpWebResponse.
                            HttpWebResponse errorResponse = (HttpWebResponse)webException.Response;

                            // Get the status code description of the error response.
                            string statusCodeDescription = errorResponse.StatusCode.ToString();

                            // Get the status code of the error response.
                            int statusCode = (int)errorResponse.StatusCode;


                            ICompressor responseCompressor = CoreHelper.GetCompressor(this.context, false);
                            if (!string.IsNullOrWhiteSpace(errorResponse.ContentEncoding) && responseCompressor != null)
                            {
                                using (var responseStream = errorResponse.GetResponseStream()) //Check for decompressing
                                {
                                    using (var decompressedStream = responseCompressor.Decompress(responseStream))
                                    {
                                        // Get the response stream.
                                        StreamReader reader = new StreamReader(decompressedStream);
                                        //StreamReader reader = new StreamReader(responseStream);

                                        // Read the Stream
                                        errorString = reader.ReadToEnd();
                                        // Close reader
                                        reader.Close();
                                    }
                                }
                            }
                            else
                            {
                                using (Stream responseStream = errorResponse.GetResponseStream())
                                {
                                    // Get the response stream.
                                    StreamReader reader = new StreamReader(responseStream);

                                    // Read the Stream
                                    errorString = reader.ReadToEnd();
                                    // Close reader
                                    reader.Close();
                                }
                            }

                            // Log the error string to disk.
                            CoreHelper.GetRequestLogging(this.context).LogPlatformRequests(errorString, false);
                        }
                    }

                        Core.Rest.FaultHandler fault = new Core.Rest.FaultHandler(this.context);
                        IdsException idsException = fault.ParseErrorResponseAndPrepareException(errorString);



                        if (idsException != null)
                        {
                            throw new RetryExceededException(webException.Message, webException.Status.ToString(), webException.Source, idsException);
                        }
                        else if(webException != null)
                        {
                            throw new RetryExceededException(webException.Message, webException.Status.ToString(), webException.Source, webException);
                        }

                        throw new RetryExceededException(ex.Message, ex);
                    }
                }

                // Perform an extra check in the delay interval. Should prevent from accidentally ending up with the value of -1 that will block a thread indefinitely. 
                // In addition, any other negative numbers will cause an ArgumentOutOfRangeException fault that will be thrown by Thread.Sleep.
                if (delay.TotalMilliseconds < 0)
                {
                    delay = TimeSpan.Zero;
                }

                this.OnRetrying(retryCount - 1, lastError, delay);

                if (retryCount > 2 && delay > TimeSpan.Zero)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        /// <summary>
        /// Returns the ShouldRetry delegate for Fixed retry policy.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        private ShouldRetry GetShouldFixedRetry()
        {
            if (this.retryCount == 0)
            {
                return delegate(int currentRetryCount, Exception lastException, out TimeSpan interval)
                {
                    interval = TimeSpan.Zero;
                    return false;
                };
            }

            return delegate(int currentRetryCount, Exception lastException, out TimeSpan interval)
            {
                if (currentRetryCount < this.retryCount)
                {
                    interval = this.retryInterval;
                    return true;
                }

                interval = TimeSpan.Zero;
                return false;
            };
        }

        /// <summary>
        /// Returns the ShouldRetry delegate for Incremental retry policy.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        private ShouldRetry GetShouldIncrementalRetry()
        {
            return delegate(int currentRetryCount, Exception lastException, out TimeSpan retryInterval)
            {
                if (currentRetryCount < this.retryCount)
                {
                    retryInterval = TimeSpan.FromMilliseconds(this.initialInterval.TotalMilliseconds + (this.increment.TotalMilliseconds * currentRetryCount));

                    return true;
                }

                retryInterval = TimeSpan.Zero;

                return false;
            };
        }

        /// <summary>
        /// Returns the ShouldRetry delegate for ExponentialBackOff retry policy.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        private ShouldRetry GetShouldExponentialBackOffRetry()
        {
            return delegate(int currentRetryCount, Exception lastException, out TimeSpan retryInterval)
            {
                if (currentRetryCount < this.retryCount)
                {
                    var random = new Random();

                    var delta = (int)((Math.Pow(2.0, currentRetryCount) - 1.0) * random.Next((int)(this.deltaBackOff.TotalMilliseconds * 0.8), (int)(this.deltaBackOff.TotalMilliseconds * 1.2)));
                    var interval = (int)Math.Min(checked(this.minBackOff.TotalMilliseconds + delta), this.maxBackOff.TotalMilliseconds);

                    retryInterval = TimeSpan.FromMilliseconds(interval);

                    return true;
                }

                retryInterval = TimeSpan.Zero;
                return false;
            };
        }
    }
}
