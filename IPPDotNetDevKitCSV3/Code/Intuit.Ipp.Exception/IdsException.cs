////*********************************************************
// <copyright file="IdsException.cs" company="Intuit">
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
// <summary>This file contains Constants.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// Represents an IdsException.
    /// </summary>
    [System.Serializable]
    public class IdsException : System.Exception
    {
        /// <summary>
        /// Error Code.
        /// </summary>
        private string errorCode;

        /// <summary>
        /// Error Message.
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Source of the exception.
        /// </summary>
        private string source;

        /// <summary>
        /// Inner Exception.
        /// </summary>
        private System.Exception innerException;

        /// <summary>
        /// Inner Exceptions.
        /// </summary>
        private IList<IdsError> innerExceptions;

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        public IdsException()
        {
            this.errorMessage = Resources.IdsExceptionDefaultMessage;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public IdsException(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public IdsException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
            this.errorMessage = errorMessage;
            this.innerException = innerException;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public IdsException(string errorMessage, string errorCode, string source)
        {
            this.errorMessage = errorMessage;
            this.errorCode = errorCode;
            this.source = source;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public IdsException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, innerException)
        {
            string errorDetail = string.Empty;
           
            if (innerException != null)
            {
                if (innerException.GetType() == typeof(ValidationException))
                {
                    ValidationException tempException = innerException as ValidationException; 

                    if (tempException.InnerExceptions != null)
                    {
                        for (int i = tempException.InnerExceptions.Count - 1; i >= 0; i--)
                        {
                            errorDetail += tempException.innerExceptions[i].Detail + " , ";
                        }

                        this.innerExceptions = tempException.InnerExceptions;
                    }

                }

                if (!string.IsNullOrEmpty(errorDetail))
                {
                    this.errorMessage = errorMessage + ". Details: " + errorDetail;

                }
                else
                {
                    this.errorMessage = errorMessage;
                }

            }
            else
            {
                this.errorMessage = errorMessage;
            }
            
            this.errorCode = errorCode;
            this.source = source;
            this.innerException = innerException;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        /// <param name="innerExceptions">Inner Exceptions.</param>
        public IdsException(string errorMessage, IList<IdsError> innerExceptions)
            : base(errorMessage)
        {
            var tempinnerExceptions = innerExceptions;
            if (tempinnerExceptions != null)
            {


                string errorDetail = "";
                int i = 0;
                var count = innerExceptions.Count;
                for (i = count - 1; i >= 0; i--)
                {
                    errorDetail += innerExceptions[i].Detail + ".";
                }

                if (errorDetail != null)
                {
                    this.errorMessage = errorMessage + "Details:" + errorDetail;

                }
                else if (errorDetail != "")
                {
                    this.errorMessage = errorMessage;
                }
                else
                {
                    this.errorMessage = errorMessage;
                }

            }
            else
            {
                this.errorMessage = errorMessage;
            }
            //this.errorMessage = errorMessage;
            this.innerExceptions = innerExceptions;
        }

        /// <summary>
        /// Initializes a new instance of the IdsException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected IdsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.errorCode = info.GetString("errorCode");
                this.errorMessage = info.GetString("errorMessage");
                this.source = info.GetString("source");
                try
                {
                    this.innerException = (System.Exception)info.GetValue("innerException", typeof(System.Exception));
                }
                catch (System.InvalidCastException)
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets Error Code.
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return this.errorCode;
            }

            set
            {
                this.errorCode = value;
            }
        }

        /// <summary>
        /// Error Message.
        /// </summary>
        public override string Message
        {
            get
            {
                return this.errorMessage;
            }
          
        }

        /// <summary>
        /// Source of the exception.
        /// </summary>
        public override string Source
        {
            get
            {
                return this.source;
            }
        }

        /// <summary>
        /// Gets inner exception.
        /// </summary>
        public new System.Exception InnerException
        {
            get
            {
                return this.innerException;
            }
        }

        /// <summary>
        /// Gets the Inner Exceptions.
        /// </summary>
        public ReadOnlyCollection<IdsError> InnerExceptions
        {
            get
            {
                if (this.innerExceptions != null)
                {
                    return new ReadOnlyCollection<IdsError>(this.innerExceptions);
                }

                return null;
            }
        }

        /// <summary>
        /// Contains the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("errorCode", this.errorCode);
                info.AddValue("errorMessage", this.errorMessage);
                info.AddValue("source", this.source);
                info.AddValue("innerException", this.innerException);
                info.AddValue("innerExceptions", this.innerExceptions);
            }
        }
    }
}
