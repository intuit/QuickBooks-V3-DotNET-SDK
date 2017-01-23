// <copyright file="CodeDOMTreeBuilder.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File holds class which builds CodeDOM tree object
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
   /// Build CodeDom tree in memory
   /// </summary>
    internal class CodeDomTreeBuilder : IXsdExtensionTask
    {
        /// <summary>
        /// Performs building codedom tree objects from Xsd schema
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext">Holds codeDomTree</param>
        public void Execute(XsdContext xsdContext, CodeDomContext codeDomContext)
        {            
             XmlSchemaImporter schemaImporter = new XmlSchemaImporter(xsdContext.XmlSchemas);
            
            // Step 1:  Create code namespace            
             xsdContext.CodeExporter = new XmlCodeExporter(codeDomContext.CodeNamespace);

            List<object> maps = new List<object>();

            // Find out schema types of objects and add to collection
            foreach (XmlSchema xsd in xsdContext.XmlSchemaList)
            {
                foreach (XmlSchemaType schemaType in xsd.SchemaTypes.Values)
                {
                    maps.Add(schemaImporter.ImportSchemaType(schemaType.QualifiedName));
                }

                foreach (XmlSchemaElement schemaElement in xsd.Elements.Values)
                {
                    maps.Add(schemaImporter.ImportTypeMapping(schemaElement.QualifiedName));
                }
            }

            // export object collection to namespace. Now defined namespace will have all schema objects such as complexTypes, simpleTypes etc.
            foreach (XmlTypeMapping map in maps)
            {
                xsdContext.CodeExporter.ExportTypeMapping(map);
            }
        }
    }
}
