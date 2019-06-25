// <copyright file="IOHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  Helper file to handle IO related operations
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.IO;

    /// <summary>
    /// Helper class to handle IO related operations
    /// </summary>
    internal static class IOHelper
    {
        /// <summary>
        /// Retrieves path of Intuit.Ipp.Data project 
        /// </summary>
        /// <returns>path of Intuit.Ipp.Data project</returns>
        public static string GetDataObjectProjectPath()
        {
            DirectoryInfo dir = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            string rootPath = dir.Parent.Parent.Parent.Parent.FullName;
            rootPath = rootPath + DataObjectConstants.DATAPROJECTPATH;
            return rootPath;
        }
    }
}
