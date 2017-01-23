////********************************************************************
// <copyright file="ContextLevels.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This enumeration lists the various contexts where menu can appear.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System;

    /// <summary>
    /// This enumeration lists the various contexts where menu can appear.
    /// </summary>
    [Flags]
    public enum ContextLevels
    {
        /// <summary>
        /// This is for None
        /// </summary>
        None = 0,

        /// <summary>
        /// In context of Solution
        /// </summary>
        Solution = 1,

        /// <summary>
        /// In context of Project
        /// </summary>
        Project = 2,

        /// <summary>
        /// In context of solution folder
        /// </summary>
        SolutionFolder = 4,

        /// <summary>
        /// In context of Folder
        /// </summary>
        Folder = 8,

        /// <summary>
        /// In context of referecnes
        /// </summary>
        References = 32,

        /// <summary>
        /// In context of Web Referecnes 
        /// </summary>
        WebReferences = 64,

        /// <summary>
        /// In context of Assembly Info File
        /// </summary>
        AssemblyInfo = 128,

        /// <summary>
        /// In context of project item
        /// </summary>
        Item = 256,

        /// <summary>
        /// In Main Menu bar
        /// </summary>
        MenuBar = 512,

        /// <summary>
        /// In context of Code Window.
        /// </summary>
        CodeWindow = 1024,

        /// <summary>
        /// In context of Html Editor window.
        /// </summary>
        HtmlContext = 1027,

        /// <summary>
        /// In context of Aspx page Editor window.
        /// </summary>
        AspxContext = 1028,

        /// <summary>
        /// In Context of HTML source Editor window.
        /// </summary>
        HtmlSourceEditing = 1029,

        /// <summary>
        /// In context of Text editor window.
        /// </summary>
        TextEditor = 1030,

        /// <summary>
        /// In context of Script editor window.
        /// </summary>
        ScriptContext = 1033,

        /// <summary>
        /// In context of any other item not list listed here.
        /// </summary>
        OtherItem = 536870912
    }
}