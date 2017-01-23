////*********************************************************
// <copyright file="IAppInfoHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  This file contains interface definition of AppInfo tag Handler
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;

    /// <summary>
    /// Defines methods to generate handle AppInfo tag
    /// </summary>
    public interface IAppInfoHandler
    {
        /// <summary>
        /// Executes appropriate actions based on implementation class
        /// </summary>
        /// <param name="memberProperty">Member of particualr class in which AppInfo is available</param>
        /// <param name="appInfoValue">value of AppInfo tag</param>
        void HandleAppInfo(CodeMemberProperty memberProperty, string appInfoValue);
    }
}
