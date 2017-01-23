////*********************************************************
// <copyright file="IXSDExtensionTask.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  This file contains interface definition of XSDExtension 
//  tasks
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines methods to generate Classes from Schema
    /// </summary>
   internal interface IXsdExtensionTask
    {     
        /// <summary>
        /// Executes appropriate actions based on implementation class
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext">Holds codeDomTree</param>
        void Execute(XsdContext xsdContext, CodeDomContext codeDomContext);
    }
}
