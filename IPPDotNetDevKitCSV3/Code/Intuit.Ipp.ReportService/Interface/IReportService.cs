////***************************************************************************************************
// <copyright file="IReportService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interface for Reports service.</summary>
////***************************************************************************************************

namespace Intuit.Ipp.ReportService
{
    using System.Collections.ObjectModel;
    using Intuit.Ipp.Data;

    /// <summary>
    /// This interface specifies the ReportService Read operation
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Executes a Report for a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="reportName">Report to Run</param>        
        Report ExecuteReport(string reportName);

    }
}
