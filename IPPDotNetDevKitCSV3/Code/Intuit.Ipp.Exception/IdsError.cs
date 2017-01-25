// -----------------------------------------------------------------------
// <copyright file="IdsError.cs" company="Intuit">
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
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Exception
{
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [System.Serializable]
    public class IdsError : System.Exception
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
        /// The name of the property (field) associated with this error, if applicable.
        /// </summary>
        private string element;

        /// <summary>
        /// Text that further describes the error, useful for debugging by the developer.
        /// </summary>
        private string detail;

        /// <summary>
        /// Initializes a new instance of the IdsError class.
        /// </summary>
        public IdsError()
        {
            this.errorMessage = Resources.IdsErrorDefaultMessage;
        }

        /// <summary>
        /// Initializes a new instance of the IdsError class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public IdsError(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the IdsError class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="element">Element of the exception.</param>
        /// <param name="detail">Detail of the exception.</param>
        public IdsError(string errorMessage, string errorCode, string element, string detail)
            : base(errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
            this.element = element;
            this.detail = detail;
        }

        /// <summary>
        /// Initializes a new instance of the IdsError class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected IdsError(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.errorCode = info.GetString("errorCode");
                this.errorMessage = info.GetString("errorMessage");
                this.element = info.GetString("element");
                this.detail = info.GetString("detail");
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
        /// Gets the name of the property (field) associated with this error, if applicable.
        /// </summary>
        public string Element
        {
            get
            {
                return this.element;
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
        /// Gets the text that further describes the error, useful for debugging by the developer.
        /// </summary>
        public string Detail
        {
            get
            {
                return this.detail;
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
                info.AddValue("element", this.element);
                info.AddValue("detail", this.detail);
            }
        }
    }
}
