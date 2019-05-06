////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
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
// 
////********************************************************************

using System;
namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class MethodNames
    {
        public static string StartsWith = "StartsWith";
        public static string EndsWith = "EndsWith";
        public static string Contains = "Contains";
        public static string In = "In";

        public static string Group = "GroupBy";
        internal const string Join = "Join";
        internal const string Take = "Take";
        internal const string Skip = "Skip";
        internal const string Where = "Where";
        internal const string Select = "Select";
        internal const string Orderby = "OrderBy";
        internal const string ThenBy = "ThenBy";
        internal const string Orderbydesc = "OrderByDescending";
    }
}