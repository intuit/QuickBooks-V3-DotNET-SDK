////*********************************************************
// <copyright file="GlobalSuppression.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Global Suppressions.</summary>
////***************************************************
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#GetIdsDateTimeFormat(System.Object)")]
[module: SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#Execute(LinqExtender.Ast.Expression,System.Boolean,System.String&)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryOperationType.#report", MessageId = "report")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryOperationType.#changedata", MessageId = "changedata")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryOperationType.#query", MessageId = "query")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ipp")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryOperationType.#changedata", MessageId = "changedata")]
[module: SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#ExecuteIdsQuery(System.String,Intuit.Ipp.QueryFilter.QueryOperationType)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#GetIdsDateTimeFormat(System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#ExecuteMultipleEntityQueries`1(System.Collections.ObjectModel.ReadOnlyCollection`1<System.String>)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Intuit.Ipp.QueryFilter.QueryService`1.#ExecuteMultipleEntityQueries`1(System.Collections.ObjectModel.ReadOnlyCollection`1<System.String>)")]