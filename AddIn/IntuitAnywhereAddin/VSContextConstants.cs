////********************************************************************
// <copyright file="VSContextConstants.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This class contains constants used for various visual studio elements.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    /// <summary>
    /// This class contains constants used for various visual studio elements.
    /// </summary>
    public static class VSContextConstants
    {
        /// <summary> The VS Constant Name for a VS Solution </summary>
        public const string Solution = "Solution";

        /// <summary> The VS Constant Name for a VS Project </summary>
        public const string Project = "Project";

        /// <summary> The VS Constant Name for a VS Solution folder </summary>
        public const string SolutionFolder = "Solution Folder";

        /// <summary> The VS Constant Name for a VS Project Folder</summary>
        public const string Folder = "Folder";

        /// <summary> The VS Constant Name for a Reference root folder </summary>
        public const string References = "Reference Root";

        /// <summary> The VS Constant Name for a Web Reference folder </summary>
        public const string WebReferences = "Web Reference Folder"; 

        /// <summary> The VS Constant Name for an AssemblyInfo.cs file</summary>
        public const string AssemblyInfo = "Item";

        /// <summary> The VS Constant Name for a VS ProjectItem </summary>
        public const string Item = "Item";

        /// <summary> The VS Constant Name for all other items </summary>
        public const string OtherItem = "";

        /// <summary>command bar constant for code window </summary>
        public const string CodeWindow = "Code Window";

        /// <summary>command bar constant for main menu bar</summary>
        public const string MenuBar = "MenuBar";

        /// <summary>
        /// command bar constant for HTML Editor
        /// </summary>
        public const string HtmlContext = "HTML Context";

        /// <summary>
        /// command bar constant for Aspx Editor
        /// </summary>
        public const string AspxContext = "ASPX Context";

        /// <summary>
        /// command bar constant for Html source editor
        /// </summary>
        public const string HtmlSourceEditing = "HTML Source Editing";

        /// <summary>
        /// command bar constant for text editor
        /// </summary>
        public const string TextEditor = "Text Editor";

        /// <summary>
        /// command bar constant for script editor
        /// </summary>
        public const string ScriptContext = "Script Context";
    }
}