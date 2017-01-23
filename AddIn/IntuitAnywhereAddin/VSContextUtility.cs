////********************************************************************
// <copyright file="VSContextUtility.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This class converts The ContextLevels enum to a string constants recognized by Visual Studio.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    /// <summary>
    /// This class converts The ContextLevels enum to a string constants recognized by Visual Studio.
    /// </summary>
    public static class VSContextUtility
    {
        /// <summary>
        /// Converts The ContextLevels enum to a string constants recognized by Visual Studio.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>string recognized by visula studio </returns>
        public static string ContextToVSContext(ContextLevels level)
        {
            switch (level)
            {
                case ContextLevels.Solution:
                    return VSContextConstants.Solution;
                case ContextLevels.Project:
                    return VSContextConstants.Project;
                case ContextLevels.SolutionFolder:
                    return VSContextConstants.SolutionFolder;
                case ContextLevels.References:
                    return VSContextConstants.References;
                case ContextLevels.Item:
                    return VSContextConstants.Item;
                case ContextLevels.WebReferences:
                    return VSContextConstants.WebReferences;
                case ContextLevels.Folder:
                    return VSContextConstants.Folder;
                case ContextLevels.MenuBar:
                    return VSContextConstants.MenuBar;
                case ContextLevels.CodeWindow:
                    return VSContextConstants.CodeWindow;
                case ContextLevels.AspxContext:
                    return VSContextConstants.AspxContext;
                case ContextLevels.HtmlContext:
                    return VSContextConstants.HtmlContext;
                case ContextLevels.HtmlSourceEditing:
                    return VSContextConstants.HtmlSourceEditing;
                case ContextLevels.ScriptContext:
                    return VSContextConstants.ScriptContext;

                default:
                    return VSContextConstants.OtherItem;
            }
        }
    }
}