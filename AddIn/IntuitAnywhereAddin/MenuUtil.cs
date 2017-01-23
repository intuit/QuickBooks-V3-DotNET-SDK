////********************************************************************
// <copyright file="MenuUtil.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This class contains logic for menu item creation and their handlers.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.CommandBars;

    /// <summary>
    /// This class contains logic for menu item creation and their handlers.
    /// </summary>
    internal class MenuUtil
    {
        /// <summary>
        /// variable to stroe referecne of visual studio DTE object
        /// </summary>
        private DTE2 visualStudio;

        /// <summary>
        /// List to add Command Bar Events Objects
        /// </summary>
        private List<CommandBarEvents> menuItemHandlerList = new List<CommandBarEvents>();

        /// <summary>
        /// stores the path string of CodeFiles Folder. This folder is copied by the installer during installtion of addin.
        /// </summary>
        private string codeFilesContentPath;

        /// <summary>
        /// Initializes a new instance of the MenuUtil class.
        /// </summary>
        /// <param name="visualStudio">The vs studio.</param>
        internal MenuUtil(DTE2 visualStudio)
        {
            this.visualStudio = visualStudio;
            this.codeFilesContentPath = Common.GetCodeFilePath();
        }

        /// <summary>
        /// Constructs the intuit anywhere menus.
        /// </summary>
        internal void ConstructIntuitAnywhereMenus()
        {
            this.BuildMenu(ContextLevels.Project);
            this.BuildMenu(ContextLevels.MenuBar);
            this.BuildMenu(ContextLevels.AspxContext);
            this.BuildMenu(ContextLevels.HtmlContext);
            this.BuildMenu(ContextLevels.HtmlSourceEditing);
        }

        #region Helper Methods

        /// <summary>
        /// Adds the Visual studio menu item.
        /// </summary>
        /// <param name="vsmainMenu">visual studio menu item</param>
        /// <param name="menuToAdd">name of the menu item</param>
        /// <param name="position">position.of menu item.</param>
        /// <param name="seperator">if set to <c>true</c> [seperator].</param>
        /// <param name="menuIcon">The menu icon.</param>
        /// <returns>
        /// CommandBarControl Object
        /// </returns>
        private static CommandBarButton AddVSMenuItem(CommandBarPopup vsmainMenu, string menuToAdd, int position, bool seperator, Bitmap menuIcon)
        {
            CommandBarButton vsmenuItem = (CommandBarButton)vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, string.Empty, position, true);
            vsmenuItem.BeginGroup = seperator;
            vsmenuItem.Tag = Guid.NewGuid().ToString();
            vsmenuItem.Style = MsoButtonStyle.msoButtonIconAndCaption;
            vsmenuItem.Caption = menuToAdd;
            vsmenuItem.TooltipText = string.Empty;
            if (menuIcon != null)
            {
                vsmenuItem.Picture = (stdole.StdPicture)Common.GetImage(menuIcon);
            }

            return vsmenuItem;
        }

        /// <summary>
        /// Adds the Visual studio menu item.
        /// </summary>
        /// <param name="vsmainMenu">visual studio menu item</param>
        /// <param name="menuToAdd">name of the menu item</param>
        /// <param name="position">position.of menu item.</param>
        /// <param name="seperator">if set to <c>true</c> [seperator].</param>
        /// <param name="menuIcon">The menu icon.</param>
        /// <returns>
        /// CommandBarControl Object
        /// </returns>
        private static CommandBarButton AddVSMenuItem(CommandBarPopup vsmainMenu, string menuToAdd, int position, bool seperator, Icon menuIcon)
        {
            CommandBarButton vsmenuItem = (CommandBarButton)vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, string.Empty, position, true);
            vsmenuItem.BeginGroup = seperator;
            vsmenuItem.Tag = Guid.NewGuid().ToString();
            vsmenuItem.Style = MsoButtonStyle.msoButtonIconAndCaption;
            vsmenuItem.Caption = menuToAdd;
            vsmenuItem.TooltipText = string.Empty;
            if (menuIcon != null)
            {
                vsmenuItem.Picture = (stdole.StdPicture)Common.GetImage(menuIcon);
            }

            return vsmenuItem;
        }

        /// <summary>
        /// Adds the Visual studio menu item.
        /// </summary>
        /// <param name="vsmainMenu">visual studio menu item</param>
        /// <param name="menuToAdd">name of the menu item</param>
        /// <param name="position">position.of menu item.</param>
        /// <param name="seperator">if set to <c>true</c> [seperator].</param>
        /// <returns>
        /// CommandBarControl Object
        /// </returns>
        private static CommandBarButton AddVSMenuItem(CommandBarPopup vsmainMenu, string menuToAdd, int position, bool seperator)
        {
            CommandBarButton vsmenuItem = (CommandBarButton)vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, string.Empty, position, true);
            vsmenuItem.BeginGroup = seperator;
            vsmenuItem.Tag = Guid.NewGuid().ToString();
            vsmenuItem.Style = MsoButtonStyle.msoButtonIconAndCaption;
            vsmenuItem.Caption = menuToAdd;
            vsmenuItem.TooltipText = string.Empty;
            return vsmenuItem;
        }

        /// <summary>
        /// Gets the Commandbar Index, Typically 1, but tries to place it right after the Tools menu, if the
        /// menu is to be placed in the Top menu (MenuBar).
        /// </summary>
        /// <param name="controls">The command bar controls.</param>
        /// <param name="level">The level.</param>
        /// <returns>Command bar index.</returns>
        private static int GetCommandbarIndex(CommandBarControls controls, ContextLevels level)
        {
            switch (level)
            {
                case ContextLevels.MenuBar:
                    for (int i = 1; i <= controls.Count; i++)
                    {
                        if (controls[i].accName == "Tools")
                        {
                            return i + 1;
                        }
                    }

                    return controls.Count + 1;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Builds the menu.
        /// </summary>
        /// <param name="level">Context level where menu needs to be dispalyed.</param>
        private void BuildMenu(ContextLevels level)
        {
            // Build Root Menu 
            CommandBarPopup menu = this.AddVSMainMenuItem(VSContextUtility.ContextToVSContext(level), IntuitAnywhereResources.mainmenu, level);
            int position = 1;

            // Build Sub Items 
            CommandBarButton javaScriptLibrary = AddVSMenuItem(menu, IntuitAnywhereResources.menu01, position++, false, IntuitAnywhereResources.JSlibrary);
            CommandBarButton openID = AddVSMenuItem(menu, IntuitAnywhereResources.menu02, position++, false, IntuitAnywhereResources.openid);

            CommandBarButton connecttoQuickBooks = AddVSMenuItem(menu, IntuitAnywhereResources.menu03, position++, false, IntuitAnywhereResources.connect_to_qb_16);
            CommandBarButton addBlueDotMenu = AddVSMenuItem(menu, IntuitAnywhereResources.menu04, position++, false, IntuitAnywhereResources.bluedot);
            CommandBarButton addLogoutMenu = AddVSMenuItem(menu, IntuitAnywhereResources.menu012, position++, false, IntuitAnywhereResources.logout_1);
            CommandBarButton dataServicesSDK = AddVSMenuItem(menu, IntuitAnywhereResources.menu05, position++, false, IntuitAnywhereResources.Dataservices);
            CommandBarButton dataServicesSDKV3 = AddVSMenuItem(menu, IntuitAnywhereResources.menu11, position++, false, IntuitAnywhereResources.Dataservices);
            CommandBarButton directConnectToIntuit = AddVSMenuItem(menu, IntuitAnywhereResources.menu07, position++, false,IntuitAnywhereResources.directconnect);
            CommandBarButton disconnet = AddVSMenuItem(menu, IntuitAnywhereResources.menu08, position++, false, IntuitAnywhereResources.disconnect_from_qb_16);
            CommandBarButton sdkDocHelp = AddVSMenuItem(menu, IntuitAnywhereResources.menu10, position++, false, IntuitAnywhereResources.Help_icon);
            CommandBarButton help = AddVSMenuItem(menu, IntuitAnywhereResources.menu06, position++, false, IntuitAnywhereResources.Help_icon);
            if (level == ContextLevels.MenuBar)
            {
                CommandBarButton ippDocumentation = AddVSMenuItem(menu, IntuitAnywhereResources.menu09, position++, true, IntuitAnywhereResources.docs);
                CommandBarButton ippSupport = AddVSMenuItem(menu, IntuitAnywhereResources.menu010, position++, false,IntuitAnywhereResources.Support);
                CommandBarButton ippFrontRunner = AddVSMenuItem(menu, IntuitAnywhereResources.menu011, position++, true,IntuitAnywhereResources.FrontrunnerLogo);

                // Add Event Handlers
                CommandBarEvents ippDocumentationMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(ippDocumentation);
                ippDocumentationMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.IppDocumentationMenuItemHandler_Click);
                this.menuItemHandlerList.Add(ippDocumentationMenuItemHandler);
                CommandBarEvents ippSupportMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(ippSupport);
                ippSupportMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.IppSupportMenuItemHandler_Click);
                this.menuItemHandlerList.Add(ippSupportMenuItemHandler);
                CommandBarEvents ippFrontRunnerMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(ippFrontRunner);
                ippFrontRunnerMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.IppFrontRunnerMenuItemHandler_Click);
                this.menuItemHandlerList.Add(ippFrontRunnerMenuItemHandler);
            }

            // Add Event Handlers
            CommandBarEvents javaScriptLibraryMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(javaScriptLibrary);
            javaScriptLibraryMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.JavaScriptLibraryMenuItemHandler_Click);
            this.menuItemHandlerList.Add(javaScriptLibraryMenuItemHandler);
            CommandBarEvents connecttoQuickBooksMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(connecttoQuickBooks);
            connecttoQuickBooksMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.ConnecttoQuickBooksMenuItemHandler_Click);
            this.menuItemHandlerList.Add(connecttoQuickBooksMenuItemHandler);
            CommandBarEvents addBlueDotMenuMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(addBlueDotMenu);
            addBlueDotMenuMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.AddBlueDotMenuMenuItemHandler_Click);
            this.menuItemHandlerList.Add(addBlueDotMenuMenuItemHandler);
            CommandBarEvents openIDMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(openID);
            openIDMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.OpenIDMenuItemHandler_Click);
            this.menuItemHandlerList.Add(openIDMenuItemHandler);
            CommandBarEvents dataServicesSDKMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(dataServicesSDK);
            dataServicesSDKMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.DataServicesSDKMenuItemHandler_Click);
            this.menuItemHandlerList.Add(dataServicesSDKMenuItemHandler);
            CommandBarEvents dataServicesSDKV3MenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(dataServicesSDKV3);
            dataServicesSDKV3MenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.DataServicesSDKV3MenuItemHandler_Click);
            this.menuItemHandlerList.Add(dataServicesSDKV3MenuItemHandler);
            CommandBarEvents directConnectToIntuitMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(directConnectToIntuit);
            directConnectToIntuitMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.DirectConnectToIntuitMenuItemHandler_Click);
            this.menuItemHandlerList.Add(directConnectToIntuitMenuItemHandler);
            CommandBarEvents disconnetMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(disconnet);
            disconnetMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.DisconnetMenuItemHandler_Click);
            this.menuItemHandlerList.Add(disconnetMenuItemHandler);
            CommandBarEvents helpMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(help);
            helpMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.HelpMenuItemHandler_Click);
            this.menuItemHandlerList.Add(helpMenuItemHandler);
            CommandBarEvents sdkDocHelpMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(sdkDocHelp);
            sdkDocHelpMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.SdkDocHelpMenuItemHandler_Click);
            this.menuItemHandlerList.Add(sdkDocHelpMenuItemHandler);
            CommandBarEvents logoutMenuItemHandler = (EnvDTE.CommandBarEvents)this.visualStudio.DTE.Events.get_CommandBarEvents(addLogoutMenu);
            logoutMenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(this.AddLogoutMenuItemHandler_Click);
            this.menuItemHandlerList.Add(logoutMenuItemHandler);

        }

        /// <summary>
        /// Adds the visual studio  main menu item.
        /// </summary>
        /// <param name="commandBarName">Name of the command bar.</param>
        /// <param name="menuName">name of the menu.</param>
        /// <param name="level">Context level where menu needs to appear.</param>
        /// <returns>CommandBarPopup Object</returns>
        private CommandBarPopup AddVSMainMenuItem(string commandBarName, string menuName, ContextLevels level)
        {
            var controls = this.GetVSMainMenu(commandBarName, 1).Controls;
            var vsmainMenu = controls.Add(MsoControlType.msoControlPopup, Missing.Value, Missing.Value, GetCommandbarIndex(controls, level), true) as CommandBarPopup;
            vsmainMenu.Caption = menuName;
            vsmainMenu.TooltipText = string.Empty;
            vsmainMenu.Tag = Guid.NewGuid().ToString();
            return vsmainMenu;
        }

        /// <summary>
        /// Gets the CommandBar object for main menu.
        /// </summary>
        /// <param name="commandBarName">Name of the command bar.</param>
        /// <param name="menuIndex">Index of the menu.</param>
        /// <returns>CommandBar object for main menu.</returns>
        private CommandBar GetVSMainMenu(string commandBarName, int menuIndex)
        {
            CommandBar theBar = null;
            int index = 0;
            foreach (CommandBar bar in (CommandBars)this.visualStudio.DTE.CommandBars)
            {
                if (bar.Name == commandBarName)
                {
                    theBar = bar;
                    index++;
                    if (index == menuIndex)
                    {
                        return theBar;
                    }
                }
            }

            return theBar;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This handler is called when user clicks "Add Java Script Library" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void JavaScriptLibraryMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project activeProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    activeProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (activeProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    activeProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            string javaScriptTag = File.ReadAllText(this.codeFilesContentPath + "JavaScriptTag.txt");

            // Validate the target window
            ((TextSelection)this.visualStudio.ActiveDocument.Selection).Insert(javaScriptTag);
        }

        /// <summary>
        /// This handler is called when user clicks "Add Connect to QuickBooks" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void ConnecttoQuickBooksMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project

            // Project selProject = m_VSStudio.SelectedItems.Item(1).Project;
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddIntuitAnywhereSourceToMVCProject(selProject, "ConnecttoQuickBooks", "OauthResponse", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddSourceToAspnetProject(selProject, "ConnecttoQuickBooks", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks "Add Blue Dot Menu" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void AddBlueDotMenuMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddIntuitAnywhereSourceToMVCProject(selProject, "BlueDot", "MenuProxy", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddSourceToAspnetProject(selProject, "BlueDot", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks "Add Logout" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void AddLogoutMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddIntuitAnywhereSourceToMVCProject(selProject, "Logout", "Logout", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddSourceToAspnetProject(selProject, "Logout", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }
        
        /// <summary>
        /// This handler is called when user clicks "Add Open ID" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void OpenIDMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddIntuitAnywhereSourceToMVCProject(selProject, "OpenID", "OpenId", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddSourceToAspnetProject(selProject, "OpenID", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks "Add Data Services SDK" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void DataServicesSDKMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (selProject == null)
            {
                if (this.visualStudio.SelectedItems != null && this.visualStudio.SelectedItems.Count == 1 && this.visualStudio.SelectedItems.Item(1) != null)
                {
                    if (this.visualStudio.SelectedItems.Item(1).Project == null)
                    {
                        MessageBox.Show(IntuitAnywhereResources.msg006, IntuitAnywhereResources.msgdialogtitle);
                        return;
                    }
                }

                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.AspNet || projectType == VSProjectType.Mvc)
            {
                //run the Nuget command
                string repositoryPath = "http://go.microsoft.com/fwlink/?LinkID=206669"; //// Can be modified to https://nuget.org/api/v2
                string packageId = "IPPDotNetDevKit";
                Common.InstallNuGetPackageForProject(packageId, selProject, repositoryPath);

                if (projectType == VSProjectType.AspNet)
                {
                    AspHandler.AddQuickBookCustomerV2FilesForAsp(selProject, this.codeFilesContentPath);
                }
                else
                {
                    MvcHandler.AddQuickBookCustomerV2FilesForMvc(selProject, this.codeFilesContentPath);
                }
            }
            else
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks "Add Data Services SDK V3" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void DataServicesSDKV3MenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (selProject == null)
            {
                if (this.visualStudio.SelectedItems != null && this.visualStudio.SelectedItems.Count == 1 && this.visualStudio.SelectedItems.Item(1) != null)
                {
                    if (this.visualStudio.SelectedItems.Item(1).Project == null)
                    {
                        MessageBox.Show(IntuitAnywhereResources.msg006, IntuitAnywhereResources.msgdialogtitle);
                        return;
                    }
                }

                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.AspNet || projectType == VSProjectType.Mvc)
            {
                //run the Nuget command
                string repositoryPath = "http://go.microsoft.com/fwlink/?LinkID=206669"; //// Can be modified to https://nuget.org/api/v2
                string packageId = "IPPDotNetDevKitV3";
                Common.InstallNuGetPackageForProject(packageId, selProject, repositoryPath);

                if (projectType == VSProjectType.AspNet)
                {
                    AspHandler.AddQuickBookCustomerFilesForAsp(selProject, this.codeFilesContentPath);
                }
                else
                {
                    MvcHandler.AddQuickBookCustomerFilesForMvc(selProject, this.codeFilesContentPath);
                }
            }
            else
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks "Add Direct Connect to Intuit" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void DirectConnectToIntuitMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddDirectConnectToIntuitFunctionality("DirectConnectToIntuit", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddDirectConnectToIntuitFunctionality("DirectConnectToIntuit", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks on "Add Disconnect" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void DisconnetMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            if (this.visualStudio.Debugger.CurrentMode == dbgDebugMode.dbgRunMode)
            {
                MessageBox.Show(IntuitAnywhereResources.msg001, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            // Get the currenlly active project
            Array activeProjectsArray = (Array)this.visualStudio.ActiveSolutionProjects;
            Project selProject = null;
            if (activeProjectsArray != null)
            {
                if (activeProjectsArray.Length > 0)
                {
                    selProject = (Project)activeProjectsArray.GetValue(0);
                }
            }

            if (this.visualStudio.ActiveDocument == null)
            {
                MessageBox.Show(IntuitAnywhereResources.msg002, IntuitAnywhereResources.msgdialogtitle);
                return;
            }

            if (selProject == null)
            {
                if (this.visualStudio.ActiveDocument.ProjectItem.ContainingProject != null)
                {
                    selProject = this.visualStudio.ActiveDocument.ProjectItem.ContainingProject;
                }
                else
                {
                    MessageBox.Show(IntuitAnywhereResources.msg003, IntuitAnywhereResources.msgdialogtitle);
                    return;
                }
            }

            // Get the project type
            VSProjectType projectType = Common.GetProjectType(selProject);
            if (projectType == VSProjectType.Mvc)
            {
                MvcHandler.AddIntuitAnywhereSourceToMVCProject(selProject, "Disconnect", "Disconnect", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.AspNet)
            {
                AspHandler.AddDisconnetFunctionality(selProject, "Disconnect", this.codeFilesContentPath, this.visualStudio.ActiveDocument);
            }

            if (projectType == VSProjectType.Other)
            {
                MessageBox.Show(IntuitAnywhereResources.msg004, IntuitAnywhereResources.msgdialogtitle);
            }
        }

        /// <summary>
        /// This handler is called when user clicks on "Help" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void HelpMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            System.Diagnostics.Process.Start("https://ipp.developer.intuit.com/0010_Intuit_Partner_Platform/0200_DevKits_for_Intuit_Partner_Platform/0150_IPP_.NET_DevKit_3.0");
        }

        /// <summary>
        /// This handler is called when user clicks on "Help" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void SdkDocHelpMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            System.Diagnostics.Process.Start("http://docs.developer.intuit.com/IntuitDataServicesSDK/");
        }

        /// <summary>
        /// This handler is called when user clicks on "Help" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void IppDocumentationMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            string target = File.ReadAllText(this.codeFilesContentPath + "IppDocumentation.txt");
            System.Diagnostics.Process.Start(target);
        }

        /// <summary>
        /// This handler is called when user clicks on "Help" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void IppSupportMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            string target = File.ReadAllText(this.codeFilesContentPath + "IppSupportURL.txt");
            System.Diagnostics.Process.Start(target);
        }

        /// <summary>
        /// This handler is called when user clicks on "Help" menu item.
        /// </summary>
        /// <param name="commandBarControl">The command bar control.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <param name="cancelDefault">if set to <c>true</c> [cancel default].</param>
        private void IppFrontRunnerMenuItemHandler_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            string target = File.ReadAllText(this.codeFilesContentPath + "IppFrontRunnerURL.txt");
            System.Diagnostics.Process.Start(target);
        }
        #endregion
    }
}