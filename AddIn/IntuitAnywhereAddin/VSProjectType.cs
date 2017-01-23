////********************************************************************
// <copyright file="VSProjectType.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This enumeration lists project types used by addin to identify ASP.net and MVC project type.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    /// <summary>
    /// This enumeration lists various project types
    /// </summary>
    public enum VSProjectType
    {
        /// <summary>
        /// MVC project
        /// </summary>
        Mvc = 0,

        /// <summary>
        /// ASP>net project
        /// </summary>
        AspNet = 1,

        /// <summary>
        /// other project types
        /// </summary>
        Other = 2
    }
}