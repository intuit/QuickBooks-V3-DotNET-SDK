////*********************************************************
// <copyright file="EncodingFixer.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains EncodingFixer for QuickBase encoding.</summary>
////*********************************************************
namespace Intuit.Ipp.Core.Rest
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Helps repair encoding of QuickBase responses.
    /// </summary>
    /// <seealso cref="FixQuickBaseEncoding"/>
    public static class EncodingFixer
    {
        /// <summary>
        /// A list of characters for which QuickBase always uses Windows-1252 encoding. For use in FixQuickBaseEncoding().
        /// "LEFT DOUBLE QUOTATION MARK"
        /// "RIGHT DOUBLE QUOTATION MARK"
        /// "EN DASH"
        /// </summary>
        private static readonly Dictionary<byte, char> Replacements = new Dictionary<byte, char> { { 147, '\u201C' }, { 148, '\u201D' }, { 150, '\u2013' } };

        /// <summary>
        /// QuickBase has a unique feature which converts certain input characters into windows-1252 encoding and stores them in the database
        /// (This assists Windows users when they use QuickBase HTML UI). When data containing such characters is queried, 
        /// the windows-1252 encoding will not change and will remain surrounded by the usual UTF8-encoded XML.
        /// If this data requires XML parsing, the windows-1252 encoded characters have to be re-encoded to UTF8 encoding.
        /// This unique feature was recently removed from appcenter, but still exists in QuickBase.
        /// </summary>
        /// <param name="encodedValue">a response from QuickBase that's mostly UTF8 encoded but has Windows-1252-encoded characters embedded in it</param>
        /// <returns>Returns the encoded response.</returns>
        public static string FixQuickBaseEncoding(byte[] encodedValue)
        {
            int restartPosition = 0;
            StringBuilder decodedString = new StringBuilder(encodedValue.Length);
            Encoding enc8 = new UTF8Encoding(false, true);
            while (restartPosition < encodedValue.Length)
            {
                try
                {
                    decodedString.Append(enc8.GetString(encodedValue, restartPosition, encodedValue.Length - restartPosition));
                    return decodedString.ToString();
                }
                catch (DecoderFallbackException e)
                {
                    int badPosition = e.Index + 1 + restartPosition;

                    decodedString.Append(enc8.GetString(encodedValue, restartPosition, badPosition - restartPosition));

                    byte trip = encodedValue[badPosition];
                    if (Replacements.ContainsKey(trip))
                    {
                        decodedString.Append(Replacements[trip]);
                    }
                    else
                    {
                        decodedString.Append(trip);
                    }

                    restartPosition = badPosition + 1;
                }
            }

            return decodedString.ToString();
        }
    }
}
