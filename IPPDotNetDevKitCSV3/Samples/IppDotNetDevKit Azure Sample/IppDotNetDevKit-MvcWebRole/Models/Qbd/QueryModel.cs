using System;
using System.Collections.Generic;
using Intuit.Ipp.Data;
using System.Web.Mvc;

namespace IppDotNetDevKit_MvcWebRole.Models
{
    public class QueryModel : OperationStatus
    {
        public IEnumerable<Invoice> Entities { get; set; }

        public class WhereQuery : QueryModel
        {
            public decimal TotalAmt { get; set; }
            public OperationEnum OperationEnum { get; set; }
        }

        public class SelectQuery
        {
            public bool TotalAmt { get; set; }
            public bool Status { get; set; }
            public bool Id
            {
                get
                {
                    return true;
                }
            }
            public IEnumerable<Customer> ClientCustomers { get; set; }
        }

        public class CountQuery
        {
            public EntityStatusEnum Status { get; set; }
            public bool StatusSpecified { get; set; }
            public int Count { get; set; }
        }

        public class Customer
        {
            public string Id { get; set; }
            public decimal TotalAmt { get; set; }
            public string Status { get; set; }
        }

        public class OrderByModel : QueryModel
        {
            public bool MetaData_LastUpdatedTime_Descending { get; set; }
            public bool MetaData_LastUpdatedTime { get; set; }
            public bool MetaData_CreateTime_Descending { get; set; }
            public bool MetaData_CreateTime { get; set; }
        }

        public class ComplexQueryModel : QueryModel
        {
            public decimal TotalAmt { get; set; }
            public OperationEnum OperationEnum { get; set; }
            public bool MetaData_CreateTime_Descending { get; set; }
            public bool MetaData_CreateTime { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public class QueryStringModel : QueryModel
        {
            public string SimpleQuery { get; set; }
        }

        public class MultipleQueriesModel : OperationStatus
        {
            public IEnumerable<IEnumerable<Invoice>> InvoicesList { get; set; }
            public string Query1 { get; set; }
            public string Query2 { get; set; }
        }
    }

    public enum OperationEnum
    {
        EQUAL,
        NOT_EQUAL,
        GREATER_THAN,
        LESS_THAN,
        GREATER_THAN_EQUAL,
        LESS_THAN_EQUAL,
        NOT,
        LIKE,
        IN
    }
}