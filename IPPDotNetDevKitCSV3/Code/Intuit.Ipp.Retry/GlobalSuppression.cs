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

[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Intuit.Ipp.Retry", Justification = "It make sense here, since Intuit Retry policy needs to handle 3 types of reties(Fixed, Incremental, Exponential BackOff")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "Intuit.Ipp.Retry", MessageId = "Ipp", Justification = "Ipp stands for Intuit Partner Platform")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "Intuit.Ipp.Retry.IntuitRetryPolicy.#.ctor(System.Int32,System.TimeSpan,System.TimeSpan,System.TimeSpan)", MessageId = "Backoff", Justification = "Backoff a parameter for exponential Retry policy")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Backoff", Justification = "Backoff a parameter for exponential Retry policy")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Intuit.Ipp.Retry", Justification = "Name spaces cannot be clubbed.")]
