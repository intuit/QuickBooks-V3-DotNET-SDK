////********************************************************************
// <copyright file="ASPHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains logic adding IPP widgets for ASP.net project.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System.Collections.Generic;
    using EnvDTE;
    using VSLangProj;

    /// <summary>
    /// This file contains logic adding IPP widgets for ASP.net project.
    /// </summary>
    internal static class AspHandler
    {
        #region ASP.Net related Methods

        /// <summary>
        /// Adds the utils to aspnet project.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="aspnetCodeFilePath">The aspnet code file path.</param>
        internal static void AddUtilsToAspnetProject(Project selProject, string aspnetCodeFilePath)
        {
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "openid.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "openid.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "ApplicationConstants.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "ApplicationConstants.cs");
            }

            // IppTag.cs
            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "IppTag.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "IppTag.cs");
            }
        }

        /// <summary>
        /// Adds the command related aspx files.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="aspnetCodeFilePath">The aspnet code file path.</param>
        internal static void AddCommandRelatedAspxFiles(Project selProject, string commandType, string aspnetCodeFilePath)
        {
            if (commandType == "ConnecttoQuickBooks")
            {
                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthGrant.aspx"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthGrant.aspx");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthHandler.aspx"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthHandler.aspx");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "CryptographyHelper.cs"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "CryptographyHelper.cs");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorageHelper.cs");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorage.xml");
                }
            }

            if (commandType == "OpenID")
            {
                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OpenIdHandler.aspx"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OpenIdHandler.aspx");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "CryptographyHelper.cs"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "CryptographyHelper.cs");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorageHelper.cs");
                }

                if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorage.xml");
                }
            }

            if (commandType == "BlueDot")
            {
                if (!Common.IsProjectItemExist(selProject.ProjectItems, "MenuProxy.aspx"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "MenuProxy.aspx");
                }
            }

            if (commandType == "Logout")
            {
                if (!Common.IsProjectItemExist(selProject.ProjectItems, "Logout.aspx"))
                {
                    selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "Logout.aspx");
                }
            }

        }

        /// <summary>
        /// Adds the quick book customer files for ASP.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="codeFilePath">The code file path.</param>
        internal static void AddQuickBookCustomerFilesForAsp(Project selProject, string codeFilePath)
        {
            string aspnetCodeFilePath = codeFilePath + AddinConstants.ASPNETContentFolder;

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "QuickBooksCustomers.aspx"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "QuickBooksCustomers.aspx");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "CryptographyHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorage.xml");
            }

            // Add Util
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "Initializer.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "Initializer.cs");
            }

            var appSettingsEntries = new Dictionary<string, string>();
            appSettingsEntries.Add("securityKey", "YourSecretKey");

            // Add the entries in web configurarion file
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.AspNet);
        }

        /// <summary>
        /// Adds the quick book customer v2 files for ASP.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="codeFilePath">The code file path.</param>
        internal static void AddQuickBookCustomerV2FilesForAsp(Project selProject, string codeFilePath)
        {
            string aspnetCodeFilePath = codeFilePath + AddinConstants.ASPNETContentFolder;

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "CryptographyHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorage.xml");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "QuickBooksCustomers.aspx"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "v2\\" + "QuickBooksCustomers.aspx");
            }

            // Add Util
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "Initializer.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "v2\\" + "Initializer.cs");
            }

            var appSettingsEntries = new Dictionary<string, string>();
            appSettingsEntries.Add("securityKey", "YourSecretKey");

            // Add the entries in web configurarion file
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.AspNet);
        }

        /// <summary>
        /// Adds the source to aspnet project.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="codeFilePath">The code file path.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddSourceToAspnetProject(Project selProject, string commandType, string codeFilePath, Document activeDocument)
        {
            string aspnetCodeFilePath = codeFilePath + AddinConstants.ASPNETContentFolder;

            // Add the referecne to the project 
            var activeProject = (VSProject)selProject.Object;

            // add referecne
            Common.AddProjectReferecne(activeProject, aspnetCodeFilePath);
            var appSettingsEntries = new Dictionary<string, string>();
            if (commandType == "ConnecttoQuickBooks")
            {
                appSettingsEntries.Add("securityKey", "YourSecretKey");
                appSettingsEntries.Add("oauth_callback_url", @"/OauthHandler.aspx");
                appSettingsEntries.Add("applicationToken", string.Empty);
                appSettingsEntries.Add("grantUrl", @"OauthGrant.aspx");
                appSettingsEntries.Add("menuProxy", @"MenuProxy.aspx");
                appSettingsEntries.Add("qbo_base_url", @"https://qbo.intuit.com/qbo1/rest/user/v2/");
                appSettingsEntries.Add("consumerKey", string.Empty);
                appSettingsEntries.Add("consumerSecret", string.Empty);
                appSettingsEntries.Add("openid_identifier", "https://openid.intuit.com/Identity-YourAppName");
                Common.AddConstantsToConfig(appSettingsEntries);
            }

            if (commandType == "OpenID")
            {
                appSettingsEntries.Add("openid_identifier", "https://openid.intuit.com/Identity-YourAppName");
                appSettingsEntries.Add("applicationToken", string.Empty);
                appSettingsEntries.Add("consumerKey", string.Empty);
                appSettingsEntries.Add("consumerSecret", string.Empty);
                Common.AddConstantsToConfig(appSettingsEntries);
            }

            if (commandType == "BlueDot")
            {
                appSettingsEntries.Add("grantUrl", @"OauthGrant.aspx");
                appSettingsEntries.Add("menuProxy", @"MenuProxy.aspx");
                Common.AddConstantsToConfig(appSettingsEntries);
            }

            Common.AddTag(commandType, codeFilePath, activeDocument, codeFilePath);

            // add util
            AddUtilsToAspnetProject(selProject, aspnetCodeFilePath);

            // add related aspx files
            AddCommandRelatedAspxFiles(selProject, commandType, aspnetCodeFilePath);

            // Add the entries in web configurarion file
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.AspNet);
        }

        /// <summary>
        /// Adds the direct connect to intuit functionality.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="codeFilePath">The code file path.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddDirectConnectToIntuitFunctionality(string commandType, string codeFilePath, Document activeDocument)
        {
            string aspnetCodeFilePath = codeFilePath + AddinConstants.ASPNETContentFolder;
            Common.AddTag(commandType, aspnetCodeFilePath, activeDocument, codeFilePath);
        }

        /// <summary>
        /// Adds the disconnet functionality.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="codeFilePath">The code file path.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddDisconnetFunctionality(Project selProject, string commandType, string codeFilePath, Document activeDocument)
        {
            string aspnetCodeFilePath = codeFilePath + AddinConstants.ASPNETContentFolder;

            // Add the referecne to the project 
            var activeProject = (VSProject)selProject.Object;

            // add referecne
            Common.AddProjectReferecne(activeProject, aspnetCodeFilePath);

            // add required utils
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "ApplicationConstants.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "ApplicationConstants.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "CryptographyHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "OauthAccessTokenStorage.xml");
            }

            var appSettingsEntries = new Dictionary<string, string>();
            appSettingsEntries.Add("securityKey", "YourSecretKey");
            Common.AddConstantsToConfig(appSettingsEntries);
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.AspNet);

            // Addtag
            Common.AddDisconnectTag(commandType, aspnetCodeFilePath, activeDocument);

            // Add required files  
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Disconnect.aspx"))
            {
                selProject.ProjectItems.AddFromFileCopy(aspnetCodeFilePath + "Disconnect.aspx");
            }
        }

        #endregion
    }
}