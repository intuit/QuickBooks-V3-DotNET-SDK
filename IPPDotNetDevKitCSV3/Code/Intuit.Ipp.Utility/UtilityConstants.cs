////*********************************************************
// <copyright file="UtilityConstants.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
