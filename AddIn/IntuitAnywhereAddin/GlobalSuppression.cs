////********************************************************************
// <copyright file="GlobalSuppression.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains messages need to be suppressed for Fx-Cop.</summary>
////********************************************************************
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Addin")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "IntuitAnyWhereAddin.VSProjectType.#Mvc", MessageId = "Mvc")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "IntuitAnyWhereAddin.ConvertImage.#Convert(System.Drawing.Image)")]
[module: SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", Scope = "resource", Target = "IntuitAnyWhereAddin.IntuitAnywhereResources.resources", MessageId = "ipp")]
[module: SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", Scope = "resource", Target = "IntuitAnyWhereAddin.IntuitAnywhereResources.resources", MessageId = "Addin")]
[module: SuppressMessage("Microsoft.Naming", "CA1701:ResourceStringCompoundWordsShouldBeCasedCorrectly", Scope = "resource", Target = "IntuitAnyWhereAddin.IntuitAnywhereResources.resources", MessageId = "Javascript")]
