////*********************************************************
// <copyright file="GlobalSuppression.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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