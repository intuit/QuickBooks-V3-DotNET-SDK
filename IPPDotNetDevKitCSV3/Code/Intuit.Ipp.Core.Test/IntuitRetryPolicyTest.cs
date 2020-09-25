////******************************************************************************************************************************
// <copyright file="IntuitRetryPolicyTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Test cases for retry mechanism for unreliable actions and transient conditions.</summary>
////******************************************************************************************************************************
namespace Intuit.Ipp.Retry.Test
{
    using System;
    using System.Net;
    using Core;
    using Exception;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for IntuitRetryPolicyTest and is intended to contain all IntuitRetryPolicyTest Unit Tests
    /// </summary>
    [TestClass()]
    public class IntuitRetryPolicyTest
    {
        /// <summary>
        /// The retry Count.
        /// </summary>
        private static int retryCount = 1;

        /// <summary>
        /// The max RetryCount.
        /// </summary>
        private static int maxRetryCount = 3;

        /// <summary>
        /// The test Context Instance
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }

        #region Sync

        /// <summary>
        /// Executes the action test with fixed retry exceeded exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithFixedRetryExceededException()
        {
            retryCount = 1;

            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            Action action = ThrowProtocolException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (RetryExceededException intuitRetryExceededException)
            {
                WebException webException = intuitRetryExceededException.InnerException as WebException;
                if (!((webException != null) && webException.Status == WebExceptionStatus.ProtocolError))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// A test for  ExtendedRetryException
        /// </summary>
        [TestMethod]
        public void ExecuteActionTestWithExtendedRetryException()
        {
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            target.ExtendedRetryException = new ExtendedRetryUnauthorizedAccessException();
            Action action = ThrowUnauthorizedAccessException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Executes the action test with fixed retry argument exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithFixedRetryArgumentException()
        {
            retryCount = 1;

            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            Action action = ThrowArgumentException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (RetryExceededException)
            {
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Executes the action test with fixed retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithFixedRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1));
            Action action = ThrowProtocolException;

            try
            {
                target.ExecuteAction(action);
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with fixed exceeded retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithFixedExceededRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1));
            Action action = ThrowProtocolException;

            try
            {
                target.ExecuteAction(action);
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with incremental retry exceeded exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithIncrementalRetryExceededException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Action action = ThrowProtocolException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with non retry exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithNonRetryException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Action action = ThrowUnauthorizedAccessException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Executes the action test with incremental retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithIncrementalRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Action action = ThrowProtocolException;
            try
            {
                target.ExecuteAction(action);
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with exponential backoff retry exceeded exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithExponentialBackoffRetryExceededException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Action action = ThrowProtocolException;
            try
            {
                target.ExecuteAction(action);
                Assert.Fail();
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with exponential backoff retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithExponentialBackoffRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Action action = ThrowProtocolException;

            try
            {
                target.ExecuteAction(action);
            }
            catch (RetryExceededException)
            {
            }
        }

        #endregion

        #region Async

        private IAsyncResult BeginRequestForProtocolException(AsyncCallback callback)
        {
            ThrowProtocolException();
            return null;
        }

        private IAsyncResult BeginRequestForArgumentException(AsyncCallback callback)
        {
            ThrowArgumentException();
            return null;
        }

        private IAsyncResult BeginRequestForUnauthorizedException(AsyncCallback callback)
        {
            ThrowUnauthorizedAccessException();
            return null;
        }

        /// <summary>
        /// Executes the action test and checks if the action was indeed called.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTest()
        {
            bool actionWasCalled = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    () =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        actionWasCalled = true;
                    });

            Assert.IsTrue(actionWasCalled);
        }

        /// <summary>
        /// Executes the action test with RetryExceededException and checks if the action was indeed called.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithRetryExceededException()
        {
            bool actionWasCalled = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    () =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        actionWasCalled = true;
                        throw new RetryExceededException();
                    });

            Assert.IsTrue(actionWasCalled);
        }

        /// <summary>
        /// Executes the action test with TimeoutException and checks if the action was indeed called.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithTimeoutException()
        {
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
                target.ExecuteAction(
                        () =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            throw new TimeoutException();
                        });

                Assert.Fail();
            }
            catch(RetryExceededException)
            {
                
            }
        }

        /// <summary>
        /// Executes the action test with TimeoutException inside RetryExceededException and checks if the action failed.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithTimeoutExceptionInsideRetryExceededException()
        {
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
                target.ExecuteAction(
                        () =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            TimeoutException timeoutException = new TimeoutException();
                            Exception retryExceededException = new Exception(string.Empty, timeoutException);
                            throw retryExceededException;
                        });

                Assert.Fail();
            }
            catch (RetryExceededException)
            {

            }
        }

        /// <summary>
        /// Executes the action test with handlers and checks if the action is indeed called.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithHandlers()
        {
            bool actionWasCalled = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    ac =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        actionWasCalled = true;
                    },
                    ar =>
                    {
                        // Invoke the end method of the asynchronous call.
                    },
                    () =>
                    {
                        // Action to perform if the asynchronous operation
                        // succeeded.

                    },
                    e =>
                    {
                        // Action to perform if the asynchronous operation
                        // failed after all the retries.
                    });

            Assert.IsTrue(actionWasCalled);
        }

        /// <summary>
        /// Executes the action test with fixed retry exceeded exception and checks if the exception is caught.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithHandlersAndRetryExceededException()
        {
            bool exceptionCaught = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    ac =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        throw new RetryExceededException();
                    },
                    ar =>
                    {
                        // Invoke the end method of the asynchronous call.
                    },
                    () =>
                    {
                        // Action to perform if the asynchronous operation
                        // succeeded.

                    },
                    e =>
                    {
                        // Action to perform if the asynchronous operation
                        // failed after all the retries.
                        exceptionCaught = true;
                    });

            Assert.IsTrue(exceptionCaught);
        }

        /// <summary>
        /// Executes the action test with fixed retry exceeded exception and checks if the inner exception is caught.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithWithHandlersAndAnInnerExceptionInsideRetryExceededException()
        {
            retryCount = 1;
            bool innerExceptionCaught = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    ac =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        Exception exception = new Exception();
                        throw new RetryExceededException(string.Empty, exception);
                    },
                    ar =>
                    {
                        // Invoke the end method of the asynchronous call.
                    },
                    () =>
                    {
                        // Action to perform if the asynchronous operation
                        // succeeded.

                    },
                    e =>
                    {
                        // Action to perform if the asynchronous operation
                        // failed after all the retries.
                        innerExceptionCaught = true;
                    });

            Assert.IsTrue(innerExceptionCaught);
        }

        /// <summary>
        /// Executes the action test with fixed retry exceeded exception and checks if the web exception is caught.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionTestWithWebException()
        {
            retryCount = 1;
            bool webExceptionCaught = false;
            IntuitRetryPolicy target = new IntuitRetryPolicy(0, TimeSpan.FromSeconds(1));
            target.ExecuteAction(
                    ac =>
                    {
                        // Invoke the begin method of the asynchronous call.
                        throw new WebException();
                    },
                    ar =>
                    {
                        // Invoke the end method of the asynchronous call.
                    },
                    () =>
                    {
                        // Action to perform if the asynchronous operation
                        // succeeded.

                    },
                    e =>
                    {
                        // Action to perform if the asynchronous operation
                        // failed after all the retries.
                        webExceptionCaught = true;
                    });

            Assert.IsTrue(webExceptionCaught);
        }

        /// <summary>
        /// Executes the action test with fixed retry exceeded exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithFixedRetryExceededException()
        {
            retryCount = 1;
            Exception exception = null;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException intuitRetryExceededException)
            {
                WebException webException = intuitRetryExceededException.InnerException as WebException;
                if (!((webException != null) && webException.Status == WebExceptionStatus.ProtocolError))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// A test for  ExtendedRetryException
        /// </summary>
        [TestMethod][Ignore]
        public void ExecuteActionAsyncTestWithExtendedRetryException()
        {
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            target.ExtendedRetryException = new ExtendedRetryUnauthorizedAccessException();
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForUnauthorizedException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Executes the action test with fixed retry argument exception.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithFixedRetryArgumentException()
        {
            retryCount = 1;

            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForArgumentException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException)
            {
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Executes the action test with fixed retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithFixedRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with fixed exceeded retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithFixedExceededRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with incremental retry exceeded exception.
        /// </summary>
        [TestMethod()][Ignore]
        public void ExecuteActionAsyncTestWithIncrementalRetryExceededException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Executes the action test with non retry exception.
        /// </summary>
        [TestMethod()][Ignore]
        public void ExecuteActionAsyncTestWithNonRetryException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForUnauthorizedException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Executes the action test with incremental retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithIncrementalRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException)
            {
            }
        }

        /// <summary>
        /// Executes the action test with exponential backoff retry exceeded exception.
        /// </summary>
        [TestMethod()][Ignore]
        public void ExecuteActionAsyncTestWithExponentialBackoffRetryExceededException()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException ex)
            {
                Console.WriteLine(@"Message: {0}", ex.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Executes the action test with exponential backoff retry.
        /// </summary>
        [TestMethod()]
        public void ExecuteActionAsyncTestWithExponentialBackoffRetry()
        {
            retryCount = 1;
            IntuitRetryPolicy target = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Exception exception = null;
            try
            {
                target.ExecuteAction(
                        ac =>
                        {
                            // Invoke the begin method of the asynchronous call.
                            BeginRequestForProtocolException(ac);
                        },
                        ar =>
                        {
                            // Invoke the end method of the asynchronous call.
                        },
                        () =>
                        {
                            // Action to perform if the asynchronous operation
                            // succeeded.
                        },
                        e =>
                        {
                            // Action to perform if the asynchronous operation
                            // failed after all the retries.
                            exception = e;
                        });

                throw exception;
            }
            catch (RetryExceededException)
            {
            }
        }

        #endregion

        /// <summary>
        /// Throws the protocol exception.
        /// </summary>
        private void ThrowProtocolException()
        {
            WebException protocolError = new WebException(string.Empty, WebExceptionStatus.ProtocolError);
            throw protocolError;
        }

        /// <summary>
        /// Throws the argument exception.
        /// </summary>
        private void ThrowArgumentException()
        {
            if (retryCount++ <= maxRetryCount)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Throws the unauthorized access exception.
        /// </summary>
        private void ThrowUnauthorizedAccessException()
        {
            throw new UnauthorizedAccessException();
        }
    }
}
