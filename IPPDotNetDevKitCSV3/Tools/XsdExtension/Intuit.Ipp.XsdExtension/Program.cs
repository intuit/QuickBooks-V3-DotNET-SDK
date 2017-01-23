// <copyright file="Program.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  Entry point to create POCO classes
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System;
    using System.Collections.Generic;
    using Intuit.Ipp.Diagnostics;

    /// <summary>
    /// Entry class for POCO class generation process
    /// </summary>
    [CLSCompliant(true)]
    public class Program
    {
        /// <summary>
        /// Entry point to perform class generation from xsd schema
        /// </summary>
        /// <param name="args">to be passed from command line</param>
        public static void Main(string[] args)
        {
            TraceLogger target = new TraceLogger();
            Console.WriteLine("V3 QuickBooks APIs class generation started..");
            target.Log(TraceLevel.Verbose, "V3 QuickBooks APIs class generation started..");
            GenerateFMSEntities();
            Console.WriteLine("V3 QuickBooks APIs class generation completed..");
            target.Log(TraceLevel.Verbose, "V3 QuickBooks APIs class generation completed..");
            Console.ReadLine();
        }

        /// <summary>
        /// Facade method to generate classes from xsd for QBO
        /// </summary>
        private static void GenerateFMSEntities()
        {
            // Create xsd context objects
            XsdContext xsdContextQbo = new XsdContext();

            // Create codedom context objects for QBO           
            CodeDomContext codedomContextQbo = new CodeDomContext(DataObjectConstants.FMSENTITYNAMESPACE);

            TraceLogger target = new TraceLogger();

            try
            {
                // create all tasks objects
                List<IXsdExtensionTask> xsdTasks = new List<IXsdExtensionTask>();
                IXsdExtensionTask loadTask = new SchemaLoader();
                IXsdExtensionTask domTreeTask = new CodeDomTreeBuilder();
                IXsdExtensionTask annotationTask = new SchemaTagHandler();
                IXsdExtensionTask hierarchyManager = new HierarchyManager();
                IXsdExtensionTask classGenerator = new ClassGenerator();

                // Add to xsdTasks to execute. It will be executed the same order.
                xsdTasks.Add(loadTask);
                xsdTasks.Add(domTreeTask);
                xsdTasks.Add(annotationTask);
                xsdTasks.Add(hierarchyManager);
                xsdTasks.Add(classGenerator);

                // Execute each tasks for QBO 
                foreach (IXsdExtensionTask xsdTask in xsdTasks)
                {
                    // QBO tasks execution
                    Console.WriteLine(xsdTask.GetType().FullName + " started..");
                    target.Log(TraceLevel.Verbose, xsdTask.GetType().FullName + " started..");

                    xsdTask.Execute(xsdContextQbo, codedomContextQbo);

                    Console.WriteLine(xsdTask.GetType().FullName + " completed");
                    target.Log(TraceLevel.Verbose, xsdTask.GetType().FullName + " completed..");
                }
            }
            catch (System.Xml.Schema.XmlSchemaException schemaException)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Error:");
                Console.WriteLine(schemaException.Message);
                target.Log(TraceLevel.Error, schemaException.Message);
                Console.WriteLine("---------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Error:");
                Console.WriteLine(ex.Message);
                target.Log(TraceLevel.Error, ex.Message);
                Console.WriteLine("---------------------------");
            }
        }
    }
}
