// <copyright file="AppInfoIgnoreHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  Used to hold all constansts
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;

    /// <summary>
    /// Handles if Ignore keyword is specified in AppInfo tag
    /// </summary>
    internal class AppInfoIgnoreHandler : IAppInfoHandler
    {
        /// <summary>
        /// Handles if Ignore keyword is specified in AppInfo tag
        /// </summary>
        /// <param name="memberProperty">Member of particualr class in which AppInfo is available</param>
        /// <param name="appInfoValue">value of AppInfo tag</param>
        public void HandleAppInfo(System.CodeDom.CodeMemberProperty memberProperty, string appInfoValue)
        {
            if (string.Equals(appInfoValue.Trim(), "IGNORE", System.StringComparison.OrdinalIgnoreCase))                
            {
            // Add [XmlIgnore] attribute
            memberProperty.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Xml.Serialization.XmlIgnoreAttribute))));

            // Add [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] attribute
            CodeDomHelper.AddEditorBrowsableAttribute(memberProperty);
            }
        }
    }
}
