////*********************************************************
// <copyright file="IppConfiguration.cs" company="Intuit">
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

// <summary>This file contains Ipp Configuration for .NET Core.</summary>
////*********************************************************

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiSettings() : this("appsettings.json")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public ApiSettings(string path)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(path)
                .Build();

            //// First way  
            //string value1 = _iconfiguration.GetSection("Data").GetSection("ConnectionString").Value;
            //// Second way  
            //string value2 = _iconfiguration.GetValue<string>("Data:ConnectionString");


            var apiSettings = builder.GetSection("Logging").GetSection("IDSLogs");

            var enableLogs = apiSettings["EnableLogs"];
            var LogDirectory = apiSettings["LogDirectory"];
            

            //if (!Enum.TryParse(apiSettings["AppType"], true, out XeroApiAppType appType))
            //{
            //    throw new ArgumentOutOfRangeException(nameof(apiSettings), apiSettings["AppType"], "AppType did not match one of: private, public, partner");
            //}

            //AppType = appType;
        }
    }
}
