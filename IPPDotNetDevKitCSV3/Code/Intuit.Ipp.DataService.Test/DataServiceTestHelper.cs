// -----------------------------------------------------------------------
// <copyright file="DataServiceTestHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.DataService.Test
{
    using System;
    using Intuit.Ipp.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;
    using System.Text;
    using Intuit.Ipp.DataService;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Intuit.Ipp.Core;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using Intuit.Ipp.Security;
    using System.Net;
    using System.Globalization;
    using System.IO;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.QueryFilter;
    using Intuit.Ipp.LinqExtender;



    /// <summary>
    /// Summary description for DataServiceTestHelper.
    /// </summary>

    internal static class DataServiceTestHelper
    {
        internal static Customer CreateCustomer()
        {
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 25);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);
            
            return customer;

        }

        internal static Vendor CreateVendor()
        {
            Vendor vendor = new Vendor();
            string guid = Guid.NewGuid().ToString("N");
            vendor.GivenName = guid.Substring(0, 25);
            vendor.Title = guid.Substring(0, 15);
            vendor.MiddleName = guid.Substring(0, 5);
            vendor.FamilyName = guid.Substring(0, 25);
            vendor.DisplayName = guid.Substring(0, 20);
            return vendor;
        }

        internal static Customer UpdateCustomerMiddleName(Customer customer, string middleName)
        {
            customer.MiddleName = middleName;
            return customer;
        }

        internal static Attachable CreateAttachable()
        {
            Attachable entity = new Attachable();
            entity.Lat = "25.293112341223";
            entity.Long = "-21.3253249834";
            entity.PlaceName = "Fake Place";
            entity.Note = "Attachable note 8cab7";
            entity.Tag = "Attachable tag f1a94";
            return entity;
        }

        internal static BillPayment CreateBillPayment_CheckPayment()
        {
            BillPayment billPayment = new BillPayment();
            
            Vendor vendor = new Vendor();
            //vendor.DisplayName = "00124a1f-ff2a-41ad-b3f9-8";
            vendor.Id = "35";

            BillPaymentCheck billPaymentCheck = new BillPaymentCheck();
            billPaymentCheck.BankAccountRef = new ReferenceType()
            {
                //name = "Bank (1091836770)",
                Value = "136"
            };

            
            CheckPayment checkPayment = new CheckPayment();
            checkPayment.AcctNum = "AcctNum" + GetGuid().Substring(0, 5);
            checkPayment.BankName = "BankName" + GetGuid().Substring(0, 5);
            checkPayment.CheckNum = "CheckNum" + GetGuid().Substring(0, 5);
            checkPayment.NameOnAcct = "Name" + GetGuid().Substring(0, 5);
            checkPayment.Status = "Status" + GetGuid().Substring(0, 5);
            billPaymentCheck.CheckDetail = checkPayment;

            PhysicalAddress payeeAddr = new PhysicalAddress();
            payeeAddr.Line1 = "Line 1";
            payeeAddr.Line2 = "Line 2";
            payeeAddr.City = "Mountain View";
            payeeAddr.CountrySubDivisionCode = "CA";
            payeeAddr.PostalCode = "94043";

            billPaymentCheck.PayeeAddr = payeeAddr;
            billPaymentCheck.PrintStatus = PrintStatusEnum.NeedToPrint;
            billPaymentCheck.PrintStatusSpecified = true;

            
            List<Line> lineList = new List<Line>();

            Line line1 = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
          
            line1.Amount = 100;
            line1.AmountSpecified = true;
            List<LinkedTxn> LinkedTxnList1 = new List<LinkedTxn>();
            LinkedTxn linkedTxn1 = new LinkedTxn();
            linkedTxn1.TxnId = "547";
            linkedTxn1.TxnType = TxnTypeEnum.Bill.ToString();
            LinkedTxnList1.Add(linkedTxn1);
            line1.LinkedTxn = LinkedTxnList1.ToArray();

            lineList.Add(line1);

            billPayment.AnyIntuitObject = billPaymentCheck;
            billPayment.PayType = BillPaymentTypeEnum.Check;
            billPayment.PayTypeSpecified = true;
            billPayment.Line = lineList.ToArray();

            billPayment.TotalAmt = 100;
            billPayment.TotalAmtSpecified = true;

            billPayment.VendorRef = new ReferenceType()
            {
                //name = vendor.DisplayName,
                Value = vendor.Id
            };

            return billPayment;
        }

        internal static String GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        internal static void VerifyBillPayment_BillPaymentCheck(BillPayment billPaymentAdded, BillPayment billPaymentToAdd)
        {

            Assert.IsNotNull(billPaymentToAdd);
            Assert.IsNotNull(billPaymentAdded);

            Assert.IsNotNull(billPaymentAdded.CurrencyRef);
            Assert.IsNotNull(billPaymentAdded.VendorRef);

            BillPaymentCheck billPayment_BillPaymentCheckToAdd = (BillPaymentCheck)billPaymentToAdd.AnyIntuitObject;
            BillPaymentCheck billPayment_BillPaymentCheckAdded = (BillPaymentCheck)billPaymentAdded.AnyIntuitObject;

            Assert.AreEqual(billPayment_BillPaymentCheckAdded.BankAccountRef.name.ToString(), billPayment_BillPaymentCheckToAdd.BankAccountRef.name.ToString());
            Assert.AreEqual(billPayment_BillPaymentCheckAdded.BankAccountRef.Value.ToString(), billPayment_BillPaymentCheckToAdd.BankAccountRef.Value.ToString());
            Assert.AreEqual(billPaymentAdded.PayType.ToString(), billPaymentToAdd.PayType.ToString());

        }
    }

}
