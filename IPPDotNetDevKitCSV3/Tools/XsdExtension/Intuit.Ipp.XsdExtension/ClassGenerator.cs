////*********************************************************
// <copyright file="ClassGenerator.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File contains class which is use to generate
//  cs files from memory stream    
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using Microsoft.CSharp;

    /// <summary>
    /// Generates class from memory stream which contains parsed xsd schema
    /// </summary>
    internal class ClassGenerator : IXsdExtensionTask
    {
        /// <summary>
        /// Executes operation which would generate class from memory stream which contains parsed xsd schema 
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext"> Holds codeDomTree </param>
        public void Execute(XsdContext xsdContext, CodeDomContext codeDomContext)
        {
            // Check for invalid characters in identifiers
            CodeGenerator.ValidateIdentifiers(codeDomContext.CodeNamespace);

            // output the C# code
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            CodeCompileUnit cu = new CodeCompileUnit();

            codeDomContext.CodeNamespace.Imports.Add(new CodeNamespaceImport("Newtonsoft.Json"));
            
            cu.Namespaces.Add(codeDomContext.CodeNamespace);

            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                codeProvider.GenerateCodeFromNamespace(codeDomContext.CodeNamespace, writer, new CodeGeneratorOptions());

                // Console.WriteLine(writer.GetStringBuilder().ToString());
                StreamWriter outfile = new StreamWriter(this.GetOutputFilePath(), false);
                outfile.Write(writer.GetStringBuilder().ToString());
                outfile.Flush();
                outfile.Close();
            }
        }

        /// <summary>
        /// Retrieves output path to place generated class. If not found uses default path
        /// </summary>        
        /// <returns>path to place generated class files</returns>
        private string GetOutputFilePath()
        {
            string outputfilePath = string.Empty;

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[DataObjectConstants.OUTPUTCLASSFILEFOLDER]))
            {
                outputfilePath = IOHelper.GetDataObjectProjectPath() + "\\" + DataObjectConstants.FMSOUTPUTFILE;
            }
            else
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings[DataObjectConstants.OUTPUTCLASSFILEFOLDER]))
                {
                    throw new FileNotFoundException(DataObjectConstants.OUTPUTDIRNOTFOUND);
                }

                outputfilePath = ConfigurationManager.AppSettings[DataObjectConstants.OUTPUTCLASSFILEFOLDER] + "\\" + DataObjectConstants.FMSOUTPUTFILE;
            }

            return outputfilePath;
        }
    }
}
