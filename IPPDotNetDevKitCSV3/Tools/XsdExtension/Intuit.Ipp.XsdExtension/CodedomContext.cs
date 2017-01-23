// <copyright file="CodedomContext.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File contains class to hold code dom tree   
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;

    /// <summary>
    /// context class to hold code dom
    /// </summary>
    internal class CodeDomContext
    {
        /// <summary>
        /// Variable to hold code dom
        /// </summary>
        private CodeNamespace codeNamespace;

        /// <summary>
        /// Output path of generated class files
        /// </summary>
        private string outputCodePath;

        /// <summary>
        /// Output generated class file name
        /// </summary>
        private string outputCodeFile;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CodeDomContext class with root namespace
        /// </summary>
        /// <param name="namespaceName">namespace name to initialize code dom</param>
        public CodeDomContext(string namespaceName)
        {
            this.codeNamespace = new CodeNamespace(namespaceName);
        }
        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets generated class file name
        /// </summary>
        public string OutputCodeFile
        {
            get { return this.outputCodeFile; }
            set { this.outputCodeFile = value; }
        }

        /// <summary>
        /// Gets or sets generated class file path
        /// </summary>
        public string OutputCodePath
        {
            get { return this.outputCodePath; }
            set { this.outputCodePath = value; }
        }

        /// <summary>
        /// Gets namespace of CodeDom object tree
        /// </summary>
        public CodeNamespace CodeNamespace
        {
            get { return this.codeNamespace; }
        }
        #endregion
    }
}
