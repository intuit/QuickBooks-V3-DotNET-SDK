using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IDSLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryResult<Customer> customers = new QueryResult<Customer>();

            var query = from cusomter in customers
                        where cusomter.ContactNameField == "ContactNameField" && cusomter.ShipSameAsBillingField ==true
                        select cusomter;

           foreach(Customer cust in query)
           {
               Console.Write(cust.ContactNameField);
           }

           #region Queries

           // query ContactNameField, OtherAddressesField.id from cusotmers where ContactNameField = "contactName" orderby OpenBalanceField STARTPOSITION EQ 2 MAXRESULTS 5
            var query1 = customers.Where<Customer>(c => c.ContactNameField=="contactName").OrderBy(cc=>cc.OpenBalanceField).Skip(2).Take(5).Select(c=> new{ c.ContactNameField, c.OtherAddressesField[0].Id});

            // query count(*) from cusotmers where ContactNameField = "contactName" 
            var query2 = customers.Where<Customer>(c => c.ContactNameField == "contactName").Count();

            // Query *, OtherAddressesField.* from cusotmers
            var query3 = customers.Select(c => new {c.OtherAddressesField});

            // Query ContactNameField, OtherAddressesField.* from cusotmers
            var query4 = customers.Select(c => new { c.ContactNameField, c.OtherAddressesField });

            // Query * from customers where ContactNameField in ("value1","value2")    // ToDo : How to do IN in Linq?
            List<string> names = new List<string> () { "value1", "value2" };
            var query5 = from Customer in customers where names.Any ( name => Customer.ContactNameField.Equals ( name ) ) select Customer; //CHECK

            // QUERY * FROM customers WHERE ContactNameField LIKE 'G*g'
            var query6 = customers.Where(c => c.ContactNameField.StartsWith("G") && c.ContactNameField.EndsWith("g"));

            // QUERY * FROM customers WHERE OtherAddressesField.Line1 = "1234"
            var query7 = customers.Where(c=> c.OtherAddressesField[0].Line1 == "1234");

            // QUERY * FROM customers ORDERBY ContactNameField DESC  // ToDo : How to do DESC in linq?
            //var query8 = customers.OrderBy(c => c.ContactNameField);
            var query8 = customers.OrderByDescending ( c => c.ContactNameField );

            // QUERY * FROM customers WHERE ContactNameField EQ '234' STARTPOSITION EQ 21 MAXRESULTS 10
            var query9 = customers.Where(c => c.ContactNameField == "234").Skip(21).Take(10);

            // query header from customers where ContactNameField eq '312' and ShipSameAsBillingField eq true;
            var query10 = customers.Where(c => c.ContactNameField == "312" && c.ShipSameAsBillingField == true);

            // query NameAndId from customers
            var query11 = customers.Select(c => new { c.Id, c.Name });

            // query Overview from customers
            var query12 = customers.Select(c => c.Header); //CHECK

            // query HeaderLite from customers
            var query13 = "";

            // query HeaderFull from cusomters
            var query14 = "";

           #endregion

            #region Reports

            //QueryResult<ReportProfitAndLoss> plReports = new QueryResult<ReportProfitAndLoss>();
            //report profitandloss with startdate="2011-08-10T10:20:30-0700"
            //var report1 = plReports.Where(c => c.ReportBasis == ReportBasisEnum.Accrual);  // start date is not available in ProfitAndLoss report

            //report profitandloss with ReportBasis="Accrual" AND OfferingId neq "cmo"
            //var report2 = plReports.Where(c => c.ReportBasis == ReportBasisEnum.Accrual && c.OfferingId != offeringId.cmo);            

            #endregion

            #region Changed Data

            //Entities in ('invoice', 'customer', 'purchaseorder', 'estimate') and lastmodified time gt '2011-10-10T09:00:00-0700'

            #endregion
            Console.Read();
        }
    }
}
