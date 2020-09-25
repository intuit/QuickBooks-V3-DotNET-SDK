﻿// -----------------------------------------------------------------------
// <copyright file="ReportEntities.cs" company="Intuit">
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
// <summary>Sample documentation for Report entities.</summary>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Data
{
    using System;

    #region V3 report classes



  

    /// <summary>
    /// Customer Balance
    /// </summary>
    public partial class CustomerBalance : ReportQueryBase
    {
    }

    /// <summary>
    /// Customer Balance Detail
    /// </summary>
    public partial class CustomerBalanceDetail : ReportQueryBase
    {
    }

    /// <summary>
    /// Aged Receivables
    /// </summary>
    public partial class AgedReceivables : ReportQueryBase
    {
    }

    /// <summary>
    /// Aged Receivable Detail
    /// </summary>
    public partial class AgedReceivableDetail : ReportQueryBase
    {
    }

    /// <summary>
    /// Vendor Balance
    /// </summary>
    public partial class VendorBalance : ReportQueryBase
    {
    }

    /// <summary>
    /// Vendor Balance detail.
    /// </summary>
    public partial class VendorBalanceDetail : ReportQueryBase
    {
    }

    /// <summary>
    /// Aged Payables
    /// </summary>
    public partial class AgedPayables : ReportQueryBase
    {
    }

    /// <summary>
    /// Aged Payable detail.
    /// </summary>
    public partial class AgedPayableDetail : ReportQueryBase
    {
    }

    /// <summary>
    /// Vendor Expenses
    /// </summary>
    public partial class VendorExpenses : ReportQueryBase
    {
    }

    /// <summary>
    /// General Ledger
    /// </summary>
    public partial class GeneralLedger : ReportQueryBase
    {
    }

    /// <summary>
    /// Item Sales
    /// </summary>
    public partial class ItemSales : ReportQueryBase
    {
    }

    /// <summary>
    /// Customer Sales
    /// </summary>
    public partial class CustomerSales : ReportQueryBase
    {
    }

    /// <summary>
    /// Department Sales
    /// </summary>
    public partial class DepartmentSales : ReportQueryBase
    {
    }

    /// <summary>
    /// Class Sales
    /// </summary>
    public partial class ClassSales : ReportQueryBase
    {
    }

    /// <summary>
    /// Customer Income
    /// </summary>
    public partial class CustomerIncome : ReportQueryBase
    {
    }

    /// <summary>
    /// Balance sheet
    /// </summary>
    public partial class BalanceSheet : ReportQueryBase
    {
    }

    /// <summary>
    /// Profit and loss.
    /// </summary>
    public partial class ProfitAndLoss : ReportQueryBase
    {
    }

    /// <summary>
    /// Profit and loss detail.
    /// </summary>
    public partial class ProfitAndLossDetail : ReportQueryBase
    {
    }

    /// <summary>
    /// Trial Balance
    /// </summary>
    public partial class TrialBalance : ReportQueryBase
    {
    }

    /// <summary>
    /// Cash Flow
    /// </summary>
    public partial class CashFlow : ReportQueryBase
    {
    }

    /// <summary>
    /// Inventory Valuation Summary
    /// </summary>
    public partial class InventoryValuationSummary : ReportQueryBase
    {
    }

    /// <summary>
    /// Account List
    /// </summary>
    public partial class AccountList : ReportQueryBase
    {
    }


    /// <summary>
    /// Transaction List
    /// </summary>
    public partial class TransactionList : ReportQueryBase
    {
    }   



    #endregion

    #region Base class
    
    /// <summary>
    /// Report Query Base. Base class for all reports.
    /// </summary>
    public abstract partial class ReportQueryBase
    {
        

        /// <summary>
        /// time
        /// </summary>
        private DateTime timeField;

        /// <summary>
        /// time specified
        /// </summary>
        private bool timeFieldSpecified;

        /// <summary>
        /// report Name
        /// </summary>
        private string reportNameField;
 

        /// <summary>
        /// report basis specified
        /// </summary>
        private bool reportBasisFieldSpecified;

        /// <summary>
        /// start period
        /// </summary>
        private string startPeriodField;

        /// <summary>
        /// end period
        /// </summary>
        private string endPeriodField;

        /// <summary>
        /// sumarizeColumnBy
        /// </summary>
        private string summarizeColumnsByField;

        /// <summary>
        /// currency
        /// </summary>
        private string currencyField;

        /// <summary>
        /// customer
        /// </summary>
        private string customerField;

        /// <summary>
        /// vendor
        /// </summary>
        private string vendorField;

        /// <summary>
        /// employee
        /// </summary>
        private string employeeField;

        /// <summary>
        /// item
        /// </summary>
        private string itemField;

        /// <summary>
        /// class
        /// </summary>
        private string classField;

        /// <summary>
        /// department
        /// </summary>
        private string departmentField;

        /// <summary>
        /// Optional 
        /// </summary>
        private NameValue[] optionField;

  

        /// <summary>
        /// start page
        /// </summary>
        private int startPageField;

        /// <summary>
        /// chunk size
        /// </summary>
        private int chunkSizeField;

        /// <summary>
        /// start transaction date
        /// </summary>
        private DateTime startTransactionDate;

        /// <summary>
        /// end transaction date
        /// </summary>
        private DateTime endTransactionDate;

        /// <summary>
        /// date macro
        /// </summary>
        private DateMacro dateMacro;

        /// <summary>
        /// report basis
        /// </summary>
        private ReportBasisEnum reportBasisField;

        /// <summary>
        /// Gets or sets the value indicating the Start page.
        /// </summary>
        public int StartPage
        {
            get
            {
                return startPageField;
            }
            set
            {
                startPageField = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating the Chunk size.
        /// </summary>
        public int ChunkSize
        {
            get
            {
                return chunkSizeField;
            }
            set
            {
                chunkSizeField = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating the Start Transaction Date.
        /// </summary>
        public DateTime StartTransactionDate
        {
            get
            {
                return startTransactionDate;
            }
            set
            {
                startTransactionDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating the End Transaction Date.
        /// </summary>
        public DateTime EndTransactionDate
        {
            get
            {
                return endTransactionDate;
            }
            set
            {
                endTransactionDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating the Date Macro.
        /// </summary>
        public DateMacro DateMacro
        {
            get
            {
                return dateMacro;
            }
            set
            {
                dateMacro = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specify the basis of the report, Cash or Accrual
        /// </summary>
        public ReportBasisEnum ReportBasis
        {
            get
            {
                return reportBasisField;
            }
            set
            {
                reportBasisField = value;
            }
        }




    

        /// <summary>
        /// time
        /// </summary>
        public DateTime TimeField
        {
            get
            {
                return timeField;
            }
            set
            {
                timeField = value;
            }
        }


        /// <summary>
        /// time specified
        /// </summary>
        public bool TimeFieldSpecified
        {
            get
            {
                return timeFieldSpecified;
            }
            set
            {
                timeFieldSpecified = value;
            }
        }

        /// <summary>
        /// report Name
        /// </summary>
        public string ReportNameField
        {
            get
            {
                return reportNameField;
            }
            set
            {
                reportNameField = value;
            }
        }


        /// <summary>
        /// report basis specified
        /// </summary>
        public bool ReportBasisFieldSpecified
        {
            get
            {
                return reportBasisFieldSpecified;
            }
            set
            {
                reportBasisFieldSpecified = value;
            }
        }

        /// <summary>
        /// start period
        /// </summary>
        public string StartPeriodField
        {
            get
            {
                return startPeriodField;
            }
            set
            {
                startPeriodField = value;
            }
        }

        /// <summary>
        /// end period
        /// </summary>
        public string EndPeriodField
        {
            get
            {
                return endPeriodField;
            }
            set
            {
                endPeriodField = value;
            }
        }

        /// <summary>
        /// sumarizeColumnBy
        /// </summary>
        public string SummarizeColumnsByField
        {
            get
            {
                return summarizeColumnsByField;
            }
            set
            {
                summarizeColumnsByField = value;
            }
        }

        /// <summary>
        /// currency
        /// </summary>
        public string CurrencyField
        {
            get
            {
                return currencyField;
            }
            set
            {
                currencyField = value;
            }
        }

        /// <summary>
        /// customer
        /// </summary>
        public string CustomerField
        {
            get
            {
                return customerField;
            }
            set
            {
                customerField = value;
            }
        }

        /// <summary>
        /// vendor
        /// </summary>
        public string VendorField
        {
            get
            {
                return vendorField;
            }
            set
            {
                vendorField = value;
            }
        }

        /// <summary>
        /// employee
        /// </summary>
        public string EmployeeField
        {
            get
            {
                return employeeField;
            }
            set
            {
                employeeField = value;
            }
        }

        /// <summary>
        /// item
        /// </summary>
        public string ItemField
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
            }
        }

        /// <summary>
        /// class
        /// </summary>
        public string ClassField
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }

        /// <summary>
        /// department
        /// </summary>
        public string DepartmentField
        {
            get
            {
                return departmentField;
            }
            set
            {
                departmentField = value;
            }
        }

        /// <summary>
        /// Optional 
        /// </summary>       
        public NameValue[] OptionField
        {
            get
            {
                return optionField;
            }
            set
            {
                optionField = value;
            }
        }

     



    }

    #endregion
}
