// <copyright file="XsdContext.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File to hold context class
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Context class used to hold meta data on xsd schema files
    /// </summary>
    internal class XsdContext
    {
        /// <summary>
        /// Holds all Schema objects
        /// </summary>
        private XmlSchemas xmlSchemas = new XmlSchemas();

        /// <summary>
        /// List of Schema objects
        /// </summary>
        private List<XmlSchema> xmlSchemaList = new List<XmlSchema>();

        /// <summary>
        /// Exports compiled schema to code namespace
        /// </summary>
        private XmlCodeExporter codeExporter;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the XsdContext class
        /// </summary>
        public XsdContext()
        {
        }
        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets exporter to export code from xsd schema to codedom
        /// </summary>
        public XmlCodeExporter CodeExporter
        {
            get { return this.codeExporter; }
            set { this.codeExporter = value; }
        }

        /// <summary>
        /// Gets or sets list of Schema
        /// </summary>
        public List<XmlSchema> XmlSchemaList
        {
            get { return this.xmlSchemaList; }
            set { this.xmlSchemaList = value; }
        }

        /// <summary>
        /// Gets or sets xml schema
        /// </summary>
        public XmlSchemas XmlSchemas
        {
            get { return this.xmlSchemas; }
            set { this.xmlSchemas = value; }
        }

        #endregion
    }
}
