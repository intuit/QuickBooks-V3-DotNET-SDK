////*********************************************************
// <copyright file="UtilityConstants.cs" company="Intuit">
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
// <summary>This file contains Utility Constants.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Constants whose values do not change.
    /// </summary>
    public static class UtilityConstants
    {
        /// <summary>
        /// XPath for errcode tag.
        /// </summary>
        public const string ERRCODEXPATH = "//errcode";

        /// <summary>
        /// XPath for errtext tag.
        /// </summary>
        public const string ERRTEXTXPATH = "//errtext";

        /// <summary>
        /// XPath for errdetail tag.
        /// </summary>
        public const string ERRDETAILXPATH = "//errdetail";

        /// <summary>
        /// QDBAPI root element.
        /// </summary>
        public const string QDBAPI = "qdbapi";

        /// <summary>
        /// Encoding attribute.
        /// </summary>
        public const string ENCODINGATTR = "encoding";

        /// <summary>
        /// Encoding attribute value.
        /// </summary>
        public const string ENCODINGATTRVALUE = "utf-8";

        /// <summary>
        /// UDATA tag.
        /// </summary>
        public const string UDATA = "udata";
    }
}
