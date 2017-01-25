////*********************************************************
// <copyright file="GlobalSuppression.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
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