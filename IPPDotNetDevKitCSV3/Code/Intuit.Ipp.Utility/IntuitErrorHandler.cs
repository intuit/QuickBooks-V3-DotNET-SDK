////********************************************************************
// <copyright file="IntuitErrorHandler.cs" company="Intuit">
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
// <summary>This file contains Error Handling methods.</summary>
////********************************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Globalization;
    using System.Xml;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility.Properties;

    /// <summary>
    /// Intuit Error Handler class.
    /// </summary>
    public static class IntuitErrorHandler
    {
        /// <summary>
        /// Check the response for any errors it might indicate. Will throw an exception if API response indicates an error.
        /// Will throw an exception if it has a problem determining success or error.
        /// </summary>
        /// <param name="response">the QuickBase response to examine</param>
        public static void HandleErrors(string response)
        {
            // To handle plain text response
            if (!IsValidXml(response))
            {
                return;
            }

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(response);
            IntuitErrorHandler.HandleErrors(responseXml);
        }

        /// <summary>
        /// Check the response for any errors it might indicate. Will throw an exception if API response indicates an error.
        /// Will throw an exception if it has a problem determining success or error.
        /// </summary>
        /// <param name="responseXml">the QuickBase response to examine</param>
        public static void HandleErrors(XmlNode responseXml)
        {
            XmlNode errCodeNode = responseXml.SelectSingleNode(UtilityConstants.ERRCODEXPATH);

            if (errCodeNode == null)
            {
                return;
            }

            int errorCode;
            if (!int.TryParse(errCodeNode.InnerText, out errorCode))
            {
                throw new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorCodeNonNemeric, errorCode));
            }

            if (errorCode == 0)
            {
                // 0 indicates success
                return;
            }

            XmlNode errTextNode = responseXml.SelectSingleNode(UtilityConstants.ERRTEXTXPATH);
            if (errTextNode == null)
            {
                throw new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorWithNoText, errorCode));
            }

            string errorText = errTextNode.InnerText;
            XmlNode errDetailNode = responseXml.SelectSingleNode(UtilityConstants.ERRDETAILXPATH);
            string errorDetail = errDetailNode != null ? errDetailNode.InnerText : null;

            if (!string.IsNullOrEmpty(errorDetail))
            {
                throw new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorDetails0, errorText, errorCode, errorDetail));
            }

            throw new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorDetails1, errorText, errorCode));
        }

        /// <summary>
        /// Validates the input string is a well formatted xml string
        /// </summary>
        /// <param name="inputString">Input xml string</param>
        /// <returns>True if 'inputString' is a valid xml</returns>
        public static bool IsValidXml(string inputString)
        {
            if (!inputString.StartsWith("<",StringComparison.Ordinal))
            {
                return false;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(inputString);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
