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
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "Intuit.Ipp.Security", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#OAUTHACCESSTOKENURL", MessageId = "OAUTHACCESSTOKENURL", Justification = "All constants are declared in capital characters")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#USERNAMEAUTHREQUESTURI", MessageId = "USERNAMEAUTHREQUESTURI", Justification = "All constants are declared in capital characters")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#OAUTHAUTHORIZEREQUESTURL", MessageId = "OAUTHAUTHORIZEREQUESTURL", Justification = "All constants are declared in capital characters")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#OAUTHREQUESTTOKENURI", MessageId = "OAUTHREQUESTTOKENURI", Justification = "All constants are declared in capital characters")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Intuit.Ipp.Security", Justification = "Name spaces cannot be clubbed.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Intuit.Ipp.Security.OAuthRequestValidator.#AdditionalParameters")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.CookieBasedRequestValidator.#.ctor(System.String,System.String)", MessageId = "iam")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.CookieBasedRequestValidator.#IamCookie", MessageId = "Iam")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#QBNCOOKIEPREFIX", MessageId = "QBNCOOKIEPREFIX")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#EQUALSSTRINGVALUE", MessageId = "EQUALSSTRINGVALUE")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#SEMICOLONSTRING", MessageId = "SEMICOLONSTRING")]
[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Intuit.Ipp.Security.SecurityConstants.#.cctor()", Justification = "Static constructor is needed to load from XML file.")]