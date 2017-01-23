////********************************************************************
// <copyright file="MVCHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This class contains logic for adding IPP widgets for MVC project.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using EnvDTE;
    using VSLangProj;

    /// <summary>
    /// This class contains logic for adding IPP widgets for MVC project.
    /// </summary>
    internal static class MvcHandler
    {
        #region MVC related Helper methods
        /// <summary>
        /// Gets the controller folder.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <returns> Folder project item.</returns>
        internal static ProjectItem GetControllerFolder(Project selProject)
        {
            ProjectItem controllersFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Controllers", out controllersFolder))
            {
                controllersFolder = selProject.ProjectItems.AddFolder("Controllers");
            }

            return controllersFolder;
        }

        /// <summary>
        /// Gets the existing MVC items.
        /// </summary>
        /// <param name="controllersFolder">The controllers folder.</param>
        /// <param name="isConnectToQuickBookAlreadyThere">if set to <c>true</c> [is connect to quick book already there].</param>
        /// <param name="isLoginAlreadyThere">if set to <c>true</c> [is login already there].</param>
        /// <param name="isBlueDotMenyAlreadyThere">if set to <c>true</c> [is blue dot meny already there].</param>
        /// <param name="isDisconnetThere">if set to <c>true</c> [is disconnet there].</param>
        internal static void GetExistingMvcItems(ProjectItem controllersFolder, out bool isConnectToQuickBookAlreadyThere, out bool isLoginAlreadyThere, out bool isBlueDotMenyAlreadyThere, out bool isDisconnetThere, out bool isLogoutThere)
        {
            isConnectToQuickBookAlreadyThere = false;
            isLoginAlreadyThere = false;
            isBlueDotMenyAlreadyThere = false;
            isDisconnetThere = false;
            isLogoutThere = false;
            if (Common.IsProjectItemExist(controllersFolder.ProjectItems, "OauthGrantController.cs"))
            {
                isConnectToQuickBookAlreadyThere = true;
            }

            if (Common.IsProjectItemExist(controllersFolder.ProjectItems, "OpenIdController.cs"))
            {
                isLoginAlreadyThere = true;
            }

            if (Common.IsProjectItemExist(controllersFolder.ProjectItems, "MenuProxyController.cs"))
            {
                isBlueDotMenyAlreadyThere = true;
            }

            if (Common.IsProjectItemExist(controllersFolder.ProjectItems, "DisconnectController.cs"))
            {
                isDisconnetThere = true;
            }

            if (Common.IsProjectItemExist(controllersFolder.ProjectItems, "LogoutController.cs"))
            {
                isLogoutThere = true;
            }

        }

        /// <summary>
        /// Adds the controller items.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="controllersFolder">The controllers folder.</param>
        /// <param name="mvcCodeFilePath">The MVC code file path.</param>
        internal static void AddControllerItems(string commandType, ProjectItem controllersFolder, string mvcCodeFilePath)
        {
            if (commandType == "ConnecttoQuickBooks")
            {
                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "OauthGrantController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthGrantController.cs");
                }

                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "OauthResponseController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthResponseController.cs");
                }
            }

            if (commandType == "OpenID")
            {
                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "OpenIdController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OpenIdController.cs");
                }
            }

            if (commandType == "BlueDot")
            {
                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "MenuProxyController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "MenuProxyController.cs");
                }
            }

            if (commandType == "Disconnect")
            {
                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "DisconnectController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "DisconnectController.cs");
                }
            }

            if (commandType == "Logout")
            {
                if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "LogoutController.cs"))
                {
                    controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "LogoutController.cs");
                }
            }
        }

        /// <summary>
        /// Adds the quick book customer files for MVC.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="codeFilePath">The code file path.</param>
        internal static void AddQuickBookCustomerFilesForMvc(Project selProject, string codeFilePath)
        {
            string mvcCodeFilePath = codeFilePath + AddinConstants.MVCContentFolder;
            ProjectItem controllersFolder = GetControllerFolder(selProject);
            if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "QuickBooksCustomersController.cs"))
            {
                controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "QuickBooksCustomersController.cs");
            }

            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "Initializer.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "Initializer.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "CustomerData.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "CustomerData.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "oAuth.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "oAuth.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "CryptographyHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorage.xml");
            }

            var appSettingsEntries = new Dictionary<string, string>();
            appSettingsEntries.Add("securityKey", "YourSecretKey");

            // add configuration entries
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.Mvc);

            // Add related view
            AddViews(selProject, "QuickBooksCustomers", mvcCodeFilePath);

            // Add View for workplace Disconnect 
            AddViews(selProject, "CleanupOnDisconnect", mvcCodeFilePath);
        }

        /// <summary>
        /// Adds the quick book customer v2 files for MVC.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="codeFilePath">The code file path.</param>
        internal static void AddQuickBookCustomerV2FilesForMvc(Project selProject, string codeFilePath)
        {
            string mvcCodeFilePath = codeFilePath + AddinConstants.MVCContentFolder;
            ProjectItem controllersFolder = GetControllerFolder(selProject);
            if (!Common.IsProjectItemExist(controllersFolder.ProjectItems, "QuickBooksCustomersController.cs"))
            {
                controllersFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "v3\\" + "QuickBooksCustomersController.cs");
            }

            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "Initializer.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "v3\\" + "Initializer.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "CustomerData.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "CustomerData.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "oAuth.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "oAuth.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "CryptographyHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorage.xml");
            }

            var appSettingsEntries = new Dictionary<string, string>();
            appSettingsEntries.Add("securityKey", "YourSecretKey");

            // add configuration entries
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.Mvc);

            // Add related view
            AddViews(selProject, "QuickBooksCustomers", mvcCodeFilePath);

            // Add View for workplace Disconnect 
            AddViews(selProject, "CleanupOnDisconnect", mvcCodeFilePath);
        }

        /// <summary>
        /// Adds the MVC utils.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="mvcCodeFilePath">The MVC code file path.</param>
        internal static void AddMvcUtils(Project selProject, string mvcCodeFilePath)
        {
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "ApplicationConstants.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "ApplicationConstants.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "openid.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "openid.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "IppTag.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "IppTag.cs");
            }

            ProjectItem fileProjectItem;
            if (Common.IsProjectItemExist(utilFolder.ProjectItems, "IntuitRegisterRoutes.cs", out fileProjectItem))
            {
                fileProjectItem.Delete();
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "IntuitRegisterRoutes.cs");
            }
            else
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "IntuitRegisterRoutes.cs");
            }
        }

        /// <summary>
        /// Adds the views.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="mvcCodeFilePath">The MVC code file path.</param>
        internal static void AddViews(Project selProject, string folderName, string mvcCodeFilePath)
        {
            // Add View
            ProjectItem viewFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Views", out viewFolder))
            {
                viewFolder = selProject.ProjectItems.AddFolder("Views");
            }

            ProjectItem outhResponseFolder;
            if (!Common.IsProjectItemExist(viewFolder.ProjectItems, folderName, out outhResponseFolder))
            {
                outhResponseFolder = viewFolder.ProjectItems.AddFolder(folderName);
            }

            if (!Common.IsProjectItemExist(outhResponseFolder.ProjectItems, "Index.cshtml"))
            {
                outhResponseFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + folderName + @"\" + "Index.cshtml");
            }
        }

        /// <summary>
        /// Inserts the routes.
        /// </summary>
        /// <param name="utilFolder">The util folder.</param>
        /// <param name="mvcCodeFilePath">The MVC code file path.</param>
        /// <param name="isConnectToQuickBookAlreadyThere">if set to <c>true</c> [is connect to quick book already there].</param>
        /// <param name="isLoginAlreadyThere">if set to <c>true</c> [is login already there].</param>
        /// <param name="isBlueDotMenyAlreadyThere">if set to <c>true</c> [is blue dot meny already there].</param>
        /// <param name="isDisconnetThere">if set to <c>true</c> [is disconnet there].</param>
        internal static void InsertRoutes(ProjectItem utilFolder, string mvcCodeFilePath, bool isConnectToQuickBookAlreadyThere, bool isLoginAlreadyThere, bool isBlueDotMenyAlreadyThere, bool isDisconnetThere, bool isLogoutThere)
        {
            ProjectItem fileProjectItem;
            if (Common.IsProjectItemExist(utilFolder.ProjectItems, "IntuitRegisterRoutes.cs", out fileProjectItem))
            {
                CodeElement registerIntuitAnywhereRoutes = null;
                foreach (CodeElement codeElement in fileProjectItem.FileCodeModel.CodeElements)
                {
                    if (codeElement.Kind == vsCMElement.vsCMElementClass)
                    {
                        foreach (CodeElement classChildElelement in codeElement.Children)
                        {
                            if (classChildElelement.Kind == vsCMElement.vsCMElementFunction &&
                                classChildElelement.Name == "RegisterIntuitAnywhereRoutes")
                            {
                                registerIntuitAnywhereRoutes = classChildElelement;
                                break;
                            }
                        }
                    }
                }

                if (registerIntuitAnywhereRoutes != null)
                {
                    EditPoint ep = registerIntuitAnywhereRoutes.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
                    if (isConnectToQuickBookAlreadyThere == true)
                    {
                        string oauth = File.ReadAllText(mvcCodeFilePath + "oAuth.txt");
                        ep.Insert(oauth);
                    }

                    if (isLoginAlreadyThere)
                    {
                        string openId = File.ReadAllText(mvcCodeFilePath + "openID.txt");
                        ep.Insert(openId);
                    }

                    if (isBlueDotMenyAlreadyThere)
                    {
                        string blueDot = File.ReadAllText(mvcCodeFilePath + "blueDot.txt");
                        ep.Insert(blueDot);
                    }

                    if (isDisconnetThere)
                    {
                        string blueDot = File.ReadAllText(mvcCodeFilePath + "disconnect.txt");
                        ep.Insert(blueDot);
                    }

                    if (isLogoutThere)
                    {
                        string logout = File.ReadAllText(mvcCodeFilePath + "logout.txt");
                        ep.Insert(logout);
                    }

                }
            }
        }

        /// <summary>
        /// Inserts the method call in global ASAX file.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        internal static void InsertMethodCallInGlobalASAXFile(Project selProject)
        {
            ProjectItem fileProjectItem;
            bool registerRootsFound = false;
            bool registerGlobalFiltersFound = false;
            if (Common.IsProjectItemExist(selProject.ProjectItems, "Global.asax", out fileProjectItem))
            {
                CodeElement registerRoutes = null;
                CodeElement registerGlobalFilters = null;
                FileCodeModel codeModel = fileProjectItem.ProjectItems.Item(1).FileCodeModel;
                foreach (CodeElement codeElement in codeModel.CodeElements)
                {
                    if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                    {
                        foreach (CodeElement namespacechildElement in codeElement.Children)
                        {
                            if (namespacechildElement.Kind == vsCMElement.vsCMElementClass)
                            {
                                foreach (CodeElement functionElement in namespacechildElement.Children)
                                {
                                    if (functionElement.Kind == vsCMElement.vsCMElementFunction &&
                                        (functionElement.Name == "RegisterRoutes" ||
                                         functionElement.Name == "RegisterGlobalFilters"))
                                    {
                                        if (functionElement.Name == "RegisterRoutes")
                                        {
                                            registerRoutes = functionElement;
                                            registerRootsFound = true;
                                        }

                                        if (functionElement.Name == "RegisterGlobalFilters")
                                        {
                                            registerGlobalFilters = functionElement;
                                            registerGlobalFiltersFound = true;
                                        }

                                        if (registerRootsFound && registerGlobalFiltersFound)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (registerRoutes != null)
                {
                    EditPoint startEp = registerRoutes.GetStartPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
                    if (!startEp.FindPattern("IntuitRegisterRoutes.RegisterIntuitAnywhereRoutes(routes);"))
                    {
                        EditPoint endEP = registerRoutes.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
                        endEP.Insert(Environment.NewLine + Environment.NewLine +
                                     "IntuitRegisterRoutes.RegisterIntuitAnywhereRoutes(routes);");
                    }
                }

                if (registerGlobalFilters != null)
                {
                    EditPoint startEp = registerGlobalFilters.GetStartPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
                    if (!startEp.FindPattern("filters.Add(new IntuitSampleMVC.utils.IppTag());"))
                    {
                        EditPoint endEP = registerGlobalFilters.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
                        endEP.Insert(Environment.NewLine + Environment.NewLine +
                                     "filters.Add(new IntuitSampleMVC.utils.IppTag());");
                    }
                }
            }
        }

        /// <summary>
        /// Adds the intuit anywhere source to MVC project.
        /// </summary>
        /// <param name="selProject">The sel project.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="viewsSubFolderName">Name of the views sub folder.</param>
        /// <param name="codeFilePath">The code file path.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddIntuitAnywhereSourceToMVCProject(Project selProject, string commandType, string viewsSubFolderName, string codeFilePath, Document activeDocument)
        {
            string mvcCodeFilePath = codeFilePath + AddinConstants.MVCContentFolder;
            VSProject activeProject = (VSProject)selProject.Object;

            // add project referecne 
            Common.AddProjectReferecne(activeProject, mvcCodeFilePath);

            // Determin what is aleary added in the project based on controller folder
            bool isLoginAlreadyThere = false;
            bool isConnectToQuickBookAlreadyThere = false;
            bool isBlueDotMenyAlreadyThere = false;
            bool isDisconnetThere = false;
            bool isLogoutThere = false;
            // Get controller folder
            ProjectItem controllersFolder = GetControllerFolder(selProject);

            // Determin what is already there based on the controller added in the project 
            GetExistingMvcItems(controllersFolder, out isConnectToQuickBookAlreadyThere, out isLoginAlreadyThere, out isBlueDotMenyAlreadyThere, out isDisconnetThere, out isLogoutThere);
            AddControllerItems(commandType, controllersFolder, mvcCodeFilePath);

            // Add utils
            AddMvcUtils(selProject, mvcCodeFilePath);

            // Add View
            var appSettingsEntries = new Dictionary<string, string>();
            if (commandType == "ConnecttoQuickBooks")
            {
                isConnectToQuickBookAlreadyThere = true;
                appSettingsEntries.Add("securityKey", "YourSecretKey");
                appSettingsEntries.Add("oauth_callback_url", "/OauthResponse");
                appSettingsEntries.Add("applicationToken", string.Empty);
                appSettingsEntries.Add("grantUrl", "OauthGrant");
                appSettingsEntries.Add("menuProxy", "MenuProxy");
                appSettingsEntries.Add("qbo_base_url", @"https://qbo.intuit.com/qbo1/rest/user/v2/");
                appSettingsEntries.Add("consumerKey", string.Empty);
                appSettingsEntries.Add("consumerSecret", string.Empty);
                appSettingsEntries.Add("openid_identifier", "https://openid.intuit.com/Identity-YourAppName");
                Common.AddConstantsToConfig(appSettingsEntries);
                PersistenceFiles(selProject, mvcCodeFilePath);
            }

            if (commandType == "OpenID")
            {
                isLoginAlreadyThere = true;
                appSettingsEntries.Add("securityKey", "YourSecretKey");
                appSettingsEntries.Add("openid_identifier", "https://openid.intuit.com/Identity-YourAppName");
                appSettingsEntries.Add("applicationToken", string.Empty);
                appSettingsEntries.Add("consumerKey", string.Empty);
                appSettingsEntries.Add("consumerSecret", string.Empty);
                Common.AddConstantsToConfig(appSettingsEntries);
                PersistenceFiles(selProject, mvcCodeFilePath);
            }

            if (commandType == "BlueDot")
            {
                appSettingsEntries.Add("grantUrl", "OauthGrant");
                appSettingsEntries.Add("menuProxy", "MenuProxy");
                isBlueDotMenyAlreadyThere = true;
                Common.AddConstantsToConfig(appSettingsEntries);
            }

            if (commandType == "Disconnect")
            {
                isDisconnetThere = true;
                appSettingsEntries.Add("securityKey", "YourSecretKey");
                Common.AddConstantsToConfig(appSettingsEntries);
                PersistenceFiles(selProject, mvcCodeFilePath);
            }

            if (commandType == "Logout")
            {
                isLogoutThere = true;
                Common.AddConstantsToConfig(appSettingsEntries);
            }

            AddViews(selProject, viewsSubFolderName, mvcCodeFilePath);

            // insert tag
            if (commandType == "Disconnect")
            {
                Common.AddDisconnectTag(commandType, mvcCodeFilePath, activeDocument);
            }
            else
            {
                Common.AddTag(commandType, mvcCodeFilePath, activeDocument, codeFilePath);
            }

            // Insert routes
            ProjectItem utilFolder;
            Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder);
            if (utilFolder != null)
            {
                InsertRoutes(utilFolder, mvcCodeFilePath, isConnectToQuickBookAlreadyThere, isLoginAlreadyThere, isBlueDotMenyAlreadyThere, isDisconnetThere, isLogoutThere);
            }

            // Add method global in asax
            InsertMethodCallInGlobalASAXFile(selProject);

            // add configuration entries
            Common.AddWebConfigurationEntries(selProject, appSettingsEntries, VSProjectType.Mvc);
        }

        private static void PersistenceFiles(Project selProject, string mvcCodeFilePath)
        {
            ProjectItem utilFolder;
            if (!Common.IsProjectItemExist(selProject.ProjectItems, "Utils", out utilFolder))
            {
                utilFolder = selProject.ProjectItems.AddFolder("Utils");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "CryptographyHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "CryptographyHelper.cs");
            }

            if (!Common.IsProjectItemExist(utilFolder.ProjectItems, "OauthAccessTokenStorageHelper.cs"))
            {
                utilFolder.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorageHelper.cs");
            }

            if (!Common.IsProjectItemExist(selProject.ProjectItems, "OauthAccessTokenStorage.xml"))
            {
                selProject.ProjectItems.AddFromFileCopy(mvcCodeFilePath + "OauthAccessTokenStorage.xml");
            }
        }

        /// <summary>
        /// Adds the direct connect to intuit functionality.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="codeFilePath">The code file path.</param>
        /// <param name="activeDocument">The active document.</param>
        internal static void AddDirectConnectToIntuitFunctionality(string commandType, string codeFilePath, Document activeDocument)
        {
            string mvcCodeFilePath = codeFilePath + AddinConstants.MVCContentFolder;
            Common.AddTag(commandType, mvcCodeFilePath, activeDocument, codeFilePath);
        }

        #endregion
    }
}