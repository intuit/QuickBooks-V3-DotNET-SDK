// <copyright file="SchemaLoader.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File contains class definition which loads all xsd schmema files
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Xml.Schema;
    using Intuit.Ipp.Diagnostics;

    /// <summary>
    /// Holds operations related to Schema loading
    /// </summary>
    internal class SchemaLoader : IXsdExtensionTask
    {
        /// <summary>
        /// Loads xsd schema files into memory and compiles.
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext">Holds code dom tree object</param>
        public void Execute(XsdContext xsdContext, CodeDomContext codeDomContext)
        {
            TraceLogger target = new TraceLogger();
            XmlSchema xmlSchema = null;

            // Get XSD files
            string[] xsdFiles = null;
            string configKey = DataObjectConstants.INPUTSCHEMAFOLDER;

            // Get QBO xsd files from specified directory
            // if path is NOT specified in the config, use default location
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[configKey]))
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings[configKey]))
                {
                    throw new FileNotFoundException(DataObjectConstants.XSDDIRINVALID);
                }

                target.Log(TraceLevel.Verbose, DataObjectConstants.FILESFROM + ConfigurationManager.AppSettings[configKey]);
                xsdFiles = Directory.GetFiles(ConfigurationManager.AppSettings[configKey], DataObjectConstants.FILESEXTENSIONS, SearchOption.AllDirectories);
            }
            else
            {
                string defaultXsdFilePath = this.GetXSDPath();
                target.Log(TraceLevel.Verbose, DataObjectConstants.FILESFROM + defaultXsdFilePath);
                xsdFiles = Directory.GetFiles(defaultXsdFilePath, DataObjectConstants.FILESEXTENSIONS, SearchOption.AllDirectories);
            }

            if (xsdFiles.Length == 0)
            {
                target.Log(TraceLevel.Verbose, DataObjectConstants.XSDFILESNOTFOUND);
            }

            // Read .xsd files and create XmlSchema List
            foreach (string xsdFilePath in xsdFiles)
            {
                target.Log(TraceLevel.Verbose, DataObjectConstants.XSDFILESLOADEDFROM + xsdFilePath);
                using (FileStream stream = new FileStream(xsdFilePath, FileMode.Open, FileAccess.Read))
                {
                    xmlSchema = XmlSchema.Read(stream, null);
                    xsdContext.XmlSchemaList.Add(xmlSchema);
                }

                // Console.WriteLine("xsd.IsCompiled {0} {1}", xsdPath, xmlSchema.IsCompiled);
                xsdContext.XmlSchemas.Add(xmlSchema);
            }

            xsdContext.XmlSchemas.Compile(null, true);
        }

        /// <summary>
        /// Retrieves Xsd schema file paths from Config. If not available, returns default paths
        /// </summary>
        /// <returns>Xsd schema file path</returns>
        private string GetXSDPath()
        {
            string xsdPath = string.Empty;
            string appPath = string.Empty;

            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            xsdPath = appPath + "\\" + DataObjectConstants.DEFAULTFOLDERNAME;

            if (!Directory.Exists(xsdPath))
            {
                appPath = this.RemoveBinPath(xsdPath);
                xsdPath = appPath + "\\" + DataObjectConstants.DEFAULTFOLDERNAME;
            }

            return xsdPath;
        }

        /// <summary>
        /// Removes bin folder path from given foler path
        /// </summary>
        /// <param name="folderPath">folder path in which "bin" will be removed</param>
        /// <returns>folder path without bin directory</returns>
        private string RemoveBinPath(string folderPath)
        {
            return folderPath.Substring(0, folderPath.LastIndexOf("bin", System.StringComparison.OrdinalIgnoreCase) - 1);
        }
    }
}
