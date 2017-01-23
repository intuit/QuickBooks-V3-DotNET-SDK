////********************************************************************
// <copyright file="Common.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains common logic for both ASP.net and MVC web projects.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using EnvDTE;
    using Microsoft.VisualStudio.ComponentModelHost;
    using Microsoft.VisualStudio.Shell.Interop;
    using NuGet.VisualStudio;
    using VSLangProj;
    using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
    using System.Text;

    /// <summary>
    /// This class handles common logic for both ASP.net and MVC web projects.
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// Adds the project referecne.
        /// </summary>
        /// <param name="activeProject">The active project.</param>
        /// <param name="path">The path of code files folder</param>
        internal static void AddProjectReferecne(VSProject activeProject, string path)
        {
            if (activeProject.References.Find("DevDefined.OAuth") == null)
            {
                activeProject.References.Add(path + "DevDefined.OAuth.dll");
            }

            if (activeProject.References.Find("DotNetOpenAuth") == null)
            {
                activeProject.References.Add(path + "DotNetOpenAuth.dll");
            }
        }

        /// <summary>
        /// Adds the data services SDK referecnes.
        /// </summary>
        /// <param name="activeProject">The active project.</param>
        /// <param name="path">The path of code files folder</param>
        internal static void AddDataServicesSdkReferecne(VSProject activeProject, string path)
        {
            // Add the data servcies SDK referecne
            if (activeProject.References.Find("DevDefined.OAuth") == null)
            {
                activeProject.References.Add(path + "DevDefined.OAuth.dll");
            }

            if (activeProject.References.Find("DotNetOpenAuth") == null)
            {
                activeProject.References.Add(path + "DotNetOpenAuth.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Utility") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Utility.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Security") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Security.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Services") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Services.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Retry") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Retry.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.PlatformServices") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.PlatformServices.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Exception") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Exception.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Diagnostics") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Diagnostics.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Data") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Data.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.Core") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.Core.dll");
            }

            if (activeProject.References.Find("Intuit.Ipp.AsyncServices") == null)
            {
                activeProject.References.Add(path + "Intuit.Ipp.AsyncServices.dll");
            }
        }

        /// <summary>
        /// Gets the configuration file list.
        /// </summary>
        /// <returns> list of configuration files. </returns>
        internal static List<string> GetConfigurationFileList()
        {
            // This list is currently fixed.Based on the requirement in future, some logic is added here to fetch the list of files.
            List<string> configurationFiles = new List<string> { "Web.config", "web.config.staging" };
            return configurationFiles;
        }

        /// <summary>
        /// Adds the web configuration entries.
        /// </summary>
        /// <param name="proj">The project object.</param>
        /// <param name="appSettingEntries">The app setting entries.</param>
        /// <param name="projType">Type of the proj.</param>
        internal static void AddWebConfigurationEntries(Project proj, Dictionary<string, string> appSettingEntries, VSProjectType projType)
        {
            // Get the path of the config file
            List<string> configurationFiles = GetConfigurationFileList();
            foreach (string configFile in configurationFiles)
            {
                string prjItemPath = Path.GetDirectoryName(proj.FullName) + "\\" + configFile;
                if (File.Exists(prjItemPath))
                {
                    bool modified = false;
                    const string AppSettingStringXpath = "/configuration/appSettings";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(prjItemPath);

                    // Add appSettings node to the webconfig if node doesn't exit and then add required keys
                    XmlNode appSettingsNode = xmlDoc.SelectSingleNode(AppSettingStringXpath);
                    if (appSettingsNode == null)
                    {
                        appSettingsNode = xmlDoc.CreateNode(XmlNodeType.Element, "appSettings", null);
                        XmlNode selectSingleNode = xmlDoc.SelectSingleNode("/configuration");
                        if (selectSingleNode != null)
                        {
                            selectSingleNode.AppendChild(appSettingsNode);
                        }

                        modified = true;
                    }

                    foreach (var appSettingEntry in appSettingEntries)
                    {
                        // check if key is present 
                        string appSettingXpath = @"//add[@key='" + appSettingEntry.Key + "']";
                        if (appSettingsNode.SelectSingleNode(appSettingXpath) == null)
                        {
                            // create a new node to the <appSettings></appSettings>.
                            XmlNode newAddNode = xmlDoc.CreateNode(XmlNodeType.Element, "add", null);
                            XmlAttribute keyAttribute = xmlDoc.CreateAttribute("key");
                            keyAttribute.Value = appSettingEntry.Key;
                            XmlAttribute valueAttribute = xmlDoc.CreateAttribute("value");
                            valueAttribute.Value = appSettingEntry.Value;

                            // add the two attribute to the "add" element.
                            if (newAddNode.Attributes != null)
                            {
                                newAddNode.Attributes.Append(keyAttribute);
                                newAddNode.Attributes.Append(valueAttribute);
                            }

                            XmlComment comment = xmlDoc.CreateComment(IntuitAnywhereResources.comment004);
                            appSettingsNode.AppendChild(comment);
                            appSettingsNode.AppendChild(newAddNode);
                            modified = true;
                        }
                    }

                    if (projType == VSProjectType.AspNet)
                    {
                        // add http module for asp .net project
                        const string HttpModulesStringXpath = "/configuration/system.web/httpModules";
                        XmlNode httpModulesNode = xmlDoc.SelectSingleNode(HttpModulesStringXpath);
                        if (httpModulesNode == null)
                        {
                            httpModulesNode = xmlDoc.CreateNode(XmlNodeType.Element, "httpModules", null);
                            XmlNode selectSingleNode = xmlDoc.SelectSingleNode("/configuration/system.web");
                            if (selectSingleNode != null)
                            {
                                selectSingleNode.AppendChild(httpModulesNode);
                            }

                            modified = true;
                        }

                        // check if htttpIPP enty is present 
                        const string HtttpIppXpath = @"//add[@name='IppTag']";
                        if (httpModulesNode.SelectSingleNode(HtttpIppXpath) == null)
                        {
                            // create a new node to the <httpModules></httpModules>.
                            XmlNode newAddNode = xmlDoc.CreateNode(XmlNodeType.Element, "add", null);
                            XmlAttribute keyAttribute = xmlDoc.CreateAttribute("name");
                            keyAttribute.Value = "IppTag";
                            XmlAttribute valueAttribute = xmlDoc.CreateAttribute("type");
                            valueAttribute.Value = "IntuitSampleWebsite.utils.IppTag";

                            // add the two attribute to the "add" element.
                            if (newAddNode.Attributes != null)
                            {
                                newAddNode.Attributes.Append(keyAttribute);
                                newAddNode.Attributes.Append(valueAttribute);
                            }

                            XmlComment comment = xmlDoc.CreateComment(IntuitAnywhereResources.comment005);
                            httpModulesNode.AppendChild(comment);
                            httpModulesNode.AppendChild(newAddNode);
                            modified = true;
                        }
                    }

                    if (modified)
                    {
                        xmlDoc.Save(prjItemPath);
                    }
                }
            }

            // ask the user to enter the values for the first time.
            if (appSettingEntries.ContainsKey("applicationToken") && appSettingEntries.ContainsKey("consumerKey") && appSettingEntries.ContainsKey("consumerSecret"))
            {
                GetKeysFromtheUser(proj);
            }
        }

        /// <summary>
        /// Gets the key values from the user.
        /// </summary>
        /// <param name="proj"> The project object.</param>
        internal static void GetKeysFromtheUser(Project proj)
        {
            DataTable keys = new DataTable();
            keys.Locale = CultureInfo.InvariantCulture;
            DataColumn configFileName = new DataColumn("Configuration File Name");
            DataColumn applicationToken = new DataColumn("Application Token");
            DataColumn consumerKey = new DataColumn("Consumer Key");
            DataColumn consumerSecret = new DataColumn("Consumer Secret");
            DataColumn appName = new DataColumn("Application Name");
            keys.Columns.Add(configFileName);
            keys.Columns.Add(applicationToken);
            keys.Columns.Add(consumerKey);
            keys.Columns.Add(consumerSecret);
            keys.Columns.Add(appName);
            List<string> configurationFiles = GetConfigurationFileList();

            foreach (string configFile in configurationFiles)
            {
                string prjItemPath = Path.GetDirectoryName(proj.FullName) + "\\" + configFile;
                if (File.Exists(prjItemPath))
                {
                    const string AppSettingStringXpath = "/configuration/appSettings";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(prjItemPath);

                    // Add appSettings node to the webconfig if node doesn't exit and then add required keys
                    XmlNode appSettingsNode = xmlDoc.SelectSingleNode(AppSettingStringXpath);

                    // There is no possibility of appSettingsNode being null as this is added before calling this method.
                    XmlNode applicationTokenNode = appSettingsNode.SelectSingleNode(@"//add[@key='applicationToken']");
                    DataRow newConfigurationRow = keys.NewRow();
                    newConfigurationRow[0] = configFile;
                    if (applicationTokenNode != null)
                    {
                        if (applicationTokenNode.Attributes != null)
                        {
                            newConfigurationRow[1] = applicationTokenNode.Attributes["value"] == null ? string.Empty : applicationTokenNode.Attributes["value"].Value;
                        }
                    }

                    XmlNode consumerKeyNode = appSettingsNode.SelectSingleNode(@"//add[@key='consumerKey']");
                    if (consumerKeyNode != null)
                    {
                        if (consumerKeyNode.Attributes != null)
                        {
                            newConfigurationRow[2] = consumerKeyNode.Attributes["value"] == null ? string.Empty : consumerKeyNode.Attributes["value"].Value;
                        }
                    }

                    XmlNode consumerSecretNode = appSettingsNode.SelectSingleNode(@"//add[@key='consumerSecret']");
                    if (consumerSecretNode != null)
                    {
                        if (consumerSecretNode.Attributes != null)
                        {
                            newConfigurationRow[3] = consumerSecretNode.Attributes["value"] == null ? string.Empty : consumerSecretNode.Attributes["value"].Value;
                        }
                    }

                    XmlNode appNameNode = appSettingsNode.SelectSingleNode(@"//add[@key='openid_identifier']");
                    if (appNameNode != null)
                    {
                        if (appNameNode.Attributes != null)
                        {
                            string value = appNameNode.Attributes["value"] == null ? string.Empty : appNameNode.Attributes["value"].Value;
                            value = value.Substring(35);
                            newConfigurationRow[4] = value;
                        }
                    }

                    // Add the new row to the data table
                    keys.Rows.Add(newConfigurationRow);
                }
            }

            bool isAllKeyValuesPresent = true;
            foreach (DataRow datarow in keys.Rows)
            {
                if (string.IsNullOrEmpty(datarow[1].ToString()) || string.IsNullOrEmpty(datarow[2].ToString()) || string.IsNullOrEmpty(datarow[3].ToString()) || string.IsNullOrEmpty(datarow[4].ToString()) || datarow[4].ToString().Equals("YourAppName"))
                {
                    isAllKeyValuesPresent = false;
                }
            }

            if (!isAllKeyValuesPresent)
            {
                // show dialog to the user
                IntuitAnywhereConfigurationDialog configurationDialog = new IntuitAnywhereConfigurationDialog();
                configurationDialog.Keys = keys;
                configurationDialog.ShowDialog();
                
                // save the changes 
                if (configurationDialog.KeysModified)
                {
                    foreach (DataRow datarow in keys.Rows)
                    {
                        string prjItemPath = Path.GetDirectoryName(proj.FullName) + "\\" + datarow[0];
                        if (File.Exists(prjItemPath))
                        {
                            const string AppSettingStringXpath = "/configuration/appSettings";
                            var xmlDoc = new XmlDocument();
                            xmlDoc.Load(prjItemPath);

                            // Add appSettings node to the webconfig if node doesn't exit and then add required keys
                            XmlNode appSettingsNode = xmlDoc.SelectSingleNode(AppSettingStringXpath);
                            XmlNode applicationTokenNode = appSettingsNode.SelectSingleNode(@"//add[@key='applicationToken']");
                            XmlNode consumerKeyNode = appSettingsNode.SelectSingleNode(@"//add[@key='consumerKey']");
                            XmlNode consumerSecretNode = appSettingsNode.SelectSingleNode(@"//add[@key='consumerSecret']");
                            XmlNode appNameNode = appSettingsNode.SelectSingleNode(@"//add[@key='openid_identifier']");
                            applicationTokenNode.Attributes["value"].Value = datarow[1].ToString();
                            consumerKeyNode.Attributes["value"].Value = datarow[2].ToString();
                            consumerSecretNode.Attributes["value"].Value = datarow[3].ToString();
                            string value = datarow[4].ToString();
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                value = "https://openid.intuit.com/Identity-YourAppName";
                            }
                            else
                            {
                                // Check for Special Characters
                                if (CheckSpecialCharacters(value))
                                {
                                    value = string.Format("https://openid.intuit.com/Identity-{0}", value);
                                }
                                else
                                {
                                    value = "https://openid.intuit.com/Identity-YourAppName";
                                }
                            }

                            appNameNode.Attributes["value"].Value = value;
                            xmlDoc.Save(prjItemPath);
                        }
                    }

                    configurationDialog.KeysModified = false;
                    configurationDialog.Dispose();
                }
            }
        }

        /// <summary>
        /// Checks whether the string contains special characters.
        /// </summary>
        /// <param name="str">String value.</param>
        /// <returns>True if no special characters.</returns>
        private static bool CheckSpecialCharacters(string str)
        {
            bool result = true;
            //foreach (char c in str)
            //{
            //    if ((c <= '0' && c >= '9') || (c <= 'A' && c >= 'Z') || (c <= 'a' && c >= 'z') || c == '.' || c == '_')
            //    {
            //        result = false;
            //        break;
            //    }
            //}

            return result;
        }

        /// <summary>
        /// Adds the Nuget Package reference
        /// </summary>
        /// <param name="packageID">Package Id as hosted in NuGet.</param>
        /// <param name="project">Project to which the operation has to be performed.</param>
        /// <param name="repositoryPath">Repository path.</param>
        internal static void InstallNuGetPackageForProject(string packageID, Project project, string repositoryPath)
        {
            try
            {
                IComponentModel componentModel = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel)) as IComponentModel;
                var installer = componentModel.GetService<IVsPackageInstaller>();
                Version ver = null;
                installer.InstallPackage(repositoryPath, project, packageID, ver, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IntuitAnywhereResources.msg005, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// Gets the type of the project.
        /// </summary>
        /// <param name="proj">The project object.</param>
        /// <returns> Project type</returns>
        internal static VSProjectType GetProjectType(Project proj)
        {
            string projectTypeGuiDs = GetProjectTypeGuids(proj);
            switch (projectTypeGuiDs)
            {
                case
                    "{E53F8FEA-EAE0-44A6-8774-FFD645390401};{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}":
                    return VSProjectType.Mvc;
                case "{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}":
                    return VSProjectType.AspNet;
            }

            return VSProjectType.Other;
        }

        /// <summary>
        /// Gets the guids from the ProjectGuid tag within the project file.These can be used to identify type of project.
        /// </summary>
        /// <param name="proj">The project object.</param>
        /// <returns>string of project Guilds separated by semicolon</returns>
        internal static string GetProjectTypeGuids(Project proj)
        {
            string projectTypeGuids = string.Empty;
            object service = null;
            IVsSolution solution = null;
            IVsHierarchy hierarchy = null;
            int result = 0;

            service = GetService(proj.DTE, typeof(IVsSolution));
            solution = (IVsSolution)service;

            result = solution.GetProjectOfUniqueName(proj.UniqueName, out hierarchy);

            if (result == 0)
            {
                var aggregatableProject = (IVsAggregatableProject)hierarchy;
                result = aggregatableProject.GetAggregateProjectTypeGuids(out projectTypeGuids);
            }

            return projectTypeGuids;
        }

        /// <summary>
        /// Gets the visual studio service.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="type">The type of IVsSolution. </param>
        /// <returns> Service Object. </returns>
        internal static object GetService(object serviceProvider, Type type)
        {
            return GetService(serviceProvider, type.GUID);
        }

        /// <summary>
        /// Gets the visual studio service.
        /// </summary>
        /// <param name="serviceProviderObject">The service provider object.</param>
        /// <param name="guid">The GUID of the type </param>
        /// <returns> Service Object.</returns>
        internal static object GetService(object serviceProviderObject, Guid guid)
        {
            object service = null;
            IServiceProvider serviceProvider = null;
            IntPtr serviceIntPtr;
            int hr = 0;

            Guid sidGuid = guid;
            Guid iidGuid = sidGuid;
            serviceProvider = (IServiceProvider)serviceProviderObject;
            hr = serviceProvider.QueryService(sidGuid, iidGuid, out serviceIntPtr);

            if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            else if (!serviceIntPtr.Equals(IntPtr.Zero))
            {
                service = Marshal.GetObjectForIUnknown(serviceIntPtr);
                Marshal.Release(serviceIntPtr);
            }

            return service;
        }

        /// <summary>
        /// Determines whether project item exists
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="itemFound">The item found.</param>
        /// <returns>
        ///   <c>true</c> if project item exists ; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsProjectItemExist(ProjectItems items, string itemName, out ProjectItem itemFound)
        {
            for (int i = 1; i <= items.Count; i++)
            {
                ProjectItem projItem = items.Item(i);
                string projItemName = projItem.Name;
                if (projItemName.ToUpper(CultureInfo.InvariantCulture) == itemName.ToUpper(CultureInfo.InvariantCulture))
                {
                    itemFound = projItem;
                    return true;
                }
            }

            itemFound = null;
            return false;
        }

        /// <summary>
        /// Determines whether project item exists
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>
        ///   <c>true</c> if project item exists ; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsProjectItemExist(ProjectItems items, string itemName)
        {
            for (int i = 1; i <= items.Count; i++)
            {
                ProjectItem projItem = items.Item(i);
                string projItemName = projItem.Name;
                if (projItemName.ToUpper(CultureInfo.InvariantCulture) == itemName.ToUpper(CultureInfo.InvariantCulture))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the path string of CodeFiles Folder. This folder is copied by the installer during installtion of addin.
        /// </summary>
        /// <returns> Gets the path string of CodeFiles Folder. </returns>
        internal static string GetCodeFilePath()
        {
            string programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string codeFilePath = programFilesPath + @"\IntuitAnywhere\V3\CodeFiles\";
            return codeFilePath;
        }

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="path">The path of tag files. </param>
        /// <param name="activeDocument">The active document.</param>
        /// <param name="javaScriptPath">The java script path.</param>
        internal static void AddTag(string commandType, string path, Document activeDocument, string javaScriptPath)
        {
            var textSelection = (TextSelection)activeDocument.Selection;

            // Remember the cursor location
            TextPoint originalAnchor = textSelection.AnchorPoint.CreateEditPoint();
            TextPoint originalActive = textSelection.ActivePoint.CreateEditPoint();
            string tag = string.Empty;
            string javaScriptTag = File.ReadAllText(javaScriptPath + "JavaScriptTag.txt");

            // Add java script tag if not already present on the page
            activeDocument.DTE.Find.FindWhat = "intuit.ipp.anywhere.js";
            activeDocument.DTE.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
            activeDocument.DTE.Find.MatchWholeWord = true;
            activeDocument.DTE.Find.Action = vsFindAction.vsFindActionFind;

            if (activeDocument.DTE.Find.Execute() != vsFindResult.vsFindResultFound)
            {
                tag = javaScriptTag + Environment.NewLine;
            }

            // Logic to prevent the duplication of <script> tag on same aspx page
            if (commandType == "ConnecttoQuickBooks" || commandType == "BlueDot" || commandType == "DirectConnectToIntuit")
            {
                activeDocument.DTE.Find.FindWhat = "intuit.ipp.anywhere.setup";
                activeDocument.DTE.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
                activeDocument.DTE.Find.MatchWholeWord = true;
                activeDocument.DTE.Find.Action = vsFindAction.vsFindActionFind;

                if (activeDocument.DTE.Find.Execute() == vsFindResult.vsFindResultFound)
                {
                    // add tag without <Script> tag
                    tag = tag + File.ReadAllText(path + commandType + "Tag.txt");
                }
                else
                {
                    tag = tag + File.ReadAllText(path + "SetupTag.txt") + Environment.NewLine;
                    tag = tag + File.ReadAllText(path + commandType + "Tag.txt");
                }
            }
            else
            {
                tag = tag + File.ReadAllText(path + commandType + "Tag.txt");
            }

            // Restore original selection 
            textSelection.MoveToAbsoluteOffset(originalAnchor.AbsoluteCharOffset);
            textSelection.SwapAnchor();
            textSelection.MoveToAbsoluteOffset(originalActive.AbsoluteCharOffset, true);

            // inser tag
            textSelection.Insert(tag);
        }

        /// <summary>
        /// Adds the disconnect tag.
        /// </summary>
        /// <param name="commandtype">The commandtype.</param>
        /// <param name="path">The path of disconnect tag file.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddDisconnectTag(string commandtype, string path, Document activeDocument)
        {
            string tag = File.ReadAllText(path + commandtype + "Tag.txt");
            var textSelection = (TextSelection)activeDocument.Selection;
            textSelection.Insert(tag);
        }

        /// <summary>
        /// Gets the Icon Image
        /// </summary>
        /// <param name="menuIcon"> Icon object to be displayed as image. </param>
        /// <returns> stdole.IPictureDisp type object. </returns>
        internal static stdole.IPictureDisp GetImage(Bitmap menuIcon)
        {
            stdole.IPictureDisp tempImage = null;
            tempImage = ConvertImage.Convert(menuIcon);
            return tempImage;
        }

        /// <summary>
        /// Gets the Icon Image
        /// </summary>
        /// <param name="menuIcon"> Icon object to be displayed as image.</param>
        /// <returns>
        /// stdole.IPictureDisp type object.
        /// </returns>
        internal static stdole.IPictureDisp GetImage(Icon menuIcon)
        {
            stdole.IPictureDisp tempImage = null;
            ImageList imageList = new ImageList();
            imageList.Images.Add(menuIcon);
            tempImage = ConvertImage.Convert(imageList.Images[0]);
            return tempImage;
        }

        /// <summary>
        /// Adds constant values to config.
        /// </summary>
        /// <param name="appSettingsEntries">Application Settings.</param>
        internal static void AddConstantsToConfig(Dictionary<string, string> appSettingsEntries)
        {
            appSettingsEntries.Add("Url_Request_Token", "/get_request_token");
            appSettingsEntries.Add("Url_Access_Token", "/get_access_token");
            appSettingsEntries.Add("Intuit_OAuth_BaseUrl", "https://oauth.intuit.com/oauth/v1");
            appSettingsEntries.Add("Intuit_Workplace_AuthorizeUrl", "https://workplace.intuit.com/Connect/Begin");
            appSettingsEntries.Add("BlueDot_AppMenuUrl", "https://workplace.intuit.com/api/v1/Account/AppMenu");
            appSettingsEntries.Add("DisconnectUrl", "https://appcenter.intuit.com/api/v1/Connection/Disconnect");
        }
    }
}