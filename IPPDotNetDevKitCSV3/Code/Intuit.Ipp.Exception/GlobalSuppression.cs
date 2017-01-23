////*********************************************************
// <copyright file="GlobalSuppression.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Global Suppressions.</summary>
////***************************************************
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ipp")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "Intuit.Ipp.Exception", MessageId = "Ipp")]
[module: SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", Scope = "resource", Target = "Intuit.Ipp.Exception.Properties.Resources.resources", MessageId = "Ips")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Intuit.Ipp.Exception.IdsError")]

[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Intuit.Ipp.Exception.IdsExceptionManager.#HandleException(System.String,Intuit.Ipp.Exception.IdsException)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Intuit.Ipp.Exception.IdsExceptionManager.#HandleException(System.String,System.String,System.String,Intuit.Ipp.Exception.IdsException)")]