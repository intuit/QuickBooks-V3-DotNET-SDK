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
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Intuit.Ipp.Utility.XmlObjectSerializer.#Deserialize`1(System.String)", Justification = "Require type information")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Intuit.Ipp.Utility.IEntitySerializer.#Deserialize`1(System.String)", Justification = "Require type information")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Intuit.Ipp.Utility", Justification = "Name spaces cannot be clubbed")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "Intuit.Ipp.Utility", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#ERRDETAILXPATH", MessageId = "ERRDETAILXPATH")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#ERRTEXTXPATH", MessageId = "ERRTEXTXPATH")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#ERRCODEXPATH", MessageId = "ERRCODEXPATH")]
[module: SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", Scope = "member", Target = "Intuit.Ipp.Utility.IntuitErrorHandler.#HandleErrors(System.Xml.XmlNode)", MessageId = "System.Xml.XmlNode")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Intuit.Ipp.Utility.JsonObjectSerializer.#Deserialize`1(System.String)", Justification = "Require type information")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Intuit.Ipp.Utility.JsonObjectSerializer", MessageId = "Json")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.BaseUrlElement.#Qbo", MessageId = "Qbo")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.BaseUrlElement.#Ips", MessageId = "Ips")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.BaseUrlElement.#Qbd", MessageId = "Qbd")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Intuit.Ipp.Utility.IppConfigurationSection", MessageId = "Ipp")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.CustomSecurityElement.#Params", MessageId = "Params")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.ExponentialRetryElement.#MinBackoff", MessageId = "Backoff")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.ExponentialRetryElement.#MaxBackoff", MessageId = "Backoff")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.ExponentialRetryElement.#DeltaBackoff", MessageId = "Backoff")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.RetryElement.#IncrementatlRetry", MessageId = "Incrementatl")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.SerializationFormat.#Json", MessageId = "Json")]
[module: SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Intuit.Ipp.Utility.CustomSecurityElement.#Type")]
[module: SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Intuit.Ipp.Utility.CustomSerializerElement.#Type")]
[module: SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Intuit.Ipp.Utility.CustomLoggerElement.#Type")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Intuit.Ipp.Utility.BaseUrlElement.#OAuthAccessTokenUrl")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#QDBAPI", MessageId = "QDBAPI", Justification = "Appropriate naming.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#UDATA", MessageId = "UDATA", Justification = "Appropriate naming.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#ENCODINGATTRVALUE", MessageId = "ENCODINGATTRVALUE", Justification = "Appropriate naming.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.UtilityConstants.#ENCODINGATTR", MessageId = "ENCODINGATTR", Justification = "Appropriate naming.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.CompressionFormat.#DEFAULT", MessageId = "DEFAULT", Justification = "Appropriate Name")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Utility.SerializationFormat.#DEFAULT", MessageId = "DEFAULT", Justification = "Appropriate Name")]