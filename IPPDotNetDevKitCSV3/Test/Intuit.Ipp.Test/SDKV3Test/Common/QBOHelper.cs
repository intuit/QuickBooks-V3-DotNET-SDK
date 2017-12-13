using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Intuit.Ipp.Data;
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



namespace Intuit.Ipp.Test
{
    class QBOHelper
    {
        #region Create Helper Methods

        internal static NameValue CreateNameValue(ServiceContext context)
        {
            NameValue nameValue = new NameValue();
            //nameValue.Name = "Name";
            //nameValue.Value = "Value";
            return nameValue;
        }



        internal static NameValue UpdateNameValue(ServiceContext context, NameValue entity)
        {
            //update the properties of entity
            return entity;
        }



        internal static Company CreateCompany(ServiceContext context)
        {
            Company company = new Company();
            company.CompanyName = "CompanyName";
            company.LegalName = "LegalName";
            PhysicalAddress companyAddr = new PhysicalAddress();
            companyAddr.Line1 = "Company_addr";
            companyAddr.Line2 = "Company_addr2";
            //companyAddr.Line3 = "line3";
            //companyAddr.Line4 = "Line4";
            //companyAddr.Line5 = "Line5";
            companyAddr.City = "Hyderabad";
            companyAddr.Country = "India";
            companyAddr.CountryCode = "009";
            //companyAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //companyAddr.PostalCode = "PostalCode";
            //companyAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //companyAddr.Lat = "Lat";
            //companyAddr.Long = "Long";
            //companyAddr.Tag = "Tag";
            //companyAddr.Note = "Note";
            company.CompanyAddr = companyAddr;
            PhysicalAddress customerCommunicationAddr = new PhysicalAddress();
            customerCommunicationAddr.Line1 = "Line1";
            customerCommunicationAddr.Line2 = "Line2";
            customerCommunicationAddr.Line3 = "Line3";
            //customerCommunicationAddr.Line4 = "Line4";
            //customerCommunicationAddr.Line5 = "Line5";
            customerCommunicationAddr.City = "hyd";
            customerCommunicationAddr.Country = "India";
            customerCommunicationAddr.CountryCode = "009";
            //customerCommunicationAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //customerCommunicationAddr.PostalCode = "PostalCode";
            //customerCommunicationAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //customerCommunicationAddr.Lat = "Lat";
            //customerCommunicationAddr.Long = "Long";
            //customerCommunicationAddr.Tag = "Tag";
            //customerCommunicationAddr.Note = "Note";
            company.CustomerCommunicationAddr = customerCommunicationAddr;
            //PhysicalAddress legalAddr = new PhysicalAddress();
            //legalAddr.Line1 = "Line1";
            //legalAddr.Line2 = "Line2";
            //legalAddr.Line3 = "Line3";
            //legalAddr.Line4 = "Line4";
            //legalAddr.Line5 = "Line5";
            //legalAddr.City = "City";
            //legalAddr.Country = "Country";
            //legalAddr.CountryCode = "CountryCode";
            //legalAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //legalAddr.PostalCode = "PostalCode";
            //legalAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //legalAddr.Lat = "Lat";
            //legalAddr.Long = "Long";
            //legalAddr.Tag = "Tag";
            //legalAddr.Note = "Note";
            //company.LegalAddr = legalAddr;
            //EmailAddress companyEmailAddr = new EmailAddress();
            //companyEmailAddr.Address = "Address";
            //companyEmailAddr.Default = true;
            //companyEmailAddr.DefaultSpecified = true;
            //companyEmailAddr.Tag = "Tag";
            //company.CompanyEmailAddr = companyEmailAddr;
            //EmailAddress customerCommunicationEmailAddr = new EmailAddress();
            //customerCommunicationEmailAddr.Address = "Address";
            //customerCommunicationEmailAddr.Default = true;
            //customerCommunicationEmailAddr.DefaultSpecified = true;
            //customerCommunicationEmailAddr.Tag = "Tag";
            //company.CustomerCommunicationEmailAddr = customerCommunicationEmailAddr;
            //WebSiteAddress companyURL = new WebSiteAddress();
            //companyURL.URI = "URI";
            //companyURL.Default = true;
            //companyURL.DefaultSpecified = true;
            //companyURL.Tag = "Tag";
            //company.CompanyURL = companyURL;
            TelephoneNumber primaryPhone = new TelephoneNumber();
            primaryPhone.DeviceType = "DeviceType";
            primaryPhone.CountryCode = "CountryCode";
            primaryPhone.AreaCode = "AreaCode";
            primaryPhone.ExchangeCode = "ExchangeCode";
            primaryPhone.Extension = "Extension";
            primaryPhone.FreeFormNumber = "FreeFormNumber";
            primaryPhone.Default = true;
            primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            company.PrimaryPhone = primaryPhone;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //company.OtherContactInfo = otherContactInfoList.ToArray();
            //company.CompanyFileName = "CompanyFileName";
            //company.FlavorStratum = "FlavorStratum";
            //company.SampleFile = true;
            //company.SampleFileSpecified = true;
            //company.CompanyUserId = "CompanyUserId";
            //company.CompanyUserAdminEmail = "CompanyUserAdminEmail";
            //company.CompanyStartDate = DateTime.UtcNow.Date;
            //company.CompanyStartDateSpecified = true;
            //company.EmployerId = "EmployerId";
            //company.FiscalYearStartMonth = MonthEnum.;
            //company.FiscalYearStartMonthSpecified = true;
            //company.TaxYearStartMonth = MonthEnum.;
            //company.TaxYearStartMonthSpecified = true;
            //company.QBVersion = "QBVersion";
            //company.Country = "Country";
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //company.ShipAddr = shipAddr;

            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //otherAddr.Line1 = "Line1";
            //otherAddr.Line2 = "Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            //otherAddr.City = "City";
            //otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //otherAddr.PostalCode = "PostalCode";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            //otherAddrList.Add(otherAddr);
            //company.OtherAddr = otherAddrList.ToArray();
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            //company.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //company.Fax = fax;
            //EmailAddress email = new EmailAddress();
            //email.Address = "Address";
            //email.Default = true;
            //email.DefaultSpecified = true;
            //email.Tag = "Tag";
            //company.Email = email;
            //WebSiteAddress webSite = new WebSiteAddress();
            //webSite.URI = "URI";
            //webSite.Default = true;
            //webSite.DefaultSpecified = true;
            //webSite.Tag = "Tag";
            //company.WebSite = webSite;
            //company.LastImportedTime = DateTime.UtcNow.Date;
            //company.LastImportedTimeSpecified = true;
            //company.SupportedLanguages = "SupportedLanguages";
            //company.DefaultTimeZone = "DefaultTimeZone";
            //company.MultiByteCharsSupported = true;
            //company.MultiByteCharsSupportedSpecified = true;

            //List<NameValue> nameValueList = new List<NameValue>();
            //NameValue nameValue = new NameValue();
            //nameValue.Name = "Name";
            //nameValue.Value = "Value";
            //nameValueList.Add(nameValue);
            //company.NameValue = nameValueList.ToArray();
            //company.CompanyInfoEx = 
            return company;
        }



        internal static Company UpdateCompany(ServiceContext context, Company entity)
        {
            //update the properties of entity
            entity.CompanyName = "ComapnyName" + Helper.GetGuid().Substring(0, 5);
            entity.LegalName = "legalName" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }


        internal static Company UpdateCompanySparse(ServiceContext context, string id, string syncToken)
        {
            Company entity = new Company();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.CompanyName = "ComapnyName" + Helper.GetGuid().Substring(0, 5);
            entity.LegalName = "legalName" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }

        internal static CompanyInfo CreateCompanyInfo(ServiceContext context)
        {
            CompanyInfo companyInfo = new CompanyInfo();
            companyInfo.CompanyName = "CompanyName";
            companyInfo.LegalName = "LegalName";
            PhysicalAddress companyAddr = new PhysicalAddress();
            companyAddr.Line1 = "company_addr";
            companyAddr.Line2 = "compnay_addr2";
            //companyAddr.Line3 = "Line3";
            //companyAddr.Line4 = "Line4";
            //companyAddr.Line5 = "Line5";
            companyAddr.City = "Bangalore";
            companyAddr.Country = "India";
            companyAddr.CountryCode = "CountryCode";
            //companyAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            companyAddr.PostalCode = "PostalCode";
            //companyAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //companyAddr.Lat = "Lat";
            //companyAddr.Long = "Long";
            //companyAddr.Tag = "Tag";
            //companyAddr.Note = "Note";
            companyInfo.CompanyAddr = companyAddr;
            PhysicalAddress customerCommunicationAddr = new PhysicalAddress();
            customerCommunicationAddr.Line1 = "addr_comunication";
            customerCommunicationAddr.Line2 = "addr2_communication";
            //customerCommunicationAddr.Line3 = "Line3";
            //customerCommunicationAddr.Line4 = "Line4";
            //customerCommunicationAddr.Line5 = "Line5";
            customerCommunicationAddr.City = "Bnagalore";
            customerCommunicationAddr.Country = "India";
            customerCommunicationAddr.CountryCode = "009";
            //customerCommunicationAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            customerCommunicationAddr.PostalCode = "PostalCode";
            //customerCommunicationAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //customerCommunicationAddr.Lat = "Lat";
            //customerCommunicationAddr.Long = "Long";
            //customerCommunicationAddr.Tag = "Tag";
            //customerCommunicationAddr.Note = "Note";
            companyInfo.CustomerCommunicationAddr = customerCommunicationAddr;
            PhysicalAddress legalAddr = new PhysicalAddress();
            legalAddr.Line1 = "legal_addr1" + Helper.GetGuid().Substring(0, 5);
            legalAddr.Line2 = "legal_addr2" + Helper.GetGuid().Substring(0, 5);
            //legalAddr.Line3 = "Line3";
            //legalAddr.Line4 = "Line4";    
            //legalAddr.Line5 = "Line5";
            legalAddr.City = "hyd";
            legalAddr.Country = "India";
            legalAddr.CountryCode = "0009";
            //legalAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //legalAddr.PostalCode = "PostalCode";
            //legalAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //legalAddr.Lat = "Lat";
            //legalAddr.Long = "Long";
            //legalAddr.Tag = "Tag";
            //legalAddr.Note = "Note";
            companyInfo.LegalAddr = legalAddr;
            //EmailAddress companyEmailAddr = new EmailAddress();
            //companyEmailAddr.Address = "Address";
            //companyEmailAddr.Default = true;
            //companyEmailAddr.DefaultSpecified = true;
            //companyEmailAddr.Tag = "Tag";
            //companyInfo.CompanyEmailAddr = companyEmailAddr;
            //EmailAddress customerCommunicationEmailAddr = new EmailAddress();
            //customerCommunicationEmailAddr.Address = "Address";
            //customerCommunicationEmailAddr.Default = true;
            //customerCommunicationEmailAddr.DefaultSpecified = true;
            //customerCommunicationEmailAddr.Tag = "Tag";
            //companyInfo.CustomerCommunicationEmailAddr = customerCommunicationEmailAddr;
            //WebSiteAddress companyURL = new WebSiteAddress();
            //companyURL.URI = "URI";
            //companyURL.Default = true;
            //companyURL.DefaultSpecified = true;
            //companyURL.Tag = "Tag";
            //companyInfo.CompanyURL = companyURL;
            //TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType";
            //primaryPhone.CountryCode = "CountryCode";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            //primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            //companyInfo.PrimaryPhone = primaryPhone;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //companyInfo.OtherContactInfo = otherContactInfoList.ToArray();
            //companyInfo.CompanyFileName = "CompanyFileName";
            //companyInfo.FlavorStratum = "FlavorStratum";
            //companyInfo.SampleFile = true;
            //companyInfo.SampleFileSpecified = true;
            //companyInfo.CompanyUserId = "CompanyUserId";
            //companyInfo.CompanyUserAdminEmail = "CompanyUserAdminEmail";
            //companyInfo.CompanyStartDate = DateTime.UtcNow.Date;
            //companyInfo.CompanyStartDateSpecified = true;
            //companyInfo.EmployerId = "EmployerId";
            //companyInfo.FiscalYearStartMonth = MonthEnum.;
            //companyInfo.FiscalYearStartMonthSpecified = true;
            //companyInfo.TaxYearStartMonth = MonthEnum.;
            //companyInfo.TaxYearStartMonthSpecified = true;
            //companyInfo.QBVersion = "QBVersion";
            //companyInfo.Country = "Country";
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //companyInfo.ShipAddr = shipAddr;

            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //otherAddr.Line1 = "Line1";
            //otherAddr.Line2 = "Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            //otherAddr.City = "City";
            //otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //otherAddr.PostalCode = "PostalCode";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            //otherAddrList.Add(otherAddr);
            //companyInfo.OtherAddr = otherAddrList.ToArray();
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            //companyInfo.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //companyInfo.Fax = fax;
            //EmailAddress email = new EmailAddress();
            //email.Address = "Address";
            //email.Default = true;
            //email.DefaultSpecified = true;
            //email.Tag = "Tag";
            //companyInfo.Email = email;
            //WebSiteAddress webAddr = new WebSiteAddress();
            //webAddr.URI = "URI";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            //companyInfo.WebAddr = webAddr;
            companyInfo.LastImportedTime = DateTime.UtcNow.Date;
            companyInfo.LastImportedTimeSpecified = true;
            //companyInfo.SupportedLanguages = "SupportedLanguages";
            companyInfo.DefaultTimeZone = "DefaultTimeZone";
            companyInfo.MultiByteCharsSupported = true;
            companyInfo.MultiByteCharsSupportedSpecified = true;

            //List<NameValue> nameValueList = new List<NameValue>();
            //NameValue nameValue = new NameValue();
            //nameValue.Name = "Name";
            //nameValue.Value = "Value";
            //nameValueList.Add(nameValue);
            //companyInfo.NameValue = nameValueList.ToArray();
            //companyInfo.CompanyInfoEx = 

            return companyInfo;
        }



        internal static CompanyInfo UpdateCompanyInfo(ServiceContext context, CompanyInfo entity)
        {
            //update the properties of entity
            entity.CompanyName = entity.CompanyName.Substring(0, Math.Min(entity.CompanyName.Length, 13)) + Helper.GetGuid().Substring(0, 5);
            entity.LegalName = "legal_updated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }


        internal static CompanyInfo SparseUpdateCompanyInfo(ServiceContext context, string Id, string syncToken, string companyName)
        {
            CompanyInfo entity = new CompanyInfo();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.CompanyName = companyName.Substring(0, 13) + Helper.GetGuid().Substring(0, 5);
            entity.LegalName = "legal_updated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }



        internal static Transaction CreateTransaction(ServiceContext context)
        {
            Transaction transaction = new Transaction();
            //transaction.DocNumber = "DocNumber";
            //transaction.TxnDate = DateTime.UtcNow.Date;
            //transaction.TxnDateSpecified = true;
            //transaction.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //transaction.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //transaction.ExchangeRate = new Decimal(100.00);
            //transaction.ExchangeRateSpecified = true;
            //transaction.PrivateNote = "PrivateNote";
            //transaction.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //transaction.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //transaction.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //transaction.TxnTaxDetail = txnTaxDetail;
            return transaction;
        }



        internal static Transaction UpdateTransaction(ServiceContext context, Transaction entity)
        {
            entity.PrivateNote = "updated note";
            return entity;
        }



        internal static Transaction UpdateTransactionSparse(ServiceContext context, string id, string syncToken)
        {
            Transaction entity = new Transaction();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrivateNote = "updated note";
            return entity;
        }


        internal static SalesTransaction CreateSalesTransaction(ServiceContext context)
        {
            SalesTransaction salesTransaction = new SalesTransaction();
            //salesTransaction.AutoDocNumber = true;
            //salesTransaction.AutoDocNumberSpecified = true;
            //salesTransaction.CustomerRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //salesTransaction.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //salesTransaction.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //salesTransaction.ShipAddr = shipAddr;
            //salesTransaction.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.DueDate = DateTime.UtcNow.Date;
            //salesTransaction.DueDateSpecified = true;
            //salesTransaction.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.PONumber = "PONumber";
            //salesTransaction.FOB = "FOB";
            //salesTransaction.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.ShipDate = DateTime.UtcNow.Date;
            //salesTransaction.ShipDateSpecified = true;
            //salesTransaction.TrackingNum = "TrackingNum";
            //salesTransaction.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //salesTransaction.GlobalTaxCalculationSpecified = true;
            //salesTransaction.TotalAmt = new Decimal(100.00);
            //salesTransaction.TotalAmtSpecified = true;
            //salesTransaction.HomeTotalAmt = new Decimal(100.00);
            //salesTransaction.HomeTotalAmtSpecified = true;
            //salesTransaction.ApplyTaxAfterDiscount = true;
            //salesTransaction.ApplyTaxAfterDiscountSpecified = true;
            //salesTransaction.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.PrintStatus = PrintStatusEnum.;
            //salesTransaction.PrintStatusSpecified = true;
            //salesTransaction.EmailStatus = EmailStatusEnum.;
            //salesTransaction.EmailStatusSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //salesTransaction.BillEmail = billEmail;
            //salesTransaction.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.Balance = new Decimal(100.00);
            //salesTransaction.BalanceSpecified = true;
            //salesTransaction.FinanceCharge = true;
            //salesTransaction.FinanceChargeSpecified = true;
            //salesTransaction.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.PaymentRefNum = "PaymentRefNum";
            //salesTransaction.PaymentType = PaymentTypeEnum.;
            //salesTransaction.PaymentTypeSpecified = true;
            //salesTransaction.AnyIntuitObject = 
            //salesTransaction.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.DocNumber = "DocNumber";
            //salesTransaction.TxnDate = DateTime.UtcNow.Date;
            //salesTransaction.TxnDateSpecified = true;
            //salesTransaction.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesTransaction.ExchangeRate = new Decimal(100.00);
            //salesTransaction.ExchangeRateSpecified = true;
            //salesTransaction.PrivateNote = "PrivateNote";
            //salesTransaction.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //salesTransaction.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 

            //lineList.Add(line);
            //salesTransaction.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //salesTransaction.TxnTaxDetail = txnTaxDetail;
            return salesTransaction;
        }



        internal static SalesTransaction UpdateSalesTransaction(ServiceContext context, SalesTransaction entity)
        {
            //update the properties of entity
            return entity;
        }


        internal static SalesTransaction UpdateSalesTransactionSparse(ServiceContext context, string id, string syncToken)
        {
            SalesTransaction entity = new SalesTransaction();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Balance = new Decimal(100);
            entity.BalanceSpecified = true;
            return entity;
        }


        internal static Invoice CreateInvoice(ServiceContext context)
        {
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            TaxCode taxCode = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
            Account account = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsReceivable, AccountClassificationEnum.Liability);

            Invoice invoice = new Invoice();
            invoice.Deposit = new Decimal(0.00);
            invoice.DepositSpecified = true;
            invoice.AllowIPNPayment = false;
            invoice.AllowIPNPaymentSpecified = true;
            //invoice.InvoiceEx = 

            //invoice.AutoDocNumber = true;
            //invoice.AutoDocNumberSpecified = true;
            invoice.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //invoice.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //invoice.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //invoice.ShipAddr = shipAddr;
            //invoice.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            invoice.DueDate = DateTime.UtcNow.Date;
            invoice.DueDateSpecified = true;
            //invoice.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.PONumber = "PONumber";
            //invoice.FOB = "FOB";
            //invoice.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.ShipDate = DateTime.UtcNow.Date;
            //invoice.ShipDateSpecified = true;
            //invoice.TrackingNum = "TrackingNum";
            invoice.GlobalTaxCalculation = GlobalTaxCalculationEnum.TaxExcluded;
            invoice.GlobalTaxCalculationSpecified = true;
            invoice.TotalAmt = new Decimal(0.00);
            invoice.TotalAmtSpecified = true;
            //invoice.HomeTotalAmt = new Decimal(100.00);
            //invoice.HomeTotalAmtSpecified = true;
            invoice.ApplyTaxAfterDiscount = false;
            invoice.ApplyTaxAfterDiscountSpecified = true;
            //invoice.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            invoice.PrintStatus = PrintStatusEnum.NotSet;
            invoice.PrintStatusSpecified = true;
            invoice.EmailStatus = EmailStatusEnum.NotSet;
            invoice.EmailStatusSpecified = true;

            EmailAddress billEmail = new EmailAddress();
            billEmail.Address = @"abc@gmail.com";
            billEmail.Default = true;
            billEmail.DefaultSpecified = true;
            billEmail.Tag = "Tag";
            invoice.BillEmail = billEmail;

            EmailAddress billEmailcc = new EmailAddress();
            billEmailcc.Address = @"def@gmail.com";
            billEmailcc.Default = true;
            billEmailcc.DefaultSpecified = true;
            billEmailcc.Tag = "Tag";
            invoice.BillEmailCc = billEmailcc;

            EmailAddress billEmailbcc = new EmailAddress();
            billEmailbcc.Address = @"xyz@gmail.com";
            billEmailbcc.Default = true;
            billEmailbcc.DefaultSpecified = true;
            billEmailbcc.Tag = "Tag";
            invoice.BillEmailBcc = billEmailbcc;


            invoice.ARAccountRef = new ReferenceType()
            {
                type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account),
                name = "Account Receivable",
                Value = "QB:37"
            };
            invoice.Balance = new Decimal(0.00);
            invoice.BalanceSpecified = true;



            //invoice.FinanceCharge = true;
            //invoice.FinanceChargeSpecified = true;
            //invoice.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.PaymentRefNum = "PaymentRefNum";
            //invoice.PaymentType = PaymentTypeEnum.;
            //invoice.PaymentTypeSpecified = true;
            //invoice.AnyIntuitObject = 
            //invoice.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.DocNumber = "DocNumber";
            invoice.TxnDate = DateTime.UtcNow.Date;
            invoice.TxnDateSpecified = true;
            //invoice.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //invoice.ExchangeRate = new Decimal(100.00);
            //invoice.ExchangeRateSpecified = true;
            //invoice.PrivateNote = "PrivateNote";
            //invoice.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //invoice.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.DescriptionOnly;
            line.DetailTypeSpecified = true;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 

            lineList.Add(line);
            invoice.Line = lineList.ToArray();
            TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            txnTaxDetail.DefaultTaxCodeRef = new ReferenceType()
            {
                Value = taxCode.Id,
                type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer),
                name = taxCode.Name
            };
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            txnTaxDetail.TotalTax = new Decimal(0.00);
            txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //invoice.TxnTaxDetail = txnTaxDetail;
            return invoice;
        }



        internal static Invoice UpdateInvoice(ServiceContext context, Invoice entity)
        {
            //update the properties of entity
            entity.DocNumber = "updated" + Helper.GetGuid().Substring(0, 3);

            return entity;
        }


        internal static Invoice SparseUpdateInvoice(ServiceContext context, string Id, string syncToken)
        {
            Invoice entity = new Invoice();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DocNumber = "sparseupdated" + Helper.GetGuid().Substring(0, 3);
            return entity;
        }



        internal static SalesReceipt CreateSalesReceipt(ServiceContext context)
        {
            SalesReceipt salesReceipt = new SalesReceipt();
            //salesReceipt.SalesReceiptEx = 
            //salesReceipt.AutoDocNumber = true;
            //salesReceipt.AutoDocNumberSpecified = true;
            //Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            //salesReceipt.CustomerRef = new ReferenceType()
            //{
            //    name = "030ee948-bda4-421d-977a-1",
            //    Value = "19"
            //};
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //salesReceipt.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //salesReceipt.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //salesReceipt.ShipAddr = shipAddr;
            //salesReceipt.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.DueDate = DateTime.UtcNow.Date;
            //salesReceipt.DueDateSpecified = true;
            //salesReceipt.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.PONumber = "PONumber";
            //salesReceipt.FOB = "FOB";
            //salesReceipt.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.ShipDate = DateTime.UtcNow.Date;
            //salesReceipt.ShipDateSpecified = true;
            //salesReceipt.TrackingNum = "TrackingNum";
            //salesReceipt.GlobalTaxCalculation = GlobalTaxCalculationEnum.TaxExcluded;
            //salesReceipt.GlobalTaxCalculationSpecified = true;
            salesReceipt.TotalAmt = new Decimal(100.00);
            salesReceipt.TotalAmtSpecified = true;
            //salesReceipt.HomeTotalAmt = new Decimal(100.00);
            //salesReceipt.HomeTotalAmtSpecified = true;
            salesReceipt.ApplyTaxAfterDiscount = false;
            salesReceipt.ApplyTaxAfterDiscountSpecified = true;
            //salesReceipt.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesReceipt.PrintStatus = PrintStatusEnum.NeedToPrint;
            salesReceipt.PrintStatusSpecified = true;
            salesReceipt.EmailStatus = EmailStatusEnum.NotSet;
            salesReceipt.EmailStatusSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address@Intuit.com";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //salesReceipt.BillEmail = billEmail;
            //salesReceipt.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesReceipt.Balance = new Decimal(0.00);
            salesReceipt.BalanceSpecified = true;
            //salesReceipt.FinanceCharge = true;
            //salesReceipt.FinanceChargeSpecified = true;
            //salesReceipt.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.PaymentRefNum = "PaymentRefNum";
            //salesReceipt.PaymentType = PaymentTypeEnum.Cash;
            //salesReceipt.PaymentTypeSpecified = true;
            //salesReceipt.AnyIntuitObject = 
            Account account = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Liability);
            salesReceipt.DepositToAccountRef = new ReferenceType()
            {
                name = account.Name,
                Value = account.Id
            };
            salesReceipt.DocNumber = "1003";
            salesReceipt.TxnDate = DateTime.UtcNow.Date;
            salesReceipt.TxnDateSpecified = true;
            //salesReceipt.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesReceipt.ExchangeRate = new Decimal(100.00);
            //salesReceipt.ExchangeRateSpecified = true;
            //salesReceipt.PrivateNote = "PrivateNote";
            //salesReceipt.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //salesReceipt.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            line.LineNum = "1";
            line.Description = "Description";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            Item item = Helper.FindOrAdd<Item>(context, new Item());
            TaxCode findOrAddResult = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
            TaxCode taxCode = Helper.FindAll<TaxCode>(context, new TaxCode())[0];
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                Qty = 1,
                QtySpecified = true,

                ItemRef = new ReferenceType()
                {
                    name = item.Name,
                    Value = item.Id
                },
                TaxCodeRef = new ReferenceType()
                {
                    name = taxCode.Name,
                    Value = taxCode.Id
                }
            };

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            lineList.Add(line);
            salesReceipt.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //salesReceipt.TxnTaxDetail = txnTaxDetail;
            return salesReceipt;
        }



        internal static SalesReceipt UpdateSalesReceipt(ServiceContext context, SalesReceipt entity)
        {
            //SalesReceipt e1 = new SalesReceipt();
            //e1.Id = "1476";
            //e1.PaymentRefNum = "38";
            //e1.sparse = true;
            //e1.sparseSpecified = true;
            //e1.EmailStatus = EmailStatusEnum.NeedToSend;
            //e1.EmailStatusSpecified = true;
            //e1.BillEmail = new EmailAddress() { Address = "address@email.com" };
            //e1.SyncToken = "1";
            //return e1;
            entity.PaymentRefNum = "33";
            entity.PrintStatus = PrintStatusEnum.NeedToPrint;
            entity.PrintStatusSpecified = true;
            entity.EmailStatus = EmailStatusEnum.NeedToSend;
            entity.EmailStatusSpecified = true;
            entity.BillEmail = new EmailAddress() { Address = "address@email.com" };
            return entity;
        }


        internal static SalesReceipt SparseUpdateSalesReceipt(ServiceContext context, string Id, string syncToken)
        {
            SalesReceipt entity = new SalesReceipt();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrintStatus = PrintStatusEnum.PrintComplete;
            entity.PrintStatusSpecified = true;
            return entity;
        }



        internal static Estimate CreateEstimate(ServiceContext context)
        {
            Estimate estimate = new Estimate();
            estimate.ExpirationDate = DateTime.UtcNow.Date.AddDays(15);
            estimate.ExpirationDateSpecified = true;
            estimate.TxnDate = DateTime.UtcNow.Date;
            estimate.TxnDateSpecified = true;
            //estimate.AcceptedBy = "AcceptedBy";
            //estimate.AcceptedDate = DateTime.UtcNow.Date;
            //estimate.AcceptedDateSpecified = true;
            //estimate.EstimateEx = 
            //estimate.AutoDocNumber = true;
            //estimate.AutoDocNumberSpecified = true;
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            estimate.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //estimate.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //estimate.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //estimate.ShipAddr = shipAddr;
            //estimate.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.DueDate = DateTime.UtcNow.Date;
            //estimate.DueDateSpecified = true;
            //estimate.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.PONumber = "PONumber";
            //estimate.FOB = "FOB";
            //estimate.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.ShipDate = DateTime.UtcNow.Date;
            //estimate.ShipDateSpecified = true;
            //estimate.TrackingNum = "TrackingNum";
            //estimate.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //estimate.GlobalTaxCalculationSpecified = true;
            estimate.TotalAmt = new Decimal(100.00);
            estimate.TotalAmtSpecified = true;
            //estimate.HomeTotalAmt = new Decimal(100.00);
            //estimate.HomeTotalAmtSpecified = true;
            //estimate.ApplyTaxAfterDiscount = true;
            //estimate.ApplyTaxAfterDiscountSpecified = true;
            //estimate.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.PrintStatus = PrintStatusEnum.;
            //estimate.PrintStatusSpecified = true;
            //estimate.EmailStatus = EmailStatusEnum.;
            //estimate.EmailStatusSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //estimate.BillEmail = billEmail;
            //estimate.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.Balance = new Decimal(100.00);
            //estimate.BalanceSpecified = true;
            //estimate.FinanceCharge = true;
            //estimate.FinanceChargeSpecified = true;
            //estimate.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.PaymentRefNum = "PaymentRefNum";
            //estimate.PaymentType = PaymentTypeEnum.;
            //estimate.PaymentTypeSpecified = true;
            //estimate.AnyIntuitObject = 
            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            estimate.DepositToAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            //estimate.DocNumber = "DocNumber";

            //estimate.TxnDateSpecified = true;
            //estimate.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //estimate.ExchangeRate = new Decimal(100.00);
            //estimate.ExchangeRateSpecified = true;
            //estimate.PrivateNote = "PrivateNote";
            //estimate.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //estimate.LinkedTxn = linkedTxnList.ToArray();
            Item item = Helper.FindOrAdd<Item>(context, new Item());

            estimate.TotalAmt = new Decimal(100.00);
            TaxCode findOrAddResult = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
            TaxCode taxcode = Helper.FindAll<TaxCode>(context, new TaxCode())[0];
            if (taxcode.SalesTaxRateList != null)
            {
                TaxRate taxRateToFind = new TaxRate();
                taxRateToFind.Id = taxcode.SalesTaxRateList.TaxRateDetail[0].TaxRateRef.Value;
                TaxRate taxRate = Helper.FindById<TaxRate>(context, taxRateToFind);
                estimate.TotalAmt += estimate.TotalAmt * (taxRate.RateValue / 100);
            }
            estimate.TotalAmtSpecified = true;

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            line.LineNum = "1";
            line.Description = "Description";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType() { name = item.Name, Value = item.Id },
                TaxCodeRef = new ReferenceType() { name = taxcode.Name, Value = taxcode.Id }
            };

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            lineList.Add(line);
            estimate.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //estimate.TxnTaxDetail = txnTaxDetail;
            return estimate;
        }



        internal static Estimate UpdateEstimate(ServiceContext context, Estimate entity)
        {
            //update the properties of entity
            entity.ExpirationDate = DateTime.UtcNow.Date.AddDays(15);
            entity.ExpirationDateSpecified = true;
            entity.TxnDate = DateTime.UtcNow.Date;
            entity.TxnDateSpecified = true;
            return entity;
        }

        internal static Estimate SparseUpdateEstimate(ServiceContext context, string Id, string syncToken)
        {
            Estimate entity = new Estimate();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.ExpirationDate = DateTime.UtcNow.Date.AddDays(15);
            entity.ExpirationDateSpecified = true;
            entity.TxnDate = DateTime.UtcNow.Date;
            entity.TxnDateSpecified = true;
            return entity;
        }



        internal static FixedAsset CreateFixedAsset(ServiceContext context)
        {
            FixedAsset fixedAsset = new FixedAsset();
            fixedAsset.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            fixedAsset.Active = true;
            fixedAsset.ActiveSpecified = true;
            fixedAsset.AcquiredAs = AcquiredAsEnum.New;
            fixedAsset.AcquiredAsSpecified = true;
            fixedAsset.PurchaseDesc = "PurchaseDesc";
            fixedAsset.PurchaseDate = DateTime.UtcNow.Date;
            fixedAsset.PurchaseDateSpecified = true;
            fixedAsset.PurchaseCost = new Decimal(100.00);
            fixedAsset.PurchaseCostSpecified = true;
            //fixedAsset.Vendor = "Vendor";
            //fixedAsset.AssetAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            fixedAsset.SalesDesc = "SalesDesc";
            fixedAsset.SalesDate = DateTime.UtcNow.Date;
            fixedAsset.SalesDateSpecified = true;
            fixedAsset.SalesPrice = new Decimal(100.00);
            fixedAsset.SalesPriceSpecified = true;
            fixedAsset.SalesExpense = new Decimal(100.00);
            fixedAsset.SalesExpenseSpecified = true;
            fixedAsset.Location = "Loc_" + Helper.GetGuid().Substring(0, 5);
            fixedAsset.PONumber = "797090";
            fixedAsset.SerialNumber = "SerialNumber";
            fixedAsset.WarrantyExpDate = DateTime.UtcNow.Date;
            fixedAsset.WarrantyExpDateSpecified = true;
            fixedAsset.Description = "Description";
            fixedAsset.Notes = "Notes";
            Int32 int32 = new Int32();
            fixedAsset.AssetNum = int32;
            fixedAsset.AssetNumSpecified = true;
            fixedAsset.CostBasis = new Decimal(100.00);
            fixedAsset.CostBasisSpecified = true;
            fixedAsset.Depreciation = new Decimal(100.00);
            fixedAsset.DepreciationSpecified = true;
            fixedAsset.BookValue = new Decimal(100.00);
            fixedAsset.BookValueSpecified = true;
            //fixedAsset.FixedAssetEx = 
            return fixedAsset;
        }



        internal static FixedAsset UpdateFixedAsset(ServiceContext context, FixedAsset entity)
        {
            //update the properties of entity
            entity.Name = "name_updated" + Helper.GetGuid().Substring(0, 5);
            entity.SalesDesc = "sales desc updated";

            return entity;
        }


        internal static FixedAsset UpdateFixedAssetSparse(ServiceContext context, string id, string syncToken)
        {
            FixedAsset entity = new FixedAsset();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "name_sparseupdated" + Helper.GetGuid().Substring(0, 5);
            entity.SalesDesc = "sales desc sparse updated";

            return entity;
        }


        internal static TaxCode CreateTaxCode(ServiceContext context)
        {
            TaxCode taxCode = new TaxCode();
            taxCode.Name = "VAT" + Helper.GetGuid().Substring(0, 10);
            taxCode.Description = "Value Added Tax";
            taxCode.Active = true;
            taxCode.ActiveSpecified = true;
            taxCode.Taxable = true;
            taxCode.TaxableSpecified = true;
            taxCode.TaxGroup = true;
            taxCode.TaxGroupSpecified = true;
            //TaxRateList salesTaxRateList = new TaxRateList();

            //List<TaxRateDetail> taxRateDetailList = new List<TaxRateDetail>();
            //TaxRateDetail taxRateDetail = new TaxRateDetail();
            //taxRateDetail.TaxRateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxRateDetail.TaxTypeApplicable = TaxTypeApplicablityEnum.;
            //taxRateDetail.TaxTypeApplicableSpecified = true;
            //taxRateDetail.TaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOrderSpecified = true;
            //taxRateDetail.TaxOnTaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOnTaxOrderSpecified = true;
            //taxRateDetailList.Add(taxRateDetail);
            //taxRateList.TaxRateDetail = taxRateDetailList.ToArray();
            //salesTaxRateList.originatingGroupId = "originatingGroupId";
            //taxCode.SalesTaxRateList = salesTaxRateList;
            //TaxRateList purchaseTaxRateList = new TaxRateList();

            //List<TaxRateDetail> taxRateDetailList = new List<TaxRateDetail>();
            //TaxRateDetail taxRateDetail = new TaxRateDetail();
            //taxRateDetail.TaxRateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxRateDetail.TaxTypeApplicable = TaxTypeApplicablityEnum.;
            //taxRateDetail.TaxTypeApplicableSpecified = true;
            //taxRateDetail.TaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOrderSpecified = true;
            //taxRateDetail.TaxOnTaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOnTaxOrderSpecified = true;
            //taxRateDetailList.Add(taxRateDetail);
            //taxRateList.TaxRateDetail = taxRateDetailList.ToArray();
            //purchaseTaxRateList.originatingGroupId = "originatingGroupId";
            //taxCode.PurchaseTaxRateList = purchaseTaxRateList;
            //TaxRateList adjustmentTaxRateList = new TaxRateList();

            //List<TaxRateDetail> taxRateDetailList = new List<TaxRateDetail>();
            //TaxRateDetail taxRateDetail = new TaxRateDetail();
            //taxRateDetail.TaxRateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxRateDetail.TaxTypeApplicable = TaxTypeApplicablityEnum.;
            //taxRateDetail.TaxTypeApplicableSpecified = true;
            //taxRateDetail.TaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOrderSpecified = true;
            //taxRateDetail.TaxOnTaxOrder = int32;
            //Int32 int32 = new Int32();

            //taxRateDetail.TaxOnTaxOrderSpecified = true;
            //taxRateDetailList.Add(taxRateDetail);
            //taxRateList.TaxRateDetail = taxRateDetailList.ToArray();
            //adjustmentTaxRateList.originatingGroupId = "originatingGroupId";
            //taxCode.AdjustmentTaxRateList = adjustmentTaxRateList;
            //taxCode.TaxCodeEx = 
            return taxCode;
        }



        internal static TaxCode UpdateTaxCode(ServiceContext context, TaxCode entity)
        {
            entity.Name = "VAT" + Helper.GetGuid().Substring(0, 10);
            entity.Description = "Value Added Tax";
            return entity;
        }

        internal static TaxCode SparseUpdateTaxCode(ServiceContext context, string Id, string syncToken)
        {
            TaxCode entity = new TaxCode();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "VAT" + Helper.GetGuid().Substring(0, 10);
            entity.Description = "Value Added Tax";
            return entity;
        }



        internal static TaxRate CreateTaxRate(ServiceContext context)
        {
            TaxRate taxRate = new TaxRate();
            taxRate.Name = "Name";
            taxRate.Description = "Description";
            taxRate.Active = true;
            taxRate.ActiveSpecified = true;
            taxRate.RateValue = new Decimal(100.00);
            taxRate.RateValueSpecified = true;
            //taxRate.AgencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxRate.TaxReturnLineRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};

            //List<EffectiveTaxRate> effectiveTaxRateList = new List<EffectiveTaxRate>();
            //EffectiveTaxRate effectiveTaxRate = new EffectiveTaxRate();
            //effectiveTaxRate.RateValue = new Decimal(100.00);
            //effectiveTaxRate.RateValueSpecified = true;
            //effectiveTaxRate.EffectiveDate = DateTime.UtcNow.Date;
            //effectiveTaxRate.EffectiveDateSpecified = true;
            //effectiveTaxRate.EndDate = DateTime.UtcNow.Date;
            //effectiveTaxRate.EndDateSpecified = true;
            //effectiveTaxRate.EffectiveTaxRateEx = 
            //effectiveTaxRateList.Add(effectiveTaxRate);
            //taxRate.EffectiveTaxRate = effectiveTaxRateList.ToArray();
            taxRate.SpecialTaxType = SpecialTaxTypeEnum.ADJUSTMENT_RATE;
            taxRate.SpecialTaxTypeSpecified = true;
            taxRate.DisplayType = TaxRateDisplayTypeEnum.ReadOnly;
            taxRate.DisplayTypeSpecified = true;
            //taxRate.TaxRateEx = 
            return taxRate;
        }



        internal static TaxRate UpdateTaxRate(ServiceContext context, TaxRate entity)
        {
            entity.Name = "UpdatedName";
            entity.Description = "UpdatedDescription";
            return entity;
        }


        internal static TaxRate SparseUpdateTaxRate(ServiceContext context, string Id, string syncToken)
        {
            TaxRate entity = new TaxRate();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "SparseUpdatedName";
            entity.Description = "SparseUpdatedDescription";
            return entity;
        }



        internal static Account CreateAccount(ServiceContext context, AccountTypeEnum accountType, AccountClassificationEnum classification)
        {
            Account account = new Account();

            String guid = Helper.GetGuid();
            account.Name = "Name_" + guid;
            //account.SubAccount = true;
            //account.SubAccountSpecified = true;
            //account.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //account.Description = "Description";
            account.FullyQualifiedName = account.Name;
            //account.Active = true;
            //account.ActiveSpecified = true;
            account.Classification = classification;
            account.ClassificationSpecified = true;
            account.AccountType = accountType;
            account.AccountTypeSpecified = true;
            //account.AccountSubType = "AccountSubType";
            //account.AcctNum = "AcctNum";
            //account.BankNum = "BankNum";
            if (accountType != AccountTypeEnum.Expense && accountType != AccountTypeEnum.AccountsPayable && accountType != AccountTypeEnum.AccountsReceivable)
            {
                //TestComment:  Opening Balances not working for QBO Item tests
                //account.OpeningBalance = new Decimal(100.00);
                //account.OpeningBalanceSpecified = true;
                //account.OpeningBalanceDate = DateTime.UtcNow.Date;
                //account.OpeningBalanceDateSpecified = true;
            }
            //account.CurrentBalance = new Decimal(100.00);
            //account.CurrentBalanceSpecified = true;
            //account.CurrentBalanceWithSubAccounts = new Decimal(100.00);
            //account.CurrentBalanceWithSubAccountsSpecified = true;
            account.CurrencyRef = new ReferenceType()
            {
                name = "United States Dollar",
                Value = "USD"
            };
            //account.TaxAccount = true;
            //account.TaxAccountSpecified = true;
            //account.TaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //account.AccountEx = 
            return account;
        }

        //France
        internal static Account CreateAccountFrance(ServiceContext context, AccountTypeEnum accountType, AccountClassificationEnum classification)
        {
            Account account = new Account();

            String guid = Helper.GetGuid();
            account.Name = "Nam_" + Helper.GetGuid().Substring(0, 3);

            //newly added field
            account.AccountAlias = "al_" + Helper.GetGuid().Substring(0, 3);
            account.TxnLocationType = "WithinFrance";
            //WithinFrance
            //FranceOverseas
            //OutsideFranceWithEU
            //OutsideEU
            account.AcctNum = "6" + "012000000";
            // 6 for expense account

            account.FullyQualifiedName = account.Name;
            account.Classification = classification;
            account.ClassificationSpecified = true;
            account.AccountType = accountType;
            account.AccountTypeSpecified = true;

            return account;
        }



        internal static Account UpdateAccount(ServiceContext context, Account entity)
        {
            //update the properties of entity
            entity.Name = "Name_" + Helper.GetGuid().Substring(0, 5);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }


        internal static Account SparseUpdateAccount(ServiceContext context, string Id, string syncToken)
        {
            Account entity = new Account();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Update Name_" + Helper.GetGuid().Substring(0, 5);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }



        internal static Purchase CreatePurchase(ServiceContext context, PaymentTypeEnum paymentType)
        {
            Purchase purchase = new Purchase();

            Account account = null;

            switch (paymentType)
            {
                case PaymentTypeEnum.Cash:
                    account = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
                    break;
                case PaymentTypeEnum.Check:
                    account = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
                    break;
                case PaymentTypeEnum.CreditCard:
                    account = Helper.FindOrAddAccount(context, AccountTypeEnum.CreditCard, AccountClassificationEnum.Expense);
                    break;
                case PaymentTypeEnum.Other:
                    break;
                default:
                    break;
            }

            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());

            purchase.AccountRef = new ReferenceType()
            {
                //type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account),
                name = account.Name,
                Value = account.Id
            };
            //purchase.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchase.PaymentRefNum = "PaymentRefNum";
            purchase.PaymentType = paymentType;
            purchase.PaymentTypeSpecified = true;
            //purchase.AnyIntuitObject = ;
            purchase.EntityRef = new ReferenceType()
            {
                name = customer.DisplayName,
                type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer),
                Value = customer.Id
            };
            if (paymentType == PaymentTypeEnum.CreditCard || paymentType == PaymentTypeEnum.Cash)
            {
                purchase.Credit = false;
                purchase.CreditSpecified = true;
            }
            //PhysicalAddress remitToAddr = new PhysicalAddress();
            //remitToAddr.Line1 = "Line1";
            //remitToAddr.Line2 = "Line2";
            //remitToAddr.Line3 = "Line3";
            //remitToAddr.Line4 = "Line4";
            //remitToAddr.Line5 = "Line5";
            //remitToAddr.City = "City";
            //remitToAddr.Country = "Country";
            //remitToAddr.CountryCode = "CountryCode";
            //remitToAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //remitToAddr.PostalCode = "PostalCode";
            //remitToAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //remitToAddr.Lat = "Lat";
            //remitToAddr.Long = "Long";
            //remitToAddr.Tag = "Tag";
            //remitToAddr.Note = "Note";
            //purchase.RemitToAddr = remitToAddr;
            purchase.TotalAmt = new Decimal(1000.00);
            purchase.TotalAmtSpecified = true;
            //purchase.TxnId = "TxnId";
            //purchase.TxnNum = "TxnNum";
            //purchase.Memo = "Memo";
            if (paymentType != PaymentTypeEnum.CreditCard)
            {
                purchase.PrintStatus = PrintStatusEnum.NotSet;
                purchase.PrintStatusSpecified = true;
            }
            //purchase.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //purchase.GlobalTaxCalculationSpecified = true;
            //purchase.PurchaseEx = 

            //purchase.DocNumber = "DocNumber";
            purchase.TxnDate = DateTime.UtcNow.Date;
            purchase.TxnDateSpecified = true;
            //purchase.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchase.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchase.ExchangeRate = new Decimal(100.00);
            //purchase.ExchangeRateSpecified = true;
            //purchase.PrivateNote = "PrivateNote";
            //purchase.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //purchase.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description for Line";
            line.Amount = new Decimal(1000.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;
            AccountBasedExpenseLineDetail lineDetail = new AccountBasedExpenseLineDetail();
            Account lineAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            lineDetail.AccountRef = new ReferenceType { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = lineAccount.Name, Value = lineAccount.Id };
            lineDetail.BillableStatus = BillableStatusEnum.NotBillable;
            lineDetail.TaxAmount = new Decimal(10.00);
            lineDetail.TaxAmountSpecified = true;
            line.AnyIntuitObject = lineDetail;

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 

            lineList.Add(line);
            purchase.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //purchase.TxnTaxDetail = txnTaxDetail;
            return purchase;
        }



        internal static Purchase UpdatePurchase(ServiceContext context, Purchase entity)
        {
            //update the properties of entity
            Line[] line = entity.Line;
            line[0].Amount = new Decimal(1001.00);

            entity.TotalAmt = new Decimal(1001.00);
            return entity;
        }


        internal static Purchase SparseUpdatePurchase(ServiceContext context, string Id, PaymentTypeEnum paymentType, string syncToken)
        {
            Purchase entity = new Purchase();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PaymentType = paymentType;
            entity.PaymentTypeSpecified = true;

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            line.Description = "Sparse Update Desc";
            line.Amount = new Decimal(1002.00);
            line.AmountSpecified = true;

            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;

            AccountBasedExpenseLineDetail lineDetail = new AccountBasedExpenseLineDetail();
            Account lineAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            lineDetail.AccountRef = new ReferenceType { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = lineAccount.Name, Value = lineAccount.Id };
            lineDetail.BillableStatus = BillableStatusEnum.NotBillable;
            lineDetail.TaxAmount = new Decimal(10.00);
            lineDetail.TaxAmountSpecified = true;
            line.AnyIntuitObject = lineDetail;

            lineList.Add(line);
            entity.Line = lineList.ToArray();

            entity.TotalAmt = new Decimal(1002.00);
            entity.TotalAmtSpecified = true;
            return entity;
        }



        internal static PurchaseByVendor CreatePurchaseByVendor(ServiceContext context)
        {
            PurchaseByVendor purchaseByVendor = new PurchaseByVendor();
            //purchaseByVendor.VendorRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseByVendor.APAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseByVendor.TotalAmt = new Decimal(100.00);
            //purchaseByVendor.TotalAmtSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //purchaseByVendor.BillEmail = billEmail;
            //EmailAddress replyEmail = new EmailAddress();
            //replyEmail.Address = "Address";
            //replyEmail.Default = true;
            //replyEmail.DefaultSpecified = true;
            //replyEmail.Tag = "Tag";
            //purchaseByVendor.ReplyEmail = replyEmail;
            //purchaseByVendor.Memo = "Memo";
            //purchaseByVendor.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //purchaseByVendor.GlobalTaxCalculationSpecified = true;
            //purchaseByVendor.DocNumber = "DocNumber";
            //purchaseByVendor.TxnDate = DateTime.UtcNow.Date;
            //purchaseByVendor.TxnDateSpecified = true;
            //purchaseByVendor.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseByVendor.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseByVendor.ExchangeRate = new Decimal(100.00);
            //purchaseByVendor.ExchangeRateSpecified = true;
            //purchaseByVendor.PrivateNote = "PrivateNote";
            //purchaseByVendor.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //purchaseByVendor.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //purchaseByVendor.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //purchaseByVendor.TxnTaxDetail = txnTaxDetail;
            return purchaseByVendor;
        }



        internal static PurchaseByVendor UpdatePurchaseByVendor(ServiceContext context, PurchaseByVendor entity)
        {
            entity.Memo = "Memo_Update";
            return entity;
        }


        internal static PurchaseByVendor UpdatePurchaseByVendorSparse(ServiceContext context, string id, string syncToken)
        {
            PurchaseByVendor entity = new PurchaseByVendor();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Memo = "Memo_SparseUpdate";
            return entity;
        }


        internal static Budget CreateBudget(ServiceContext context)
        {
            Budget budget = new Budget();
            return budget;
        }

        internal static Budget UpdateBudget(ServiceContext context, Budget entity)
        {
            Budget budget = new Budget();
            return budget;
        }

        internal static Budget UpdateBudgetSparse(ServiceContext context, string Id, string SyncToken)
        {
            Budget budget = new Budget();
            return budget;
        }


        internal static void VerifyBudget(Budget expected, Budget actual)
        {
            //Assert.AreEqual(expected.PayerRef.name, actual.PayerRef.name);
            //Assert.AreEqual(expected.PayerRef.type, actual.PayerRef.type);

        }

        internal static void VerifyBudgetSparse(Budget expected, Budget actual)
        {
            //Assert.AreEqual(expected.PayerRef.name, actual.PayerRef.name);
            //Assert.AreEqual(expected.PayerRef.type, actual.PayerRef.type);

        }


        internal static Bill CreateBill(ServiceContext context)
        {

            Vendor vendors = Helper.FindOrAdd<Vendor>(context, new Vendor());
            Account account = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsPayable, AccountClassificationEnum.Liability);
            Account accountExpense = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());

            Bill bill = new Bill();
            //bill.PayerRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //bill.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            bill.DueDate = DateTime.UtcNow.Date;
            bill.DueDateSpecified = true;
            //PhysicalAddress remitToAddr = new PhysicalAddress();
            //remitToAddr.Line1 = "Line1";
            //remitToAddr.Line2 = "Line2";
            //remitToAddr.Line3 = "Line3";
            //remitToAddr.Line4 = "Line4";
            //remitToAddr.Line5 = "Line5";
            //remitToAddr.City = "City";
            //remitToAddr.Country = "Country";
            //remitToAddr.CountryCode = "CountryCode";
            //remitToAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //remitToAddr.PostalCode = "PostalCode";
            //remitToAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //remitToAddr.Lat = "Lat";
            //remitToAddr.Long = "Long";
            //remitToAddr.Tag = "Tag";
            //remitToAddr.Note = "Note";
            //bill.RemitToAddr = remitToAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //bill.ShipAddr = shipAddr;
            //bill.Balance = new Decimal(100.00);
            //bill.BalanceSpecified = true;
            //bill.BillEx = 

            bill.VendorRef = new ReferenceType()
            {
                //type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Vendor),
                name = vendors.DisplayName,
                Value = vendors.Id
            };
            bill.APAccountRef = new ReferenceType()
            {
                //type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account),
                name = account.Name,
                Value = account.Id
            };
            bill.TotalAmt = new Decimal(100.00);
            bill.TotalAmtSpecified = true;
            bill.Balance = new Decimal(100.00);
            bill.BalanceSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "test@testing.com";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //bill.BillEmail = billEmail;
            //EmailAddress replyEmail = new EmailAddress();
            //replyEmail.Address = "Address";
            //replyEmail.Default = true;
            //replyEmail.DefaultSpecified = true;
            //replyEmail.Tag = "Tag";
            //bill.ReplyEmail = replyEmail;
            //bill.Memo = "Memo";
            //bill.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //bill.GlobalTaxCalculationSpecified = true;
            //bill.DocNumber = "DocNumber";
            bill.TxnDate = DateTime.UtcNow.Date;
            bill.TxnDateSpecified = true;
            //bill.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //bill.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //bill.ExchangeRate = new Decimal(100.00);
            //bill.ExchangeRateSpecified = true;
            //bill.PrivateNote = "PrivateNote";
            //bill.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //bill.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //    linkedTxn.TxnId = "TxnId";
            //    linkedTxn.TxnType = "TxnType";
            //    linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;

            AccountBasedExpenseLineDetail detail = new AccountBasedExpenseLineDetail();
            detail.CustomerRef = new ReferenceType { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer), name = customer.DisplayName, Value = customer.Id };
            detail.AccountRef = new ReferenceType { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = accountExpense.Name, Value = accountExpense.Id };
            detail.BillableStatus = BillableStatusEnum.NotBillable;

            line.AnyIntuitObject = detail;

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //    customField.DefinitionId = "DefinitionId";
            //    customField.Name = "Name";
            //    customField.Type = CustomFieldTypeEnum.;
            //    customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 

            lineList.Add(line);
            bill.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //bill.TxnTaxDetail = txnTaxDetail;
            return bill;
        }



        internal static Bill UpdateBill(ServiceContext context, Bill entity)
        {
            //update the properties of entity
            entity.DocNumber = "docNo" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }

        internal static Bill UpdateBillSparse(ServiceContext context, string Id, string SyncToken)
        {
            //update the properties of entity
            Bill entity = new Bill();
            entity.Id = Id;
            entity.SyncToken = SyncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DocNumber = "docNo" + Helper.GetGuid().Substring(0, 5);

            Vendor vendors = Helper.FindOrAdd<Vendor>(context, new Vendor());
            entity.VendorRef = new ReferenceType()
            {
                name = vendors.DisplayName,
                Value = vendors.Id
            };
            return entity;
        }



        internal static VendorCredit CreateVendorCredit(ServiceContext context)
        {
            VendorCredit vendorCredit = new VendorCredit();
            //vendorCredit.VendorCreditEx =
            Vendor vendor = Helper.FindOrAdd<Vendor>(context, new Vendor());
            vendorCredit.VendorRef = new ReferenceType()
            {
                name = vendor.DisplayName,
                Value = vendor.Id
            };
            Account liabilityAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsPayable, AccountClassificationEnum.Asset);
            vendorCredit.APAccountRef = new ReferenceType()
            {
                name = liabilityAccount.Name,
                Value = liabilityAccount.Id
            };

            vendorCredit.TotalAmt = new Decimal(50.00);
            vendorCredit.TotalAmtSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //vendorCredit.BillEmail = billEmail;
            //EmailAddress replyEmail = new EmailAddress();
            //replyEmail.Address = "Address";
            //replyEmail.Default = true;
            //replyEmail.DefaultSpecified = true;
            //replyEmail.Tag = "Tag";
            //vendorCredit.ReplyEmail = replyEmail;
            //vendorCredit.Memo = "Memo";
            //vendorCredit.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //vendorCredit.GlobalTaxCalculationSpecified = true;
            //vendorCredit.DocNumber = "DocNumber";
            vendorCredit.TxnDate = DateTime.UtcNow.Date;
            vendorCredit.TxnDateSpecified = true;
            //vendorCredit.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //vendorCredit.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //vendorCredit.ExchangeRate = new Decimal(100.00);
            //vendorCredit.ExchangeRateSpecified = true;
            vendorCredit.PrivateNote = "PrivateNote";
            //vendorCredit.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //vendorCredit.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description";
            line.Amount = new Decimal(50.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;


            //Account expenseAccount = QBOHelper.CreateAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            Account expenseAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            line.AnyIntuitObject = new AccountBasedExpenseLineDetail()
            {
                AccountRef = new ReferenceType() { name = expenseAccount.Name, Value = expenseAccount.Id }
            };

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            lineList.Add(line);
            vendorCredit.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //vendorCredit.TxnTaxDetail = txnTaxDetail;
            return vendorCredit;
        }



        internal static VendorCredit UpdateVendorCredit(ServiceContext context, VendorCredit entity)
        {
            //update the properties of entity
            entity.TxnDate = DateTime.UtcNow.Date.AddDays(2);
            entity.TxnDateSpecified = true;
            entity.PrivateNote = "UpdatedPrivateNote";
            return entity;
        }

        internal static VendorCredit UpdateVendorCreditSparse(ServiceContext context, string id, string syncToken, ReferenceType vendorRef)
        {
            VendorCredit entity = new VendorCredit();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.TxnDate = DateTime.UtcNow.Date.AddDays(2);
            entity.TxnDateSpecified = true;
            entity.PrivateNote = "UpdatedPrivateNote";
            entity.VendorRef = vendorRef; //Required for sparse update
            return entity;
        }

        internal static StatementCharge CreateStatementCharge(ServiceContext context)
        {
            StatementCharge statementCharge = new StatementCharge();
            //statementCharge.Credit = true;
            //statementCharge.CreditSpecified = true;
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            statementCharge.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };
            //statementCharge.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            Account arBankAccount = Helper.FindOrAdd<Account>(context, new Account());
            statementCharge.ARAccountRef = new ReferenceType()
            {
                name = arBankAccount.Name,
                Value = arBankAccount.Id
            };
            Class class1 = Helper.FindOrAdd<Class>(context, new Class());
            statementCharge.ClassRef = new ReferenceType()
            {
                name = class1.Name,
                Value = class1.Id
            };
            statementCharge.DueDate = DateTime.UtcNow.Date.AddDays(45);
            statementCharge.DueDateSpecified = true;
            statementCharge.BilledDate = DateTime.UtcNow.Date;
            statementCharge.BilledDateSpecified = true;
            statementCharge.TotalAmt = new Decimal(100.00);
            statementCharge.TotalAmtSpecified = true;
            //statementCharge.StatementChargeEx = 
            //statementCharge.DocNumber = "DocNumber";
            statementCharge.TxnDate = DateTime.UtcNow.Date;
            statementCharge.TxnDateSpecified = true;
            //statementCharge.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //statementCharge.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //statementCharge.ExchangeRate = new Decimal(100.00);
            //statementCharge.ExchangeRateSpecified = true;
            //statementCharge.PrivateNote = "PrivateNote";
            //statementCharge.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //statementCharge.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //statementCharge.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //statementCharge.TxnTaxDetail = txnTaxDetail;
            return statementCharge;
        }



        internal static StatementCharge UpdateStatementCharge(ServiceContext context, StatementCharge entity)
        {
            //update the properties of entity

            entity.DueDate = DateTime.UtcNow.Date.AddDays(30);
            entity.DueDateSpecified = true;
            entity.BilledDate = DateTime.UtcNow.Date.AddDays(1);
            entity.BilledDateSpecified = true;
            entity.TotalAmt = new Decimal(100.00);
            entity.TotalAmtSpecified = true;
            return entity;
        }


        internal static StatementCharge UpdateStatementChargeSparse(ServiceContext context, string id, string syncToken)
        {
            StatementCharge entity = new StatementCharge();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DueDate = DateTime.UtcNow.Date.AddDays(30);
            entity.DueDateSpecified = true;
            entity.BilledDate = DateTime.UtcNow.Date.AddDays(1);
            entity.BilledDateSpecified = true;
            entity.TotalAmt = new Decimal(100.00);
            entity.TotalAmtSpecified = true;
            return entity;
        }


        internal static Class CreateClass(ServiceContext context)
        {
            Class class1 = new Class();
            class1.Name = "ClassName" + Helper.GetGuid().Substring(0, 20);
            class1.SubClass = true;
            class1.SubClassSpecified = true;
            //class.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            class1.FullyQualifiedName = class1.Name;
            class1.Active = true;
            class1.ActiveSpecified = true;
            //class.ClassEx = 
            return class1;
        }



        internal static Class UpdateClass(ServiceContext context, Class entity)
        {
            entity.Name = "UpdatedName" + Helper.GetGuid().Substring(0, 16);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }


        internal static Class SparseUpdateClass(ServiceContext context, string Id, string syncToken)
        {
            Class entity = new Class();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "UpdatedName" + Helper.GetGuid().Substring(0, 16);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }

        internal static Payment CreatePaymentCheck(ServiceContext context)
        {
            Payment payment = new Payment();
            payment.TxnDate = DateTime.UtcNow.Date;
            payment.TxnDateSpecified = true;
            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            payment.DepositToAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            PaymentMethod paymentMethod = Helper.FindOrAdd<PaymentMethod>(context, new PaymentMethod());
            payment.PaymentMethodRef = new ReferenceType()
            {
                name = paymentMethod.Name,
                Value = paymentMethod.Id
            };
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            payment.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };

            payment.PaymentType = PaymentTypeEnum.Check;
            CheckPayment checkPayment = new CheckPayment();
            checkPayment.AcctNum = "Acctnum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.BankName = "BankName" + Helper.GetGuid().Substring(0, 5);
            checkPayment.CheckNum = "CheckNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.NameOnAcct = "Name" + Helper.GetGuid().Substring(0, 5);
            checkPayment.Status = "Status" + Helper.GetGuid().Substring(0, 5);
            payment.AnyIntuitObject = checkPayment;

            payment.TotalAmt = new Decimal(100.00);
            payment.TotalAmtSpecified = true;
            payment.UnappliedAmt = new Decimal(100.00);
            payment.UnappliedAmtSpecified = true;
            return payment;
        }

        internal static Payment CreatePaymentCreditCard(ServiceContext context)
        {
            Payment payment = new Payment();
            payment.TxnDate = DateTime.UtcNow.Date;
            payment.TxnDateSpecified = true;
            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            payment.DepositToAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            PaymentMethod paymentMethod = Helper.FindOrAdd<PaymentMethod>(context, new PaymentMethod());
            payment.PaymentMethodRef = new ReferenceType()
            {
                name = paymentMethod.Name,
                Value = paymentMethod.Id
            };
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            payment.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };

            payment.PaymentType = PaymentTypeEnum.CreditCard;

            CreditCardPayment creditCardPayment = new CreditCardPayment();
            CreditChargeInfo creditChargeInfo = new CreditChargeInfo();
            creditChargeInfo.BillAddrStreet = "BillAddrStreet" + Helper.GetGuid().Substring(0, 5);
            creditChargeInfo.CcExpiryMonth = 10;
            creditChargeInfo.CcExpiryMonthSpecified = true;
            creditChargeInfo.CcExpiryYear = DateTime.Today.Year;
            creditChargeInfo.CcExpiryYearSpecified = true;
            creditChargeInfo.CCTxnMode = CCTxnModeEnum.CardNotPresent;
            creditChargeInfo.CCTxnModeSpecified = true;
            creditChargeInfo.CCTxnType = CCTxnTypeEnum.Charge;
            creditChargeInfo.CCTxnTypeSpecified = true;
            creditChargeInfo.CommercialCardCode = "Cardcode" + Helper.GetGuid().Substring(0, 5);
            creditChargeInfo.NameOnAcct = "Name" + Helper.GetGuid().Substring(0, 5);
            creditChargeInfo.Number = Helper.GetGuid().Substring(0, 5);
            creditChargeInfo.PostalCode = Helper.GetGuid().Substring(0, 5);
            creditCardPayment.CreditChargeInfo = creditChargeInfo;

            payment.AnyIntuitObject = creditCardPayment;
            payment.TotalAmt = new Decimal(100.00);
            payment.TotalAmtSpecified = true;
            payment.UnappliedAmt = new Decimal(100.00);
            payment.UnappliedAmtSpecified = true;
            return payment;
        }

        internal static Payment CreatePayment(ServiceContext context)
        {
            Payment payment = new Payment();
            payment.TxnDate = DateTime.UtcNow.Date;
            payment.TxnDateSpecified = true;

            Account ARAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsReceivable, AccountClassificationEnum.Asset);
            payment.ARAccountRef = new ReferenceType()
            {
                name = ARAccount.Name,
                Value = ARAccount.Id
            };

            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            payment.DepositToAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            PaymentMethod paymentMethod = Helper.FindOrAdd<PaymentMethod>(context, new PaymentMethod());
            payment.PaymentMethodRef = new ReferenceType()
            {
                name = paymentMethod.Name,
                Value = paymentMethod.Id
            };
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            payment.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };

            payment.PaymentType = PaymentTypeEnum.Check;
            CheckPayment checkPayment = new CheckPayment();
            checkPayment.AcctNum = "Acctnum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.BankName = "BankName" + Helper.GetGuid().Substring(0, 5);
            checkPayment.CheckNum = "CheckNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.NameOnAcct = "Name" + Helper.GetGuid().Substring(0, 5);
            checkPayment.Status = "Status" + Helper.GetGuid().Substring(0, 5);
            checkPayment.CheckPaymentEx = new IntuitAnyType();
            payment.AnyIntuitObject = checkPayment;

            payment.TotalAmt = new Decimal(100.00);
            payment.TotalAmtSpecified = true;
            payment.UnappliedAmt = new Decimal(100.00);
            payment.UnappliedAmtSpecified = true;
            return payment;
        }



        internal static Payment UpdatePayment(ServiceContext context, Payment entity)
        {
            //update the properties of entity
            entity.TxnDate = DateTime.UtcNow.Date.AddDays(10);
            entity.TxnDateSpecified = true;
            return entity;
        }

        internal static Payment SparseUpdatePayment(ServiceContext context, string Id, string syncToken)
        {
            Payment entity = new Payment();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrivateNote = "updated Private Note";
            return entity;
        }


        internal static PaymentMethod CreatePaymentMethod(ServiceContext context)
        {
            PaymentMethod paymentMethod = new PaymentMethod();
            paymentMethod.Name = "CreditCard" + Helper.GetGuid().Substring(0, 13);
            paymentMethod.Active = true;
            paymentMethod.ActiveSpecified = true;
            paymentMethod.Type = "CREDIT_CARD"; //Need to be replaced by PaymentTyprEnum
            //paymentMethod.PaymentMethodEx = 
            return paymentMethod;
        }



        internal static PaymentMethod UpdatePaymentMethod(ServiceContext context, PaymentMethod entity)
        {
            entity.Name = "Cash" + Helper.GetGuid().Substring(0, 13);
            entity.Type = "NON_CREDIT_CARD";
            return entity;
        }



        internal static PaymentMethod SparseUpdatePaymentMethod(ServiceContext context, string Id, string syncToken)
        {
            PaymentMethod entity = new PaymentMethod();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Sparse Cash" + Helper.GetGuid().Substring(0, 13);
            entity.Type = "NON_CREDIT_CARD";
            return entity;
        }



        internal static Department CreateDepartment(ServiceContext context)
        {
            Department department = new Department();
            department.Name = "DeptName" + Helper.GetGuid().Substring(0, 13);
            department.SubDepartment = true;
            department.SubDepartmentSpecified = true;
            //department.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            department.FullyQualifiedName = department.Name;
            department.Active = true;
            department.ActiveSpecified = true;
            //department.DepartmentEx = 
            return department;
        }



        internal static Department UpdateDepartment(ServiceContext context, Department entity)
        {
            entity.Name = "DeptName" + Helper.GetGuid().Substring(0, 13);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }


        internal static Department UpdateDepartmentSparse(ServiceContext context, string Id, string SyncToken)
        {
            Department entity = new Department();
            entity.Id = Id;
            entity.SyncToken = SyncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "DeptName" + Helper.GetGuid().Substring(0, 13);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }


        internal static Item CreateItem(ServiceContext context)
        {

            Item item = new Item();


            item.Name = "Name" + Helper.GetGuid().Substring(0, 5); ;
            item.Description = "Description";
            item.Type = ItemTypeEnum.NonInventory;
            item.TypeSpecified = true;

            item.Active = true;
            item.ActiveSpecified = true;


            //item.SubItem = true;
            //item.SubItemSpecified = true;
            //item.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.Level = int32;
            //Int32 int32 = new Int32();

            //item.LevelSpecified = true;
            //item.FullyQualifiedName = "FullyQualifiedName";
            item.Taxable = false;
            item.TaxableSpecified = true;
            //item.SalesTaxIncluded = true;
            //item.SalesTaxIncludedSpecified = true;
            //item.PercentBased = true;
            //item.PercentBasedSpecified = true;
            item.UnitPrice = new Decimal(100.00);
            item.UnitPriceSpecified = true;
            //item.RatePercent = new Decimal(100.00);
            //item.RatePercentSpecified = true;
            //item.TypeSpecified = true;
            //item.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            Account incomeAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Income, AccountClassificationEnum.Revenue);
            item.IncomeAccountRef = new ReferenceType()
            {
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };
            //item.PurchaseDesc = "PurchaseDesc";
            //item.PurchaseTaxIncluded = true;
            //item.PurchaseTaxIncludedSpecified = true;
            item.PurchaseCost = new Decimal(100.00);
            item.PurchaseCostSpecified = true;
            Account expenseAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            item.ExpenseAccountRef = new ReferenceType()
            {
                name = expenseAccount.Name,
                Value = expenseAccount.Id
            };
            //item.COGSAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.AssetAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.PrefVendorRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.AvgCost = new Decimal(100.00);
            //item.AvgCostSpecified = true;
            item.TrackQtyOnHand = false;
            item.TrackQtyOnHandSpecified = true;
            //item.QtyOnHand = new Decimal(100.00);
            //item.QtyOnHandSpecified = true;
            //item.QtyOnPurchaseOrder = new Decimal(100.00);
            //item.QtyOnPurchaseOrderSpecified = true;
            //item.QtyOnSalesOrder = new Decimal(100.00);
            //item.QtyOnSalesOrderSpecified = true;
            //item.ReorderPoint = new Decimal(100.00);
            //item.ReorderPointSpecified = true;
            //item.ManPartNum = "ManPartNum";
            //item.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.SalesTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.PurchaseTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.InvStartDate = DateTime.UtcNow.Date;
            //item.InvStartDateSpecified = true;
            //item.BuildPoint = new Decimal(100.00);
            //item.BuildPointSpecified = true;
            //item.PrintGroupedItems = true;
            //item.PrintGroupedItemsSpecified = true;
            //item.SpecialItem = true;
            //item.SpecialItemSpecified = true;
            //item.SpecialItemType = SpecialItemTypeEnum.;
            //item.SpecialItemTypeSpecified = true;

            //List<ItemComponentLine> itemGroupDetailList = new List<ItemComponentLine>();
            //ItemComponentLine itemGroupDetail = new ItemComponentLine();
            //itemGroupDetail.ItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemGroupDetail.Qty = new Decimal(100.00);
            //itemGroupDetail.QtySpecified = true;
            //UOMRef uOMRef = new UOMRef();
            //itemGroupDetail.uOMRef.Unit = "Unit";
            //itemGroupDetail.uOMRef.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemComponentLine.UOMRef = uOMRef;
            //itemGroupDetailList.Add(itemGroupDetail);
            //item.ItemGroupDetail = itemGroupDetailList.ToArray();

            //List<ItemComponentLine> itemAssemblyDetailList = new List<ItemComponentLine>();
            //ItemComponentLine itemAssemblyDetail = new ItemComponentLine();
            //itemAssemblyDetail.ItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemAssemblyDetail.Qty = new Decimal(100.00);
            //itemAssemblyDetail.QtySpecified = true;
            //UOMRef uOMRef = new UOMRef();
            //itemAssemblyDetail.uOMRef.Unit = "Unit";
            //itemAssemblyDetail.uOMRef.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemComponentLine.UOMRef = uOMRef;
            //itemAssemblyDetailList.Add(itemAssemblyDetail);
            //item.ItemAssemblyDetail = itemAssemblyDetailList.ToArray();
            //item.ItemEx = 

            return item;

        }

        internal static Item CreateItemFrance(ServiceContext context)
        {

            Item item = new Item();
            item.Name = "Name" + Helper.GetGuid().Substring(0, 5); ;
            item.Description = "Description_Check";
            item.Active = true;
            item.ActiveSpecified = true;

            item.Type = ItemTypeEnum.Service;
            item.TypeSpecified = true;
            item.ItemCategoryType = "Service";

            //item.SubItem = true;
            //item.SubItemSpecified = true;
            //item.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.Level = int32;
            //Int32 int32 = new Int32();

            //item.LevelSpecified = true;
            //item.FullyQualifiedName = "FullyQualifiedName";
            item.Taxable = false;
            item.TaxableSpecified = true;
            //item.SalesTaxIncluded = true;
            //item.SalesTaxIncludedSpecified = true;
            //item.PercentBased = true;
            //item.PercentBasedSpecified = true;
            item.UnitPrice = new Decimal(100.00);
            item.UnitPriceSpecified = true;
            //item.RatePercent = new Decimal(100.00);
            //item.RatePercentSpecified = true;
            //item.Type = ItemTypeEnum.;
            //item.TypeSpecified = true;
            //item.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            Account incomeAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Income, AccountClassificationEnum.Revenue);
            item.IncomeAccountRef = new ReferenceType()
            {
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };
            //item.PurchaseDesc = "PurchaseDesc";
            //item.PurchaseTaxIncluded = true;
            //item.PurchaseTaxIncludedSpecified = true;
            item.PurchaseCost = new Decimal(100.00);
            item.PurchaseCostSpecified = true;
            Account expenseAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            item.ExpenseAccountRef = new ReferenceType()
            {
                name = expenseAccount.Name,
                Value = expenseAccount.Id
            };
            //item.COGSAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.AssetAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.PrefVendorRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.AvgCost = new Decimal(100.00);
            //item.AvgCostSpecified = true;
            item.TrackQtyOnHand = false;
            item.TrackQtyOnHandSpecified = true;
            //item.QtyOnHand = new Decimal(100.00);
            //item.QtyOnHandSpecified = true;
            //item.QtyOnPurchaseOrder = new Decimal(100.00);
            //item.QtyOnPurchaseOrderSpecified = true;
            //item.QtyOnSalesOrder = new Decimal(100.00);
            //item.QtyOnSalesOrderSpecified = true;
            //item.ReorderPoint = new Decimal(100.00);
            //item.ReorderPointSpecified = true;
            //item.ManPartNum = "ManPartNum";
            //item.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.SalesTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.PurchaseTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //item.InvStartDate = DateTime.UtcNow.Date;
            //item.InvStartDateSpecified = true;
            //item.BuildPoint = new Decimal(100.00);
            //item.BuildPointSpecified = true;
            //item.PrintGroupedItems = true;
            //item.PrintGroupedItemsSpecified = true;
            //item.SpecialItem = true;
            //item.SpecialItemSpecified = true;
            //item.SpecialItemType = SpecialItemTypeEnum.;
            //item.SpecialItemTypeSpecified = true;

            //List<ItemComponentLine> itemGroupDetailList = new List<ItemComponentLine>();
            //ItemComponentLine itemGroupDetail = new ItemComponentLine();
            //itemGroupDetail.ItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemGroupDetail.Qty = new Decimal(100.00);
            //itemGroupDetail.QtySpecified = true;
            //UOMRef uOMRef = new UOMRef();
            //itemGroupDetail.uOMRef.Unit = "Unit";
            //itemGroupDetail.uOMRef.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemComponentLine.UOMRef = uOMRef;
            //itemGroupDetailList.Add(itemGroupDetail);
            //item.ItemGroupDetail = itemGroupDetailList.ToArray();

            //List<ItemComponentLine> itemAssemblyDetailList = new List<ItemComponentLine>();
            //ItemComponentLine itemAssemblyDetail = new ItemComponentLine();
            //itemAssemblyDetail.ItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemAssemblyDetail.Qty = new Decimal(100.00);
            //itemAssemblyDetail.QtySpecified = true;
            //UOMRef uOMRef = new UOMRef();
            //itemAssemblyDetail.uOMRef.Unit = "Unit";
            //itemAssemblyDetail.uOMRef.UOMSetRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //itemComponentLine.UOMRef = uOMRef;
            //itemAssemblyDetailList.Add(itemAssemblyDetail);
            //item.ItemAssemblyDetail = itemAssemblyDetailList.ToArray();
            //item.ItemEx = 
            return item;
        }


        internal static Item UpdateItem(ServiceContext context, Item entity)
        {
            //update the properties of entity
            entity.Name = "updatedName" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "updatedDescription";
            return entity;
        }


        internal static Item UpdateItemFrance(ServiceContext context, Item entity)
        {
            //update the properties of entity
            entity.Name = "updatedName" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "updatedDescription";

            Account incomeAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Income, AccountClassificationEnum.Revenue);
            entity.IncomeAccountRef = new ReferenceType()
            {
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };
            return entity;
        }


        internal static Item SparseUpdateItem(ServiceContext context, string Id, string syncToken)
        {
            Item entity = new Item();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "sparseupdatedName" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "sparseupdatedDescription";
            return entity;
        }


        internal static Term CreateTerm(ServiceContext context)
        {
            Term term = new Term();
            term.Name = "Name" + Helper.GetGuid().Substring(0, 15);
            term.Active = true;
            term.ActiveSpecified = true;
            term.Type = "STANDARD";
            term.DiscountPercent = new Decimal(50.00);
            term.DiscountPercentSpecified = true;
            term.AnyIntuitObjects = new Object[] { 10 };
            term.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.DueDays };
            return term;
        }


        internal static Term UpdateTerm(ServiceContext context, Term entity)
        {
            entity.Name = "UpdateName" + Helper.GetGuid().Substring(0, 15);
            entity.Active = true;
            entity.ActiveSpecified = true;
            return entity;
        }


        internal static Term SparseUpdateTerm(ServiceContext context, string Id, string syncToken)
        {
            Term entity = new Term();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "SparseUpdateName" + Helper.GetGuid().Substring(0, 15);
            entity.Active = true;
            entity.ActiveSpecified = true;
            return entity;
        }


        internal static BillPayment CreateBillPaymentCheck(ServiceContext context)
        {
            BillPayment billPayment = new BillPayment();
            VendorCredit vendorCredit = Helper.Add(context, QBOHelper.CreateVendorCredit(context));
            billPayment.PayType = BillPaymentTypeEnum.Check;
            billPayment.PayTypeSpecified = true;
            //billPayment.AnyIntuitObject = 
            billPayment.TotalAmt = vendorCredit.TotalAmt;
            billPayment.TotalAmtSpecified = true;
            //billPayment.BillPaymentEx = 
            //billPayment.DocNumber = "DocNumber";
            billPayment.TxnDate = DateTime.UtcNow.Date;
            billPayment.TxnDateSpecified = true;
            //billPayment.ExchangeRate = new Decimal(100.00);
            //billPayment.ExchangeRateSpecified = true;
            billPayment.PrivateNote = "PrivateNote";
            //billPayment.TxnStatus = "TxnStatus";

            //Account liabilityAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsPayable, AccountClassificationEnum.Liability);
            //billPayment.APAccountRef = new ReferenceType()
            //{
            //    name = liabilityAccount.Name,
            //    Value = liabilityAccount.Id
            //};

            Vendor vendor = Helper.FindOrAdd<Vendor>(context, new Vendor());
            billPayment.VendorRef = new ReferenceType()
            {
                name = vendor.DisplayName,
                type = "Vendor",
                Value = vendor.Id
            };

            Account bankAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            BillPaymentCheck billPaymentCheck = new BillPaymentCheck();
            billPaymentCheck.BankAccountRef = new ReferenceType()
            {
                name = bankAccount.Name,
                Value = bankAccount.Id
            };

            CheckPayment checkPayment = new CheckPayment();
            checkPayment.AcctNum = "AcctNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.BankName = "BankName" + Helper.GetGuid().Substring(0, 5);
            checkPayment.CheckNum = "CheckNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.NameOnAcct = "Name" + Helper.GetGuid().Substring(0, 5);
            checkPayment.Status = "Status" + Helper.GetGuid().Substring(0, 5);
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
            billPayment.AnyIntuitObject = billPaymentCheck;

            List<Line> lineList = new List<Line>();

            Line line1 = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";

            Bill bill = Helper.Add<Bill>(context, QBOHelper.CreateBill(context));
            line1.Amount = bill.TotalAmt;
            line1.AmountSpecified = true;
            List<LinkedTxn> LinkedTxnList1 = new List<LinkedTxn>();
            LinkedTxn linkedTxn1 = new LinkedTxn();
            linkedTxn1.TxnId = bill.Id;
            linkedTxn1.TxnType = TxnTypeEnum.Bill.ToString();
            LinkedTxnList1.Add(linkedTxn1);
            line1.LinkedTxn = LinkedTxnList1.ToArray();

            lineList.Add(line1);

            Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";

            line.Amount = vendorCredit.TotalAmt;
            line.AmountSpecified = true;

            List<LinkedTxn> LinkedTxnList = new List<LinkedTxn>();
            LinkedTxn linkedTxn = new LinkedTxn();
            linkedTxn.TxnId = vendorCredit.Id;
            linkedTxn.TxnType = TxnTypeEnum.VendorCredit.ToString();
            LinkedTxnList.Add(linkedTxn);
            line.LinkedTxn = LinkedTxnList.ToArray();

            lineList.Add(line);

            billPayment.Line = lineList.ToArray();

            return billPayment;
        }

        internal static BillPayment CreateBillPaymentCreditCard(ServiceContext context)
        {
            BillPayment billPayment = new BillPayment();
            VendorCredit vendorCredit = Helper.Add(context, QBOHelper.CreateVendorCredit(context));
            billPayment.PayType = BillPaymentTypeEnum.Check;
            billPayment.PayTypeSpecified = true;
            //billPayment.AnyIntuitObject = 
            billPayment.TotalAmt = vendorCredit.TotalAmt;
            billPayment.TotalAmtSpecified = true;
            //billPayment.BillPaymentEx = 
            //billPayment.DocNumber = "DocNumber";
            billPayment.TxnDate = DateTime.UtcNow.Date;
            billPayment.TxnDateSpecified = true;
            //billPayment.ExchangeRate = new Decimal(100.00);
            //billPayment.ExchangeRateSpecified = true;
            billPayment.PrivateNote = "PrivateNote";
            //billPayment.TxnStatus = "TxnStatus";

            //Account liabilityAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsPayable, AccountClassificationEnum.Liability);
            //billPayment.APAccountRef = new ReferenceType()
            //{
            //    name = liabilityAccount.Name,
            //    Value = liabilityAccount.Id
            //};

            Vendor vendor = Helper.FindOrAdd<Vendor>(context, new Vendor());
            billPayment.VendorRef = new ReferenceType()
            {
                name = vendor.DisplayName,
                type = "Vendor",
                Value = vendor.Id
            };

            Account bankAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.CreditCard, AccountClassificationEnum.Expense);
            BillPaymentCreditCard billPaymentCreditCard = new BillPaymentCreditCard();
            billPaymentCreditCard.CCAccountRef = new ReferenceType()
            {
                name = bankAccount.Name,
                Value = bankAccount.Id
            };

            CreditCardPayment creditCardPayment = new CreditCardPayment();
            creditCardPayment.CreditChargeInfo = new CreditChargeInfo()
            {
                Amount = new Decimal(10.00),
                AmountSpecified = true,
                Number = "124124124",
                NameOnAcct = bankAccount.Name,
                CcExpiryMonth = 10,
                CcExpiryMonthSpecified = true,
                CcExpiryYear = 2015,
                CcExpiryYearSpecified = true,
                BillAddrStreet = "BillAddrStreetba7cca47",
                PostalCode = "560045",
                CommercialCardCode = "CardCodeba7cca47",
                CCTxnMode = CCTxnModeEnum.CardPresent,
                CCTxnType = CCTxnTypeEnum.Charge
            };

            billPaymentCreditCard.CCDetail = creditCardPayment;
            billPayment.AnyIntuitObject = billPaymentCreditCard;

            List<Line> lineList = new List<Line>();

            Line line1 = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";

            Bill bill = Helper.Add<Bill>(context, QBOHelper.CreateBill(context));
            line1.Amount = bill.TotalAmt;
            line1.AmountSpecified = true;
            List<LinkedTxn> LinkedTxnList1 = new List<LinkedTxn>();
            LinkedTxn linkedTxn1 = new LinkedTxn();
            linkedTxn1.TxnId = bill.Id;
            linkedTxn1.TxnType = TxnTypeEnum.Bill.ToString();
            LinkedTxnList1.Add(linkedTxn1);
            line1.LinkedTxn = LinkedTxnList1.ToArray();

            lineList.Add(line1);

            Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";

            line.Amount = vendorCredit.TotalAmt;
            line.AmountSpecified = true;

            List<LinkedTxn> LinkedTxnList = new List<LinkedTxn>();
            LinkedTxn linkedTxn = new LinkedTxn();
            linkedTxn.TxnId = vendorCredit.Id;
            linkedTxn.TxnType = TxnTypeEnum.VendorCredit.ToString();
            LinkedTxnList.Add(linkedTxn);
            line.LinkedTxn = LinkedTxnList.ToArray();

            lineList.Add(line);

            billPayment.Line = lineList.ToArray();

            return billPayment;
        }

        internal static BillPayment UpdateBillPayment(ServiceContext context, BillPayment entity)
        {
            //update the properties of entity
            entity.PrivateNote = "Updated PrivateNote";

            Account bankAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            BillPaymentCheck billPaymentCheck = new BillPaymentCheck();
            billPaymentCheck.BankAccountRef = new ReferenceType()
            {
                name = bankAccount.Name,
                Value = bankAccount.Id
            };

            CheckPayment checkPayment = new CheckPayment();
            checkPayment.AcctNum = "AcctNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.BankName = "BankName" + Helper.GetGuid().Substring(0, 5);
            checkPayment.CheckNum = "CheckNum" + Helper.GetGuid().Substring(0, 5);
            checkPayment.NameOnAcct = "Name" + Helper.GetGuid().Substring(0, 5);
            checkPayment.Status = "Status" + Helper.GetGuid().Substring(0, 5);
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
            entity.AnyIntuitObject = billPaymentCheck;
            return entity;
        }

        internal static BillPayment UpdateBillPaymentSparse(ServiceContext context, string id, string syncToken)
        {
            BillPayment entity = new BillPayment();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrivateNote = "Updated PrivateNote";
            return entity;
        }

        internal static Deposit CreateDeposit(ServiceContext context)
        {
            Deposit deposit = new Deposit();
            Account bankAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            deposit.DepositToAccountRef = new ReferenceType()
            {
                name = bankAccount.Name,
                Value = bankAccount.Id
            };

            deposit.TotalAmt = new Decimal(100.00);
            deposit.TotalAmtSpecified = true;

            deposit.TxnDate = DateTime.UtcNow.Date;
            deposit.TxnDateSpecified = true;
            //deposit.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //deposit.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            deposit.ExchangeRate = new Decimal(1.00);
            deposit.ExchangeRateSpecified = true;
            deposit.PrivateNote = "PrivateNote" + Helper.GetGuid().Substring(0, 8); ;
            //deposit.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //deposit.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            line.DetailType = LineDetailTypeEnum.DepositLineDetail;
            line.DetailTypeSpecified = true;

            DepositLineDetail lineDepositLineDetail = new DepositLineDetail();

            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            lineDepositLineDetail.Entity = new ReferenceType()
            {
                //add customer/job detail
                name = customer.DisplayName,
                Value = customer.Id
            };

            Account incomeAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Income, AccountClassificationEnum.Revenue);
            lineDepositLineDetail.AccountRef = new ReferenceType()
            {
                //add account detail
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };

            PaymentMethod paymentMethod = Helper.FindOrAddPaymentMethod(context, PaymentMethodEnum.Cash.ToString());
            lineDepositLineDetail.PaymentMethodRef = new ReferenceType()
            {
                //add paymentMethod 
                name = paymentMethod.Name,
                Value = paymentMethod.Id

            };

            line.AnyIntuitObject = lineDepositLineDetail;
            lineList.Add(line);
            deposit.Line = lineList.ToArray();

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 


            return deposit;
        }



        internal static Deposit UpdateDeposit(ServiceContext context, Deposit entity)
        {
            //update the properties of entity
            entity.PrivateNote = "upd_Note" + Helper.GetGuid().Substring(0, 8);

            return entity;
        }


        internal static Deposit UpdateDepositSparse(ServiceContext context, string id, string syncToken)
        {
            Deposit entity = new Deposit();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrivateNote = "spa_Note" + Helper.GetGuid().Substring(0, 8);
            Account bankAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            entity.DepositToAccountRef = new ReferenceType()
            {
                name = bankAccount.Name,
                Value = bankAccount.Id
            };


            return entity;
        }

        internal static Transfer CreateTransfer(ServiceContext context)
        {
            Transfer transfer = new Transfer();
            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            transfer.FromAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            Account creditAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.CreditCard, AccountClassificationEnum.Liability);
            transfer.ToAccountRef = new ReferenceType()
            {
                name = creditAccount.Name,
                Value = creditAccount.Id
            };
            transfer.Amount = new Decimal(100.00);
            transfer.AmountSpecified = true;
            //transfer.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //transfer.TransferEx = 
            //transfer.DocNumber = "DocNumber";
            //transfer.TxnDate = DateTime.UtcNow.Date;
            //transfer.TxnDateSpecified = true;
            //transfer.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //transfer.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //transfer.ExchangeRate = new Decimal(100.00);
            //transfer.ExchangeRateSpecified = true;
            //transfer.PrivateNote = "PrivateNote";
            //transfer.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //transfer.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //transfer.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //transfer.TxnTaxDetail = txnTaxDetail;
            return transfer;
        }



        internal static Transfer UpdateTransfer(ServiceContext context, Transfer entity)
        {
            //update the properties of entity
            entity.Amount = new Decimal(200.00);
            entity.AmountSpecified = true;

            return entity;
        }

        internal static Transfer UpdateTransferSparse(ServiceContext context, string id, string syncToken)
        {
            Transfer entity = new Transfer();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Amount = new Decimal(200.00);
            entity.AmountSpecified = true;

            return entity;
        }



        internal static PurchaseOrder CreatePurchaseOrder(ServiceContext context)
        {
            Vendor vendors = Helper.FindOrAdd<Vendor>(context, new Vendor());
            Account accountsForDetail = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);

            PurchaseOrder purchaseOrder = new PurchaseOrder();
            //purchaseOrder.TaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.ReimbursableInfoRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.DueDate = DateTime.UtcNow.Date;
            //purchaseOrder.DueDateSpecified = true;
            //purchaseOrder.ExpectedDate = DateTime.UtcNow.Date;
            //purchaseOrder.ExpectedDateSpecified = true;
            //PhysicalAddress vendorAddr = new PhysicalAddress();
            //vendorAddr.Line1 = "Line1";
            //vendorAddr.Line2 = "Line2";
            //vendorAddr.Line3 = "Line3";
            //vendorAddr.Line4 = "Line4";
            //vendorAddr.Line5 = "Line5";
            //vendorAddr.City = "City";
            //vendorAddr.Country = "Country";
            //vendorAddr.CountryCode = "CountryCode";
            //vendorAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //vendorAddr.PostalCode = "PostalCode";
            //vendorAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //vendorAddr.Lat = "Lat";
            //vendorAddr.Long = "Long";
            //vendorAddr.Tag = "Tag";
            //vendorAddr.Note = "Note";
            //purchaseOrder.VendorAddr = vendorAddr;
            //purchaseOrder.AnyIntuitObject = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.ItemElementName = itemChoiceType1;
            //ItemChoiceType1 itemChoiceType1 = new ItemChoiceType1();

            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //purchaseOrder.ShipAddr = shipAddr;
            //purchaseOrder.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.FOB = "FOB";
            //EmailAddress pOEmail = new EmailAddress();
            //pOEmail.Address = "Address";
            //pOEmail.Default = true;
            //pOEmail.DefaultSpecified = true;
            //pOEmail.Tag = "Tag";
            //purchaseOrder.POEmail = pOEmail;
            //purchaseOrder.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.PrintStatus = PrintStatusEnum.;
            //purchaseOrder.PrintStatusSpecified = true;
            //purchaseOrder.EmailStatus = EmailStatusEnum.;
            //purchaseOrder.EmailStatusSpecified = true;
            //purchaseOrder.ManuallyClosed = true;
            //purchaseOrder.ManuallyClosedSpecified = true;
            //purchaseOrder.POStatus = PurchaseOrderStatusEnum.;
            //purchaseOrder.POStatusSpecified = true;
            //purchaseOrder.PurchaseOrderEx = 

            purchaseOrder.VendorRef = new ReferenceType()
            {
                //type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Vendor),
                name = vendors.DisplayName,
                Value = vendors.Id
            };
            //purchaseOrder.APAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            purchaseOrder.TotalAmt = new Decimal(10.00);
            purchaseOrder.TotalAmtSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //purchaseOrder.BillEmail = billEmail;
            //EmailAddress replyEmail = new EmailAddress();
            //replyEmail.Address = "Address";
            //replyEmail.Default = true;
            //replyEmail.DefaultSpecified = true;
            //replyEmail.Tag = "Tag";
            //purchaseOrder.ReplyEmail = replyEmail;
            //purchaseOrder.Memo = "Memo";
            //purchaseOrder.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //purchaseOrder.GlobalTaxCalculationSpecified = true;
            //purchaseOrder.DocNumber = "DocNumber";
            purchaseOrder.TxnDate = DateTime.UtcNow.Date;
            purchaseOrder.TxnDateSpecified = true;
            //purchaseOrder.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //purchaseOrder.ExchangeRate = new Decimal(100.00);
            //purchaseOrder.ExchangeRateSpecified = true;
            //purchaseOrder.PrivateNote = "PrivateNote";
            //purchaseOrder.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //purchaseOrder.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();
            line.LineNum = "1";
            //line.Description = "Description";
            line.Amount = new Decimal(10.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;
            AccountBasedExpenseLineDetail accountBasedExpenseDetails = new AccountBasedExpenseLineDetail();
            accountBasedExpenseDetails.AccountRef = new ReferenceType { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = accountsForDetail.Name, Value = accountsForDetail.Id };

            line.AnyIntuitObject = accountBasedExpenseDetails;

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 

            lineList.Add(line);
            purchaseOrder.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //purchaseOrder.TxnTaxDetail = txnTaxDetail;
            return purchaseOrder;
        }



        internal static PurchaseOrder UpdatePurchaseOrder(ServiceContext context, PurchaseOrder entity)
        {
            //update the properties of entity
            entity.DocNumber = Helper.GetGuid().Substring(0, 13);
            return entity;
        }

        internal static PurchaseOrder UpdatePurchaseOrderSparse(ServiceContext context, string Id, string SyncToken)
        {
            PurchaseOrder entity = new PurchaseOrder();
            entity.Id = Id;
            entity.SyncToken = SyncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DocNumber = Helper.GetGuid().Substring(0, 13);
            return entity;
        }


        internal static SalesOrder CreateSalesOrder(ServiceContext context)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.ManuallyClosed = true;
            salesOrder.ManuallyClosedSpecified = true;
            //salesOrder.SalesOrderEx = 

            salesOrder.AutoDocNumber = true;
            salesOrder.AutoDocNumberSpecified = true;
            //salesOrder.CustomerRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //salesOrder.CustomerMemo = customerMemo;
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "Line1";
            billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            billAddr.City = "City";
            billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            salesOrder.BillAddr = billAddr;
            PhysicalAddress shipAddr = new PhysicalAddress();
            shipAddr.Line1 = "Line1";
            shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            shipAddr.City = "City";
            shipAddr.Country = "Country";
            shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            salesOrder.ShipAddr = shipAddr;
            //salesOrder.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesOrder.DueDate = DateTime.UtcNow.Date;
            salesOrder.DueDateSpecified = true;
            //salesOrder.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.PONumber = "PONumber";
            //salesOrder.FOB = "FOB";
            //salesOrder.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesOrder.ShipDate = DateTime.UtcNow.Date;
            salesOrder.ShipDateSpecified = true;
            salesOrder.TrackingNum = "TrackingNum";
            salesOrder.GlobalTaxCalculation = GlobalTaxCalculationEnum.TaxInclusive;
            salesOrder.GlobalTaxCalculationSpecified = true;
            salesOrder.TotalAmt = new Decimal(100.00);
            salesOrder.TotalAmtSpecified = true;
            salesOrder.HomeTotalAmt = new Decimal(100.00);
            salesOrder.HomeTotalAmtSpecified = true;
            salesOrder.ApplyTaxAfterDiscount = true;
            salesOrder.ApplyTaxAfterDiscountSpecified = true;
            //salesOrder.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.PrintStatus = PrintStatusEnum.;
            //salesOrder.PrintStatusSpecified = true;
            //salesOrder.EmailStatus = EmailStatusEnum.;
            //salesOrder.EmailStatusSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //salesOrder.BillEmail = billEmail;
            //salesOrder.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesOrder.Balance = new Decimal(100.00);
            salesOrder.BalanceSpecified = true;
            salesOrder.FinanceCharge = true;
            salesOrder.FinanceChargeSpecified = true;
            //salesOrder.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.PaymentRefNum = "PaymentRefNum";
            salesOrder.PaymentType = PaymentTypeEnum.CreditCard;
            salesOrder.PaymentTypeSpecified = true;
            //salesOrder.AnyIntuitObject = 
            //salesOrder.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesOrder.DocNumber = "DocNumber";
            salesOrder.TxnDate = DateTime.UtcNow.Date;
            salesOrder.TxnDateSpecified = true;
            //salesOrder.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesOrder.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            salesOrder.ExchangeRate = new Decimal(100.00);
            salesOrder.ExchangeRateSpecified = true;
            salesOrder.PrivateNote = "PrivateNote";
            salesOrder.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //salesOrder.LinkedTxn = linkedTxnList.ToArray();

            //List<Line> lineList = new List<Line>();
            //Line line = new Line();
            //line.LineNum = "LineNum";
            //line.Description = "Description";
            //line.Amount = new Decimal(100.00);
            //line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //salesOrder.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //salesOrder.TxnTaxDetail = txnTaxDetail;
            return salesOrder;
        }



        internal static SalesOrder UpdateSalesOrder(ServiceContext context, SalesOrder entity)
        {
            //update the properties of entity
            entity.TrackingNum = "trackingNum_updated";
            entity.DocNumber = "DocNumber_updated";
            return entity;
        }


        internal static SalesOrder UpdateSalesOrderSparse(ServiceContext context, string id, string syncToken)
        {
            SalesOrder entity = new SalesOrder();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.TrackingNum = "trackingNum_Sparseupdated";
            entity.DocNumber = "DocNumber_Sparseupdated";
            return entity;
        }


        internal static CreditMemo CreateCreditMemo(ServiceContext context)
        {
            CreditMemo creditMemo = new CreditMemo();
            //creditMemo.RemainingCredit = new Decimal(100.00);
            //creditMemo.RemainingCreditSpecified = true;
            //creditMemo.CreditMemoEx = 
            //creditMemo.AutoDocNumber = true;
            //creditMemo.AutoDocNumberSpecified = true;
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            creditMemo.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //creditMemo.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //creditMemo.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //creditMemo.ShipAddr = shipAddr;
            //creditMemo.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.DueDate = DateTime.UtcNow.Date;
            //creditMemo.DueDateSpecified = true;
            //creditMemo.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.PONumber = "PONumber";
            //creditMemo.FOB = "FOB";
            //creditMemo.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.ShipDate = DateTime.UtcNow.Date;
            //creditMemo.ShipDateSpecified = true;
            //creditMemo.TrackingNum = "TrackingNum";
            //creditMemo.GlobalTaxCalculation = GlobalTaxCalculationEnum.;
            //creditMemo.GlobalTaxCalculationSpecified = true;
            creditMemo.TotalAmt = new Decimal(100.00);
            creditMemo.TotalAmtSpecified = true;
            //creditMemo.HomeTotalAmt = new Decimal(100.00);
            //creditMemo.HomeTotalAmtSpecified = true;
            creditMemo.ApplyTaxAfterDiscount = true;
            creditMemo.ApplyTaxAfterDiscountSpecified = true;
            //creditMemo.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.PrintStatus = PrintStatusEnum.;
            //creditMemo.PrintStatusSpecified = true;
            //creditMemo.EmailStatus = EmailStatusEnum.;
            //creditMemo.EmailStatusSpecified = true;
            //EmailAddress billEmail = new EmailAddress();
            //billEmail.Address = "Address";
            //billEmail.Default = true;
            //billEmail.DefaultSpecified = true;
            //billEmail.Tag = "Tag";
            //creditMemo.BillEmail = billEmail;
            //creditMemo.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.Balance = new Decimal(100.00);
            //creditMemo.BalanceSpecified = true;
            //creditMemo.FinanceCharge = true;
            //creditMemo.FinanceChargeSpecified = true;
            //creditMemo.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.PaymentRefNum = "PaymentRefNum";
            //creditMemo.PaymentType = PaymentTypeEnum.;
            //creditMemo.PaymentTypeSpecified = true;
            //creditMemo.AnyIntuitObject = 
            Account depositAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            creditMemo.DepositToAccountRef = new ReferenceType()
            {
                name = depositAccount.Name,
                Value = depositAccount.Id
            };
            creditMemo.DocNumber = "DocNumber" + Helper.GetGuid().Substring(0, 3); ;
            creditMemo.TxnDate = DateTime.UtcNow.Date;
            creditMemo.TxnDateSpecified = true;
            //creditMemo.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //creditMemo.ExchangeRate = new Decimal(100.00);
            //creditMemo.ExchangeRateSpecified = true;
            //creditMemo.PrivateNote = "PrivateNote";
            //creditMemo.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //creditMemo.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line1 = new Line();
            line1.LineNum = "1";
            line1.Description = "Description";
            line1.Amount = new Decimal(100.00);
            line1.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            line1.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line1.DetailTypeSpecified = true;
            Item item = Helper.FindOrAdd<Item>(context, new Item());
            TaxCode taxcode = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
            line1.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType() { name = item.Name, Value = item.Id },
                TaxCodeRef = new ReferenceType() { name = taxcode.Name, Value = taxcode.Id }
            };

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            Line line2 = new Line();
            line2.Amount = new Decimal(100.00);
            line2.DetailType = LineDetailTypeEnum.SubTotalLineDetail;
            line2.DetailTypeSpecified = true;
            lineList.Add(line1);
            lineList.Add(line2);
            creditMemo.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //creditMemo.TxnTaxDetail = txnTaxDetail;
            return creditMemo;
        }

        internal static CreditMemo UpdateCreditMemo(ServiceContext context, CreditMemo entity)
        {
            //update the properties of entity
            entity.DocNumber = "UpdatedDocNum" + Helper.GetGuid().Substring(0, 3); ;
            entity.TxnDate = DateTime.UtcNow.Date.AddDays(2);
            entity.TxnDateSpecified = true;
            return entity;
        }

        internal static CreditMemo UpdateCreditMemoSparse(ServiceContext context, string Id, string SyncToken)
        {
            //update the properties of entity
            CreditMemo entity = new CreditMemo();
            entity.Id = Id;
            entity.SyncToken = SyncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DocNumber = "UpdatedDocNum" + Helper.GetGuid().Substring(0, 3); ;
            entity.TxnDate = DateTime.UtcNow.Date.AddDays(2);
            entity.TxnDateSpecified = true;
            return entity;
        }

        internal static RefundReceipt CreateRefundReceipt(ServiceContext context)
        {
            RefundReceipt refundReceipt = new RefundReceipt();
            //refundReceipt.RemainingCredit = new Decimal(100.00);
            //refundReceipt.RemainingCreditSpecified = true;
            ////refundReceipt.RefundReceiptEx = 
            //refundReceipt.AutoDocNumber = true;
            //refundReceipt.AutoDocNumberSpecified = true;
            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            refundReceipt.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer),
                Value = customer.Id
            };
            //MemoRef customerMemo = new MemoRef();
            //customerMemo.id = "id";
            //customerMemo.Value = "Value";
            //refundReceipt.CustomerMemo = customerMemo;
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //refundReceipt.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //refundReceipt.ShipAddr = shipAddr;
            //refundReceipt.RemitToRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.DueDate = DateTime.UtcNow.Date;
            //refundReceipt.DueDateSpecified = true;
            //refundReceipt.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.PONumber = "PONumber";
            //refundReceipt.FOB = "FOB";
            //refundReceipt.ShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.ShipDate = DateTime.UtcNow.Date;
            //refundReceipt.ShipDateSpecified = true;
            //refundReceipt.TrackingNum = "TrackingNum";

            //refundReceipt.TotalAmt = new Decimal(100.00);
            //refundReceipt.TotalAmtSpecified = true;
            //refundReceipt.HomeTotalAmt = new Decimal(100.00);
            //refundReceipt.HomeTotalAmtSpecified = true;

            //refundReceipt.GlobalTaxCalculation = GlobalTaxCalculationEnum.TaxInclusive;
            //refundReceipt.GlobalTaxCalculationSpecified = true;
            refundReceipt.ApplyTaxAfterDiscount = true;
            refundReceipt.ApplyTaxAfterDiscountSpecified = true;
            //refundReceipt.TemplateRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            refundReceipt.PrintStatus = PrintStatusEnum.NotSet;
            refundReceipt.PrintStatusSpecified = true;
            //refundReceipt.EmailStatus = EmailStatusEnum.EmailSent;
            //refundReceipt.EmailStatusSpecified = true;
            EmailAddress billEmail = new EmailAddress();
            billEmail.Address = "Address@Intuit.com";
            billEmail.Default = true;
            billEmail.DefaultSpecified = true;
            billEmail.Tag = "Tag";
            refundReceipt.BillEmail = billEmail;
            //refundReceipt.ARAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.Balance = new Decimal(100.00);
            refundReceipt.BalanceSpecified = true;
            //refundReceipt.FinanceCharge = true;
            //refundReceipt.FinanceChargeSpecified = true;
            //refundReceipt.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            refundReceipt.PaymentRefNum = "PaymentRefNum";
            refundReceipt.PaymentType = PaymentTypeEnum.CreditCard;
            refundReceipt.PaymentTypeSpecified = true;
            //refundReceipt.AnyIntuitObject = 
            //refundReceipt.DepositToAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            refundReceipt.DocNumber = "DocNumber";
            refundReceipt.TxnDate = DateTime.UtcNow.Date;
            refundReceipt.TxnDateSpecified = true;
            //refundReceipt.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //refundReceipt.ExchangeRate = new Decimal(100.00);
            //refundReceipt.ExchangeRateSpecified = true;
            refundReceipt.PrivateNote = "PrivateNote";
            //refundReceipt.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //refundReceipt.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();
            Line line = new Line();

            line.Description = "Description12";
            line.Amount = new Decimal(100.00);
            line.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            Item item = Helper.FindOrAdd<Item>(context, new Item());
            TaxCode taxCode = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    name = item.Name,
                    Value = item.Id
                },
                TaxCodeRef = new ReferenceType()
                {
                    name = taxCode.Name,
                    Value = taxCode.Id
                }
            };
            Line line2 = new Line();
            line2.Amount = new Decimal(100.00);
            line2.DetailType = LineDetailTypeEnum.SubTotalLineDetail;
            line2.DetailTypeSpecified = true;
            line2.AnyIntuitObject = new SubTotalLineDetail()
            {
            };

            TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            txnTaxDetail.TxnTaxCodeRef = new ReferenceType()
            {
                name = taxCode.Name,
                Value = taxCode.Id

            };
            txnTaxDetail.TotalTax = new Decimal(100.00);
            txnTaxDetail.TotalTaxSpecified = true;


            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            lineList.Add(line);
            refundReceipt.Line = lineList.ToArray();


            Account account = Helper.FindOrAddAccount(context, AccountTypeEnum.Bank, AccountClassificationEnum.Liability);
            refundReceipt.DepositToAccountRef = new ReferenceType()
            {
                name = account.Name,
                Value = account.Id
            };
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};


            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //refundReceipt.TxnTaxDetail = txnTaxDetail;
            return refundReceipt;
        }



        internal static RefundReceipt UpdateRefundReceipt(ServiceContext context, RefundReceipt entity)
        {
            entity.PrintStatus = PrintStatusEnum.NeedToPrint;
            entity.PrintStatusSpecified = true;
            //entity.EmailStatus = EmailStatusEnum.NotSet;
            //entity.EmailStatusSpecified = true;
            return entity;
        }


        internal static RefundReceipt UpdateRefundReceiptSparse(ServiceContext context, string id, string syncToken)
        {
            RefundReceipt entity = new RefundReceipt();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.PrintStatus = PrintStatusEnum.NeedToPrint;
            entity.PrintStatusSpecified = true;
            //entity.EmailStatus = EmailStatusEnum.NotSet;
            //entity.EmailStatusSpecified = true;
            return entity;
        }


        internal static CompanyCurrency CreateCompanyCurrency(ServiceContext context)
        {
            CompanyCurrency companyCurrency = new CompanyCurrency();

            companyCurrency.Active = true;
            companyCurrency.ActiveSpecified = true;


            companyCurrency.Code = "MEM";
            // companyCurrency.Name = "Euro";


            //currency.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            //currency.Active = true;
            //currency.ActiveSpecified = true;

            //currencyCode currencyCode = new currencyCode();
            //currency.Code = currencyCode.AMD;

            //currency.CodeSpecified = true;
            //currency.Separator = "Separator" + Helper.GetGuid().Substring(0, 5);
            //currency.Format = "####";
            //currency.DecimalPlaces = "DecimalPlaces";
            //currency.DecimalSeparator = ",";
            //currency.Symbol = "Symbol";
            //currency.SymbolPosition = SymbolPositionEnum.Leading;
            //currency.SymbolPositionSpecified = true;
            //currency.UserDefined = true;
            //currency.UserDefinedSpecified = true;
            //currency.ExchangeRate = new Decimal(100.00);
            //currency.ExchangeRateSpecified = true;
            //currency.AsOfDate = DateTime.UtcNow.Date;
            //currency.AsOfDateSpecified = true;
            //currency.CurrencyEx = 
            return companyCurrency;
        }



        internal static CompanyCurrency UpdateCompanyCurrency(ServiceContext context, CompanyCurrency entity)
        {
            //update the properties of entity
            entity.Name = "Name_updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }



        internal static CompanyCurrency UpdateCompanyCurrencySparse(ServiceContext context, string id, string syncToken)
        {
            CompanyCurrency entity = new CompanyCurrency();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Name_sparseupdated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }




        internal static ExchangeRate UpdateExchangeRate(ServiceContext context, ExchangeRate entity)
        {
            //update the properties of entity
            entity.RateSpecified = true;
            entity.Rate = 34.00m;
            entity.AsOfDateSpecified = true;
            entity.AsOfDate = DateTime.Now;
            return entity;
        }



        internal static ExchangeRate UpdateExchangeRateSparse(ServiceContext context, string id, string syncToken)
        {
            ExchangeRate entity = new ExchangeRate();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.RateSpecified = true;
            entity.Rate = 34.00m;
            entity.AsOfDateSpecified = true;
            entity.AsOfDate = DateTime.Now;
            return entity;
        }

        internal static SalesRep CreateSalesRep(ServiceContext context)
        {
            SalesRep salesRep = new SalesRep();
            salesRep.NameOf = SalesRepTypeEnum.Employee;
            salesRep.NameOfSpecified = true;
            salesRep.Active = true;
            salesRep.ActiveSpecified = true;
            //salesRep.AnyIntuitObject = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            ItemChoiceType2 itemChoiceType2 = new ItemChoiceType2();
            salesRep.ItemElementName = itemChoiceType2;

            salesRep.Initials = "Initials";
            //salesRep.SalesRepEx = 
            return salesRep;
        }



        internal static SalesRep UpdateSalesRep(ServiceContext context, SalesRep entity)
        {
            //update the properties of entity
            entity.Initials = "Initials_updated";
            return entity;
        }


        internal static SalesRep UpdateSalesRepSparse(ServiceContext context, string id, string syncToken)
        {
            SalesRep entity = new SalesRep();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Initials = "Initials_updated";
            return entity;
        }

        internal static PriceLevel CreatePriceLevel(ServiceContext context)
        {
            PriceLevel priceLevel = new PriceLevel();
            //priceLevel.Name = object;
            //Object object = new Object();

            priceLevel.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            priceLevel.Active = true;
            priceLevel.PriceLevelType = PriceLevelTypeEnum.FixedPercentage;

            priceLevel.CurrencyRef = new ReferenceType()
            {
                name = "United States Dollar",
                Value = "USD"
            };
            //priceLevel.PriceLevelEx = 
            return priceLevel;
        }



        internal static PriceLevel UpdatePriceLevel(ServiceContext context, PriceLevel entity)
        {
            //update the properties of entity
            entity.Name = "updated_name" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }

        internal static PriceLevel UpdatePriceLevelSparse(ServiceContext context, string id, string syncToken)
        {
            PriceLevel entity = new PriceLevel();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "sparseupdated_name" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static PriceLevelPerItem CreatePriceLevelPerItem(ServiceContext context)
        {
            PriceLevelPerItem priceLevelPerItem = new PriceLevelPerItem();
            //priceLevelPerItem.ItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //priceLevelPerItem.AnyIntuitObject = new Decimal(100.00);
            //priceLevelPerItem.ItemElementName = itemChoiceType7;
            //ItemChoiceType7 itemChoiceType7 = new ItemChoiceType7();

            //priceLevelPerItem.PriceLevelPerItemEx = 
            return priceLevelPerItem;
        }



        internal static PriceLevelPerItem UpdatePriceLevelPerItem(ServiceContext context, PriceLevelPerItem entity)
        {
            //update the properties of entity
            entity.Overview = "Overview_sparse";
            return entity;
        }


        internal static PriceLevelPerItem UpdatePriceLevelPerItemSparse(ServiceContext context, string id, string syncToken)
        {
            PriceLevelPerItem entity = new PriceLevelPerItem();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Overview = "Overview_sparse";
            return entity;
        }


        internal static CustomerMsg CreateCustomerMsg(ServiceContext context)
        {
            CustomerMsg customerMsg = new CustomerMsg();
            customerMsg.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            customerMsg.Active = true;
            customerMsg.ActiveSpecified = true;
            //customerMsg.CustomerMsgEx = 
            return customerMsg;
        }



        internal static CustomerMsg UpdateCustomerMsg(ServiceContext context, CustomerMsg entity)
        {
            //update the properties of entity
            entity.Name = "Name_updated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }

        internal static CustomerMsg UpdateCustomerMsgSparse(ServiceContext context, string id, string syncToken)
        {
            CustomerMsg entity = new CustomerMsg();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Name_updated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }


        internal static JournalCode CreateJournalCode(ServiceContext context, JournalCodeTypeEnum journalCodeType)
        {
            JournalCode journalCode = new JournalCode();
            journalCode.Name = "JC" + Helper.GetGuid().Substring(0, 5);
            //journalCode.Description = "Desc " + journalCodeType.ToString();
            journalCode.Type = journalCodeType.ToString();

            return journalCode;
        }



        internal static JournalEntry CreateJournalEntry(ServiceContext context)
        {
            JournalEntry journalEntry = new JournalEntry();
            journalEntry.Adjustment = true;
            journalEntry.AdjustmentSpecified = true;
            //journalEntry.JournalEntryEx = 

            journalEntry.DocNumber = "DocNumber" + Helper.GetGuid().Substring(0, 5);
            journalEntry.TxnDate = DateTime.UtcNow.Date;
            journalEntry.TxnDateSpecified = true;
            //journalEntry.DepartmentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //journalEntry.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //journalEntry.PrivateNote = "PrivateNote";
            //journalEntry.TxnStatus = "TxnStatus";

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //journalEntry.LinkedTxn = linkedTxnList.ToArray();

            List<Line> lineList = new List<Line>();

            Line debitLine = new Line();
            debitLine.Description = "nov portion of rider insurance";
            debitLine.Amount = new Decimal(100.00);
            debitLine.AmountSpecified = true;
            debitLine.DetailType = LineDetailTypeEnum.JournalEntryLineDetail;
            debitLine.DetailTypeSpecified = true;
            JournalEntryLineDetail journalEntryLineDetail = new JournalEntryLineDetail();
            journalEntryLineDetail.PostingType = PostingTypeEnum.Debit;
            journalEntryLineDetail.PostingTypeSpecified = true;
            Account expenseAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            journalEntryLineDetail.AccountRef = new ReferenceType() { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = expenseAccount.Name, Value = expenseAccount.Id };
            debitLine.AnyIntuitObject = journalEntryLineDetail;
            lineList.Add(debitLine);

            Line creditLine = new Line();
            creditLine.Description = "nov portion of rider insurance";
            creditLine.Amount = new Decimal(100.00);
            creditLine.AmountSpecified = true;
            creditLine.DetailType = LineDetailTypeEnum.JournalEntryLineDetail;
            creditLine.DetailTypeSpecified = true;
            JournalEntryLineDetail journalEntryLineDetailCredit = new JournalEntryLineDetail();
            journalEntryLineDetailCredit.PostingType = PostingTypeEnum.Credit;
            journalEntryLineDetailCredit.PostingTypeSpecified = true;
            Account assetAccount = Helper.FindOrAddAccount(context, AccountTypeEnum.OtherCurrentAsset, AccountClassificationEnum.Asset);
            journalEntryLineDetailCredit.AccountRef = new ReferenceType() { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = assetAccount.Name, Value = assetAccount.Id };
            creditLine.AnyIntuitObject = journalEntryLineDetailCredit;
            lineList.Add(creditLine);

            journalEntry.Line = lineList.ToArray();

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //line.DetailType = LineDetailTypeEnum.;
            //line.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //line.LineEx = 
            //lineList.Add(line);
            //journalEntry.Line = lineList.ToArray();
            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //txnTaxDetail.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //txnTaxDetail.TotalTax = new Decimal(100.00);
            //txnTaxDetail.TotalTaxSpecified = true;

            //List<Line> taxLineList = new List<Line>();
            //Line taxLine = new Line();
            //taxLine.LineNum = "LineNum";
            //taxLine.Description = "Description";
            //taxLine.Amount = new Decimal(100.00);
            //taxLine.AmountSpecified = true;

            //List<LinkedTxn> linkedTxnList = new List<LinkedTxn>();
            //LinkedTxn linkedTxn = new LinkedTxn();
            //linkedTxn.TxnId = "TxnId";
            //linkedTxn.TxnType = "TxnType";
            //linkedTxn.TxnLineId = "TxnLineId";
            //linkedTxnList.Add(linkedTxn);
            //line.LinkedTxn = linkedTxnList.ToArray();
            //taxLine.DetailType = LineDetailTypeEnum.;
            //taxLine.AnyIntuitObject = 

            //List<CustomField> customFieldList = new List<CustomField>();
            //CustomField customField = new CustomField();
            //customField.DefinitionId = "DefinitionId";
            //customField.Name = "Name";
            //customField.Type = CustomFieldTypeEnum.;
            //customField.AnyIntuitObject = 
            //customFieldList.Add(customField);
            //line.CustomField = customFieldList.ToArray();
            //taxLine.LineEx = 

            //taxLineList.Add(taxLine);
            //txnTaxDetail.TaxLine = taxLineList.ToArray();
            //journalEntry.TxnTaxDetail = txnTaxDetail;
            return journalEntry;
        }


        internal static JournalCode UpdateJournalCode(ServiceContext context, JournalCode journalCode)
        {
            //update the properties of JournalCode
            journalCode.Description = "Updated" + Helper.GetGuid().Substring(0, 5);

            return journalCode;
        }

        internal static JournalCode UpdateJournalCodeSparse(ServiceContext context, string id, string syncToken)
        {
            JournalCode entity = new JournalCode();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "JC_Sparse" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "sparseupdated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }



        internal static JournalEntry UpdateJournalEntry(ServiceContext context, JournalEntry entity)
        {
            //update the properties of entity
            entity.DocNumber = "udpated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }


        internal static JournalEntry UpdateJournalEntrySparse(ServiceContext context, string id, string syncToken)
        {
            JournalEntry entity = new JournalEntry();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DocNumber = "sparseudpated" + Helper.GetGuid().Substring(0, 5);

            return entity;
        }




        internal static TimeActivity CreateTimeActivity(ServiceContext context)
        {
            TimeActivity timeActivity = new TimeActivity();
            //timeActivity.TimeZone = "PST";
            timeActivity.TxnDate = DateTime.UtcNow.Date;
            timeActivity.TxnDateSpecified = true;
            timeActivity.NameOf = TimeActivityTypeEnum.Vendor;
            timeActivity.NameOfSpecified = true;
            //OtherName otherName = Helper.FindOrAdd<OtherName>(context, new OtherName());
            //ReferenceType otherNameRef = new ReferenceType();
            //otherNameRef.name = otherName.DisplayName;
            //otherNameRef.Value = otherName.Id;
            //Employee emp = Helper.FindOrAdd(context, new Employee());
            Vendor vendor = Helper.FindOrAdd(context, new Vendor());

            timeActivity.AnyIntuitObject = new ReferenceType() { name = vendor.DisplayName, Value = vendor.Id };
            timeActivity.ItemElementName = ItemChoiceType5.VendorRef;

            Customer cust = Helper.FindOrAdd(context, new Customer());
            timeActivity.CustomerRef = new ReferenceType()
            {
                name = cust.DisplayName,
                Value = cust.Id
            };

            Item item = Helper.FindOrAdd(context, new Item());
            timeActivity.ItemRef = new ReferenceType()
            {
                name = item.Name,
                Value = item.Id
            };

            //timeActivity.ClassRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //timeActivity.PayrollItemRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            timeActivity.BillableStatus = BillableStatusEnum.NotBillable;
            timeActivity.BillableStatusSpecified = true;
            timeActivity.Taxable = false;
            timeActivity.TaxableSpecified = true;
            timeActivity.HourlyRate = new Decimal(0.00);
            timeActivity.HourlyRateSpecified = true;
            timeActivity.Hours = 10;

            timeActivity.HoursSpecified = true;
            timeActivity.Minutes = 0;

            timeActivity.MinutesSpecified = true;
            //timeActivity.BreakHours = int32;
            //Int32 int32 = new Int32();

            //timeActivity.BreakHoursSpecified = true;
            //timeActivity.BreakMinutes = int32;
            //Int32 int32 = new Int32();

            //timeActivity.BreakMinutesSpecified = true;
            //timeActivity.StartTime = DateTime.UtcNow.Date;
            //timeActivity.StartTimeSpecified = true;
            //timeActivity.EndTime = DateTime.UtcNow.Date;
            //timeActivity.EndTimeSpecified = true;
            timeActivity.Description = "Description" + Helper.GetGuid().Substring(0, 5);
            //timeActivity.TimeActivityEx = 
            return timeActivity;
        }



        internal static TimeActivity UpdateTimeActivity(ServiceContext context, TimeActivity entity)
        {
            //update the properties of entity
            entity.Description = "UpdatedDesc" + Helper.GetGuid().Substring(0, 3);
            return entity;
        }

        internal static TimeActivity UpdateTimeActivitySparse(ServiceContext context, string Id, string syncToken)
        {
            //update the properties of entity
            TimeActivity entity = new TimeActivity();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Description = "UpdatedDesc" + Helper.GetGuid().Substring(0, 3);
            return entity;
        }

        internal static InventorySite CreateInventorySite(ServiceContext context)
        {
            InventorySite inventorySite = new InventorySite();
            inventorySite.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            inventorySite.Active = true;
            inventorySite.ActiveSpecified = true;
            inventorySite.DefaultSite = true;
            inventorySite.DefaultSiteSpecified = true;
            inventorySite.Description = "description about inventory site";
            inventorySite.Contact = "Contact";

            List<PhysicalAddress> addrList = new List<PhysicalAddress>();
            PhysicalAddress addr = new PhysicalAddress();
            addr.Line1 = "address_inventory";
            addr.Line2 = "address_inverntory";
            //addr.Line3 = "Line3";
            //addr.Line4 = "Line4";
            //addr.Line5 = "Line5";
            addr.City = "Bangalore";
            addr.Country = "karnataka";
            //addr.CountryCode = "CountryCode";
            //addr.CountrySubDivisionCode = "CountrySubDivisionCode";
            addr.PostalCode = "Code" + Helper.GetGuid().Substring(0, 5);
            //addr.PostalCodeSuffix = "PostalCodeSuffix";
            //addr.Lat = "Lat";
            //addr.Long = "Long";
            //addr.Tag = "Tag";
            //addr.Note = "Note";
            addrList.Add(addr);
            inventorySite.Addr = addrList.ToArray();

            //List<ContactInfo> contactInfoList = new List<ContactInfo>();
            //ContactInfo contactInfo = new ContactInfo();
            //contactInfo.Type = ContactTypeEnum.;
            //contactInfo.TypeSpecified = true;
            //contactInfo.AnyIntuitObject = 
            //contactInfoList.Add(contactInfo);
            //inventorySite.ContactInfo = contactInfoList.ToArray();
            //inventorySite.InventorySiteEx = 
            return inventorySite;
        }



        internal static InventorySite UpdateInventorySite(ServiceContext context, InventorySite entity)
        {
            //update the properties of entity
            entity.Name = "UPdated" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "description" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static InventorySite UpdateInventorySiteSparse(ServiceContext context, string id, string syncToken)
        {
            InventorySite entity = new InventorySite();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "sparseUPdated" + Helper.GetGuid().Substring(0, 5);
            entity.Description = "description" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static ShipMethod CreateShipMethod(ServiceContext context)
        {
            ShipMethod shipMethod = new ShipMethod();
            shipMethod.Name = "Name" + Helper.GetGuid().Substring(0, 5); ;
            shipMethod.Active = true;
            //shipMethod.ShipMethodEx = 
            return shipMethod;
        }



        internal static ShipMethod UpdateShipMethod(ServiceContext context, ShipMethod entity)
        {
            //update the properties of entity
            entity.Name = "updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static ShipMethod UpdateShipMethodSparse(ServiceContext context, string id, string syncToken)
        {
            ShipMethod entity = new ShipMethod();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }

        internal static Task CreateTask(ServiceContext context)
        {
            Task task = new Task();
            //task.Notes = "Notes";
            task.From = "Quality";
            task.Active = true;
            task.ActiveSpecified = true;
            task.Done = true;
            task.DoneSpecified = true;
            task.ReminderDate = DateTime.UtcNow.Date;
            task.ReminderDateSpecified = true;
            //task.TaskEx = 
            return task;
        }



        internal static Task UpdateTask(ServiceContext context, Task entity)
        {
            //update the properties of entity
            entity.From = "Quality";
            return entity;
        }



        internal static Task UpdateTaskSparse(ServiceContext context, string id, string syncToken)
        {
            Task entity = new Task();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.From = "Quality";
            return entity;
        }



        internal static Preferences CreatePreferences(ServiceContext context)
        {
            Preferences preferences = new Preferences();

            //CompanyAccountingPrefs accountingInfoPrefs = new CompanyAccountingPrefs();
            //    accountingInfoPrefs.UseAccountNumbers = true;
            //    accountingInfoPrefs.UseAccountNumbersSpecified = true;

            //accountingInfoPrefs.DefaultARAccount = new ReferenceType() 
            //  { 
            //name =  
            //type = 
            //Value = 
            //};
            //accountingInfoPrefs.DefaultAPAccount = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //accountingInfoPrefs.RequiresAccounts = true;
            //accountingInfoPrefs.RequiresAccountsSpecified = true;
            //accountingInfoPrefs.TrackDepartments = true;
            //accountingInfoPrefs.TrackDepartmentsSpecified = true;
            //accountingInfoPrefs.DepartmentTerminology = "DepartmentTerminology";
            //accountingInfoPrefs.ClassTrackingPerTxn = true;
            //accountingInfoPrefs.ClassTrackingPerTxnSpecified = true;
            //accountingInfoPrefs.ClassTrackingPerTxnLine = true;
            //accountingInfoPrefs.ClassTrackingPerTxnLineSpecified = true;
            //accountingInfoPrefs.AutoJournalEntryNumber = true;
            //accountingInfoPrefs.AutoJournalEntryNumberSpecified = true;
            //accountingInfoPrefs.FirstMonthOfFiscalYear = MonthEnum.;
            //accountingInfoPrefs.FirstMonthOfFiscalYearSpecified = true;
            //accountingInfoPrefs.TaxYearMonth = MonthEnum.;
            //accountingInfoPrefs.TaxYearMonthSpecified = true;
            //accountingInfoPrefs.TaxForm = "TaxForm";
            //accountingInfoPrefs.BookCloseDate = DateTime.UtcNow.Date;
            //accountingInfoPrefs.BookCloseDateSpecified = true;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //    otherContactInfo.Type = ContactTypeEnum.TelephoneNumber;
            //    otherContactInfo.TypeSpecified = true;
            //    //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //accountingInfoPrefs.OtherContactInfo = otherContactInfoList.ToArray();
            //accountingInfoPrefs.CustomerTerminology = "CustomerTerminology";
            //preferences.AccountingInfoPrefs = accountingInfoPrefs;
            //AdvancedInventoryPrefs advancedInventoryPrefs = new AdvancedInventoryPrefs();
            //advancedInventoryPrefs.MLIAvailable = true;
            //advancedInventoryPrefs.MLIAvailableSpecified = true;
            //advancedInventoryPrefs.MLIEnabled = true;
            //advancedInventoryPrefs.MLIEnabledSpecified = true;
            //advancedInventoryPrefs.EnhancedInventoryReceivingEnabled = true;
            //advancedInventoryPrefs.EnhancedInventoryReceivingEnabledSpecified = true;
            //advancedInventoryPrefs.TrackingSerialOrLotNumber = true;
            //advancedInventoryPrefs.TrackingSerialOrLotNumberSpecified = true;
            //advancedInventoryPrefs.TrackingOnSalesTransactionsEnabled = true;
            //advancedInventoryPrefs.TrackingOnSalesTransactionsEnabledSpecified = true;
            //advancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabled = true;
            //advancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabledSpecified = true;
            //advancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabled = true;
            //advancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabledSpecified = true;
            //advancedInventoryPrefs.TrackingOnBuildAssemblyEnabled = true;
            //advancedInventoryPrefs.TrackingOnBuildAssemblyEnabledSpecified = true;
            //advancedInventoryPrefs.FIFOEnabled = true;
            //advancedInventoryPrefs.FIFOEnabledSpecified = true;
            //advancedInventoryPrefs.FIFOEffectiveDate = DateTime.UtcNow.Date;
            //advancedInventoryPrefs.FIFOEffectiveDateSpecified = true;
            //advancedInventoryPrefs.RowShelfBinEnabled = true;
            //advancedInventoryPrefs.RowShelfBinEnabledSpecified = true;
            //advancedInventoryPrefs.BarcodeEnabled = true;
            //advancedInventoryPrefs.BarcodeEnabledSpecified = true;
            //preferences.AdvancedInventoryPrefs = advancedInventoryPrefs;
            ProductAndServicesPrefs productAndServicesPrefs = new ProductAndServicesPrefs();
            productAndServicesPrefs.ForSales = true;
            productAndServicesPrefs.ForSalesSpecified = true;
            productAndServicesPrefs.ForPurchase = true;
            productAndServicesPrefs.ForPurchaseSpecified = true;
            productAndServicesPrefs.InventoryAndPurchaseOrder = true;
            productAndServicesPrefs.InventoryAndPurchaseOrderSpecified = true;
            productAndServicesPrefs.QuantityWithPriceAndRate = true;
            productAndServicesPrefs.QuantityWithPriceAndRateSpecified = true;
            productAndServicesPrefs.QuantityOnHand = true;
            productAndServicesPrefs.QuantityOnHandSpecified = true;
            productAndServicesPrefs.UOM = UOMFeatureTypeEnum.SinglePerItem;
            productAndServicesPrefs.UOMSpecified = true;
            preferences.ProductAndServicesPrefs = productAndServicesPrefs;
            SalesFormsPrefs salesFormsPrefs = new SalesFormsPrefs();
            salesFormsPrefs.UsingProgressInvoicing = true;
            salesFormsPrefs.UsingProgressInvoicingSpecified = true;

            //List<CustomFieldDefinition> customFieldList = new List<CustomFieldDefinition>();
            //CustomFieldDefinition customField = new CustomFieldDefinition();
            //customField.EntityType = "EntityType";
            //customField.Name = "Name";
            //customField.Hidden = true;
            //customField.Required = true;
            //customFieldList.Add(customField);
            //salesFormsPrefs.CustomField = customFieldList.ToArray();
            salesFormsPrefs.CustomTxnNumbers = true;
            salesFormsPrefs.CustomTxnNumbersSpecified = true;
            salesFormsPrefs.DelayedCharges = true;
            salesFormsPrefs.DelayedChargesSpecified = true;
            salesFormsPrefs.AllowDeposit = true;
            salesFormsPrefs.AllowDepositSpecified = true;
            salesFormsPrefs.AllowDiscount = true;
            salesFormsPrefs.AllowDiscountSpecified = true;
            salesFormsPrefs.DefaultDiscountAccount = "DefaultDiscountAccount";
            salesFormsPrefs.AllowEstimates = true;
            salesFormsPrefs.AllowEstimatesSpecified = true;
            salesFormsPrefs.IPNSupportEnabled = true;
            salesFormsPrefs.IPNSupportEnabledSpecified = true;
            salesFormsPrefs.InvoiceMessage = "InvoiceMessage";
            salesFormsPrefs.AllowServiceDate = true;
            salesFormsPrefs.AllowServiceDateSpecified = true;
            salesFormsPrefs.AllowShipping = true;
            salesFormsPrefs.AllowShippingSpecified = true;
            //salesFormsPrefs.DefaultShippingAccount = "DefaultShippingAccount";
            //salesFormsPrefs.DefaultItem = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesFormsPrefs.DefaultTerms = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesFormsPrefs.DefaultDeliveryMethod = "DefaultDeliveryMethod";
            //salesFormsPrefs.AutoApplyCredit = true;
            //salesFormsPrefs.AutoApplyCreditSpecified = true;
            //salesFormsPrefs.AutoApplyPayments = true;
            //salesFormsPrefs.AutoApplyPaymentsSpecified = true;
            //salesFormsPrefs.PrintItemWithZeroAmount = true;
            //salesFormsPrefs.PrintItemWithZeroAmountSpecified = true;
            //salesFormsPrefs.DefaultShipMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //salesFormsPrefs.DefaultMarkup = new Decimal(100.00);
            //salesFormsPrefs.DefaultMarkupSpecified = true;
            //salesFormsPrefs.TrackReimbursableExpensesAsIncome = true;
            //salesFormsPrefs.TrackReimbursableExpensesAsIncomeSpecified = true;
            //salesFormsPrefs.UsingSalesOrders = true;
            //salesFormsPrefs.UsingSalesOrdersSpecified = true;
            //salesFormsPrefs.UsingPriceLevels = true;
            //salesFormsPrefs.UsingPriceLevelsSpecified = true;
            //salesFormsPrefs.DefaultFOB = "DefaultFOB";
            //salesFormsPrefs.DefaultCustomerMessage = "DefaultCustomerMessage";
            preferences.SalesFormsPrefs = salesFormsPrefs;

            //List<NameValue> emailMessagesPrefsList = new List<NameValue>();
            //NameValue emailMessagesPrefs = new NameValue();
            //emailMessagesPrefs.Name = "Name";
            //emailMessagesPrefs.Value = "Value";
            //emailMessagesPrefsList.Add(emailMessagesPrefs);
            //preferences.EmailMessagesPrefs = emailMessagesPrefsList.ToArray();

            //List<NameValue> printDocumentPrefsList = new List<NameValue>();
            //NameValue printDocumentPrefs = new NameValue();
            //printDocumentPrefs.Name = "Name";
            //printDocumentPrefs.Value = "Value";
            //printDocumentPrefsList.Add(printDocumentPrefs);
            //preferences.PrintDocumentPrefs = printDocumentPrefsList.ToArray();
            VendorAndPurchasesPrefs vendorAndPurchasesPrefs = new VendorAndPurchasesPrefs();
            vendorAndPurchasesPrefs.EnableBills = true;
            vendorAndPurchasesPrefs.EnableBillsSpecified = true;
            vendorAndPurchasesPrefs.TrackingByCustomer = true;
            vendorAndPurchasesPrefs.TrackingByCustomerSpecified = true;
            vendorAndPurchasesPrefs.BillableExpenseTracking = true;
            vendorAndPurchasesPrefs.BillableExpenseTrackingSpecified = true;
            //vendorAndPurchasesPrefs.DefaultTerms = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //vendorAndPurchasesPrefs.DefaultMarkup = new Decimal(100.00);
            //vendorAndPurchasesPrefs.DefaultMarkupSpecified = true;
            //vendorAndPurchasesPrefs.DefaultMarkupAccount = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //vendorAndPurchasesPrefs.AutomaticBillPayment = true;
            //vendorAndPurchasesPrefs.AutomaticBillPaymentSpecified = true;

            //List<CustomFieldDefinition> pOCustomFieldList = new List<CustomFieldDefinition>();
            //CustomFieldDefinition pOCustomField = new CustomFieldDefinition();
            //pOCustomField.EntityType = "EntityType";
            //pOCustomField.Name = "Name";
            //pOCustomField.Hidden = true;
            //pOCustomField.Required = true;
            //pOCustomFieldList.Add(pOCustomField);
            //vendorAndPurchasesPrefs.POCustomField = pOCustomFieldList.ToArray();
            //vendorAndPurchasesPrefs.MsgToVendors = "MsgToVendors";
            //vendorAndPurchasesPrefs.UsingInventory = true;
            //vendorAndPurchasesPrefs.UsingInventorySpecified = true;
            //vendorAndPurchasesPrefs.UsingMultiLocationInventory = true;
            //vendorAndPurchasesPrefs.UsingMultiLocationInventorySpecified = true;
            //vendorAndPurchasesPrefs.DaysBillsAreDue = int32;
            //Int32 int32 = new Int32();
            //vendorAndPurchasesPrefs.DaysBillsAreDueSpecified = true;
            //vendorAndPurchasesPrefs.DiscountAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            preferences.VendorAndPurchasesPrefs = vendorAndPurchasesPrefs;
            //TimeTrackingPrefs timeTrackingPrefs = new TimeTrackingPrefs();
            //timeTrackingPrefs.UseServices = true;
            //timeTrackingPrefs.UseServicesSpecified = true;
            //timeTrackingPrefs.DefaultTimeItem = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //timeTrackingPrefs.BillCustomers = true;
            //timeTrackingPrefs.BillCustomersSpecified = true;
            //timeTrackingPrefs.ShowBillRateToAll = true;
            //timeTrackingPrefs.ShowBillRateToAllSpecified = true;
            //timeTrackingPrefs.WorkWeekStartDate = WeekEnum.;
            //timeTrackingPrefs.WorkWeekStartDateSpecified = true;
            //timeTrackingPrefs.TimeTrackingEnabled = true;
            //timeTrackingPrefs.TimeTrackingEnabledSpecified = true;
            //timeTrackingPrefs.MarkTimeEntriesBillable = true;
            //timeTrackingPrefs.MarkTimeEntriesBillableSpecified = true;
            //timeTrackingPrefs.MarkExpensesAsBillable = true;
            //timeTrackingPrefs.MarkExpensesAsBillableSpecified = true;
            //preferences.TimeTrackingPrefs = timeTrackingPrefs;
            TaxPrefs taxPrefs = new TaxPrefs();
            taxPrefs.UsingSalesTax = true;
            taxPrefs.UsingSalesTaxSpecified = true;
            //taxPrefs.AnyIntuitObject = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxPrefs.ItemElementName = itemChoiceType3;
            //ItemChoiceType3 itemChoiceType3 = new ItemChoiceType3();

            taxPrefs.PaySalesTax = PaySalesTaxEnum.Annually;
            taxPrefs.PaySalesTaxSpecified = true;
            //taxPrefs.NonTaxableSalesTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxPrefs.TaxableSalesTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            preferences.TaxPrefs = taxPrefs;
            //FinanceChargePrefs financeChargesPrefs = new FinanceChargePrefs();
            //financeChargesPrefs.AnnualInterestRate = new Decimal(100.00);
            //financeChargesPrefs.AnnualInterestRateSpecified = true;
            //financeChargesPrefs.MinFinChrg = new Decimal(100.00);
            //financeChargesPrefs.MinFinChrgSpecified = true;
            //financeChargesPrefs.GracePeriod = "GracePeriod";
            //financeChargesPrefs.CalcFinChrgFromTxnDate = true;
            //financeChargesPrefs.CalcFinChrgFromTxnDateSpecified = true;
            //financeChargesPrefs.AssessFinChrgForOverdueCharges = true;
            //financeChargesPrefs.AssessFinChrgForOverdueChargesSpecified = true;
            //financeChargesPrefs.FinChrgAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //preferences.FinanceChargesPrefs = financeChargesPrefs;
            CurrencyPrefs currencyPrefs = new CurrencyPrefs();
            currencyPrefs.MultiCurrencyEnabled = true;
            currencyPrefs.MultiCurrencyEnabledSpecified = true;
            //currencyPrefs.HomeCurrency = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            preferences.CurrencyPrefs = currencyPrefs;
            //ReportPrefs reportPrefs = new ReportPrefs();
            //reportPrefs.ReportBasis = ReportBasisEnum.;
            //reportPrefs.ReportBasisSpecified = true;
            //reportPrefs.CalcAgingReportFromTxnDate = true;
            //reportPrefs.CalcAgingReportFromTxnDateSpecified = true;
            //preferences.ReportPrefs = reportPrefs;

            //List<NameValue> otherPrefsList = new List<NameValue>();
            //NameValue otherPrefs = new NameValue();
            //otherPrefs.Name = "Name";
            //otherPrefs.Value = "Value";
            //otherPrefsList.Add(otherPrefs);
            //preferences.OtherPrefs = otherPrefsList.ToArray();
            return preferences;
        }



        internal static Preferences UpdatePreferences(ServiceContext context, Preferences entity)
        {
            //update the properties of entity
            SalesFormsPrefs salesFormsPrefs = new SalesFormsPrefs();
            salesFormsPrefs.UsingProgressInvoicing = true;
            salesFormsPrefs.UsingProgressInvoicingSpecified = true;


            salesFormsPrefs.CustomTxnNumbers = false;
            salesFormsPrefs.CustomTxnNumbersSpecified = true;
            salesFormsPrefs.AllowDeposit = false;
            salesFormsPrefs.AllowDepositSpecified = true;
            salesFormsPrefs.AllowDiscount = false;
            salesFormsPrefs.AllowDiscountSpecified = true;
            salesFormsPrefs.AllowEstimates = true;
            salesFormsPrefs.AllowEstimatesSpecified = true;
            salesFormsPrefs.IPNSupportEnabled = false;
            salesFormsPrefs.IPNSupportEnabledSpecified = true;

            entity.SalesFormsPrefs = salesFormsPrefs;

            //Output only field.  Need to set to null for Update operation.
            entity.OtherPrefs = null;

            return entity;
        }



        internal static Preferences SparseUpdatePreferences(ServiceContext context, string Id, string syncToken)
        {
            Preferences entity = new Preferences();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            var emailMessagesPrefs = new EmailMessagesPrefs();
            emailMessagesPrefs.InvoiceMessage = new EmailMessage() { Subject = "InvoiceMessage_sparseupdated" };

            entity.EmailMessagesPrefs = emailMessagesPrefs;
            return entity;
        }



        internal static UOM CreateUOM(ServiceContext context)
        {
            UOM uOM = new UOM();
            uOM.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            uOM.Abbrv = "Abbrv";
            uOM.BaseType = UOMBaseTypeEnum.Area;
            uOM.BaseTypeSpecified = true;

            List<UOMConvUnit> convUnitList = new List<UOMConvUnit>();
            UOMConvUnit convUnit = new UOMConvUnit();
            convUnit.Name = "Name";
            convUnit.Abbrv = "Abbrv";
            convUnit.ConvRatio = new Decimal(50.00);
            convUnit.ConvRatioSpecified = true;
            convUnitList.Add(convUnit);
            uOM.ConvUnit = convUnitList.ToArray();
            return uOM;
        }



        internal static UOM UpdateUOM(ServiceContext context, UOM entity)
        {
            //update the properties of entity
            entity.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            entity.Abbrv = "Abbrv";
            return entity;
        }

        internal static UOM UpdateUOMSparse(ServiceContext context, string id, string syncToken)
        {
            UOM entity = new UOM();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            entity.Abbrv = "Abbrv";
            return entity;
        }

        internal static TemplateName CreateTemplateName(ServiceContext context)
        {
            TemplateName templateName = new TemplateName();
            templateName.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            templateName.Active = true;
            templateName.ActiveSpecified = true;
            templateName.Type = TemplateTypeEnum.PurchaseOrder;
            templateName.TypeSpecified = true;
            return templateName;
        }



        internal static TemplateName UpdateTemplateName(ServiceContext context, TemplateName entity)
        {
            //update the properties of entity

            entity.Type = TemplateTypeEnum.SalesOrder;
            entity.TypeSpecified = true;
            return entity;
        }


        internal static TemplateName UpdateTemplateNameSparse(ServiceContext context, string id, string syncToken)
        {
            TemplateName entity = new TemplateName();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Type = TemplateTypeEnum.SalesOrder;
            entity.TypeSpecified = true;
            return entity;
        }



        internal static Attachable CreateAttachable(ServiceContext context)
        {
            Attachable attachable = new Attachable();
            //attachable.FileName = "FileName";
            //attachable.FileAccessUri = "FileAccessUri";
            //attachable.TempDownloadUri = "TempDownloadUri";
            //attachable.Size = int64;
            //Int64 int64 = new Int64();

            //attachable.SizeSpecified = true;
            //attachable.ContentType = "ContentType";
            //attachable.Category = "Category";
            attachable.Lat = "25.293112341223";
            attachable.Long = "-21.3253249834";
            attachable.PlaceName = "Fake Place";
            attachable.Note = "Attachable note " + Helper.GetGuid().Substring(0, 5); ;
            attachable.Tag = "Attachable tag " + Helper.GetGuid().Substring(0, 5); ;
            //attachable.ThumbnailFileAccessUri = "ThumbnailFileAccessUri";
            //attachable.ThumbnailTempDownloadUri = "ThumbnailTempDownloadUri";
            //attachable.AttachableEx = 

            Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
            ReferenceType customerRef = new ReferenceType();
            customerRef.name = customer.DisplayName;
            customerRef.Value = customer.Id;
            customerRef.type = "Customer";

            Vendor vendor = Helper.FindOrAdd<Vendor>(context, new Vendor());
            ReferenceType vendorRef = new ReferenceType();
            vendorRef.name = vendor.DisplayName;
            vendorRef.Value = vendor.Id;
            vendorRef.type = "Vendor";

            AttachableRef attachableRef1 = new AttachableRef();
            attachableRef1.EntityRef = customerRef;
            AttachableRef attachableRef2 = new AttachableRef();
            attachableRef2.EntityRef = vendorRef;

            List<AttachableRef> list = new List<AttachableRef>();
            list.Add(attachableRef1);
            list.Add(attachableRef2);

            attachable.AttachableRef = list.ToArray();
            return attachable;
        }

        internal static Attachable CreateAttachableUpload(ServiceContext context)
        {
            Attachable attachable = new Attachable();
            //attachable.FileName = "Test.jpg";
            //attachable.FileAccessUri = "FileAccessUri";
            //attachable.TempDownloadUri = "TempDownloadUri";
            //attachable.Size = int64;
            //Int64 int64 = new Int64();

            //attachable.SizeSpecified = true;
            //attachable.ContentType = "image/jpeg";
            //attachable.Category = "Category";
            attachable.Lat = "25.293112341223";
            attachable.Long = "-21.3253249834";
            attachable.PlaceName = "Fake Place";
            attachable.Note = "Attachable note " + Helper.GetGuid().Substring(0, 5); ;
            attachable.Tag = "Attachable tag " + Helper.GetGuid().Substring(0, 5); ;
            //attachable.ThumbnailFileAccessUri = "ThumbnailFileAccessUri";
            //attachable.ThumbnailTempDownloadUri = "ThumbnailTempDownloadUri";
            //attachable.AttachableEx = 

            return attachable;
        }


        internal static Attachable UpdateAttachable(ServiceContext context, Attachable entity)
        {
            //update the properties of entity
            entity.Note = "Attachable note " + Helper.GetGuid().Substring(0, 5); ;
            entity.Tag = "Attachable tag " + Helper.GetGuid().Substring(0, 5); ;

            return entity;
        }


        internal static Attachable SparseUpdateAttachable(ServiceContext context, string Id, string syncToken)
        {
            Attachable entity = new Attachable();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.Note = "Sparse Attachable note " + Helper.GetGuid().Substring(0, 5); ;
            entity.Tag = "Sparse Attachable tag " + Helper.GetGuid().Substring(0, 5); ;

            return entity;
        }


        internal static StringTypeCustomFieldDefinition CreateStringTypeCustomFieldDefinition(ServiceContext context)
        {
            StringTypeCustomFieldDefinition stringTypeCustomFieldDefinition = new StringTypeCustomFieldDefinition();
            //stringTypeCustomFieldDefinition.DefaultString = "DefaultString";
            //stringTypeCustomFieldDefinition.RegularExpression = "RegularExpression";
            //stringTypeCustomFieldDefinition.MaxLength = int32;
            //Int32 int32 = new Int32();

            //stringTypeCustomFieldDefinition.MaxLengthSpecified = true;
            //stringTypeCustomFieldDefinition.EntityType = "EntityType";
            //stringTypeCustomFieldDefinition.Name = "Name";
            //stringTypeCustomFieldDefinition.Hidden = true;
            //stringTypeCustomFieldDefinition.Required = true;
            return stringTypeCustomFieldDefinition;
        }



        internal static StringTypeCustomFieldDefinition UpdateStringTypeCustomFieldDefinition(ServiceContext context, StringTypeCustomFieldDefinition entity)
        {
            //update the properties of entity
            return entity;
        }



        internal static NumberTypeCustomFieldDefinition CreateNumberTypeCustomFieldDefinition(ServiceContext context)
        {
            NumberTypeCustomFieldDefinition numberTypeCustomFieldDefinition = new NumberTypeCustomFieldDefinition();
            //numberTypeCustomFieldDefinition.DefaultValue = new Decimal(100.00);
            //numberTypeCustomFieldDefinition.DefaultValueSpecified = true;
            //numberTypeCustomFieldDefinition.MinValue = new Decimal(100.00);
            //numberTypeCustomFieldDefinition.MinValueSpecified = true;
            //numberTypeCustomFieldDefinition.MaxValue = new Decimal(100.00);
            //numberTypeCustomFieldDefinition.MaxValueSpecified = true;
            //numberTypeCustomFieldDefinition.EntityType = "EntityType";
            //numberTypeCustomFieldDefinition.Name = "Name";
            //numberTypeCustomFieldDefinition.Hidden = true;
            //numberTypeCustomFieldDefinition.Required = true;
            return numberTypeCustomFieldDefinition;
        }



        internal static NumberTypeCustomFieldDefinition UpdateNumberTypeCustomFieldDefinition(ServiceContext context, NumberTypeCustomFieldDefinition entity)
        {
            //update the properties of entity
            return entity;
        }



        internal static DateTypeCustomFieldDefinition CreateDateTypeCustomFieldDefinition(ServiceContext context)
        {
            DateTypeCustomFieldDefinition dateTypeCustomFieldDefinition = new DateTypeCustomFieldDefinition();
            //dateTypeCustomFieldDefinition.DefaultDate = DateTime.UtcNow.Date;
            //dateTypeCustomFieldDefinition.DefaultDateSpecified = true;
            //dateTypeCustomFieldDefinition.MinDate = DateTime.UtcNow.Date;
            //dateTypeCustomFieldDefinition.MinDateSpecified = true;
            //dateTypeCustomFieldDefinition.MaxDate = DateTime.UtcNow.Date;
            //dateTypeCustomFieldDefinition.MaxDateSpecified = true;
            //dateTypeCustomFieldDefinition.EntityType = "EntityType";
            //dateTypeCustomFieldDefinition.Name = "Name";
            //dateTypeCustomFieldDefinition.Hidden = true;
            //dateTypeCustomFieldDefinition.Required = true;
            return dateTypeCustomFieldDefinition;
        }



        internal static DateTypeCustomFieldDefinition UpdateDateTypeCustomFieldDefinition(ServiceContext context, DateTypeCustomFieldDefinition entity)
        {
            //update the properties of entity
            return entity;
        }



        internal static BooleanTypeCustomFieldDefinition CreateBooleanTypeCustomFieldDefinition(ServiceContext context)
        {
            BooleanTypeCustomFieldDefinition booleanTypeCustomFieldDefinition = new BooleanTypeCustomFieldDefinition();
            //booleanTypeCustomFieldDefinition.DefaultValue = true;
            //booleanTypeCustomFieldDefinition.DefaultValueSpecified = true;
            //booleanTypeCustomFieldDefinition.EntityType = "EntityType";
            //booleanTypeCustomFieldDefinition.Name = "Name";
            //booleanTypeCustomFieldDefinition.Hidden = true;
            //booleanTypeCustomFieldDefinition.Required = true;
            return booleanTypeCustomFieldDefinition;
        }



        internal static BooleanTypeCustomFieldDefinition UpdateBooleanTypeCustomFieldDefinition(ServiceContext context, BooleanTypeCustomFieldDefinition entity)
        {
            //update the properties of entity
            return entity;
        }



        internal static NameBase CreateNameBase(ServiceContext context)
        {
            NameBase nameBase = new NameBase();
            //nameBase.Organization = true;
            //nameBase.OrganizationSpecified = true;
            //nameBase.Title = "Title";
            //nameBase.GivenName = "GivenName";
            //nameBase.MiddleName = "MiddleName";
            //nameBase.FamilyName = "FamilyName";
            //nameBase.Suffix = "Suffix";
            //nameBase.FullyQualifiedName = "FullyQualifiedName";
            //nameBase.CompanyName = "CompanyName";
            //nameBase.DisplayName = "DisplayName";
            //nameBase.PrintOnCheckName = "PrintOnCheckName";
            //nameBase.UserId = "UserId";
            //nameBase.Active = true;
            //nameBase.ActiveSpecified = true;
            //TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType";
            //primaryPhone.CountryCode = "CountryCode";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            //primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            //nameBase.PrimaryPhone = primaryPhone;
            //TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType";
            //alternatePhone.CountryCode = "CountryCode";
            //alternatePhone.AreaCode = "AreaCode";
            //alternatePhone.ExchangeCode = "ExchangeCode";
            //alternatePhone.Extension = "Extension";
            //alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true;
            //alternatePhone.DefaultSpecified = true;
            //alternatePhone.Tag = "Tag";
            //nameBase.AlternatePhone = alternatePhone;
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            //nameBase.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //nameBase.Fax = fax;
            //EmailAddress primaryEmailAddr = new EmailAddress();
            //primaryEmailAddr.Address = "Address";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            //nameBase.PrimaryEmailAddr = primaryEmailAddr;
            //WebSiteAddress webAddr = new WebSiteAddress();
            //webAddr.URI = "URI";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            //nameBase.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //nameBase.OtherContactInfo = otherContactInfoList.ToArray();
            //nameBase.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            return nameBase;
        }



        internal static NameBase UpdateNameBase(ServiceContext context, NameBase entity)
        {
            //update the properties of entity
            return entity;
        }


        internal static Customer CreateCustomerFrance(ServiceContext context)
        {

            String guid = Helper.GetGuid();

            Customer customer = new Customer();
            customer.Taxable = true;
            customer.TaxableSpecified = true;
            customer.GivenName = "GivenName";
            customer.MiddleName = "MiddleName";
            customer.FamilyName = "FamilyName";
            customer.FullyQualifiedName = "Name_" + guid;
            customer.CompanyName = "CompanyName";
            customer.DisplayName = "Name_" + guid;

            return customer;
        }


        internal static Customer CreateCustomer(ServiceContext context)
        {

            String guid = Helper.GetGuid();
            Customer customer = new Customer();
            customer.Taxable = true;
            customer.TaxableSpecified = true;
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "Line1";
            billAddr.Line2 = "Line2";
            billAddr.Line3 = "Line3";
            billAddr.Line4 = "Line4";
            billAddr.Line5 = "Line5";
            billAddr.City = "City";
            billAddr.Country = "Country";
            //billAddr.CountryCode = "IN";
            billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            customer.BillAddr = billAddr;
            PhysicalAddress shipAddr = new PhysicalAddress();
            shipAddr.Line1 = "Line1";
            shipAddr.Line2 = "Line2";
            shipAddr.Line3 = "Line3";
            shipAddr.Line4 = "Line4";
            shipAddr.Line5 = "Line5";
            shipAddr.City = "City";
            shipAddr.Country = "Country";
            //shipAddr.CountryCode = "IN";
            shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            customer.ShipAddr = shipAddr;

            List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            PhysicalAddress otherAddr = new PhysicalAddress();
            otherAddr.Line1 = "Line1";
            otherAddr.Line2 = "Line2";
            otherAddr.Line3 = "Line3";
            otherAddr.Line4 = "Line4";
            otherAddr.Line5 = "Line5";
            otherAddr.City = "City";
            otherAddr.Country = "Country";
            //otherAddr.CountryCode = "IN";
            otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            otherAddr.PostalCode = "PostalCode";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            otherAddrList.Add(otherAddr);
            customer.OtherAddr = otherAddrList.ToArray();
            //customer.ContactName = "ContactName";
            //customer.AltContactName = "AltContactName";
            customer.Notes = "Notes";
            customer.Job = false;
            customer.JobSpecified = true;
            customer.BillWithParent = false;
            customer.BillWithParentSpecified = true;
            //customer.RootCustomerRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.Level = 1;
            //customer.LevelSpecified = true;
            //customer.CustomerTypeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.SalesTermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.SalesRepRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.AnyIntuitObject = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.ItemElementName = itemChoiceType4;
            //ItemChoiceType4 itemChoiceType4 = new ItemChoiceType4();

            //customer.PaymentMethodRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //CreditChargeInfo cCDetail = new CreditChargeInfo();
            //cCDetail.Number = "Number";
            //cCDetail.Type = "Type";
            //cCDetail.NameOnAcct = "NameOnAcct";
            //cCDetail.CcExpiryMonth = int32;
            //Int32 int32 = new Int32();
            //cCDetail.CcExpiryMonthSpecified = true;
            //cCDetail.CcExpiryYear = int32;
            //Int32 int32 = new Int32();
            //cCDetail.CcExpiryYearSpecified = true;
            //cCDetail.BillAddrStreet = "BillAddrStreet";
            //cCDetail.PostalCode = "PostalCode";
            //cCDetail.CommercialCardCode = "CommercialCardCode";
            //cCDetail.CCTxnMode = CCTxnModeEnum.;
            //cCDetail.CCTxnModeSpecified = true;
            //cCDetail.CCTxnType = CCTxnTypeEnum.;
            //cCDetail.CCTxnTypeSpecified = true;
            //cCDetail.PrevCCTransId = "PrevCCTransId";
            //cCDetail.CreditCardChargeInfoEx = 

            //customer.CCDetail = cCDetail;
            //customer.PriceLevelRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            customer.Balance = new Decimal(100.00);
            customer.BalanceSpecified = true;
            //customer.OpenBalanceDate = DateTime.UtcNow.Date;
            //customer.OpenBalanceDateSpecified = true;
            customer.BalanceWithJobs = new Decimal(100.00);
            customer.BalanceWithJobsSpecified = true;
            //customer.CreditLimit = new Decimal(100.00);
            //customer.CreditLimitSpecified = true;
            //customer.AcctNum = "AcctNum";
            //customer.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //customer.OverDueBalance = new Decimal(100.00);
            //customer.OverDueBalanceSpecified = true;
            //customer.TotalRevenue = new Decimal(100.00);
            //customer.TotalRevenueSpecified = true;
            //customer.TotalExpense = new Decimal(100.00);
            //customer.TotalExpenseSpecified = true;
            customer.PreferredDeliveryMethod = "Print";
            customer.ResaleNum = "ResaleNum";
            //JobInfo jobInfo = new JobInfo();
            //jobInfo.Status = JobStatusEnum.Pending;
            //jobInfo.StatusSpecified = true;
            //jobInfo.StartDate = DateTime.UtcNow.Date;
            //jobInfo.StartDateSpecified = true;
            //jobInfo.ProjectedEndDate = DateTime.UtcNow.Date;
            //jobInfo.ProjectedEndDateSpecified = true;
            //jobInfo.EndDate = DateTime.UtcNow.Date;
            //jobInfo.EndDateSpecified = true;
            //jobInfo.Description = "Description";
            //jobInfo.JobTypeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //customer.JobInfo = jobInfo;
            //customer.CustomerEx = 

            //customer.Organization = true;
            //customer.OrganizationSpecified = true;
            customer.Title = "Title";
            customer.GivenName = "GivenName";
            customer.MiddleName = "MiddleName";
            customer.FamilyName = "FamilyName";
            customer.Suffix = "Suffix";
            customer.FullyQualifiedName = "Name_" + guid;
            customer.CompanyName = "CompanyName";
            customer.DisplayName = "Name_" + guid;
            customer.PrintOnCheckName = "PrintOnCheckName";
            //customer.UserId = "UserId";
            customer.Active = true;
            customer.ActiveSpecified = true;
            TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType";
            //primaryPhone.CountryCode = "CountryCode";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            customer.PrimaryPhone = primaryPhone;
            TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType";
            //alternatePhone.CountryCode = "CountryCode";
            //alternatePhone.AreaCode = "AreaCode";
            //alternatePhone.ExchangeCode = "ExchangeCode";
            //alternatePhone.Extension = "Extension";
            alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true;
            //alternatePhone.DefaultSpecified = true;
            //alternatePhone.Tag = "Tag";
            customer.AlternatePhone = alternatePhone;
            TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            customer.Mobile = mobile;
            TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            customer.Fax = fax;
            EmailAddress primaryEmailAddr = new EmailAddress();
            primaryEmailAddr.Address = "test@tesing.com";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            customer.PrimaryEmailAddr = primaryEmailAddr;
            WebSiteAddress webAddr = new WebSiteAddress();
            webAddr.URI = "http://uri.com";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            customer.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.EmailAddress;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = new EmailAddress() { Address = "address@domain.com" };

            //otherContactInfoList.Add(otherContactInfo);
            //customer.OtherContactInfo = otherContactInfoList.ToArray();
            //customer.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            return customer;
        }



        internal static Customer UpdateCustomer(ServiceContext context, Customer entity)
        {
            //update the properties of entity
            entity.GivenName = "ChangedName";
            entity.Taxable = true;
            entity.TaxableSpecified = true;
            return entity;
        }


        internal static Customer UpdateCustomerFrance(ServiceContext context, Customer entity)
        {
            //update the properties of entity
            entity.GivenName = "ChangedName";
            entity.ARAccountRef = new ReferenceType()
            {
                Value = "13"
            };
            return entity;
        }

        internal static Customer SparseUpdateCustomer(ServiceContext context, string Id, string syncToken)
        {
            Customer entity = new Customer();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.MiddleName = "Sparse" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static User CreateUser(ServiceContext context)
        {
            User user = new User();
            user.DisplayName = "DisplayName" + Helper.GetGuid().Substring(0, 5);
            user.Title = "Title" + Helper.GetGuid().Substring(0, 5);
            user.GivenName = "GivenName" + Helper.GetGuid().Substring(0, 5);
            user.MiddleName = "MiddleName" + Helper.GetGuid().Substring(0, 5);
            user.FamilyName = "FamilyName" + Helper.GetGuid().Substring(0, 5);
            user.Suffix = "Mr/Ms";

            List<EmailAddress> emailAddrList = new List<EmailAddress>();
            EmailAddress emailAddr = new EmailAddress();
            emailAddr.Address = "Address";
            emailAddr.Default = true;
            emailAddr.DefaultSpecified = true;
            emailAddr.Tag = "Tag";
            emailAddrList.Add(emailAddr);
            user.EmailAddr = emailAddrList.ToArray();

            //List<PhysicalAddress> addrList = new List<PhysicalAddress>();
            //PhysicalAddress addr = new PhysicalAddress();
            //addr.Line1 = "Line1";
            //addr.Line2 = "Line2";
            //addr.Line3 = "Line3";
            //addr.Line4 = "Line4";
            //addr.Line5 = "Line5";
            //addr.City = "City";
            //addr.Country = "Country";
            //addr.CountryCode = "CountryCode";
            //addr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //addr.PostalCode = "PostalCode";
            //addr.PostalCodeSuffix = "PostalCodeSuffix";
            //addr.Lat = "Lat";
            //addr.Long = "Long";
            //addr.Tag = "Tag";
            //addr.Note = "Note";
            //addrList.Add(addr);
            //user.Addr = addrList.ToArray();

            //List<TelephoneNumber> phoneNumberList = new List<TelephoneNumber>();
            //TelephoneNumber phoneNumber = new TelephoneNumber();
            //phoneNumber.DeviceType = "DeviceType";
            //phoneNumber.CountryCode = "CountryCode";
            //phoneNumber.AreaCode = "AreaCode";
            //phoneNumber.ExchangeCode = "ExchangeCode";
            //phoneNumber.Extension = "Extension";
            //phoneNumber.FreeFormNumber = "FreeFormNumber";
            //phoneNumber.Default = true;
            //phoneNumber.DefaultSpecified = true;
            //phoneNumber.Tag = "Tag";
            //phoneNumberList.Add(phoneNumber);
            //user.PhoneNumber = phoneNumberList.ToArray();
            //user.LocaleCountry = "LocaleCountry";
            //user.LocaleLanguage = "LocaleLanguage";
            //user.LocalePostalCode = "LocalePostalCode";
            //user.LocaleTimeZone = "LocaleTimeZone";

            //List<NameValue> nameValueAttrList = new List<NameValue>();
            //NameValue nameValueAttr = new NameValue();
            //nameValueAttr.Name = "Name";
            //nameValueAttr.Value = "Value";
            //nameValueAttrList.Add(nameValueAttr);
            //user.NameValueAttr = nameValueAttrList.ToArray();
            return user;
        }



        internal static User UpdateUser(ServiceContext context, User entity)
        {
            //update the properties of entity

            entity.DisplayName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.Title = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.GivenName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.MiddleName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.FamilyName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.Suffix = "Mr/Ms.";
            return entity;
        }


        internal static User UpdateUserSparse(ServiceContext context, string id, string syncToken)
        {
            User entity = new User();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.DisplayName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.Title = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.GivenName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.MiddleName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.FamilyName = "Updated" + Helper.GetGuid().Substring(0, 5);
            entity.Suffix = "Mr/Ms.";
            return entity;
        }

        internal static Vendor CreateVendor(ServiceContext context)
        {
            Vendor vendor = new Vendor();
            //vendor.ContactName = "ContactName"; //Service is not returning back
            //vendor.AltContactName = "AltContactName"; //Service is not returning back
            //vendor.Notes = "Notes"; //Service is not returning back
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "Line1";
            billAddr.Line2 = "Line2";
            billAddr.Line3 = "Line3";
            billAddr.Line4 = "Line4";
            billAddr.Line5 = "Line5";
            billAddr.City = "City";
            billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode"; //Service is not returning back
            billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix"; //Service is not returning back
            //billAddr.Lat = "12"; //Service returning INVALID
            //billAddr.Long = "12"; //Service returning INVALID
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            vendor.BillAddr = billAddr;
            ////shipAddr is not returned by service
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //    shipAddr.Line1 = "Line1";
            //    shipAddr.Line2 = "Line2";
            //    shipAddr.Line3 = "Line3";
            //    shipAddr.Line4 = "Line4";
            //    shipAddr.Line5 = "Line5";
            //    shipAddr.City = "City";
            //    shipAddr.Country = "Country";
            //    shipAddr.CountryCode = "CountryCode";
            //    shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //    shipAddr.PostalCode = "PostalCode";
            //    shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //    shipAddr.Lat = "12";
            //    shipAddr.Long = "12";
            //    shipAddr.Tag = "Tag";
            //    shipAddr.Note = "Note";
            //vendor.ShipAddr = shipAddr;

            ////otherAddr is not returned by service
            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //    otherAddr.Line1 = "Line1";
            //    otherAddr.Line2 = "Line2";
            //    otherAddr.Line3 = "Line3";
            //    otherAddr.Line4 = "Line4";
            //    otherAddr.Line5 = "Line5";
            //    otherAddr.City = "City";
            //    otherAddr.Country = "Country";
            //    otherAddr.CountryCode = "CountryCode";
            //    otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //    otherAddr.PostalCode = "PostalCode";
            //    //otherAddr.PostalCodeSuffix = "PostalCodeSuffix"; //Service is not returning back
            //    otherAddr.Lat = "Lat";
            //    otherAddr.Long = "Long";
            //    otherAddr.Tag = "12";
            //    otherAddr.Note = "12";
            //otherAddrList.Add(otherAddr);
            //vendor.OtherAddr = otherAddrList.ToArray();
            //vendor.TaxCountry = "TaxCountry"; //Service is not returning back
            vendor.TaxIdentifier = "TaxIdentifier";
            //vendor.BusinessNumber = "BusinessNumber"; //Service is not returning back
            //vendor.ParentRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //vendor.VendorTypeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //Term term = Helper.FindOrAdd<Term>(context, new Term());
            //vendor.TermRef = new ReferenceType() 
            //{ 
            //    name = term.Name,
            //    //type =
            //    Value = term.Id
            //};
            //vendor.PrefillAccountRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            vendor.Balance = new Decimal(100.00);
            vendor.BalanceSpecified = true;
            vendor.OpenBalanceDate = DateTime.UtcNow.Date;
            vendor.OpenBalanceDateSpecified = true;
            //vendor.CreditLimit = new Decimal(100.00); //Service is not returning back
            //vendor.CreditLimitSpecified = true;
            vendor.AcctNum = "AcctNum";
            vendor.Vendor1099 = true;
            vendor.Vendor1099Specified = true;
            //vendor.T4AEligible = true; //Service is not returning back
            //vendor.T4AEligibleSpecified = true;
            //vendor.T5018Eligible = true; //Service is not returning back
            //vendor.T5018EligibleSpecified = true;
            //vendor.CurrencyRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //vendor.VendorEx = 
            //vendor.Organization = true; //Service is not returning back
            //vendor.OrganizationSpecified = true;
            vendor.Title = "Title";
            vendor.GivenName = "GivenName";
            vendor.MiddleName = "MiddleName";
            vendor.FamilyName = "FamilyName";
            vendor.Suffix = "Suffix";
            //vendor.FullyQualifiedName = "FullyQualifiedName"; //Service is not returning back
            vendor.CompanyName = "CompanyName";
            vendor.DisplayName = "DisplayName_" + Helper.GetGuid();
            vendor.PrintOnCheckName = "PrintOnCheckName";
            //vendor.UserId = "UserId"; //Service is not returning back
            vendor.Active = true;
            vendor.ActiveSpecified = true;
            TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType"; //Service is not returning back
            //primaryPhone.CountryCode = "CountryCode"; //Service is not returning back
            //primaryPhone.AreaCode = "AreaCode"; //Service is not returning back
            //primaryPhone.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //primaryPhone.Extension = "Extension"; //Service is not returning back
            primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true; //Service is not returning back
            //primaryPhone.DefaultSpecified = true; //Service is not returning back
            //primaryPhone.Tag = "Tag"; //Service is not returning back
            vendor.PrimaryPhone = primaryPhone;
            TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType"; //Service is not returning back
            //alternatePhone.CountryCode = "CountryCode"; //Service is not returning back
            //alternatePhone.AreaCode = "AreaCode"; //Service is not returning back
            //alternatePhone.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //alternatePhone.Extension = "Extension"; //Service is not returning back
            alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true; //Service is not returning back
            //alternatePhone.DefaultSpecified = true; //Service is not returning back
            //alternatePhone.Tag = "Tag"; //Service is not returning back
            vendor.AlternatePhone = alternatePhone;
            TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType"; //Service is not returning back
            //mobile.CountryCode = "CountryCode"; //Service is not returning back
            //mobile.AreaCode = "AreaCode"; //Service is not returning back
            //mobile.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //mobile.Extension = "Extension"; //Service is not returning back
            mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true; //Service is not returning back
            //mobile.DefaultSpecified = true; //Service is not returning back
            //mobile.Tag = "Tag"; //Service is not returning back
            vendor.Mobile = mobile;
            TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType"; 
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            vendor.Fax = fax;
            EmailAddress primaryEmailAddr = new EmailAddress();
            primaryEmailAddr.Address = "Address@add.com";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            vendor.PrimaryEmailAddr = primaryEmailAddr;
            WebSiteAddress webAddr = new WebSiteAddress();
            webAddr.URI = "http://site.com";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            vendor.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //    otherContactInfo.Type = ContactTypeEnum.EmailAddress;
            //    otherContactInfo.TypeSpecified = true;
            //    otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //vendor.OtherContactInfo = otherContactInfoList.ToArray();
            //vendor.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            return vendor;
        }


        internal static Vendor CreateVendorFrance(ServiceContext context)
        {
            Vendor vendor = new Vendor();
            //vendor.ContactName = "ContactName"; //Service is not returning back
            //vendor.AltContactName = "AltContactName"; //Service is not returning back
            //vendor.Notes = "Notes"; //Service is not returning back
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "Line1";
            billAddr.Line2 = "Line2";
            billAddr.Line3 = "Line3";
            billAddr.Line4 = "Line4";
            billAddr.Line5 = "Line5";
            billAddr.City = "City";
            billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode"; //Service is not returning back
            billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix"; //Service is not returning back
            //billAddr.Lat = "12"; //Service returning INVALID
            //billAddr.Long = "12"; //Service returning INVALID
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            vendor.BillAddr = billAddr;
            ////shipAddr is not returned by service
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //    shipAddr.Line1 = "Line1";
            //    shipAddr.Line2 = "Line2";
            //    shipAddr.Line3 = "Line3";
            //    shipAddr.Line4 = "Line4";
            //    shipAddr.Line5 = "Line5";
            //    shipAddr.City = "City";
            //    shipAddr.Country = "Country";
            //    shipAddr.CountryCode = "CountryCode";
            //    shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //    shipAddr.PostalCode = "PostalCode";
            //    shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //    shipAddr.Lat = "12";
            //    shipAddr.Long = "12";
            //    shipAddr.Tag = "Tag";
            //    shipAddr.Note = "Note";
            //vendor.ShipAddr = shipAddr;

            ////otherAddr is not returned by service
            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //    otherAddr.Line1 = "Line1";
            //    otherAddr.Line2 = "Line2";
            //    otherAddr.Line3 = "Line3";
            //    otherAddr.Line4 = "Line4";
            //    otherAddr.Line5 = "Line5";
            //    otherAddr.City = "City";
            //    otherAddr.Country = "Country";
            //    otherAddr.CountryCode = "CountryCode";
            //    otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //    otherAddr.PostalCode = "PostalCode";
            //    //otherAddr.PostalCodeSuffix = "PostalCodeSuffix"; //Service is not returning back
            //    otherAddr.Lat = "Lat";
            //    otherAddr.Long = "Long";
            //    otherAddr.Tag = "12";
            //    otherAddr.Note = "12";
            //otherAddrList.Add(otherAddr);
            //vendor.OtherAddr = otherAddrList.ToArray();
            //vendor.TaxCountry = "TaxCountry"; //Service is not returning back
            //  vendor.TaxIdentifier = "TaxIdentifier";
            //vendor.BusinessNumber = "BusinessNumber"; //Service is not returning back
            //vendor.ParentRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //vendor.VendorTypeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //Term term = Helper.FindOrAdd<Term>(context, new Term());
            //vendor.TermRef = new ReferenceType() 
            //{ 
            //    name = term.Name,
            //    //type =
            //    Value = term.Id
            //};
            //vendor.PrefillAccountRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //  vendor.Balance = new Decimal(100.00);
            // vendor.BalanceSpecified = true;
            //vendor.OpenBalanceDate = DateTime.UtcNow.Date;
            //vendor.OpenBalanceDateSpecified = true;
            //vendor.CreditLimit = new Decimal(100.00); //Service is not returning back
            //vendor.CreditLimitSpecified = true;
            //vendor.AcctNum = "AcctNum";
            //vendor.Vendor1099 = true;
            //vendor.Vendor1099Specified = true;
            //vendor.T4AEligible = true; //Service is not returning back
            //vendor.T4AEligibleSpecified = true;
            //vendor.T5018Eligible = true; //Service is not returning back
            //vendor.T5018EligibleSpecified = true;
            //vendor.CurrencyRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            //vendor.VendorEx = 
            //vendor.Organization = true; //Service is not returning back
            //vendor.OrganizationSpecified = true;
            vendor.Title = "Title";
            vendor.GivenName = "GivenName";
            vendor.MiddleName = "MiddleName";
            vendor.FamilyName = "FamilyName";
            vendor.Suffix = "Suffix";
            //vendor.FullyQualifiedName = "FullyQualifiedName"; //Service is not returning back
            //vendor.CompanyName = "CompanyName";
            vendor.DisplayName = "DisplayName_" + Helper.GetGuid();
            //vendor.PrintOnCheckName = "PrintOnCheckName";
            //vendor.UserId = "UserId"; //Service is not returning back
            //vendor.Active = true;
            //vendor.ActiveSpecified = true;
            //TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType"; //Service is not returning back
            //primaryPhone.CountryCode = "CountryCode"; //Service is not returning back
            //primaryPhone.AreaCode = "AreaCode"; //Service is not returning back
            //primaryPhone.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //primaryPhone.Extension = "Extension"; //Service is not returning back
            //primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true; //Service is not returning back
            //primaryPhone.DefaultSpecified = true; //Service is not returning back
            //primaryPhone.Tag = "Tag"; //Service is not returning back
            //vendor.PrimaryPhone = primaryPhone;
            //TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType"; //Service is not returning back
            //alternatePhone.CountryCode = "CountryCode"; //Service is not returning back
            //alternatePhone.AreaCode = "AreaCode"; //Service is not returning back
            //alternatePhone.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //alternatePhone.Extension = "Extension"; //Service is not returning back
            //alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true; //Service is not returning back
            //alternatePhone.DefaultSpecified = true; //Service is not returning back
            //alternatePhone.Tag = "Tag"; //Service is not returning back
            //vendor.AlternatePhone = alternatePhone;
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType"; //Service is not returning back
            //mobile.CountryCode = "CountryCode"; //Service is not returning back
            //mobile.AreaCode = "AreaCode"; //Service is not returning back
            //mobile.ExchangeCode = "ExchangeCode"; //Service is not returning back
            //mobile.Extension = "Extension"; //Service is not returning back
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true; //Service is not returning back
            //mobile.DefaultSpecified = true; //Service is not returning back
            //mobile.Tag = "Tag"; //Service is not returning back
            //vendor.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType"; 
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //vendor.Fax = fax;
            //EmailAddress primaryEmailAddr = new EmailAddress();
            //primaryEmailAddr.Address = "Address@add.com";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            //vendor.PrimaryEmailAddr = primaryEmailAddr;
            //WebSiteAddress webAddr = new WebSiteAddress();
            //webAddr.URI = "http://site.com";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            //vendor.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //    otherContactInfo.Type = ContactTypeEnum.EmailAddress;
            //    otherContactInfo.TypeSpecified = true;
            //    otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //vendor.OtherContactInfo = otherContactInfoList.ToArray();
            //vendor.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            return vendor;
        }


        internal static Vendor UpdateVendor(ServiceContext context, Vendor entity)
        {
            entity.Title = "Title updated";
            entity.GivenName = "GivenName updated";
            entity.MiddleName = "MiddleName updated";
            entity.FamilyName = "FamilyName updated";
            entity.Suffix = "Mr.";
            return entity;
        }


        internal static Vendor UpdateVendorFrance(ServiceContext context, Vendor entity)
        {
            entity.Title = "Title updated";
            entity.GivenName = "GivenName updated";
            entity.MiddleName = "MiddleName updated";
            entity.FamilyName = "FamilyName updated";
            entity.Suffix = "Mr.";

            entity.APAccountRef = new ReferenceType()
            {
                Value = "12"
            };

            return entity;
        }



        internal static Vendor SparseUpdateVendor(ServiceContext context, string id, string syncToken)
        {
            Vendor entity = new Vendor();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.GivenName = "sparse" + Helper.GetGuid().Substring(0, 5);
            entity.MiddleName = "sparse" + Helper.GetGuid().Substring(0, 5);
            entity.FamilyName = "sparse" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }



        internal static CustomerType CreateCustomerType(ServiceContext context)
        {
            CustomerType customerType = new CustomerType();
            customerType.Name = "Name" + Helper.GetGuid().Substring(0, 5);

            //customerType.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            customerType.FullyQualifiedName = customerType.Name;
            customerType.Active = true;
            customerType.ActiveSpecified = true;
            return customerType;
        }



        internal static CustomerType UpdateCustomerType(ServiceContext context, CustomerType entity)
        {
            //update the properties of entity
            entity.Name = "Name_updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static CustomerType UpdateCustomerTypeSparse(ServiceContext context, string id, string syncToken)
        {
            CustomerType entity = new CustomerType();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Name_updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static Employee CreateEmployee(ServiceContext context)
        {
            Employee employee = new Employee();
            employee.EmployeeType = EmployeeTypeEnum.Regular.ToString();
            employee.EmployeeNumber = "ENO" + Helper.GetGuid().Substring(0, 6);
            employee.SSN = "111-22-3333";
            //PhysicalAddress primaryAddr = new PhysicalAddress();
            //primaryAddr.Line1 = "Line1";
            //primaryAddr.Line2 = "Line2";
            //primaryAddr.Line3 = "Line3";
            //primaryAddr.Line4 = "Line4";
            //primaryAddr.Line5 = "Line5";
            //primaryAddr.City = "City";
            //primaryAddr.Country = "Country";
            //primaryAddr.CountryCode = "CountryCode";
            //primaryAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //primaryAddr.PostalCode = "PostalCode";
            //primaryAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //primaryAddr.Lat = "Lat";
            //primaryAddr.Long = "Long";
            //primaryAddr.Tag = "Tag";
            //primaryAddr.Note = "Note";
            //employee.PrimaryAddr = primaryAddr;

            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //otherAddr.Line1 = "Line1";
            //otherAddr.Line2 = "Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            //otherAddr.City = "City";
            //otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //otherAddr.PostalCode = "PostalCode";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            //otherAddrList.Add(otherAddr);
            //employee.OtherAddr = otherAddrList.ToArray();
            //employee.BillableTime = true;
            //employee.BillableTimeSpecified = true;
            employee.BirthDate = DateTime.UtcNow.Date;
            employee.BirthDateSpecified = true;
            employee.Gender = gender.Male;


            employee.GenderSpecified = true;
            employee.HiredDate = DateTime.UtcNow.Date;
            employee.HiredDateSpecified = true;
            employee.ReleasedDate = DateTime.UtcNow.Date;
            employee.ReleasedDateSpecified = true;
            employee.UseTimeEntry = TimeEntryUsedForPaychecksEnum.UseTimeEntry;
            //employee.UseTimeEntrySpecified = true;
            //employee.EmployeeEx = 

            employee.Organization = true;
            employee.OrganizationSpecified = true;
            employee.Title = "Title";
            employee.GivenName = "GivenName" + Helper.GetGuid().Substring(0, 8);
            employee.MiddleName = "MiddleName" + Helper.GetGuid().Substring(0, 8);
            employee.FamilyName = "FamilyName" + Helper.GetGuid().Substring(0, 8);
            employee.CompanyName = "CompanyName" + Helper.GetGuid().Substring(0, 8);
            employee.DisplayName = "DisplayName" + Helper.GetGuid().Substring(0, 8);
            employee.PrintOnCheckName = "PrintOnCheckName" + Helper.GetGuid().Substring(0, 8); ;
            employee.UserId = "UserId" + Helper.GetGuid().Substring(0, 8); ;
            employee.Active = true;
            employee.ActiveSpecified = true;
            //TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType";
            //primaryPhone.CountryCode = "CountryCode";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            //primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            //employee.PrimaryPhone = primaryPhone;
            //TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType";
            //alternatePhone.CountryCode = "CountryCode";
            //alternatePhone.AreaCode = "AreaCode";
            //alternatePhone.ExchangeCode = "ExchangeCode";
            //alternatePhone.Extension = "Extension";
            //alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true;
            //alternatePhone.DefaultSpecified = true;
            //alternatePhone.Tag = "Tag";
            //employee.AlternatePhone = alternatePhone;
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            //employee.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //employee.Fax = fax;
            //EmailAddress primaryEmailAddr = new EmailAddress();
            //primaryEmailAddr.Address = "Address";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            //employee.PrimaryEmailAddr = primaryEmailAddr;
            //WebSiteAddress webAddr = new WebSiteAddress();
            //webAddr.URI = "URI";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            //employee.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //employee.OtherContactInfo = otherContactInfoList.ToArray();
            //employee.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            return employee;
        }



        internal static Employee UpdateEmployee(ServiceContext context, Employee entity)
        {
            //update the properties of entity
            entity.GivenName = "updated" + Helper.GetGuid().Substring(0, 8);
            entity.MiddleName = "updated" + Helper.GetGuid().Substring(0, 8);
            entity.FamilyName = "updated" + Helper.GetGuid().Substring(0, 8);
            entity.CompanyName = "updated" + Helper.GetGuid().Substring(0, 8);
            return entity;
        }


        internal static Employee SparseUpdateEmployee(ServiceContext context, string Id, string syncToken)
        {
            Employee entity = new Employee();
            entity.Id = Id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.GivenName = "sparseupdated" + Helper.GetGuid().Substring(0, 8);
            return entity;
        }


        internal static JobType CreateJobType(ServiceContext context)
        {
            JobType jobType = new JobType();
            jobType.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            //jobType.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            jobType.FullyQualifiedName = jobType.Name;
            jobType.Active = true;
            jobType.ActiveSpecified = true;
            return jobType;
        }



        internal static JobType UpdateJobType(ServiceContext context, JobType entity)
        {
            //update the properties of entity
            entity.Name = "updated_name" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static JobType UpdateJobTypeSparse(ServiceContext context, string id, string syncToken)
        {
            JobType entity = new JobType();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "updated_name" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }

        internal static OtherName CreateOtherName(ServiceContext context)
        {
            OtherName otherName = new OtherName();
            otherName.AcctNum = "AcctNum" + Helper.GetGuid().Substring(0, 5);
            PhysicalAddress primaryAddr = new PhysicalAddress();
            primaryAddr.Line1 = "address line1";
            primaryAddr.Line2 = "address Line2";
            //primaryAddr.Line3 = "Line3";
            //primaryAddr.Line4 = "Line4";
            //primaryAddr.Line5 = "Line5";
            primaryAddr.City = "City";
            primaryAddr.Country = "Country";
            //primaryAddr.CountryCode = "CountryCode";
            //primaryAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            primaryAddr.PostalCode = "485065";
            //primaryAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //primaryAddr.Lat = "Lat";
            //primaryAddr.Long = "Long";
            //primaryAddr.Tag = "Tag";
            //primaryAddr.Note = "Note";
            otherName.PrimaryAddr = primaryAddr;

            List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            PhysicalAddress otherAddr = new PhysicalAddress();
            otherAddr.Line1 = "Other address Line1";
            otherAddr.Line2 = "Other address Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            otherAddr.City = "City";
            otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            otherAddr.PostalCode = "7438957";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            otherAddrList.Add(otherAddr);
            //otherName.OtherAddr = otherAddrList.ToArray();
            //otherName.OtherNameEx = 

            otherName.Organization = true;
            otherName.OrganizationSpecified = true;
            otherName.Title = "Title";
            otherName.GivenName = "GivenName" + Helper.GetGuid().Substring(0, 5);
            otherName.MiddleName = "MiddleName" + Helper.GetGuid().Substring(0, 5);
            otherName.FamilyName = "FamilyName" + Helper.GetGuid().Substring(0, 5);
            //otherName.Suffix = "Suffix";
            otherName.FullyQualifiedName = "fully_name" + Helper.GetGuid().Substring(0, 5);
            otherName.CompanyName = "CompanyName";
            otherName.DisplayName = "DisplayName";
            otherName.PrintOnCheckName = "Test";
            //otherName.UserId = "UserId";
            otherName.Active = true;
            otherName.ActiveSpecified = true;
            TelephoneNumber primaryPhone = new TelephoneNumber();
            primaryPhone.DeviceType = "DeviceType";
            primaryPhone.CountryCode = "5245";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            primaryPhone.FreeFormNumber = "23454262";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            primaryPhone.Tag = "Home";
            otherName.PrimaryPhone = primaryPhone;
            //TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType";
            //alternatePhone.CountryCode = "CountryCode";
            //alternatePhone.AreaCode = "AreaCode";
            //alternatePhone.ExchangeCode = "ExchangeCode";
            //alternatePhone.Extension = "Extension";
            //alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true;
            //alternatePhone.DefaultSpecified = true;
            //alternatePhone.Tag = "Tag";
            //otherName.AlternatePhone = alternatePhone;
            TelephoneNumber mobile = new TelephoneNumber();
            mobile.DeviceType = "DeviceType";
            mobile.CountryCode = "6756";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            mobile.FreeFormNumber = "9987567445";
            mobile.Default = true;
            mobile.DefaultSpecified = true;
            mobile.Tag = "Business";
            otherName.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //otherName.Fax = fax;
            EmailAddress primaryEmailAddr = new EmailAddress();
            primaryEmailAddr.Address = "intuit@aditi.com";
            primaryEmailAddr.Default = true;
            primaryEmailAddr.DefaultSpecified = true;
            primaryEmailAddr.Tag = "office";
            otherName.PrimaryEmailAddr = primaryEmailAddr;
            WebSiteAddress webAddr = new WebSiteAddress();
            webAddr.URI = "http://intuit.com";
            webAddr.Default = true;
            webAddr.DefaultSpecified = true;
            webAddr.Tag = "Tag";
            otherName.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //    otherContactInfo.Type = ContactTypeEnum.;
            //    otherContactInfo.TypeSpecified = true;
            //    otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //otherName.OtherContactInfo = otherContactInfoList.ToArray();
            //otherName.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //    name = 
            //    type = 
            //    Value = 
            //};
            return otherName;
        }



        internal static OtherName UpdateOtherName(ServiceContext context, OtherName entity)
        {
            //update the properties of entity
            entity.AcctNum = "AcctNum" + Helper.GetGuid().Substring(0, 5);
            entity.GivenName = "name_updated" + Helper.GetGuid().Substring(0, 5);
            entity.FamilyName = "family_updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }



        internal static OtherName UpdateOtherNameSparse(ServiceContext context, string id, string syncToken)
        {
            OtherName entity = new OtherName();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.AcctNum = "AcctNum" + Helper.GetGuid().Substring(0, 5);
            entity.GivenName = "name_updated" + Helper.GetGuid().Substring(0, 5);
            entity.FamilyName = "family_updated" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static VendorType CreateVendorType(ServiceContext context)
        {
            VendorType vendorType = new VendorType();
            vendorType.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            //vendorType.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            vendorType.FullyQualifiedName = vendorType.Name;

            ModificationMetaData modification = new ModificationMetaData();
            modification.CreateTime = DateTime.UtcNow.AddDays(-20);
            modification.CreateTimeSpecified = true;
            modification.LastUpdatedTime = DateTime.UtcNow;
            modification.LastUpdatedTimeSpecified = true;

            vendorType.MetaData = modification;
            //vendorType.Active = true;
            //vendorType.ActiveSpecified = true;
            return vendorType;
        }



        internal static VendorType UpdateVendorType(ServiceContext context, VendorType entity)
        {
            //update the properties of entity
            entity.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }


        internal static VendorType UpdateVendorTypeSparse(ServiceContext context, string id, string syncToken)
        {
            VendorType entity = new VendorType();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.Name = "Name" + Helper.GetGuid().Substring(0, 5);
            entity.FullyQualifiedName = entity.Name;
            return entity;
        }



        internal static TaxAgency CreateTaxAgency(ServiceContext context)
        {
            TaxAgency taxAgency = new TaxAgency();
            taxAgency.DisplayName = "Name" + Helper.GetGuid().Substring(0, 5);

            //taxAgency.SalesTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.SalesTaxCountry = "SalesTaxCountry";
            //taxAgency.SalesTaxReturnRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.TaxRegistrationNumber = "TaxRegistrationNumber";
            //taxAgency.ReportingPeriod = "ReportingPeriod";
            //taxAgency.TaxTrackedOnPurchases = true;
            //taxAgency.TaxTrackedOnPurchasesSpecified = true;
            //taxAgency.TaxOnPurchasesAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.TaxTrackedOnSales = true;
            //taxAgency.TaxTrackedOnSalesSpecified = true;
            //taxAgency.TaxTrackedOnSalesAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.TaxOnTax = true;
            //taxAgency.TaxOnTaxSpecified = true;
            //taxAgency.TaxAgencyExt = 
            //taxAgency.ContactName = "ContactName";
            //taxAgency.AltContactName = "AltContactName";
            //taxAgency.Notes = "Notes";
            //PhysicalAddress billAddr = new PhysicalAddress();
            //billAddr.Line1 = "Line1";
            //billAddr.Line2 = "Line2";
            //billAddr.Line3 = "Line3";
            //billAddr.Line4 = "Line4";
            //billAddr.Line5 = "Line5";
            //billAddr.City = "City";
            //billAddr.Country = "Country";
            //billAddr.CountryCode = "CountryCode";
            //billAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //billAddr.PostalCode = "PostalCode";
            //billAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //billAddr.Lat = "Lat";
            //billAddr.Long = "Long";
            //billAddr.Tag = "Tag";
            //billAddr.Note = "Note";
            //taxAgency.BillAddr = billAddr;
            //PhysicalAddress shipAddr = new PhysicalAddress();
            //shipAddr.Line1 = "Line1";
            //shipAddr.Line2 = "Line2";
            //shipAddr.Line3 = "Line3";
            //shipAddr.Line4 = "Line4";
            //shipAddr.Line5 = "Line5";
            //shipAddr.City = "City";
            //shipAddr.Country = "Country";
            //shipAddr.CountryCode = "CountryCode";
            //shipAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //shipAddr.PostalCode = "PostalCode";
            //shipAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //shipAddr.Lat = "Lat";
            //shipAddr.Long = "Long";
            //shipAddr.Tag = "Tag";
            //shipAddr.Note = "Note";
            //taxAgency.ShipAddr = shipAddr;

            //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            //PhysicalAddress otherAddr = new PhysicalAddress();
            //otherAddr.Line1 = "Line1";
            //otherAddr.Line2 = "Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            //otherAddr.City = "City";
            //otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            //otherAddr.PostalCode = "PostalCode";
            //otherAddr.PostalCodeSuffix = "PostalCodeSuffix";
            //otherAddr.Lat = "Lat";
            //otherAddr.Long = "Long";
            //otherAddr.Tag = "Tag";
            //otherAddr.Note = "Note";
            //otherAddrList.Add(otherAddr);
            //taxAgency.OtherAddr = otherAddrList.ToArray();
            //taxAgency.TaxCountry = "TaxCountry";
            //taxAgency.TaxIdentifier = "TaxIdentifier";
            //taxAgency.BusinessNumber = "BusinessNumber";
            //taxAgency.ParentRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.VendorTypeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.TermRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.PrefillAccountRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.Balance = new Decimal(100.00);
            //taxAgency.BalanceSpecified = true;
            //taxAgency.OpenBalanceDate = DateTime.UtcNow.Date;
            //taxAgency.OpenBalanceDateSpecified = true;
            //taxAgency.CreditLimit = new Decimal(100.00);
            //taxAgency.CreditLimitSpecified = true;
            //taxAgency.AcctNum = "AcctNum";
            //taxAgency.Vendor1099 = true;
            //taxAgency.Vendor1099Specified = true;
            //taxAgency.T4AEligible = true;
            //taxAgency.T4AEligibleSpecified = true;
            //taxAgency.T5018Eligible = true;
            //taxAgency.T5018EligibleSpecified = true;
            //taxAgency.CurrencyRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            //taxAgency.VendorEx = 
            //taxAgency.Organization = true;
            //taxAgency.OrganizationSpecified = true;
            //taxAgency.Title = "Title";
            //taxAgency.GivenName = "GivenName";
            //taxAgency.MiddleName = "MiddleName";
            //taxAgency.FamilyName = "FamilyName";
            //taxAgency.Suffix = "Suffix";
            //taxAgency.FullyQualifiedName = "FullyQualifiedName";
            //taxAgency.CompanyName = "CompanyName";
            //taxAgency.DisplayName = "DisplayName";
            //taxAgency.PrintOnCheckName = "PrintOnCheckName";
            //taxAgency.UserId = "UserId";
            //taxAgency.Active = true;
            //taxAgency.ActiveSpecified = true;
            //TelephoneNumber primaryPhone = new TelephoneNumber();
            //primaryPhone.DeviceType = "DeviceType";
            //primaryPhone.CountryCode = "CountryCode";
            //primaryPhone.AreaCode = "AreaCode";
            //primaryPhone.ExchangeCode = "ExchangeCode";
            //primaryPhone.Extension = "Extension";
            //primaryPhone.FreeFormNumber = "FreeFormNumber";
            //primaryPhone.Default = true;
            //primaryPhone.DefaultSpecified = true;
            //primaryPhone.Tag = "Tag";
            //taxAgency.PrimaryPhone = primaryPhone;
            //TelephoneNumber alternatePhone = new TelephoneNumber();
            //alternatePhone.DeviceType = "DeviceType";
            //alternatePhone.CountryCode = "CountryCode";
            //alternatePhone.AreaCode = "AreaCode";
            //alternatePhone.ExchangeCode = "ExchangeCode";
            //alternatePhone.Extension = "Extension";
            //alternatePhone.FreeFormNumber = "FreeFormNumber";
            //alternatePhone.Default = true;
            //alternatePhone.DefaultSpecified = true;
            //alternatePhone.Tag = "Tag";
            //taxAgency.AlternatePhone = alternatePhone;
            //TelephoneNumber mobile = new TelephoneNumber();
            //mobile.DeviceType = "DeviceType";
            //mobile.CountryCode = "CountryCode";
            //mobile.AreaCode = "AreaCode";
            //mobile.ExchangeCode = "ExchangeCode";
            //mobile.Extension = "Extension";
            //mobile.FreeFormNumber = "FreeFormNumber";
            //mobile.Default = true;
            //mobile.DefaultSpecified = true;
            //mobile.Tag = "Tag";
            //taxAgency.Mobile = mobile;
            //TelephoneNumber fax = new TelephoneNumber();
            //fax.DeviceType = "DeviceType";
            //fax.CountryCode = "CountryCode";
            //fax.AreaCode = "AreaCode";
            //fax.ExchangeCode = "ExchangeCode";
            //fax.Extension = "Extension";
            //fax.FreeFormNumber = "FreeFormNumber";
            //fax.Default = true;
            //fax.DefaultSpecified = true;
            //fax.Tag = "Tag";
            //taxAgency.Fax = fax;
            //EmailAddress primaryEmailAddr = new EmailAddress();
            //primaryEmailAddr.Address = "Address";
            //primaryEmailAddr.Default = true;
            //primaryEmailAddr.DefaultSpecified = true;
            //primaryEmailAddr.Tag = "Tag";
            //taxAgency.PrimaryEmailAddr = primaryEmailAddr;
            //WebSiteAddress webAddr = new WebSiteAddress();
            //webAddr.URI = "URI";
            //webAddr.Default = true;
            //webAddr.DefaultSpecified = true;
            //webAddr.Tag = "Tag";
            //taxAgency.WebAddr = webAddr;

            //List<ContactInfo> otherContactInfoList = new List<ContactInfo>();
            //ContactInfo otherContactInfo = new ContactInfo();
            //otherContactInfo.Type = ContactTypeEnum.;
            //otherContactInfo.TypeSpecified = true;
            //otherContactInfo.AnyIntuitObject = 
            //otherContactInfoList.Add(otherContactInfo);
            //taxAgency.OtherContactInfo = otherContactInfoList.ToArray();
            //taxAgency.DefaultTaxCodeRef = new ReferenceType() 
            //{ 
            //name = 
            //type = 
            //Value = 
            //};
            return taxAgency;
        }



        internal static TaxAgency UpdateTaxAgency(ServiceContext context, TaxAgency entity)
        {
            //update the properties of entity
            return entity;
        }


        internal static TaxAgency UpdateTaxAgencySparse(ServiceContext context, string id, string syncToken)
        {
            TaxAgency entity = new TaxAgency();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            return entity;
        }


        internal static Status CreateStatus(ServiceContext context)
        {
            Status status = new Status();
            status.RequestId = "RId" + Helper.GetGuid().Substring(0, 3);
            status.BatchId = "BId" + Helper.GetGuid().Substring(0, 3);
            status.ObjectType = "ObjectType";
            status.StateCode = "StateCode";
            status.StateDesc = "StateDesc";
            status.MessageCode = "MessageCode";
            status.MessageDesc = "MessageDesc";
            return status;
        }



        internal static Status UpdateStatus(ServiceContext context, Status entity)
        {
            //update the properties of entity
            entity.StateCode = "UpdatedStateCode";
            entity.StateDesc = "UpdatedStateDesc";
            return entity;
        }



        internal static Status UpdateStatusSparse(ServiceContext context, string id, string syncToken)
        {
            Status entity = new Status();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.StateCode = "SparseUpdatedStateCode";
            entity.StateDesc = "SparseUpdatedStateDesc";
            return entity;
        }



        internal static SyncActivity CreateSyncActivity(ServiceContext context)
        {
            SyncActivity syncActivity = new SyncActivity();
            //syncActivity.LatestUploadDateTime = DateTime.UtcNow.Date;
            //syncActivity.LatestUploadDateTimeSpecified = true;
            //syncActivity.LatestWriteBackDateTime = DateTime.UtcNow.Date;
            //syncActivity.LatestWriteBackDateTimeSpecified = true;
            //syncActivity.SyncType = syncType;
            //SyncType syncType = new SyncType();

            syncActivity.SyncTypeSpecified = true;
            syncActivity.StartSyncTMS = DateTime.UtcNow.Date;
            syncActivity.StartSyncTMSSpecified = true;
            syncActivity.EndSyncTMS = DateTime.UtcNow.Date;
            syncActivity.EndSyncTMSSpecified = true;
            syncActivity.EntityName = "EntityName" + Helper.GetGuid().Substring(0, 5);
            //syncActivity.EntityRowCount = int32;
            //Int32 int32 = new Int32();

            //syncActivity.EntityRowCountSpecified = true;
            return syncActivity;
        }



        internal static SyncActivity UpdateSyncActivity(ServiceContext context, SyncActivity entity)
        {
            //update the properties of entity
            entity.EntityName = "EntityName" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }


        internal static SyncActivity UpdateSyncActivitySparse(ServiceContext context, string id, string syncToken)
        {
            SyncActivity entity = new SyncActivity();
            entity.Id = id;
            entity.SyncToken = syncToken;
            entity.sparse = true;
            entity.sparseSpecified = true;
            entity.EntityName = "EntityName" + Helper.GetGuid().Substring(0, 5);
            return entity;
        }



        #endregion

        #region Verify Helper Methods

        internal static void VerifyNameValue(NameValue expected, NameValue actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Value, actual.Value);
        }



        internal static void VerifyCompany(Company expected, Company actual)
        {

            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.LegalName, actual.LegalName);
            Assert.AreEqual(expected.CompanyAddr.Line1, actual.CompanyAddr.Line1);
            Assert.AreEqual(expected.CompanyAddr.Line2, actual.CompanyAddr.Line2);
            Assert.AreEqual(expected.CompanyAddr.Line3, actual.CompanyAddr.Line3);
            Assert.AreEqual(expected.CompanyAddr.Line4, actual.CompanyAddr.Line4);
            Assert.AreEqual(expected.CompanyAddr.Line5, actual.CompanyAddr.Line5);
            Assert.AreEqual(expected.CompanyAddr.City, actual.CompanyAddr.City);
            Assert.AreEqual(expected.CompanyAddr.Country, actual.CompanyAddr.Country);
            Assert.AreEqual(expected.CompanyAddr.CountryCode, actual.CompanyAddr.CountryCode);
            Assert.AreEqual(expected.CompanyAddr.CountrySubDivisionCode, actual.CompanyAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.CompanyAddr.PostalCode, actual.CompanyAddr.PostalCode);
            Assert.AreEqual(expected.CompanyAddr.PostalCodeSuffix, actual.CompanyAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.CompanyAddr.Lat, actual.CompanyAddr.Lat);
            Assert.AreEqual(expected.CompanyAddr.Long, actual.CompanyAddr.Long);
            Assert.AreEqual(expected.CompanyAddr.Tag, actual.CompanyAddr.Tag);
            Assert.AreEqual(expected.CompanyAddr.Note, actual.CompanyAddr.Note);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Line1, actual.CustomerCommunicationAddr.Line1);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Line2, actual.CustomerCommunicationAddr.Line2);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Line3, actual.CustomerCommunicationAddr.Line3);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Line4, actual.CustomerCommunicationAddr.Line4);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Line5, actual.CustomerCommunicationAddr.Line5);
            Assert.AreEqual(expected.CustomerCommunicationAddr.City, actual.CustomerCommunicationAddr.City);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Country, actual.CustomerCommunicationAddr.Country);
            Assert.AreEqual(expected.CustomerCommunicationAddr.CountryCode, actual.CustomerCommunicationAddr.CountryCode);
            Assert.AreEqual(expected.CustomerCommunicationAddr.CountrySubDivisionCode, actual.CustomerCommunicationAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.CustomerCommunicationAddr.PostalCode, actual.CustomerCommunicationAddr.PostalCode);
            Assert.AreEqual(expected.CustomerCommunicationAddr.PostalCodeSuffix, actual.CustomerCommunicationAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Lat, actual.CustomerCommunicationAddr.Lat);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Long, actual.CustomerCommunicationAddr.Long);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Tag, actual.CustomerCommunicationAddr.Tag);
            Assert.AreEqual(expected.CustomerCommunicationAddr.Note, actual.CustomerCommunicationAddr.Note);
            //Assert.AreEqual(expected.LegalAddr.Line1, actual.LegalAddr.Line1);
            //Assert.AreEqual(expected.LegalAddr.Line2, actual.LegalAddr.Line2);
            //Assert.AreEqual(expected.LegalAddr.Line3, actual.LegalAddr.Line3);
            //Assert.AreEqual(expected.LegalAddr.Line4, actual.LegalAddr.Line4);
            //Assert.AreEqual(expected.LegalAddr.Line5, actual.LegalAddr.Line5);
            //Assert.AreEqual(expected.LegalAddr.City, actual.LegalAddr.City);
            //Assert.AreEqual(expected.LegalAddr.Country, actual.LegalAddr.Country);
            //Assert.AreEqual(expected.LegalAddr.CountryCode, actual.LegalAddr.CountryCode);
            //Assert.AreEqual(expected.LegalAddr.CountrySubDivisionCode, actual.LegalAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.LegalAddr.PostalCode, actual.LegalAddr.PostalCode);
            //Assert.AreEqual(expected.LegalAddr.PostalCodeSuffix, actual.LegalAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.LegalAddr.Lat, actual.LegalAddr.Lat);
            //Assert.AreEqual(expected.LegalAddr.Long, actual.LegalAddr.Long);
            //Assert.AreEqual(expected.LegalAddr.Tag, actual.LegalAddr.Tag);
            //Assert.AreEqual(expected.LegalAddr.Note, actual.LegalAddr.Note);
            //Assert.AreEqual(expected.CompanyEmailAddr.Address, actual.CompanyEmailAddr.Address);
            //Assert.AreEqual(expected.CompanyEmailAddr.Default, actual.CompanyEmailAddr.Default);
            //Assert.AreEqual(expected.CompanyEmailAddr.DefaultSpecified, actual.CompanyEmailAddr.DefaultSpecified);
            //Assert.AreEqual(expected.CompanyEmailAddr.Tag, actual.CompanyEmailAddr.Tag);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Address, actual.CustomerCommunicationEmailAddr.Address);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Default, actual.CustomerCommunicationEmailAddr.Default);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.DefaultSpecified, actual.CustomerCommunicationEmailAddr.DefaultSpecified);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Tag, actual.CustomerCommunicationEmailAddr.Tag);
            //Assert.AreEqual(expected.CompanyURL.URI, actual.CompanyURL.URI);
            //Assert.AreEqual(expected.CompanyURL.Default, actual.CompanyURL.Default);
            //Assert.AreEqual(expected.CompanyURL.DefaultSpecified, actual.CompanyURL.DefaultSpecified);
            //Assert.AreEqual(expected.CompanyURL.Tag, actual.CompanyURL.Tag);
            //Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            //Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            //Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            //Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            //Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            //Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            //Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            //Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            //Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //Assert.AreEqual(expected.CompanyFileName, actual.CompanyFileName);
            //Assert.AreEqual(expected.FlavorStratum, actual.FlavorStratum);
            //Assert.AreEqual(expected.SampleFile, actual.SampleFile);
            //Assert.AreEqual(expected.SampleFileSpecified, actual.SampleFileSpecified);
            //Assert.AreEqual(expected.CompanyUserId, actual.CompanyUserId);
            //Assert.AreEqual(expected.CompanyUserAdminEmail, actual.CompanyUserAdminEmail);
            //Assert.AreEqual(expected.CompanyStartDate, actual.CompanyStartDate);
            //Assert.AreEqual(expected.CompanyStartDateSpecified, actual.CompanyStartDateSpecified);
            //Assert.AreEqual(expected.EmployerId, actual.EmployerId);
            //Assert.AreEqual(expected.FiscalYearStartMonthSpecified, actual.FiscalYearStartMonthSpecified);
            //Assert.AreEqual(expected.TaxYearStartMonthSpecified, actual.TaxYearStartMonthSpecified);
            //Assert.AreEqual(expected.QBVersion, actual.QBVersion);
            //Assert.AreEqual(expected.Country, actual.Country);
            //    Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //    Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //    Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //    Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //    Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //    Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //    Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //    Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //    Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //    Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //    Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //    Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            //    Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            //    Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            //    Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            //    Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            //    Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            //    Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            //    Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            //    Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            //    Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            //    Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            //    Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            //    Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            //    Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            //    Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            //    Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            //    Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            //    Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            //    Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            //    Assert.AreEqual(expected.Email.Address, actual.Email.Address);
            //    Assert.AreEqual(expected.Email.Default, actual.Email.Default);
            //    Assert.AreEqual(expected.Email.DefaultSpecified, actual.Email.DefaultSpecified);
            //    Assert.AreEqual(expected.Email.Tag, actual.Email.Tag);
            //    Assert.AreEqual(expected.WebSite.URI, actual.WebSite.URI);
            //    Assert.AreEqual(expected.WebSite.Default, actual.WebSite.Default);
            //    Assert.AreEqual(expected.WebSite.DefaultSpecified, actual.WebSite.DefaultSpecified);
            //    Assert.AreEqual(expected.WebSite.Tag, actual.WebSite.Tag);
            //Assert.AreEqual(expected.LastImportedTime, actual.LastImportedTime);
            //Assert.AreEqual(expected.LastImportedTimeSpecified, actual.LastImportedTimeSpecified);
            //Assert.AreEqual(expected.SupportedLanguages, actual.SupportedLanguages);
            //Assert.AreEqual(expected.DefaultTimeZone, actual.DefaultTimeZone);
            //Assert.AreEqual(expected.MultiByteCharsSupported, actual.MultiByteCharsSupported);
            //Assert.AreEqual(expected.MultiByteCharsSupportedSpecified, actual.MultiByteCharsSupportedSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.NameValue, actual.NameValue));
            //    Assert.AreEqual(expected.CompanyInfoEx.Any, actual.CompanyInfoEx.Any);

        }

        internal static void VerifyCompanySparse(Company expected, Company actual)
        {
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.LegalName, actual.LegalName);
        }

        internal static void VerifyCompanyInfo(CompanyInfo expected, CompanyInfo actual)
        {
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.LegalName, actual.LegalName);
            //Assert.AreEqual(expected.CompanyAddr.Line1, actual.CompanyAddr.Line1);
            //Assert.AreEqual(expected.CompanyAddr.Line2, actual.CompanyAddr.Line2);
            //Assert.AreEqual(expected.CompanyAddr.Line3, actual.CompanyAddr.Line3);
            //Assert.AreEqual(expected.CompanyAddr.Line4, actual.CompanyAddr.Line4);
            //Assert.AreEqual(expected.CompanyAddr.Line5, actual.CompanyAddr.Line5);
            //Assert.AreEqual(expected.CompanyAddr.City, actual.CompanyAddr.City);
            //Assert.AreEqual(expected.CompanyAddr.Country, actual.CompanyAddr.Country);
            //Assert.AreEqual(expected.CompanyAddr.CountryCode, actual.CompanyAddr.CountryCode);
            //Assert.AreEqual(expected.CompanyAddr.CountrySubDivisionCode, actual.CompanyAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.CompanyAddr.PostalCode, actual.CompanyAddr.PostalCode);
            //Assert.AreEqual(expected.CompanyAddr.PostalCodeSuffix, actual.CompanyAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.CompanyAddr.Lat, actual.CompanyAddr.Lat);
            //Assert.AreEqual(expected.CompanyAddr.Long, actual.CompanyAddr.Long);
            //Assert.AreEqual(expected.CompanyAddr.Tag, actual.CompanyAddr.Tag);
            //Assert.AreEqual(expected.CompanyAddr.Note, actual.CompanyAddr.Note);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Line1, actual.CustomerCommunicationAddr.Line1);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Line2, actual.CustomerCommunicationAddr.Line2);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Line3, actual.CustomerCommunicationAddr.Line3);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Line4, actual.CustomerCommunicationAddr.Line4);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Line5, actual.CustomerCommunicationAddr.Line5);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.City, actual.CustomerCommunicationAddr.City);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Country, actual.CustomerCommunicationAddr.Country);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.CountryCode, actual.CustomerCommunicationAddr.CountryCode);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.CountrySubDivisionCode, actual.CustomerCommunicationAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.PostalCode, actual.CustomerCommunicationAddr.PostalCode);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.PostalCodeSuffix, actual.CustomerCommunicationAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Lat, actual.CustomerCommunicationAddr.Lat);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Long, actual.CustomerCommunicationAddr.Long);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Tag, actual.CustomerCommunicationAddr.Tag);
            //Assert.AreEqual(expected.CustomerCommunicationAddr.Note, actual.CustomerCommunicationAddr.Note);
            //Assert.AreEqual(expected.LegalAddr.Line1, actual.LegalAddr.Line1);
            //Assert.AreEqual(expected.LegalAddr.Line2, actual.LegalAddr.Line2);
            //Assert.AreEqual(expected.LegalAddr.Line3, actual.LegalAddr.Line3);
            //Assert.AreEqual(expected.LegalAddr.Line4, actual.LegalAddr.Line4);
            //Assert.AreEqual(expected.LegalAddr.Line5, actual.LegalAddr.Line5);
            //Assert.AreEqual(expected.LegalAddr.City, actual.LegalAddr.City);
            //Assert.AreEqual(expected.LegalAddr.Country, actual.LegalAddr.Country);
            //Assert.AreEqual(expected.LegalAddr.CountryCode, actual.LegalAddr.CountryCode);
            //Assert.AreEqual(expected.LegalAddr.CountrySubDivisionCode, actual.LegalAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.LegalAddr.PostalCode, actual.LegalAddr.PostalCode);
            //Assert.AreEqual(expected.LegalAddr.PostalCodeSuffix, actual.LegalAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.LegalAddr.Lat, actual.LegalAddr.Lat);
            //Assert.AreEqual(expected.LegalAddr.Long, actual.LegalAddr.Long);
            //Assert.AreEqual(expected.LegalAddr.Tag, actual.LegalAddr.Tag);
            //Assert.AreEqual(expected.LegalAddr.Note, actual.LegalAddr.Note);
            //Assert.AreEqual(expected.CompanyEmailAddr.Address, actual.CompanyEmailAddr.Address);
            //Assert.AreEqual(expected.CompanyEmailAddr.Default, actual.CompanyEmailAddr.Default);
            //Assert.AreEqual(expected.CompanyEmailAddr.DefaultSpecified, actual.CompanyEmailAddr.DefaultSpecified);
            //Assert.AreEqual(expected.CompanyEmailAddr.Tag, actual.CompanyEmailAddr.Tag);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Address, actual.CustomerCommunicationEmailAddr.Address);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Default, actual.CustomerCommunicationEmailAddr.Default);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.DefaultSpecified, actual.CustomerCommunicationEmailAddr.DefaultSpecified);
            //Assert.AreEqual(expected.CustomerCommunicationEmailAddr.Tag, actual.CustomerCommunicationEmailAddr.Tag);
            //Assert.AreEqual(expected.CompanyURL.URI, actual.CompanyURL.URI);
            //Assert.AreEqual(expected.CompanyURL.Default, actual.CompanyURL.Default);
            //Assert.AreEqual(expected.CompanyURL.DefaultSpecified, actual.CompanyURL.DefaultSpecified);
            //Assert.AreEqual(expected.CompanyURL.Tag, actual.CompanyURL.Tag);
            Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            Assert.AreEqual(expected.CompanyFileName, actual.CompanyFileName);
            Assert.AreEqual(expected.FlavorStratum, actual.FlavorStratum);
            Assert.AreEqual(expected.SampleFile, actual.SampleFile);
            Assert.AreEqual(expected.SampleFileSpecified, actual.SampleFileSpecified);
            Assert.AreEqual(expected.CompanyUserId, actual.CompanyUserId);
            Assert.AreEqual(expected.CompanyUserAdminEmail, actual.CompanyUserAdminEmail);
            Assert.AreEqual(expected.CompanyStartDate, actual.CompanyStartDate);
            Assert.AreEqual(expected.CompanyStartDateSpecified, actual.CompanyStartDateSpecified);
            Assert.AreEqual(expected.EmployerId, actual.EmployerId);
            Assert.AreEqual(expected.FiscalYearStartMonthSpecified, actual.FiscalYearStartMonthSpecified);
            Assert.AreEqual(expected.TaxYearStartMonthSpecified, actual.TaxYearStartMonthSpecified);
            Assert.AreEqual(expected.QBVersion, actual.QBVersion);
            Assert.AreEqual(expected.Country, actual.Country);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            //Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            //Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            //Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            //Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            //Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            //Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            //Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            //Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            //Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            //Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            //Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            //Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            //Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            //Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            //Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            //Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            //Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            //Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            Assert.AreEqual(expected.Email.Address, actual.Email.Address);
            Assert.AreEqual(expected.Email.Default, actual.Email.Default);
            Assert.AreEqual(expected.Email.DefaultSpecified, actual.Email.DefaultSpecified);
            Assert.AreEqual(expected.Email.Tag, actual.Email.Tag);
            Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            Assert.AreEqual(expected.LastImportedTime, actual.LastImportedTime);
            Assert.AreEqual(expected.LastImportedTimeSpecified, actual.LastImportedTimeSpecified);
            Assert.AreEqual(expected.SupportedLanguages, actual.SupportedLanguages);
            Assert.AreEqual(expected.DefaultTimeZone, actual.DefaultTimeZone);
            //Assert.AreEqual(expected.MultiByteCharsSupported, actual.MultiByteCharsSupported);
            //Assert.AreEqual(expected.MultiByteCharsSupportedSpecified, actual.MultiByteCharsSupportedSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.NameValue, actual.NameValue));
            //Assert.AreEqual(expected.CompanyInfoEx.Any, actual.CompanyInfoEx.Any);
        }


        internal static void VerifyCompanyInfoSparseUpdate(CompanyInfo expected, CompanyInfo actual)
        {
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.LegalName, actual.LegalName);
        }



        internal static void VerifyTransaction(Transaction expected, Transaction actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyTransactionSparse(Transaction expected, Transaction actual)
        {
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
        }

        internal static void VerifySalesTransaction(SalesTransaction expected, SalesTransaction actual)
        {
            Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            Assert.AreEqual(expected.PONumber, actual.PONumber);
            Assert.AreEqual(expected.FOB, actual.FOB);
            Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifySalesTransactionSparse(SalesTransaction expected, SalesTransaction actual)
        {
            Assert.AreEqual(expected.Balance, actual.Balance);
        }

        internal static void VerifyInvoice(Invoice expected, Invoice actual)
        {
            Assert.AreEqual(expected.Deposit, actual.Deposit);
            Assert.AreEqual(expected.DepositSpecified, actual.DepositSpecified);
            Assert.AreEqual(expected.AllowIPNPayment, actual.AllowIPNPayment);
            Assert.AreEqual(expected.AllowIPNPaymentSpecified, actual.AllowIPNPaymentSpecified);
            //Assert.AreEqual(expected.InvoiceEx.Any, actual.InvoiceEx.Any);
            Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            //Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            Assert.AreEqual(expected.PONumber, actual.PONumber);
            Assert.AreEqual(expected.FOB, actual.FOB);
            //Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            //Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified); //Fails for global realms
            //Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject); //IgnoreReason:
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }



        internal static void VerifyInvoiceSparseUpdate(Invoice expected, Invoice actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
        }



        internal static void VerifySalesReceipt(SalesReceipt expected, SalesReceipt actual)
        {
            //Assert.AreEqual(expected.SalesReceiptEx.Any, actual.SalesReceiptEx.Any);
            //Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            //Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            //Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            //Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            //Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            //Assert.AreEqual(expected.DueDate, actual.DueDate);
            //Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.PONumber, actual.PONumber);
            //Assert.AreEqual(expected.FOB, actual.FOB);
            //    Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //    Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //    Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            //Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            //Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            //Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            //Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.Balance, actual.Balance);
            //Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            //Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }



        internal static void VerifySalesReceiptSparseUpdate(SalesReceipt expected, SalesReceipt actual)
        {
            Assert.AreEqual(expected.PrintStatus, actual.PrintStatus);
        }



        internal static void VerifyEstimate(Estimate expected, Estimate actual)
        {
            Assert.AreEqual(expected.ExpirationDate, actual.ExpirationDate);
            Assert.AreEqual(expected.ExpirationDateSpecified, actual.ExpirationDateSpecified);
            Assert.AreEqual(expected.AcceptedBy, actual.AcceptedBy);
            Assert.AreEqual(expected.AcceptedDate, actual.AcceptedDate);
            Assert.AreEqual(expected.AcceptedDateSpecified, actual.AcceptedDateSpecified);
            //Assert.AreEqual(expected.EstimateEx.Any, actual.EstimateEx.Any);
            Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            //Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            //Assert.AreEqual(expected.DueDate, actual.DueDate);
            //Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.PONumber, actual.PONumber);
            //Assert.AreEqual(expected.FOB, actual.FOB);
            //Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            //Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            //Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            //Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            //Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            //Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            //Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            //Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.Balance, actual.Balance);
            //Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            //Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            //Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            //Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyEstimateSparseUpdate(Estimate expected, Estimate actual)
        {
            Assert.AreEqual(expected.ExpirationDate, actual.ExpirationDate);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
        }



        internal static void VerifyExchangeRate(ExchangeRate expected, ExchangeRate actual)
        {
            Assert.AreEqual(expected.SourceCurrencyCode, actual.SourceCurrencyCode);
            Assert.AreEqual(expected.TargetCurrencyCode, actual.TargetCurrencyCode);
            Assert.AreEqual(expected.Rate, actual.Rate);
            Assert.AreEqual(expected.RateSpecified, actual.RateSpecified);
            Assert.AreEqual(expected.AsOfDate, actual.AsOfDate);
            Assert.AreEqual(expected.AsOfDateSpecified, actual.AsOfDateSpecified);
        }


        internal static void VerifyExchangeRateSparse(ExchangeRate expected, ExchangeRate actual)
        {
            Assert.AreEqual(expected.SourceCurrencyCode, actual.SourceCurrencyCode);
            Assert.AreEqual(expected.TargetCurrencyCode, actual.TargetCurrencyCode);
            Assert.AreEqual(expected.Rate, actual.Rate);
            Assert.AreEqual(expected.RateSpecified, actual.RateSpecified);
            Assert.AreEqual(expected.AsOfDate, actual.AsOfDate);
            Assert.AreEqual(expected.AsOfDateSpecified, actual.AsOfDateSpecified);
        }


        internal static void VerifyFixedAsset(FixedAsset expected, FixedAsset actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.AcquiredAsSpecified, actual.AcquiredAsSpecified);
            Assert.AreEqual(expected.PurchaseDesc, actual.PurchaseDesc);
            Assert.AreEqual(expected.PurchaseDate, actual.PurchaseDate);
            Assert.AreEqual(expected.PurchaseDateSpecified, actual.PurchaseDateSpecified);
            Assert.AreEqual(expected.PurchaseCost, actual.PurchaseCost);
            Assert.AreEqual(expected.PurchaseCostSpecified, actual.PurchaseCostSpecified);
            //Assert.AreEqual(expected.Vendor, actual.Vendor);
            //Assert.AreEqual(expected.AssetAccountRef.name, actual.AssetAccountRef.name);
            //Assert.AreEqual(expected.AssetAccountRef.type, actual.AssetAccountRef.type);
            //Assert.AreEqual(expected.AssetAccountRef.Value, actual.AssetAccountRef.Value);
            Assert.AreEqual(expected.SalesDesc, actual.SalesDesc);
            Assert.AreEqual(expected.SalesDate, actual.SalesDate);
            Assert.AreEqual(expected.SalesDateSpecified, actual.SalesDateSpecified);
            Assert.AreEqual(expected.SalesPrice, actual.SalesPrice);
            Assert.AreEqual(expected.SalesPriceSpecified, actual.SalesPriceSpecified);
            Assert.AreEqual(expected.SalesExpense, actual.SalesExpense);
            Assert.AreEqual(expected.SalesExpenseSpecified, actual.SalesExpenseSpecified);
            Assert.AreEqual(expected.Location, actual.Location);
            Assert.AreEqual(expected.PONumber, actual.PONumber);
            Assert.AreEqual(expected.SerialNumber, actual.SerialNumber);
            Assert.AreEqual(expected.WarrantyExpDate, actual.WarrantyExpDate);
            Assert.AreEqual(expected.WarrantyExpDateSpecified, actual.WarrantyExpDateSpecified);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.AssetNum, actual.AssetNum);
            Assert.AreEqual(expected.AssetNumSpecified, actual.AssetNumSpecified);
            Assert.AreEqual(expected.CostBasis, actual.CostBasis);
            Assert.AreEqual(expected.CostBasisSpecified, actual.CostBasisSpecified);
            Assert.AreEqual(expected.Depreciation, actual.Depreciation);
            Assert.AreEqual(expected.DepreciationSpecified, actual.DepreciationSpecified);
            Assert.AreEqual(expected.BookValue, actual.BookValue);
            Assert.AreEqual(expected.BookValueSpecified, actual.BookValueSpecified);
            //Assert.AreEqual(expected.FixedAssetEx.Any, actual.FixedAssetEx.Any);
        }


        internal static void VerifyFixedAssetSparse(FixedAsset expected, FixedAsset actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.SalesDesc, actual.SalesDesc);
        }

        internal static void VerifyTaxCode(TaxCode expected, TaxCode actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.Taxable, actual.Taxable);
            //Assert.AreEqual(expected.TaxableSpecified, actual.TaxableSpecified);
            Assert.AreEqual(expected.TaxGroup, actual.TaxGroup);
            //Assert.AreEqual(expected.TaxGroupSpecified, actual.TaxGroupSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.SalesTaxRateList.TaxRateDetail, actual.SalesTaxRateList.TaxRateDetail));
            //Assert.AreEqual(expected.SalesTaxRateList.originatingGroupId, actual.SalesTaxRateList.originatingGroupId);
            //Assert.IsTrue(Helper.CheckEqual(expected.PurchaseTaxRateList.TaxRateDetail, actual.PurchaseTaxRateList.TaxRateDetail));
            //Assert.AreEqual(expected.PurchaseTaxRateList.originatingGroupId, actual.PurchaseTaxRateList.originatingGroupId);
            //Assert.IsTrue(Helper.CheckEqual(expected.AdjustmentTaxRateList.TaxRateDetail, actual.AdjustmentTaxRateList.TaxRateDetail));
            //Assert.AreEqual(expected.AdjustmentTaxRateList.originatingGroupId, actual.AdjustmentTaxRateList.originatingGroupId);
            //Assert.AreEqual(expected.TaxCodeEx.Any, actual.TaxCodeEx.Any);
        }


        internal static void VerifyTaxCodeSparseUpdate(TaxCode expected, TaxCode actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        internal static void VerifyTaxRate(TaxRate expected, TaxRate actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.RateValue, actual.RateValue);
            //Assert.AreEqual(expected.RateValueSpecified, actual.RateValueSpecified);
            //    Assert.AreEqual(expected.AgencyRef.name, actual.AgencyRef.name);
            //    Assert.AreEqual(expected.AgencyRef.type, actual.AgencyRef.type);
            //    Assert.AreEqual(expected.AgencyRef.Value, actual.AgencyRef.Value);
            //    Assert.AreEqual(expected.TaxReturnLineRef.name, actual.TaxReturnLineRef.name);
            //    Assert.AreEqual(expected.TaxReturnLineRef.type, actual.TaxReturnLineRef.type);
            //    Assert.AreEqual(expected.TaxReturnLineRef.Value, actual.TaxReturnLineRef.Value);
            Assert.IsTrue(Helper.CheckEqual(expected.EffectiveTaxRate, actual.EffectiveTaxRate));
            Assert.AreEqual(expected.SpecialTaxTypeSpecified, actual.SpecialTaxTypeSpecified);
            Assert.AreEqual(expected.DisplayTypeSpecified, actual.DisplayTypeSpecified);
            //Assert.AreEqual(expected.TaxRateEx.Any, actual.TaxRateEx.Any);
        }


        internal static void VerifyTaxRateSparseUpdate(TaxRate expected, TaxRate actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }


        internal static void VerifyAccountSparseUpdate(Account expected, Account actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
        }




        internal static void VerifyAccount(Account expected, Account actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.SubAccount, actual.SubAccount);
            //Assert.AreEqual(expected.SubAccountSpecified, actual.SubAccountSpecified);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.ClassificationSpecified, actual.ClassificationSpecified);
            Assert.AreEqual(expected.AccountTypeSpecified, actual.AccountTypeSpecified);
            //Assert.AreEqual(expected.AccountSubType, actual.AccountSubType);
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            Assert.AreEqual(expected.BankNum, actual.BankNum);
            //Assert.AreEqual(expected.OpeningBalance, actual.OpeningBalance);
            //Assert.AreEqual(expected.OpeningBalanceSpecified, actual.OpeningBalanceSpecified);
            //Assert.AreEqual(expected.OpeningBalanceDate, actual.OpeningBalanceDate);
            //Assert.AreEqual(expected.OpeningBalanceDateSpecified, actual.OpeningBalanceDateSpecified);
            //Assert.AreEqual(expected.CurrentBalance, actual.CurrentBalance);
            //Assert.AreEqual(expected.CurrentBalanceSpecified, actual.CurrentBalanceSpecified);
            //Assert.AreEqual(expected.CurrentBalanceWithSubAccounts, actual.CurrentBalanceWithSubAccounts);
            //Assert.AreEqual(expected.CurrentBalanceWithSubAccountsSpecified, actual.CurrentBalanceWithSubAccountsSpecified);
            Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            Assert.AreEqual(expected.TaxAccount, actual.TaxAccount);
            Assert.AreEqual(expected.TaxAccountSpecified, actual.TaxAccountSpecified);
            //Assert.AreEqual(expected.TaxCodeRef.name, actual.TaxCodeRef.name);
            //Assert.AreEqual(expected.TaxCodeRef.type, actual.TaxCodeRef.type);
            //Assert.AreEqual(expected.TaxCodeRef.Value, actual.TaxCodeRef.Value);
            //Assert.AreEqual(expected.AccountEx.Any, actual.AccountEx.Any);
        }

        // France
        internal static void VerifyAccountFrance(Account expected, Account actual)
        {

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.AccountAlias, actual.AccountAlias);
            Assert.AreEqual(expected.TxnLocationType, actual.TxnLocationType);
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.ClassificationSpecified, actual.ClassificationSpecified);
            Assert.AreEqual(expected.AccountTypeSpecified, actual.AccountTypeSpecified);
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);

        }


        internal static void VerifyPurchase(Purchase expected, Purchase actual)
        {
            Assert.AreEqual(expected.AccountRef.name, actual.AccountRef.name);
            Assert.AreEqual(expected.AccountRef.type, actual.AccountRef.type);
            Assert.AreEqual(expected.AccountRef.Value, actual.AccountRef.Value);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            Assert.AreEqual(expected.EntityRef.name, actual.EntityRef.name);
            Assert.AreEqual(expected.EntityRef.type, actual.EntityRef.type);
            Assert.AreEqual(expected.EntityRef.Value, actual.EntityRef.Value);
            //Assert.AreEqual(expected.Credit, actual.Credit);
            //Assert.AreEqual(expected.CreditSpecified, actual.CreditSpecified);
            //Assert.AreEqual(expected.RemitToAddr.Line1, actual.RemitToAddr.Line1);
            //Assert.AreEqual(expected.RemitToAddr.Line2, actual.RemitToAddr.Line2);
            //Assert.AreEqual(expected.RemitToAddr.Line3, actual.RemitToAddr.Line3);
            //Assert.AreEqual(expected.RemitToAddr.Line4, actual.RemitToAddr.Line4);
            //Assert.AreEqual(expected.RemitToAddr.Line5, actual.RemitToAddr.Line5);
            //Assert.AreEqual(expected.RemitToAddr.City, actual.RemitToAddr.City);
            //Assert.AreEqual(expected.RemitToAddr.Country, actual.RemitToAddr.Country);
            //Assert.AreEqual(expected.RemitToAddr.CountryCode, actual.RemitToAddr.CountryCode);
            //Assert.AreEqual(expected.RemitToAddr.CountrySubDivisionCode, actual.RemitToAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.RemitToAddr.PostalCode, actual.RemitToAddr.PostalCode);
            //Assert.AreEqual(expected.RemitToAddr.PostalCodeSuffix, actual.RemitToAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.RemitToAddr.Lat, actual.RemitToAddr.Lat);
            //Assert.AreEqual(expected.RemitToAddr.Long, actual.RemitToAddr.Long);
            //Assert.AreEqual(expected.RemitToAddr.Tag, actual.RemitToAddr.Tag);
            //Assert.AreEqual(expected.RemitToAddr.Note, actual.RemitToAddr.Note);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            Assert.AreEqual(expected.TxnId, actual.TxnId);
            Assert.AreEqual(expected.TxnNum, actual.TxnNum);
            Assert.AreEqual(expected.Memo, actual.Memo);
            //Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            //Assert.AreEqual(expected.PurchaseEx.Any, actual.PurchaseEx.Any);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }



        internal static void VerifyPurchaseSparseUpdate(Purchase expected, Purchase actual)
        {
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
        }



        internal static void VerifyPurchaseByVendor(PurchaseByVendor expected, PurchaseByVendor actual)
        {
            Assert.AreEqual(expected.VendorRef.name, actual.VendorRef.name);
            Assert.AreEqual(expected.VendorRef.type, actual.VendorRef.type);
            Assert.AreEqual(expected.VendorRef.Value, actual.VendorRef.Value);
            Assert.AreEqual(expected.APAccountRef.name, actual.APAccountRef.name);
            Assert.AreEqual(expected.APAccountRef.type, actual.APAccountRef.type);
            Assert.AreEqual(expected.APAccountRef.Value, actual.APAccountRef.Value);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            Assert.AreEqual(expected.ReplyEmail.Address, actual.ReplyEmail.Address);
            Assert.AreEqual(expected.ReplyEmail.Default, actual.ReplyEmail.Default);
            Assert.AreEqual(expected.ReplyEmail.DefaultSpecified, actual.ReplyEmail.DefaultSpecified);
            Assert.AreEqual(expected.ReplyEmail.Tag, actual.ReplyEmail.Tag);
            Assert.AreEqual(expected.Memo, actual.Memo);
            Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyPurchaseByVendorSparse(PurchaseByVendor expected, PurchaseByVendor actual)
        {
            Assert.AreEqual(expected.Memo, actual.Memo);
        }


        internal static void VerifyBill(Bill expected, Bill actual)
        {
            //Assert.AreEqual(expected.PayerRef.name, actual.PayerRef.name);
            //Assert.AreEqual(expected.PayerRef.type, actual.PayerRef.type);
            //Assert.AreEqual(expected.PayerRef.Value, actual.PayerRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //Assert.AreEqual(expected.RemitToAddr.Line1, actual.RemitToAddr.Line1);
            //Assert.AreEqual(expected.RemitToAddr.Line2, actual.RemitToAddr.Line2);
            //Assert.AreEqual(expected.RemitToAddr.Line3, actual.RemitToAddr.Line3);
            //Assert.AreEqual(expected.RemitToAddr.Line4, actual.RemitToAddr.Line4);
            //Assert.AreEqual(expected.RemitToAddr.Line5, actual.RemitToAddr.Line5);
            //Assert.AreEqual(expected.RemitToAddr.City, actual.RemitToAddr.City);
            //Assert.AreEqual(expected.RemitToAddr.Country, actual.RemitToAddr.Country);
            //Assert.AreEqual(expected.RemitToAddr.CountryCode, actual.RemitToAddr.CountryCode);
            //Assert.AreEqual(expected.RemitToAddr.CountrySubDivisionCode, actual.RemitToAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.RemitToAddr.PostalCode, actual.RemitToAddr.PostalCode);
            //Assert.AreEqual(expected.RemitToAddr.PostalCodeSuffix, actual.RemitToAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.RemitToAddr.Lat, actual.RemitToAddr.Lat);
            //Assert.AreEqual(expected.RemitToAddr.Long, actual.RemitToAddr.Long);
            //Assert.AreEqual(expected.RemitToAddr.Tag, actual.RemitToAddr.Tag);
            //Assert.AreEqual(expected.RemitToAddr.Note, actual.RemitToAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.BillEx.Any, actual.BillEx.Any);
            Assert.AreEqual(expected.VendorRef.name, actual.VendorRef.name);
            Assert.AreEqual(expected.VendorRef.type, actual.VendorRef.type);
            Assert.AreEqual(expected.VendorRef.Value, actual.VendorRef.Value);
            Assert.AreEqual(expected.APAccountRef.name, actual.APAccountRef.name);
            Assert.AreEqual(expected.APAccountRef.type, actual.APAccountRef.type);
            Assert.AreEqual(expected.APAccountRef.Value, actual.APAccountRef.Value);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ReplyEmail.Address, actual.ReplyEmail.Address);
            //Assert.AreEqual(expected.ReplyEmail.Default, actual.ReplyEmail.Default);
            //Assert.AreEqual(expected.ReplyEmail.DefaultSpecified, actual.ReplyEmail.DefaultSpecified);
            //Assert.AreEqual(expected.ReplyEmail.Tag, actual.ReplyEmail.Tag);
            Assert.AreEqual(expected.Memo, actual.Memo);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);  //fails for global realms
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyBillSparse(Bill expected, Bill actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
        }



        internal static void VerifyVendorCredit(VendorCredit expected, VendorCredit actual)
        {
            //Assert.AreEqual(expected.VendorCreditEx.Any, actual.VendorCreditEx.Any);
            //Assert.AreEqual(expected.VendorRef.name, actual.VendorRef.name);
            //Assert.AreEqual(expected.VendorRef.type, actual.VendorRef.type);
            Assert.AreEqual(expected.VendorRef.Value, actual.VendorRef.Value);
            //Assert.AreEqual(expected.APAccountRef.name, actual.APAccountRef.name);
            //Assert.AreEqual(expected.APAccountRef.type, actual.APAccountRef.type);
            Assert.AreEqual(expected.APAccountRef.Value, actual.APAccountRef.Value);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //    Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //    Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //    Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //    Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //    Assert.AreEqual(expected.ReplyEmail.Address, actual.ReplyEmail.Address);
            //    Assert.AreEqual(expected.ReplyEmail.Default, actual.ReplyEmail.Default);
            //    Assert.AreEqual(expected.ReplyEmail.DefaultSpecified, actual.ReplyEmail.DefaultSpecified);
            //    Assert.AreEqual(expected.ReplyEmail.Tag, actual.ReplyEmail.Tag);
            //Assert.AreEqual(expected.Memo, actual.Memo);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyVendorCreditSparse(VendorCredit expected, VendorCredit actual)
        {
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
        }

        internal static void VerifyStatementCharge(StatementCharge expected, StatementCharge actual)
        {
            //Assert.AreEqual(expected.Credit, actual.Credit);
            //Assert.AreEqual(expected.CreditSpecified, actual.CreditSpecified);
            //    Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //    Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            Assert.AreEqual(expected.BilledDate, actual.BilledDate);
            Assert.AreEqual(expected.BilledDateSpecified, actual.BilledDateSpecified);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //    Assert.AreEqual(expected.StatementChargeEx.Any, actual.StatementChargeEx.Any);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyStatementChargeSparse(StatementCharge expected, StatementCharge actual)
        {
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.BilledDate, actual.BilledDate);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
        }


        internal static void VerifyClass(Class expected, Class actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            //Assert.AreEqual(expected.SubClass, actual.SubClass);
            //Assert.AreEqual(expected.SubClassSpecified, actual.SubClassSpecified);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //    Assert.AreEqual(expected.ClassEx.Any, actual.ClassEx.Any);
        }


        internal static void VerifyClassSparseUpdate(Class expected, Class actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
        }

        internal static void VerifyPayment(Payment expected, Payment actual)
        {
            //Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.UnappliedAmt, actual.UnappliedAmt);
            //Assert.AreEqual(expected.UnappliedAmtSpecified, actual.UnappliedAmtSpecified);
            //Assert.AreEqual(expected.ProcessPayment, actual.ProcessPayment);
            //Assert.AreEqual(expected.ProcessPaymentSpecified, actual.ProcessPaymentSpecified);
            //    Assert.AreEqual(expected.PaymentEx.Any, actual.PaymentEx.Any);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            //Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyPaymentSparseUpdate(Payment expected, Payment actual)
        {
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
        }

        internal static void VerifyPaymentMethod(PaymentMethod expected, PaymentMethod actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.Type, actual.Type);
            //Assert.AreEqual(expected.PaymentMethodEx.Any, actual.PaymentMethodEx.Any);
        }


        internal static void VerifyPaymentMethodSparseUpdate(PaymentMethod expected, PaymentMethod actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
        }



        internal static void VerifyDepartment(Department expected, Department actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            //Assert.AreEqual(expected.SubDepartment, actual.SubDepartment);
            //Assert.AreEqual(expected.SubDepartmentSpecified, actual.SubDepartmentSpecified);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.DepartmentEx.Any, actual.DepartmentEx.Any);
        }

        internal static void VerifyDepartmentSparse(Department expected, Department actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
        }


        internal static void VerifyItem(Item expected, Item actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.SubItem, actual.SubItem);
            Assert.AreEqual(expected.SubItemSpecified, actual.SubItemSpecified);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.Level, actual.Level);
            Assert.AreEqual(expected.LevelSpecified, actual.LevelSpecified);
            Assert.AreEqual(expected.Taxable, actual.Taxable);
            Assert.AreEqual(expected.TaxableSpecified, actual.TaxableSpecified);
            //Assert.AreEqual(expected.SalesTaxIncluded, actual.SalesTaxIncluded);
            //Assert.AreEqual(expected.SalesTaxIncludedSpecified, actual.SalesTaxIncludedSpecified);
            Assert.AreEqual(expected.PercentBased, actual.PercentBased);
            Assert.AreEqual(expected.PercentBasedSpecified, actual.PercentBasedSpecified);
            Assert.AreEqual(expected.UnitPrice, actual.UnitPrice);
            Assert.AreEqual(expected.UnitPriceSpecified, actual.UnitPriceSpecified);
            Assert.AreEqual(expected.RatePercent, actual.RatePercent);
            Assert.AreEqual(expected.RatePercentSpecified, actual.RatePercentSpecified);
            //Assert.AreEqual(expected.TypeSpecified, actual.TypeSpecified);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.UOMSetRef.name, actual.UOMSetRef.name);
            //Assert.AreEqual(expected.UOMSetRef.type, actual.UOMSetRef.type);
            //Assert.AreEqual(expected.UOMSetRef.Value, actual.UOMSetRef.Value);
            //Assert.AreEqual(expected.IncomeAccountRef.name, actual.IncomeAccountRef.name);
            //Assert.AreEqual(expected.IncomeAccountRef.type, actual.IncomeAccountRef.type);
            Assert.AreEqual(expected.IncomeAccountRef.Value, actual.IncomeAccountRef.Value);
            //Assert.AreEqual(expected.PurchaseDesc, actual.PurchaseDesc);
            //Assert.AreEqual(expected.PurchaseTaxIncluded, actual.PurchaseTaxIncluded);
            //Assert.AreEqual(expected.PurchaseTaxIncludedSpecified, actual.PurchaseTaxIncludedSpecified);
            Assert.AreEqual(expected.PurchaseCost, actual.PurchaseCost);
            Assert.AreEqual(expected.PurchaseCostSpecified, actual.PurchaseCostSpecified);
            //Assert.AreEqual(expected.ExpenseAccountRef.name, actual.ExpenseAccountRef.name);
            //Assert.AreEqual(expected.ExpenseAccountRef.type, actual.ExpenseAccountRef.type);
            Assert.AreEqual(expected.ExpenseAccountRef.Value, actual.ExpenseAccountRef.Value);
            //    Assert.AreEqual(expected.COGSAccountRef.name, actual.COGSAccountRef.name);
            //    Assert.AreEqual(expected.COGSAccountRef.type, actual.COGSAccountRef.type);
            //    Assert.AreEqual(expected.COGSAccountRef.Value, actual.COGSAccountRef.Value);
            //    Assert.AreEqual(expected.AssetAccountRef.name, actual.AssetAccountRef.name);
            //    Assert.AreEqual(expected.AssetAccountRef.type, actual.AssetAccountRef.type);
            //    Assert.AreEqual(expected.AssetAccountRef.Value, actual.AssetAccountRef.Value);
            //    Assert.AreEqual(expected.PrefVendorRef.name, actual.PrefVendorRef.name);
            //    Assert.AreEqual(expected.PrefVendorRef.type, actual.PrefVendorRef.type);
            //    Assert.AreEqual(expected.PrefVendorRef.Value, actual.PrefVendorRef.Value);
            //Assert.AreEqual(expected.AvgCost, actual.AvgCost);
            //Assert.AreEqual(expected.AvgCostSpecified, actual.AvgCostSpecified);
            Assert.AreEqual(expected.TrackQtyOnHand, actual.TrackQtyOnHand);
            Assert.AreEqual(expected.TrackQtyOnHandSpecified, actual.TrackQtyOnHandSpecified);
            Assert.AreEqual(expected.QtyOnHand, actual.QtyOnHand);
            Assert.AreEqual(expected.QtyOnHandSpecified, actual.QtyOnHandSpecified);
            Assert.AreEqual(expected.QtyOnPurchaseOrder, actual.QtyOnPurchaseOrder);
            Assert.AreEqual(expected.QtyOnPurchaseOrderSpecified, actual.QtyOnPurchaseOrderSpecified);
            Assert.AreEqual(expected.QtyOnSalesOrder, actual.QtyOnSalesOrder);
            Assert.AreEqual(expected.QtyOnSalesOrderSpecified, actual.QtyOnSalesOrderSpecified);
            Assert.AreEqual(expected.ReorderPoint, actual.ReorderPoint);
            Assert.AreEqual(expected.ReorderPointSpecified, actual.ReorderPointSpecified);
            Assert.AreEqual(expected.ManPartNum, actual.ManPartNum);
            //    Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //    Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //    Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            //    Assert.AreEqual(expected.SalesTaxCodeRef.name, actual.SalesTaxCodeRef.name);
            //    Assert.AreEqual(expected.SalesTaxCodeRef.type, actual.SalesTaxCodeRef.type);
            //    Assert.AreEqual(expected.SalesTaxCodeRef.Value, actual.SalesTaxCodeRef.Value);
            //    Assert.AreEqual(expected.PurchaseTaxCodeRef.name, actual.PurchaseTaxCodeRef.name);
            //    Assert.AreEqual(expected.PurchaseTaxCodeRef.type, actual.PurchaseTaxCodeRef.type);
            //    Assert.AreEqual(expected.PurchaseTaxCodeRef.Value, actual.PurchaseTaxCodeRef.Value);
            //Assert.AreEqual(expected.InvStartDate, actual.InvStartDate);
            //Assert.AreEqual(expected.InvStartDateSpecified, actual.InvStartDateSpecified);
            //Assert.AreEqual(expected.BuildPoint, actual.BuildPoint);
            //Assert.AreEqual(expected.BuildPointSpecified, actual.BuildPointSpecified);
            //Assert.AreEqual(expected.PrintGroupedItems, actual.PrintGroupedItems);
            //Assert.AreEqual(expected.PrintGroupedItemsSpecified, actual.PrintGroupedItemsSpecified);
            //Assert.AreEqual(expected.SpecialItem, actual.SpecialItem);
            //Assert.AreEqual(expected.SpecialItemSpecified, actual.SpecialItemSpecified);
            //Assert.AreEqual(expected.SpecialItemTypeSpecified, actual.SpecialItemTypeSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.ItemGroupDetail, actual.ItemGroupDetail));
            //Assert.IsTrue(Helper.CheckEqual(expected.ItemAssemblyDetail, actual.ItemAssemblyDetail));
            //    Assert.AreEqual(expected.ItemEx.Any, actual.ItemEx.Any);
        }

        internal static void VerifyItemFrance(Item expected, Item actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.SubItem, actual.SubItem);
            Assert.AreEqual(expected.SubItemSpecified, actual.SubItemSpecified);

            Assert.AreEqual(expected.Level, actual.Level);
            Assert.AreEqual(expected.LevelSpecified, actual.LevelSpecified);
            Assert.AreEqual(expected.Taxable, actual.Taxable);
            Assert.AreEqual(expected.TaxableSpecified, actual.TaxableSpecified);

            Assert.AreEqual(expected.PercentBased, actual.PercentBased);
            Assert.AreEqual(expected.PercentBasedSpecified, actual.PercentBasedSpecified);
            Assert.AreEqual(expected.UnitPrice, actual.UnitPrice);
            Assert.AreEqual(expected.UnitPriceSpecified, actual.UnitPriceSpecified);
            Assert.AreEqual(expected.RatePercent, actual.RatePercent);
            Assert.AreEqual(expected.RatePercentSpecified, actual.RatePercentSpecified);

            Assert.AreEqual(expected.IncomeAccountRef.Value, actual.IncomeAccountRef.Value);

            Assert.AreEqual(expected.PurchaseCost, actual.PurchaseCost);
            Assert.AreEqual(expected.PurchaseCostSpecified, actual.PurchaseCostSpecified);

            Assert.AreEqual(expected.ExpenseAccountRef.Value, actual.ExpenseAccountRef.Value);

            Assert.AreEqual(expected.TrackQtyOnHand, actual.TrackQtyOnHand);
            Assert.AreEqual(expected.TrackQtyOnHandSpecified, actual.TrackQtyOnHandSpecified);
            Assert.AreEqual(expected.QtyOnHand, actual.QtyOnHand);
            Assert.AreEqual(expected.QtyOnHandSpecified, actual.QtyOnHandSpecified);
            Assert.AreEqual(expected.QtyOnPurchaseOrder, actual.QtyOnPurchaseOrder);
            Assert.AreEqual(expected.QtyOnPurchaseOrderSpecified, actual.QtyOnPurchaseOrderSpecified);
            Assert.AreEqual(expected.QtyOnSalesOrder, actual.QtyOnSalesOrder);
            Assert.AreEqual(expected.QtyOnSalesOrderSpecified, actual.QtyOnSalesOrderSpecified);
            Assert.AreEqual(expected.ReorderPoint, actual.ReorderPoint);
            Assert.AreEqual(expected.ReorderPointSpecified, actual.ReorderPointSpecified);
            Assert.AreEqual(expected.ManPartNum, actual.ManPartNum);

            Assert.AreEqual(expected.Type, actual.Type);
            //Assert.AreEqual(expected.ItemCategoryType, actual.ItemCategoryType);

        }

        internal static void VerifyItemSparseUpdate(Item expected, Item actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }



        internal static void VerifyTerm(Term expected, Term actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.DiscountPercent, actual.DiscountPercent);
            Assert.AreEqual(expected.DiscountPercentSpecified, actual.DiscountPercentSpecified);
            //Assert.AreEqual(expected.DueDays, actual.DueDays);
            //Assert.IsTrue(Helper.CheckEqual(expected.ItemsElementName, actual.ItemsElementName));
            //    Assert.AreEqual(expected.SalesTermEx.Any, actual.SalesTermEx.Any);
        }


        internal static void VerifyTermSparseUpdate(Term expected, Term actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
        }



        internal static void VerifyBillPayment(BillPayment expected, BillPayment actual)
        {
            //Assert.AreEqual(expected.VendorRef.name, actual.VendorRef.name);
            //Assert.AreEqual(expected.VendorRef.type, actual.VendorRef.type);
            //Assert.AreEqual(expected.VendorRef.Value, actual.VendorRef.Value);
            //Assert.AreEqual(expected.APAccountRef.name, actual.APAccountRef.name);
            //Assert.AreEqual(expected.APAccountRef.type, actual.APAccountRef.type);
            //Assert.AreEqual(expected.APAccountRef.Value, actual.APAccountRef.Value);
            //Assert.AreEqual(expected.PayTypeSpecified, actual.PayTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //	Assert.AreEqual(expected.BillPaymentEx.Any, actual.BillPaymentEx.Any);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyBillPaymentSparse(BillPayment expected, BillPayment actual)
        {
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
        }

        internal static void VerifyDeposit(Deposit expected, Deposit actual)
        {
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            //    Assert.AreEqual(expected.CashBack.AccountRef.name, actual.CashBack.AccountRef.name);
            //    Assert.AreEqual(expected.CashBack.AccountRef.type, actual.CashBack.AccountRef.type);
            //    Assert.AreEqual(expected.CashBack.AccountRef.Value, actual.CashBack.AccountRef.Value);
            //Assert.AreEqual(expected.CashBack.Amount, actual.CashBack.Amount);
            //Assert.AreEqual(expected.CashBack.AmountSpecified, actual.CashBack.AmountSpecified);
            //Assert.AreEqual(expected.CashBack.Memo, actual.CashBack.Memo);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            //Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //    Assert.AreEqual(expected.DepositEx.Any, actual.DepositEx.Any);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            //Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyDepositSparse(Deposit expected, Deposit actual)
        {
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
        }

        internal static void VerifyTransfer(Transfer expected, Transfer actual)
        {
            //Assert.AreEqual(expected.FromAccountRef.name, actual.FromAccountRef.name);
            //Assert.AreEqual(expected.FromAccountRef.type, actual.FromAccountRef.type);
            Assert.AreEqual(expected.FromAccountRef.Value, actual.FromAccountRef.Value);
            //Assert.AreEqual(expected.ToAccountRef.name, actual.ToAccountRef.name);
            //Assert.AreEqual(expected.ToAccountRef.type, actual.ToAccountRef.type);
            Assert.AreEqual(expected.ToAccountRef.Value, actual.ToAccountRef.Value);
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.AmountSpecified, actual.AmountSpecified);
            //    Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //    Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //    Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //    Assert.AreEqual(expected.TransferEx.Any, actual.TransferEx.Any);
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            //Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            //Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyTransferSparse(Transfer expected, Transfer actual)
        {
            Assert.AreEqual(expected.Amount, actual.Amount);
        }

        internal static void VerifyPurchaseOrder(PurchaseOrder expected, PurchaseOrder actual)
        {
            //Assert.AreEqual(expected.TaxCodeRef.name, actual.TaxCodeRef.name);
            //Assert.AreEqual(expected.TaxCodeRef.type, actual.TaxCodeRef.type);
            //Assert.AreEqual(expected.TaxCodeRef.Value, actual.TaxCodeRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //Assert.AreEqual(expected.ReimbursableInfoRef.name, actual.ReimbursableInfoRef.name);
            //Assert.AreEqual(expected.ReimbursableInfoRef.type, actual.ReimbursableInfoRef.type);
            //Assert.AreEqual(expected.ReimbursableInfoRef.Value, actual.ReimbursableInfoRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            Assert.AreEqual(expected.ExpectedDate, actual.ExpectedDate);
            Assert.AreEqual(expected.ExpectedDateSpecified, actual.ExpectedDateSpecified);
            //Assert.AreEqual(expected.VendorAddr.Line1, actual.VendorAddr.Line1);
            //Assert.AreEqual(expected.VendorAddr.Line2, actual.VendorAddr.Line2);
            //Assert.AreEqual(expected.VendorAddr.Line3, actual.VendorAddr.Line3);
            //Assert.AreEqual(expected.VendorAddr.Line4, actual.VendorAddr.Line4);
            //Assert.AreEqual(expected.VendorAddr.Line5, actual.VendorAddr.Line5);
            //Assert.AreEqual(expected.VendorAddr.City, actual.VendorAddr.City);
            //Assert.AreEqual(expected.VendorAddr.Country, actual.VendorAddr.Country);
            //Assert.AreEqual(expected.VendorAddr.CountryCode, actual.VendorAddr.CountryCode);
            //Assert.AreEqual(expected.VendorAddr.CountrySubDivisionCode, actual.VendorAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.VendorAddr.PostalCode, actual.VendorAddr.PostalCode);
            //Assert.AreEqual(expected.VendorAddr.PostalCodeSuffix, actual.VendorAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.VendorAddr.Lat, actual.VendorAddr.Lat);
            //Assert.AreEqual(expected.VendorAddr.Long, actual.VendorAddr.Long);
            //Assert.AreEqual(expected.VendorAddr.Tag, actual.VendorAddr.Tag);
            //Assert.AreEqual(expected.VendorAddr.Note, actual.VendorAddr.Note);
            //Assert.AreEqual(expected.AnyIntuitObject.name, actual.AnyIntuitObject.name);
            //Assert.AreEqual(expected.AnyIntuitObject.type, actual.AnyIntuitObject.type);
            //Assert.AreEqual(expected.AnyIntuitObject.Value, actual.AnyIntuitObject.Value);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            Assert.AreEqual(expected.FOB, actual.FOB);
            //Assert.AreEqual(expected.POEmail.Address, actual.POEmail.Address);
            //Assert.AreEqual(expected.POEmail.Default, actual.POEmail.Default);
            //Assert.AreEqual(expected.POEmail.DefaultSpecified, actual.POEmail.DefaultSpecified);
            //Assert.AreEqual(expected.POEmail.Tag, actual.POEmail.Tag);
            //Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            Assert.AreEqual(expected.ManuallyClosed, actual.ManuallyClosed);
            Assert.AreEqual(expected.ManuallyClosedSpecified, actual.ManuallyClosedSpecified);
            //Assert.AreEqual(expected.POStatusSpecified, actual.POStatusSpecified);
            //Assert.AreEqual(expected.PurchaseOrderEx.Any, actual.PurchaseOrderEx.Any);
            Assert.AreEqual(expected.VendorRef.name, actual.VendorRef.name);
            Assert.AreEqual(expected.VendorRef.type, actual.VendorRef.type);
            Assert.AreEqual(expected.VendorRef.Value, actual.VendorRef.Value);
            //Assert.AreEqual(expected.APAccountRef.name, actual.APAccountRef.name);
            //Assert.AreEqual(expected.APAccountRef.type, actual.APAccountRef.type);
            //Assert.AreEqual(expected.APAccountRef.Value, actual.APAccountRef.Value);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ReplyEmail.Address, actual.ReplyEmail.Address);
            //Assert.AreEqual(expected.ReplyEmail.Default, actual.ReplyEmail.Default);
            //Assert.AreEqual(expected.ReplyEmail.DefaultSpecified, actual.ReplyEmail.DefaultSpecified);
            //Assert.AreEqual(expected.ReplyEmail.Tag, actual.ReplyEmail.Tag);
            Assert.AreEqual(expected.Memo, actual.Memo);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified); //fails for global realms
            //Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifyPurchaseOrderSparse(PurchaseOrder expected, PurchaseOrder actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
        }

        internal static void VerifySalesOrder(SalesOrder expected, SalesOrder actual)
        {
            Assert.AreEqual(expected.ManuallyClosed, actual.ManuallyClosed);
            Assert.AreEqual(expected.ManuallyClosedSpecified, actual.ManuallyClosedSpecified);
            //Assert.AreEqual(expected.SalesOrderEx.Any, actual.SalesOrderEx.Any);
            Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            //Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            //Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //    Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //    Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //    Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.PONumber, actual.PONumber);
            //Assert.AreEqual(expected.FOB, actual.FOB);
            //    Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //    Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //    Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            //Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            //Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.Balance, actual.Balance);
            //Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            //Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //    Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //    Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //    Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            //    Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //    Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //    Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }

        internal static void VerifySalesOrderSparse(SalesOrder expected, SalesOrder actual)
        {
            Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
        }


        internal static void VerifyCreditMemo(CreditMemo expected, CreditMemo actual)
        {
            //Assert.AreEqual(expected.RemainingCredit, actual.RemainingCredit);
            //Assert.AreEqual(expected.RemainingCreditSpecified, actual.RemainingCreditSpecified);
            //    Assert.AreEqual(expected.CreditMemoEx.Any, actual.CreditMemoEx.Any);
            //Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            //Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            //    Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //    Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //    Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //    Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            //    Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //    Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //    Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //    Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //    Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //    Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //    Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //    Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //    Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //    Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //    Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //    Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //    Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //    Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //    Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //    Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //    Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //    Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //    Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //    Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //    Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //    Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //    Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //    Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //    Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //    Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //    Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //    Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //    Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //    Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //    Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //    Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //    Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //    Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            //Assert.AreEqual(expected.DueDate, actual.DueDate);
            //Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //    Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //    Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //    Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.PONumber, actual.PONumber);
            //Assert.AreEqual(expected.FOB, actual.FOB);
            //    Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //    Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //    Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            //Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            //Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            //Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            //Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            //Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            //Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            //    Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //    Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //    Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            //Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            //Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            //    Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //    Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //    Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //    Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //    Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //    Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //    Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            //Assert.AreEqual(expected.Balance, actual.Balance);
            //Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            //Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //    Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //    Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //    Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            //Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //    Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyCreditMemoSparse(CreditMemo expected, CreditMemo actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
        }

        internal static void VerifyRefundReceipt(RefundReceipt expected, RefundReceipt actual)
        {
            //Assert.AreEqual(expected.RemainingCredit, actual.RemainingCredit);
            //Assert.AreEqual(expected.RemainingCreditSpecified, actual.RemainingCreditSpecified);
            //Assert.AreEqual(expected.RefundReceiptEx.Any, actual.RefundReceiptEx.Any);
            //Assert.AreEqual(expected.AutoDocNumber, actual.AutoDocNumber);
            //Assert.AreEqual(expected.AutoDocNumberSpecified, actual.AutoDocNumberSpecified);
            Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //Assert.AreEqual(expected.CustomerMemo.id, actual.CustomerMemo.id);
            //Assert.AreEqual(expected.CustomerMemo.Value, actual.CustomerMemo.Value);
            //    Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //    Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //    Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //    Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //    Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //    Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //    Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //    Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //    Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //    Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //    Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //    Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //    Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //    Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //    Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //    Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //    Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //    Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //    Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //    Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //    Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //    Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //    Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //    Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //    Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //    Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //    Assert.AreEqual(expected.RemitToRef.name, actual.RemitToRef.name);
            //    Assert.AreEqual(expected.RemitToRef.type, actual.RemitToRef.type);
            //    Assert.AreEqual(expected.RemitToRef.Value, actual.RemitToRef.Value);
            //    Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //    Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //    Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //    Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //    Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //    Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            //Assert.AreEqual(expected.DueDate, actual.DueDate);
            //Assert.AreEqual(expected.DueDateSpecified, actual.DueDateSpecified);
            //    Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //    Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //    Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.PONumber, actual.PONumber);
            //Assert.AreEqual(expected.FOB, actual.FOB);
            //    Assert.AreEqual(expected.ShipMethodRef.name, actual.ShipMethodRef.name);
            //    Assert.AreEqual(expected.ShipMethodRef.type, actual.ShipMethodRef.type);
            //    Assert.AreEqual(expected.ShipMethodRef.Value, actual.ShipMethodRef.Value);
            //Assert.AreEqual(expected.ShipDate, actual.ShipDate);
            //Assert.AreEqual(expected.ShipDateSpecified, actual.ShipDateSpecified);
            //Assert.AreEqual(expected.TrackingNum, actual.TrackingNum);
            //Assert.AreEqual(expected.GlobalTaxCalculationSpecified, actual.GlobalTaxCalculationSpecified);
            //Assert.AreEqual(expected.TotalAmt, actual.TotalAmt);
            //Assert.AreEqual(expected.TotalAmtSpecified, actual.TotalAmtSpecified);
            //Assert.AreEqual(expected.HomeTotalAmt, actual.HomeTotalAmt);
            //Assert.AreEqual(expected.HomeTotalAmtSpecified, actual.HomeTotalAmtSpecified);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscount, actual.ApplyTaxAfterDiscount);
            //Assert.AreEqual(expected.ApplyTaxAfterDiscountSpecified, actual.ApplyTaxAfterDiscountSpecified);
            //    Assert.AreEqual(expected.TemplateRef.name, actual.TemplateRef.name);
            //    Assert.AreEqual(expected.TemplateRef.type, actual.TemplateRef.type);
            //    Assert.AreEqual(expected.TemplateRef.Value, actual.TemplateRef.Value);
            Assert.AreEqual(expected.PrintStatusSpecified, actual.PrintStatusSpecified);
            //Assert.AreEqual(expected.EmailStatusSpecified, actual.EmailStatusSpecified);
            Assert.AreEqual(expected.BillEmail.Address, actual.BillEmail.Address);
            //Assert.AreEqual(expected.BillEmail.Default, actual.BillEmail.Default);
            //Assert.AreEqual(expected.BillEmail.DefaultSpecified, actual.BillEmail.DefaultSpecified);
            //Assert.AreEqual(expected.BillEmail.Tag, actual.BillEmail.Tag);
            //Assert.AreEqual(expected.ARAccountRef.name, actual.ARAccountRef.name);
            //Assert.AreEqual(expected.ARAccountRef.type, actual.ARAccountRef.type);
            //Assert.AreEqual(expected.ARAccountRef.Value, actual.ARAccountRef.Value);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.FinanceCharge, actual.FinanceCharge);
            //Assert.AreEqual(expected.FinanceChargeSpecified, actual.FinanceChargeSpecified);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.PaymentRefNum, actual.PaymentRefNum);
            //Assert.AreEqual(expected.PaymentTypeSpecified, actual.PaymentTypeSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject, actual.AnyIntuitObject);
            //    Assert.AreEqual(expected.DepositToAccountRef.name, actual.DepositToAccountRef.name);
            //    Assert.AreEqual(expected.DepositToAccountRef.type, actual.DepositToAccountRef.type);
            //    Assert.AreEqual(expected.DepositToAccountRef.Value, actual.DepositToAccountRef.Value);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            //Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyRefundReceiptSparse(RefundReceipt expected, RefundReceipt actual)
        {
            Assert.AreEqual(expected.PrintStatus, actual.PrintStatus);
            Assert.AreEqual(expected.EmailStatus, actual.EmailStatus);
        }

        internal static void VerifyCompanyCurrency(CompanyCurrency expected, CompanyCurrency actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.CodeSpecified, actual.CodeSpecified);
            //Assert.AreEqual(expected.Separator, actual.Separator);
            //Assert.AreEqual(expected.Format, actual.Format);
            //Assert.AreEqual(expected.DecimalPlaces, actual.DecimalPlaces);
            //Assert.AreEqual(expected.DecimalSeparator, actual.DecimalSeparator);
            //Assert.AreEqual(expected.Symbol, actual.Symbol);
            //Assert.AreEqual(expected.SymbolPositionSpecified, actual.SymbolPositionSpecified);
            //Assert.AreEqual(expected.UserDefined, actual.UserDefined);
            //Assert.AreEqual(expected.UserDefinedSpecified, actual.UserDefinedSpecified);
            //Assert.AreEqual(expected.ExchangeRate, actual.ExchangeRate);
            //Assert.AreEqual(expected.ExchangeRateSpecified, actual.ExchangeRateSpecified);
            //Assert.AreEqual(expected.AsOfDate, actual.AsOfDate);
            //Assert.AreEqual(expected.AsOfDateSpecified, actual.AsOfDateSpecified);
            //Assert.AreEqual(expected.CurrencyEx.Any, actual.CurrencyEx.Any);
        }


        internal static void VerifyCompanyCurrencySparse(CompanyCurrency expected, CompanyCurrency actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }

        internal static void VerifySalesRep(SalesRep expected, SalesRep actual)
        {
            Assert.AreEqual(expected.NameOfSpecified, actual.NameOfSpecified);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject.name, actual.AnyIntuitObject.name);
            //Assert.AreEqual(expected.AnyIntuitObject.type, actual.AnyIntuitObject.type);
            //Assert.AreEqual(expected.AnyIntuitObject.Value, actual.AnyIntuitObject.Value);
            Assert.AreEqual(expected.Initials, actual.Initials);
            //Assert.AreEqual(expected.SalesRepEx.Any, actual.SalesRepEx.Any);
        }

        internal static void VerifySalesRepSparse(SalesRep expected, SalesRep actual)
        {
            Assert.AreEqual(expected.Initials, actual.Initials);
        }

        internal static void VerifyPriceLevel(PriceLevel expected, PriceLevel actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.AnyIntuitObjects, actual.AnyIntuitObjects);
            Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.PriceLevelEx.Any, actual.PriceLevelEx.Any);
        }

        internal static void VerifyPriceLevelSparse(PriceLevel expected, PriceLevel actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }

        internal static void VerifyPriceLevelPerItem(PriceLevelPerItem expected, PriceLevelPerItem actual)
        {
            Assert.AreEqual(expected.Overview, actual.Overview);
        }


        internal static void VerifyPriceLevelPerItemSparse(PriceLevelPerItem expected, PriceLevelPerItem actual)
        {
            Assert.AreEqual(expected.Overview, actual.Overview);
        }

        internal static void VerifyCustomerMsg(CustomerMsg expected, CustomerMsg actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.CustomerMsgEx.Any, actual.CustomerMsgEx.Any);
        }

        internal static void VerifyCustomerMsgSparse(CustomerMsg expected, CustomerMsg actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }


        internal static void VerifyJournalCode(JournalCode expected, JournalCode actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);

        }

        internal static void VerifyJournalCodeSparse(JournalCode expected, JournalCode actual)
        {
            Assert.AreEqual(expected.Description, actual.Description);
        }


        internal static void VerifyJournalEntry(JournalEntry expected, JournalEntry actual)
        {
            Assert.AreEqual(expected.Adjustment, actual.Adjustment);
            Assert.AreEqual(expected.AdjustmentSpecified, actual.AdjustmentSpecified);
            //	Assert.AreEqual(expected.JournalEntryEx.Any, actual.JournalEntryEx.Any);
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            //    Assert.AreEqual(expected.DepartmentRef.name, actual.DepartmentRef.name);
            //    Assert.AreEqual(expected.DepartmentRef.type, actual.DepartmentRef.type);
            //    Assert.AreEqual(expected.DepartmentRef.Value, actual.DepartmentRef.Value);
            //    Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //    Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //    Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.PrivateNote, actual.PrivateNote);
            //Assert.AreEqual(expected.TxnStatus, actual.TxnStatus);
            //Assert.IsTrue(Helper.CheckEqual(expected.LinkedTxn, actual.LinkedTxn));
            //Assert.IsTrue(Helper.CheckEqual(expected.Line, actual.Line));
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.name, actual.TxnTaxDetail.DefaultTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.type, actual.TxnTaxDetail.DefaultTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.DefaultTaxCodeRef.Value, actual.TxnTaxDetail.DefaultTaxCodeRef.Value);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.name, actual.TxnTaxDetail.TxnTaxCodeRef.name);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.type, actual.TxnTaxDetail.TxnTaxCodeRef.type);
            //        Assert.AreEqual(expected.TxnTaxDetail.TxnTaxCodeRef.Value, actual.TxnTaxDetail.TxnTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTax, actual.TxnTaxDetail.TotalTax);
            //    Assert.AreEqual(expected.TxnTaxDetail.TotalTaxSpecified, actual.TxnTaxDetail.TotalTaxSpecified);
            //    Assert.IsTrue(Helper.CheckEqual(expected.TxnTaxDetail.TaxLine, actual.TxnTaxDetail.TaxLine));
        }


        internal static void VerifyJournalEntrySparse(JournalEntry expected, JournalEntry actual)
        {
            Assert.AreEqual(expected.DocNumber, actual.DocNumber);
        }

        internal static void VerifyTimeActivity(TimeActivity expected, TimeActivity actual)
        {
            Assert.AreEqual(expected.TimeZone, actual.TimeZone);
            Assert.AreEqual(expected.TxnDate, actual.TxnDate);
            Assert.AreEqual(expected.TxnDateSpecified, actual.TxnDateSpecified);
            Assert.AreEqual(expected.NameOfSpecified, actual.NameOfSpecified);
            //Assert.AreEqual(expected.AnyIntuitObject.name, actual.AnyIntuitObject.name);
            //Assert.AreEqual(expected.AnyIntuitObject.type, actual.AnyIntuitObject.type);
            //Assert.AreEqual(expected.AnyIntuitObject.Value, actual.AnyIntuitObject.Value);
            //    Assert.AreEqual(expected.CustomerRef.name, actual.CustomerRef.name);
            //    Assert.AreEqual(expected.CustomerRef.type, actual.CustomerRef.type);
            //    Assert.AreEqual(expected.CustomerRef.Value, actual.CustomerRef.Value);
            //    Assert.AreEqual(expected.ItemRef.name, actual.ItemRef.name);
            //    Assert.AreEqual(expected.ItemRef.type, actual.ItemRef.type);
            //    Assert.AreEqual(expected.ItemRef.Value, actual.ItemRef.Value);
            //    Assert.AreEqual(expected.ClassRef.name, actual.ClassRef.name);
            //    Assert.AreEqual(expected.ClassRef.type, actual.ClassRef.type);
            //    Assert.AreEqual(expected.ClassRef.Value, actual.ClassRef.Value);
            //    Assert.AreEqual(expected.PayrollItemRef.name, actual.PayrollItemRef.name);
            //    Assert.AreEqual(expected.PayrollItemRef.type, actual.PayrollItemRef.type);
            //    Assert.AreEqual(expected.PayrollItemRef.Value, actual.PayrollItemRef.Value);
            //Assert.AreEqual(expected.BillableStatusSpecified, actual.BillableStatusSpecified);
            Assert.AreEqual(expected.Taxable, actual.Taxable);
            //Assert.AreEqual(expected.TaxableSpecified, actual.TaxableSpecified);
            Assert.AreEqual(expected.HourlyRate, actual.HourlyRate);
            //Assert.AreEqual(expected.HourlyRateSpecified, actual.HourlyRateSpecified);
            Assert.AreEqual(expected.Hours, actual.Hours);
            //Assert.AreEqual(expected.HoursSpecified, actual.HoursSpecified);
            //Assert.AreEqual(expected.Minutes, actual.Minutes);
            //Assert.AreEqual(expected.MinutesSpecified, actual.MinutesSpecified);
            //Assert.AreEqual(expected.BreakHours, actual.BreakHours);
            //Assert.AreEqual(expected.BreakHoursSpecified, actual.BreakHoursSpecified);
            //Assert.AreEqual(expected.BreakMinutes, actual.BreakMinutes);
            //Assert.AreEqual(expected.BreakMinutesSpecified, actual.BreakMinutesSpecified);
            Assert.AreEqual(expected.StartTime, actual.StartTime);
            Assert.AreEqual(expected.StartTimeSpecified, actual.StartTimeSpecified);
            Assert.AreEqual(expected.EndTime, actual.EndTime);
            Assert.AreEqual(expected.EndTimeSpecified, actual.EndTimeSpecified);
            Assert.AreEqual(expected.Description, actual.Description);
            //Assert.AreEqual(expected.TimeActivityEx.Any, actual.TimeActivityEx.Any);
        }

        internal static void VerifyTimeActivitySparse(TimeActivity expected, TimeActivity actual)
        {
            Assert.AreEqual(expected.Description, actual.Description);
        }

        internal static void VerifyInventorySite(InventorySite expected, InventorySite actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.DefaultSite, actual.DefaultSite);
            //Assert.AreEqual(expected.DefaultSiteSpecified, actual.DefaultSiteSpecified);
            Assert.AreEqual(expected.Description, actual.Description);
            //Assert.AreEqual(expected.Contact, actual.Contact);
            //Assert.IsTrue(Helper.CheckEqual(expected.Addr, actual.Addr));
            //Assert.IsTrue(Helper.CheckEqual(expected.ContactInfo, actual.ContactInfo));
            //    Assert.AreEqual(expected.InventorySiteEx.Any, actual.InventorySiteEx.Any);
        }


        internal static void VerifyInventorySiteSparse(InventorySite expected, InventorySite actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        internal static void VerifyShipMethod(ShipMethod expected, ShipMethod actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            //	Assert.AreEqual(expected.ShipMethodEx.Any, actual.ShipMethodEx.Any);
        }

        internal static void VerifyShipMethodSparse(ShipMethod expected, ShipMethod actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }

        internal static void VerifyTask(Task expected, Task actual)
        {
            //Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.From, actual.From);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.Done, actual.Done);
            Assert.AreEqual(expected.DoneSpecified, actual.DoneSpecified);
            Assert.AreEqual(expected.ReminderDate, actual.ReminderDate);
            Assert.AreEqual(expected.ReminderDateSpecified, actual.ReminderDateSpecified);
            //Assert.AreEqual(expected.TaskEx.Any, actual.TaskEx.Any);
        }

        internal static void VerifyTaskSparse(Task expected, Task actual)
        {
            Assert.AreEqual(expected.From, actual.From);
        }

        internal static void VerifyPreferences(Preferences expected, Preferences actual)
        {
            //Assert.AreEqual(expected.AccountingInfoPrefs.UseAccountNumbers, actual.AccountingInfoPrefs.UseAccountNumbers);
            //Assert.AreEqual(expected.AccountingInfoPrefs.UseAccountNumbersSpecified, actual.AccountingInfoPrefs.UseAccountNumbersSpecified);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultARAccount.name, actual.AccountingInfoPrefs.DefaultARAccount.name);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultARAccount.type, actual.AccountingInfoPrefs.DefaultARAccount.type);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultARAccount.Value, actual.AccountingInfoPrefs.DefaultARAccount.Value);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultAPAccount.name, actual.AccountingInfoPrefs.DefaultAPAccount.name);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultAPAccount.type, actual.AccountingInfoPrefs.DefaultAPAccount.type);
            //    Assert.AreEqual(expected.AccountingInfoPrefs.DefaultAPAccount.Value, actual.AccountingInfoPrefs.DefaultAPAccount.Value);
            //Assert.AreEqual(expected.AccountingInfoPrefs.RequiresAccounts, actual.AccountingInfoPrefs.RequiresAccounts);
            //Assert.AreEqual(expected.AccountingInfoPrefs.RequiresAccountsSpecified, actual.AccountingInfoPrefs.RequiresAccountsSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.TrackDepartments, actual.AccountingInfoPrefs.TrackDepartments);
            //Assert.AreEqual(expected.AccountingInfoPrefs.TrackDepartmentsSpecified, actual.AccountingInfoPrefs.TrackDepartmentsSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.DepartmentTerminology, actual.AccountingInfoPrefs.DepartmentTerminology);
            //Assert.AreEqual(expected.AccountingInfoPrefs.ClassTrackingPerTxn, actual.AccountingInfoPrefs.ClassTrackingPerTxn);
            //Assert.AreEqual(expected.AccountingInfoPrefs.ClassTrackingPerTxnSpecified, actual.AccountingInfoPrefs.ClassTrackingPerTxnSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.ClassTrackingPerTxnLine, actual.AccountingInfoPrefs.ClassTrackingPerTxnLine);
            //Assert.AreEqual(expected.AccountingInfoPrefs.ClassTrackingPerTxnLineSpecified, actual.AccountingInfoPrefs.ClassTrackingPerTxnLineSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.AutoJournalEntryNumber, actual.AccountingInfoPrefs.AutoJournalEntryNumber);
            //Assert.AreEqual(expected.AccountingInfoPrefs.AutoJournalEntryNumberSpecified, actual.AccountingInfoPrefs.AutoJournalEntryNumberSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.FirstMonthOfFiscalYearSpecified, actual.AccountingInfoPrefs.FirstMonthOfFiscalYearSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.TaxYearMonthSpecified, actual.AccountingInfoPrefs.TaxYearMonthSpecified);
            //Assert.AreEqual(expected.AccountingInfoPrefs.TaxForm, actual.AccountingInfoPrefs.TaxForm);
            //Assert.AreEqual(expected.AccountingInfoPrefs.BookCloseDate, actual.AccountingInfoPrefs.BookCloseDate);
            //Assert.AreEqual(expected.AccountingInfoPrefs.BookCloseDateSpecified, actual.AccountingInfoPrefs.BookCloseDateSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.AccountingInfoPrefs.OtherContactInfo, actual.AccountingInfoPrefs.OtherContactInfo));
            //Assert.AreEqual(expected.AccountingInfoPrefs.CustomerTerminology, actual.AccountingInfoPrefs.CustomerTerminology);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.MLIAvailable, actual.AdvancedInventoryPrefs.MLIAvailable);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.MLIAvailableSpecified, actual.AdvancedInventoryPrefs.MLIAvailableSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.MLIEnabled, actual.AdvancedInventoryPrefs.MLIEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.MLIEnabledSpecified, actual.AdvancedInventoryPrefs.MLIEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.EnhancedInventoryReceivingEnabled, actual.AdvancedInventoryPrefs.EnhancedInventoryReceivingEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.EnhancedInventoryReceivingEnabledSpecified, actual.AdvancedInventoryPrefs.EnhancedInventoryReceivingEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingSerialOrLotNumber, actual.AdvancedInventoryPrefs.TrackingSerialOrLotNumber);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingSerialOrLotNumberSpecified, actual.AdvancedInventoryPrefs.TrackingSerialOrLotNumberSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnSalesTransactionsEnabled, actual.AdvancedInventoryPrefs.TrackingOnSalesTransactionsEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnSalesTransactionsEnabledSpecified, actual.AdvancedInventoryPrefs.TrackingOnSalesTransactionsEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabled, actual.AdvancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabledSpecified, actual.AdvancedInventoryPrefs.TrackingOnPurchaseTransactionsEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabled, actual.AdvancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabledSpecified, actual.AdvancedInventoryPrefs.TrackingOnInventoryAdjustmentEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnBuildAssemblyEnabled, actual.AdvancedInventoryPrefs.TrackingOnBuildAssemblyEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.TrackingOnBuildAssemblyEnabledSpecified, actual.AdvancedInventoryPrefs.TrackingOnBuildAssemblyEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.FIFOEnabled, actual.AdvancedInventoryPrefs.FIFOEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.FIFOEnabledSpecified, actual.AdvancedInventoryPrefs.FIFOEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.FIFOEffectiveDate, actual.AdvancedInventoryPrefs.FIFOEffectiveDate);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.FIFOEffectiveDateSpecified, actual.AdvancedInventoryPrefs.FIFOEffectiveDateSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.RowShelfBinEnabled, actual.AdvancedInventoryPrefs.RowShelfBinEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.RowShelfBinEnabledSpecified, actual.AdvancedInventoryPrefs.RowShelfBinEnabledSpecified);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.BarcodeEnabled, actual.AdvancedInventoryPrefs.BarcodeEnabled);
            //Assert.AreEqual(expected.AdvancedInventoryPrefs.BarcodeEnabledSpecified, actual.AdvancedInventoryPrefs.BarcodeEnabledSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.ForSales, actual.ProductAndServicesPrefs.ForSales);
            Assert.AreEqual(expected.ProductAndServicesPrefs.ForSalesSpecified, actual.ProductAndServicesPrefs.ForSalesSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.ForPurchase, actual.ProductAndServicesPrefs.ForPurchase);
            Assert.AreEqual(expected.ProductAndServicesPrefs.ForPurchaseSpecified, actual.ProductAndServicesPrefs.ForPurchaseSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.InventoryAndPurchaseOrder, actual.ProductAndServicesPrefs.InventoryAndPurchaseOrder);
            Assert.AreEqual(expected.ProductAndServicesPrefs.InventoryAndPurchaseOrderSpecified, actual.ProductAndServicesPrefs.InventoryAndPurchaseOrderSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.QuantityWithPriceAndRate, actual.ProductAndServicesPrefs.QuantityWithPriceAndRate);
            Assert.AreEqual(expected.ProductAndServicesPrefs.QuantityWithPriceAndRateSpecified, actual.ProductAndServicesPrefs.QuantityWithPriceAndRateSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.QuantityOnHand, actual.ProductAndServicesPrefs.QuantityOnHand);
            Assert.AreEqual(expected.ProductAndServicesPrefs.QuantityOnHandSpecified, actual.ProductAndServicesPrefs.QuantityOnHandSpecified);
            Assert.AreEqual(expected.ProductAndServicesPrefs.UOMSpecified, actual.ProductAndServicesPrefs.UOMSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.UsingProgressInvoicing, actual.SalesFormsPrefs.UsingProgressInvoicing);
            //Assert.AreEqual(expected.SalesFormsPrefs.UsingProgressInvoicingSpecified, actual.SalesFormsPrefs.UsingProgressInvoicingSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.SalesFormsPrefs.CustomField, actual.SalesFormsPrefs.CustomField));
            //Assert.AreEqual(expected.SalesFormsPrefs.CustomTxnNumbers, actual.SalesFormsPrefs.CustomTxnNumbers);
            //Assert.AreEqual(expected.SalesFormsPrefs.CustomTxnNumbersSpecified, actual.SalesFormsPrefs.CustomTxnNumbersSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.DelayedCharges, actual.SalesFormsPrefs.DelayedCharges);
            //Assert.AreEqual(expected.SalesFormsPrefs.DelayedChargesSpecified, actual.SalesFormsPrefs.DelayedChargesSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowDeposit, actual.SalesFormsPrefs.AllowDeposit);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowDepositSpecified, actual.SalesFormsPrefs.AllowDepositSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowDiscount, actual.SalesFormsPrefs.AllowDiscount);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowDiscountSpecified, actual.SalesFormsPrefs.AllowDiscountSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.DefaultDiscountAccount, actual.SalesFormsPrefs.DefaultDiscountAccount);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowEstimates, actual.SalesFormsPrefs.AllowEstimates);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowEstimatesSpecified, actual.SalesFormsPrefs.AllowEstimatesSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.IPNSupportEnabled, actual.SalesFormsPrefs.IPNSupportEnabled);
            //Assert.AreEqual(expected.SalesFormsPrefs.IPNSupportEnabledSpecified, actual.SalesFormsPrefs.IPNSupportEnabledSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.InvoiceMessage, actual.SalesFormsPrefs.InvoiceMessage);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowServiceDate, actual.SalesFormsPrefs.AllowServiceDate);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowServiceDateSpecified, actual.SalesFormsPrefs.AllowServiceDateSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowShipping, actual.SalesFormsPrefs.AllowShipping);
            //Assert.AreEqual(expected.SalesFormsPrefs.AllowShippingSpecified, actual.SalesFormsPrefs.AllowShippingSpecified);
            //Assert.AreEqual(expected.SalesFormsPrefs.DefaultShippingAccount, actual.SalesFormsPrefs.DefaultShippingAccount);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultItem.name, actual.SalesFormsPrefs.DefaultItem.name);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultItem.type, actual.SalesFormsPrefs.DefaultItem.type);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultItem.Value, actual.SalesFormsPrefs.DefaultItem.Value);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultTerms.name, actual.SalesFormsPrefs.DefaultTerms.name);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultTerms.type, actual.SalesFormsPrefs.DefaultTerms.type);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultTerms.Value, actual.SalesFormsPrefs.DefaultTerms.Value);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultDeliveryMethod, actual.SalesFormsPrefs.DefaultDeliveryMethod);
            //    Assert.AreEqual(expected.SalesFormsPrefs.AutoApplyCredit, actual.SalesFormsPrefs.AutoApplyCredit);
            //    Assert.AreEqual(expected.SalesFormsPrefs.AutoApplyCreditSpecified, actual.SalesFormsPrefs.AutoApplyCreditSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.AutoApplyPayments, actual.SalesFormsPrefs.AutoApplyPayments);
            //    Assert.AreEqual(expected.SalesFormsPrefs.AutoApplyPaymentsSpecified, actual.SalesFormsPrefs.AutoApplyPaymentsSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.PrintItemWithZeroAmount, actual.SalesFormsPrefs.PrintItemWithZeroAmount);
            //    Assert.AreEqual(expected.SalesFormsPrefs.PrintItemWithZeroAmountSpecified, actual.SalesFormsPrefs.PrintItemWithZeroAmountSpecified);
            //        Assert.AreEqual(expected.SalesFormsPrefs.DefaultShipMethodRef.name, actual.SalesFormsPrefs.DefaultShipMethodRef.name);
            //        Assert.AreEqual(expected.SalesFormsPrefs.DefaultShipMethodRef.type, actual.SalesFormsPrefs.DefaultShipMethodRef.type);
            //        Assert.AreEqual(expected.SalesFormsPrefs.DefaultShipMethodRef.Value, actual.SalesFormsPrefs.DefaultShipMethodRef.Value);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultMarkup, actual.SalesFormsPrefs.DefaultMarkup);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultMarkupSpecified, actual.SalesFormsPrefs.DefaultMarkupSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.TrackReimbursableExpensesAsIncome, actual.SalesFormsPrefs.TrackReimbursableExpensesAsIncome);
            //    Assert.AreEqual(expected.SalesFormsPrefs.TrackReimbursableExpensesAsIncomeSpecified, actual.SalesFormsPrefs.TrackReimbursableExpensesAsIncomeSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.UsingSalesOrders, actual.SalesFormsPrefs.UsingSalesOrders);
            //    Assert.AreEqual(expected.SalesFormsPrefs.UsingSalesOrdersSpecified, actual.SalesFormsPrefs.UsingSalesOrdersSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.UsingPriceLevels, actual.SalesFormsPrefs.UsingPriceLevels);
            //    Assert.AreEqual(expected.SalesFormsPrefs.UsingPriceLevelsSpecified, actual.SalesFormsPrefs.UsingPriceLevelsSpecified);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultFOB, actual.SalesFormsPrefs.DefaultFOB);
            //    Assert.AreEqual(expected.SalesFormsPrefs.DefaultCustomerMessage, actual.SalesFormsPrefs.DefaultCustomerMessage);
            //Assert.IsTrue(Helper.CheckEqual(expected.EmailMessagesPrefs, actual.EmailMessagesPrefs));
            //Assert.IsTrue(Helper.CheckEqual(expected.PrintDocumentPrefs, actual.PrintDocumentPrefs));
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.EnableBills, actual.VendorAndPurchasesPrefs.EnableBills);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.EnableBillsSpecified, actual.VendorAndPurchasesPrefs.EnableBillsSpecified);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.TrackingByCustomer, actual.VendorAndPurchasesPrefs.TrackingByCustomer);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.TrackingByCustomerSpecified, actual.VendorAndPurchasesPrefs.TrackingByCustomerSpecified);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.BillableExpenseTracking, actual.VendorAndPurchasesPrefs.BillableExpenseTracking);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.BillableExpenseTrackingSpecified, actual.VendorAndPurchasesPrefs.BillableExpenseTrackingSpecified);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultTerms.name, actual.VendorAndPurchasesPrefs.DefaultTerms.name);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultTerms.type, actual.VendorAndPurchasesPrefs.DefaultTerms.type);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultTerms.Value, actual.VendorAndPurchasesPrefs.DefaultTerms.Value);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultMarkup, actual.VendorAndPurchasesPrefs.DefaultMarkup);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultMarkupSpecified, actual.VendorAndPurchasesPrefs.DefaultMarkupSpecified);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultMarkupAccount.name, actual.VendorAndPurchasesPrefs.DefaultMarkupAccount.name);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultMarkupAccount.type, actual.VendorAndPurchasesPrefs.DefaultMarkupAccount.type);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DefaultMarkupAccount.Value, actual.VendorAndPurchasesPrefs.DefaultMarkupAccount.Value);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.AutomaticBillPayment, actual.VendorAndPurchasesPrefs.AutomaticBillPayment);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.AutomaticBillPaymentSpecified, actual.VendorAndPurchasesPrefs.AutomaticBillPaymentSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.VendorAndPurchasesPrefs.POCustomField, actual.VendorAndPurchasesPrefs.POCustomField));
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.MsgToVendors, actual.VendorAndPurchasesPrefs.MsgToVendors);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.UsingInventory, actual.VendorAndPurchasesPrefs.UsingInventory);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.UsingInventorySpecified, actual.VendorAndPurchasesPrefs.UsingInventorySpecified);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.UsingMultiLocationInventory, actual.VendorAndPurchasesPrefs.UsingMultiLocationInventory);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.UsingMultiLocationInventorySpecified, actual.VendorAndPurchasesPrefs.UsingMultiLocationInventorySpecified);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.DaysBillsAreDue, actual.VendorAndPurchasesPrefs.DaysBillsAreDue);
            //Assert.AreEqual(expected.VendorAndPurchasesPrefs.DaysBillsAreDueSpecified, actual.VendorAndPurchasesPrefs.DaysBillsAreDueSpecified);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DiscountAccountRef.name, actual.VendorAndPurchasesPrefs.DiscountAccountRef.name);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DiscountAccountRef.type, actual.VendorAndPurchasesPrefs.DiscountAccountRef.type);
            //    Assert.AreEqual(expected.VendorAndPurchasesPrefs.DiscountAccountRef.Value, actual.VendorAndPurchasesPrefs.DiscountAccountRef.Value);
            //Assert.AreEqual(expected.TimeTrackingPrefs.UseServices, actual.TimeTrackingPrefs.UseServices);
            //Assert.AreEqual(expected.TimeTrackingPrefs.UseServicesSpecified, actual.TimeTrackingPrefs.UseServicesSpecified);
            //    Assert.AreEqual(expected.TimeTrackingPrefs.DefaultTimeItem.name, actual.TimeTrackingPrefs.DefaultTimeItem.name);
            //    Assert.AreEqual(expected.TimeTrackingPrefs.DefaultTimeItem.type, actual.TimeTrackingPrefs.DefaultTimeItem.type);
            //    Assert.AreEqual(expected.TimeTrackingPrefs.DefaultTimeItem.Value, actual.TimeTrackingPrefs.DefaultTimeItem.Value);
            //Assert.AreEqual(expected.TimeTrackingPrefs.BillCustomers, actual.TimeTrackingPrefs.BillCustomers);
            //Assert.AreEqual(expected.TimeTrackingPrefs.BillCustomersSpecified, actual.TimeTrackingPrefs.BillCustomersSpecified);
            //Assert.AreEqual(expected.TimeTrackingPrefs.ShowBillRateToAll, actual.TimeTrackingPrefs.ShowBillRateToAll);
            //Assert.AreEqual(expected.TimeTrackingPrefs.ShowBillRateToAllSpecified, actual.TimeTrackingPrefs.ShowBillRateToAllSpecified);
            //Assert.AreEqual(expected.TimeTrackingPrefs.WorkWeekStartDateSpecified, actual.TimeTrackingPrefs.WorkWeekStartDateSpecified);
            //Assert.AreEqual(expected.TimeTrackingPrefs.TimeTrackingEnabled, actual.TimeTrackingPrefs.TimeTrackingEnabled);
            //Assert.AreEqual(expected.TimeTrackingPrefs.TimeTrackingEnabledSpecified, actual.TimeTrackingPrefs.TimeTrackingEnabledSpecified);
            //Assert.AreEqual(expected.TimeTrackingPrefs.MarkTimeEntriesBillable, actual.TimeTrackingPrefs.MarkTimeEntriesBillable);
            //Assert.AreEqual(expected.TimeTrackingPrefs.MarkTimeEntriesBillableSpecified, actual.TimeTrackingPrefs.MarkTimeEntriesBillableSpecified);
            //Assert.AreEqual(expected.TimeTrackingPrefs.MarkExpensesAsBillable, actual.TimeTrackingPrefs.MarkExpensesAsBillable);
            //Assert.AreEqual(expected.TimeTrackingPrefs.MarkExpensesAsBillableSpecified, actual.TimeTrackingPrefs.MarkExpensesAsBillableSpecified);
            Assert.AreEqual(expected.TaxPrefs.UsingSalesTax, actual.TaxPrefs.UsingSalesTax);
            Assert.AreEqual(expected.TaxPrefs.UsingSalesTaxSpecified, actual.TaxPrefs.UsingSalesTaxSpecified);
            //Assert.AreEqual(expected.TaxPrefs.AnyIntuitObject.name, actual.TaxPrefs.AnyIntuitObject.name);
            //Assert.AreEqual(expected.TaxPrefs.AnyIntuitObject.type, actual.TaxPrefs.AnyIntuitObject.type);
            //Assert.AreEqual(expected.TaxPrefs.AnyIntuitObject.Value, actual.TaxPrefs.AnyIntuitObject.Value);
            Assert.AreEqual(expected.TaxPrefs.PaySalesTaxSpecified, actual.TaxPrefs.PaySalesTaxSpecified);
            //    Assert.AreEqual(expected.TaxPrefs.NonTaxableSalesTaxCodeRef.name, actual.TaxPrefs.NonTaxableSalesTaxCodeRef.name);
            //    Assert.AreEqual(expected.TaxPrefs.NonTaxableSalesTaxCodeRef.type, actual.TaxPrefs.NonTaxableSalesTaxCodeRef.type);
            //    Assert.AreEqual(expected.TaxPrefs.NonTaxableSalesTaxCodeRef.Value, actual.TaxPrefs.NonTaxableSalesTaxCodeRef.Value);
            //    Assert.AreEqual(expected.TaxPrefs.TaxableSalesTaxCodeRef.name, actual.TaxPrefs.TaxableSalesTaxCodeRef.name);
            //    Assert.AreEqual(expected.TaxPrefs.TaxableSalesTaxCodeRef.type, actual.TaxPrefs.TaxableSalesTaxCodeRef.type);
            //    Assert.AreEqual(expected.TaxPrefs.TaxableSalesTaxCodeRef.Value, actual.TaxPrefs.TaxableSalesTaxCodeRef.Value);
            //Assert.AreEqual(expected.FinanceChargesPrefs.AnnualInterestRate, actual.FinanceChargesPrefs.AnnualInterestRate);
            //Assert.AreEqual(expected.FinanceChargesPrefs.AnnualInterestRateSpecified, actual.FinanceChargesPrefs.AnnualInterestRateSpecified);
            //Assert.AreEqual(expected.FinanceChargesPrefs.MinFinChrg, actual.FinanceChargesPrefs.MinFinChrg);
            //Assert.AreEqual(expected.FinanceChargesPrefs.MinFinChrgSpecified, actual.FinanceChargesPrefs.MinFinChrgSpecified);
            //Assert.AreEqual(expected.FinanceChargesPrefs.GracePeriod, actual.FinanceChargesPrefs.GracePeriod);
            //Assert.AreEqual(expected.FinanceChargesPrefs.CalcFinChrgFromTxnDate, actual.FinanceChargesPrefs.CalcFinChrgFromTxnDate);
            //Assert.AreEqual(expected.FinanceChargesPrefs.CalcFinChrgFromTxnDateSpecified, actual.FinanceChargesPrefs.CalcFinChrgFromTxnDateSpecified);
            //Assert.AreEqual(expected.FinanceChargesPrefs.AssessFinChrgForOverdueCharges, actual.FinanceChargesPrefs.AssessFinChrgForOverdueCharges);
            //Assert.AreEqual(expected.FinanceChargesPrefs.AssessFinChrgForOverdueChargesSpecified, actual.FinanceChargesPrefs.AssessFinChrgForOverdueChargesSpecified);
            //    Assert.AreEqual(expected.FinanceChargesPrefs.FinChrgAccountRef.name, actual.FinanceChargesPrefs.FinChrgAccountRef.name);
            //    Assert.AreEqual(expected.FinanceChargesPrefs.FinChrgAccountRef.type, actual.FinanceChargesPrefs.FinChrgAccountRef.type);
            //    Assert.AreEqual(expected.FinanceChargesPrefs.FinChrgAccountRef.Value, actual.FinanceChargesPrefs.FinChrgAccountRef.Value);
            Assert.AreEqual(expected.CurrencyPrefs.MultiCurrencyEnabled, actual.CurrencyPrefs.MultiCurrencyEnabled);
            Assert.AreEqual(expected.CurrencyPrefs.MultiCurrencyEnabledSpecified, actual.CurrencyPrefs.MultiCurrencyEnabledSpecified);
            //        Assert.AreEqual(expected.CurrencyPrefs.HomeCurrency.name, actual.CurrencyPrefs.HomeCurrency.name);
            //        Assert.AreEqual(expected.CurrencyPrefs.HomeCurrency.type, actual.CurrencyPrefs.HomeCurrency.type);
            //        Assert.AreEqual(expected.CurrencyPrefs.HomeCurrency.Value, actual.CurrencyPrefs.HomeCurrency.Value);
            //    Assert.AreEqual(expected.ReportPrefs.ReportBasisSpecified, actual.ReportPrefs.ReportBasisSpecified);
            //    Assert.AreEqual(expected.ReportPrefs.CalcAgingReportFromTxnDate, actual.ReportPrefs.CalcAgingReportFromTxnDate);
            //    Assert.AreEqual(expected.ReportPrefs.CalcAgingReportFromTxnDateSpecified, actual.ReportPrefs.CalcAgingReportFromTxnDateSpecified);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherPrefs, actual.OtherPrefs));
        }


        internal static void VerifyPreferencesSparseUpdate(Preferences expected, Preferences actual)
        {
            Assert.AreEqual(expected.EmailMessagesPrefs.InvoiceMessage.Subject, actual.EmailMessagesPrefs.InvoiceMessage.Subject);
        }



        internal static void VerifyUOM(UOM expected, UOM actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Abbrv, actual.Abbrv);
            Assert.AreEqual(expected.BaseTypeSpecified, actual.BaseTypeSpecified);
            Assert.IsTrue(Helper.CheckEqual(expected.ConvUnit, actual.ConvUnit));
        }

        internal static void VerifyUOMSparse(UOM expected, UOM actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Abbrv, actual.Abbrv);
        }

        internal static void VerifyTemplateName(TemplateName expected, TemplateName actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.TypeSpecified, actual.TypeSpecified);
        }

        internal static void VerifyTemplateNameSparse(TemplateName expected, TemplateName actual)
        {
            Assert.AreEqual(expected.Type, actual.Type);
        }


        internal static void VerifyAttachable(Attachable expected, Attachable actual)
        {
            //Assert.AreEqual(expected.FileName, actual.FileName);
            Assert.AreEqual(expected.FileAccessUri, actual.FileAccessUri);
            Assert.AreEqual(expected.TempDownloadUri, actual.TempDownloadUri);
            Assert.AreEqual(expected.Size, actual.Size);
            Assert.AreEqual(expected.SizeSpecified, actual.SizeSpecified);
            //Assert.AreEqual(expected.ContentType, actual.ContentType);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.Lat, actual.Lat);
            Assert.AreEqual(expected.Long, actual.Long);
            Assert.AreEqual(expected.PlaceName, actual.PlaceName);
            Assert.AreEqual(expected.Note, actual.Note);
            Assert.AreEqual(expected.Tag, actual.Tag);
            //Assert.AreEqual(expected.ThumbnailFileAccessUri, actual.ThumbnailFileAccessUri);
            //Assert.AreEqual(expected.ThumbnailTempDownloadUri, actual.ThumbnailTempDownloadUri);
            //Assert.AreEqual(expected.AttachableEx.Any, actual.AttachableEx.Any);
            VerifyAttachableRefs(expected.AttachableRef, actual.AttachableRef);
        }

        internal static void VerifyAttachableSparseUpdate(Attachable expected, Attachable actual)
        {
            Assert.AreEqual(expected.Note, actual.Note);
            Assert.AreEqual(expected.Tag, actual.Tag);
        }

        private static void VerifyAttachableRefs(AttachableRef[] expectedAttachableRef, AttachableRef[] actualAttachableRef)
        {
            if (expectedAttachableRef != null && actualAttachableRef != null)
            {
                int size = actualAttachableRef.Length;
                for (int i = 0; i < size; i++)
                {
                    Assert.AreEqual(expectedAttachableRef[i].EntityRef.Value, actualAttachableRef[i].EntityRef.Value);
                    //Assert.AreEqual(expectedAttachableRef[i].EntityRef.name, actualAttachableRef[i].EntityRef.name);
                    //Assert.AreEqual(expectedAttachableRef[i].EntityRef.type, actualAttachableRef[i].EntityRef.type);
                }
            }
        }

        internal static void VerifyStringTypeCustomFieldDefinition(StringTypeCustomFieldDefinition expected, StringTypeCustomFieldDefinition actual)
        {
            Assert.AreEqual(expected.DefaultString, actual.DefaultString);
            Assert.AreEqual(expected.RegularExpression, actual.RegularExpression);
            Assert.AreEqual(expected.MaxLength, actual.MaxLength);
            Assert.AreEqual(expected.MaxLengthSpecified, actual.MaxLengthSpecified);
            Assert.AreEqual(expected.EntityType, actual.EntityType);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Hidden, actual.Hidden);
            Assert.AreEqual(expected.Required, actual.Required);
        }



        internal static void VerifyNumberTypeCustomFieldDefinition(NumberTypeCustomFieldDefinition expected, NumberTypeCustomFieldDefinition actual)
        {
            Assert.AreEqual(expected.DefaultValue, actual.DefaultValue);
            Assert.AreEqual(expected.DefaultValueSpecified, actual.DefaultValueSpecified);
            Assert.AreEqual(expected.MinValue, actual.MinValue);
            Assert.AreEqual(expected.MinValueSpecified, actual.MinValueSpecified);
            Assert.AreEqual(expected.MaxValue, actual.MaxValue);
            Assert.AreEqual(expected.MaxValueSpecified, actual.MaxValueSpecified);
            Assert.AreEqual(expected.EntityType, actual.EntityType);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Hidden, actual.Hidden);
            Assert.AreEqual(expected.Required, actual.Required);
        }



        internal static void VerifyDateTypeCustomFieldDefinition(DateTypeCustomFieldDefinition expected, DateTypeCustomFieldDefinition actual)
        {
            Assert.AreEqual(expected.DefaultDate, actual.DefaultDate);
            Assert.AreEqual(expected.DefaultDateSpecified, actual.DefaultDateSpecified);
            Assert.AreEqual(expected.MinDate, actual.MinDate);
            Assert.AreEqual(expected.MinDateSpecified, actual.MinDateSpecified);
            Assert.AreEqual(expected.MaxDate, actual.MaxDate);
            Assert.AreEqual(expected.MaxDateSpecified, actual.MaxDateSpecified);
            Assert.AreEqual(expected.EntityType, actual.EntityType);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Hidden, actual.Hidden);
            Assert.AreEqual(expected.Required, actual.Required);
        }



        internal static void VerifyBooleanTypeCustomFieldDefinition(BooleanTypeCustomFieldDefinition expected, BooleanTypeCustomFieldDefinition actual)
        {
            Assert.AreEqual(expected.DefaultValue, actual.DefaultValue);
            Assert.AreEqual(expected.DefaultValueSpecified, actual.DefaultValueSpecified);
            Assert.AreEqual(expected.EntityType, actual.EntityType);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Hidden, actual.Hidden);
            Assert.AreEqual(expected.Required, actual.Required);
        }



        internal static void VerifyNameBase(NameBase expected, NameBase actual)
        {
            Assert.AreEqual(expected.Organization, actual.Organization);
            Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }



        internal static void VerifyCustomer(Customer expected, Customer actual)
        {
            Assert.AreEqual(expected.Taxable, actual.Taxable);
            Assert.AreEqual(expected.TaxableSpecified, actual.TaxableSpecified);
            Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            Assert.AreEqual(expected.ContactName, actual.ContactName);
            Assert.AreEqual(expected.AltContactName, actual.AltContactName);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.Job, actual.Job);
            Assert.AreEqual(expected.JobSpecified, actual.JobSpecified);
            Assert.AreEqual(expected.BillWithParent, actual.BillWithParent);
            Assert.AreEqual(expected.BillWithParentSpecified, actual.BillWithParentSpecified);
            //Assert.AreEqual(expected.RootCustomerRef.name, actual.RootCustomerRef.name);
            //Assert.AreEqual(expected.RootCustomerRef.type, actual.RootCustomerRef.type);
            //Assert.AreEqual(expected.RootCustomerRef.Value, actual.RootCustomerRef.Value);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.Level, actual.Level);
            Assert.AreEqual(expected.LevelSpecified, actual.LevelSpecified);
            //Assert.AreEqual(expected.CustomerTypeRef.name, actual.CustomerTypeRef.name);
            //Assert.AreEqual(expected.CustomerTypeRef.type, actual.CustomerTypeRef.type);
            //Assert.AreEqual(expected.CustomerTypeRef.Value, actual.CustomerTypeRef.Value);
            //Assert.AreEqual(expected.SalesTermRef.name, actual.SalesTermRef.name);
            //Assert.AreEqual(expected.SalesTermRef.type, actual.SalesTermRef.type);
            //Assert.AreEqual(expected.SalesTermRef.Value, actual.SalesTermRef.Value);
            //Assert.AreEqual(expected.SalesRepRef.name, actual.SalesRepRef.name);
            //Assert.AreEqual(expected.SalesRepRef.type, actual.SalesRepRef.type);
            //Assert.AreEqual(expected.SalesRepRef.Value, actual.SalesRepRef.Value);
            //Assert.AreEqual(expected.AnyIntuitObject.name, actual.AnyIntuitObject.name);
            //Assert.AreEqual(expected.AnyIntuitObject.type, actual.AnyIntuitObject.type);
            //Assert.AreEqual(expected.AnyIntuitObject.Value, actual.AnyIntuitObject.Value);
            //Assert.AreEqual(expected.PaymentMethodRef.name, actual.PaymentMethodRef.name);
            //Assert.AreEqual(expected.PaymentMethodRef.type, actual.PaymentMethodRef.type);
            //Assert.AreEqual(expected.PaymentMethodRef.Value, actual.PaymentMethodRef.Value);
            //Assert.AreEqual(expected.CCDetail.Number, actual.CCDetail.Number);
            //Assert.AreEqual(expected.CCDetail.Type, actual.CCDetail.Type);
            //Assert.AreEqual(expected.CCDetail.NameOnAcct, actual.CCDetail.NameOnAcct);
            //Assert.AreEqual(expected.CCDetail.CcExpiryMonth, actual.CCDetail.CcExpiryMonth);
            //Assert.AreEqual(expected.CCDetail.CcExpiryMonthSpecified, actual.CCDetail.CcExpiryMonthSpecified);
            //Assert.AreEqual(expected.CCDetail.CcExpiryYear, actual.CCDetail.CcExpiryYear);
            //Assert.AreEqual(expected.CCDetail.CcExpiryYearSpecified, actual.CCDetail.CcExpiryYearSpecified);
            //Assert.AreEqual(expected.CCDetail.BillAddrStreet, actual.CCDetail.BillAddrStreet);
            //Assert.AreEqual(expected.CCDetail.PostalCode, actual.CCDetail.PostalCode);
            //Assert.AreEqual(expected.CCDetail.CommercialCardCode, actual.CCDetail.CommercialCardCode);
            //Assert.AreEqual(expected.CCDetail.CCTxnModeSpecified, actual.CCDetail.CCTxnModeSpecified);
            //Assert.AreEqual(expected.CCDetail.CCTxnTypeSpecified, actual.CCDetail.CCTxnTypeSpecified);
            //Assert.AreEqual(expected.CCDetail.PrevCCTransId, actual.CCDetail.PrevCCTransId);
            //    Assert.AreEqual(expected.CCDetail.CreditCardChargeInfoEx.Any, actual.CCDetail.CreditCardChargeInfoEx.Any);
            //Assert.AreEqual(expected.PriceLevelRef.name, actual.PriceLevelRef.name);
            //Assert.AreEqual(expected.PriceLevelRef.type, actual.PriceLevelRef.type);
            //Assert.AreEqual(expected.PriceLevelRef.Value, actual.PriceLevelRef.Value);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            Assert.AreEqual(expected.OpenBalanceDate, actual.OpenBalanceDate);
            Assert.AreEqual(expected.OpenBalanceDateSpecified, actual.OpenBalanceDateSpecified);
            Assert.AreEqual(expected.BalanceWithJobs, actual.BalanceWithJobs);
            Assert.AreEqual(expected.BalanceWithJobsSpecified, actual.BalanceWithJobsSpecified);
            Assert.AreEqual(expected.CreditLimit, actual.CreditLimit);
            Assert.AreEqual(expected.CreditLimitSpecified, actual.CreditLimitSpecified);
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            Assert.AreEqual(expected.OverDueBalance, actual.OverDueBalance);
            Assert.AreEqual(expected.OverDueBalanceSpecified, actual.OverDueBalanceSpecified);
            Assert.AreEqual(expected.TotalRevenue, actual.TotalRevenue);
            Assert.AreEqual(expected.TotalRevenueSpecified, actual.TotalRevenueSpecified);
            Assert.AreEqual(expected.TotalExpense, actual.TotalExpense);
            Assert.AreEqual(expected.TotalExpenseSpecified, actual.TotalExpenseSpecified);
            Assert.AreEqual(expected.PreferredDeliveryMethod, actual.PreferredDeliveryMethod);
            Assert.AreEqual(expected.ResaleNum, actual.ResaleNum);
            //Assert.AreEqual(expected.JobInfo.StatusSpecified, actual.JobInfo.StatusSpecified);
            //Assert.AreEqual(expected.JobInfo.StartDate, actual.JobInfo.StartDate);
            //Assert.AreEqual(expected.JobInfo.StartDateSpecified, actual.JobInfo.StartDateSpecified);
            //Assert.AreEqual(expected.JobInfo.ProjectedEndDate, actual.JobInfo.ProjectedEndDate);
            //Assert.AreEqual(expected.JobInfo.ProjectedEndDateSpecified, actual.JobInfo.ProjectedEndDateSpecified);
            //Assert.AreEqual(expected.JobInfo.EndDate, actual.JobInfo.EndDate);
            //Assert.AreEqual(expected.JobInfo.EndDateSpecified, actual.JobInfo.EndDateSpecified);
            //Assert.AreEqual(expected.JobInfo.Description, actual.JobInfo.Description);
            //    Assert.AreEqual(expected.JobInfo.JobTypeRef.name, actual.JobInfo.JobTypeRef.name);
            //    Assert.AreEqual(expected.JobInfo.JobTypeRef.type, actual.JobInfo.JobTypeRef.type);
            //    Assert.AreEqual(expected.JobInfo.JobTypeRef.Value, actual.JobInfo.JobTypeRef.Value);
            //Assert.AreEqual(expected.CustomerEx.Any, actual.CustomerEx.Any);
            Assert.AreEqual(expected.Organization, actual.Organization);
            Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }


        internal static void VerifyCustomerFrance(Customer expected, Customer actual)
        {

            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);

        }


        internal static void VerifyCustomerSparseUpdate(Customer expected, Customer actual)
        {
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
        }


        internal static void VerifyUser(User expected, User actual)
        {
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.IsTrue(Helper.CheckEqual(expected.EmailAddr, actual.EmailAddr));
            //Assert.IsTrue(Helper.CheckEqual(expected.Addr, actual.Addr));
            //Assert.IsTrue(Helper.CheckEqual(expected.PhoneNumber, actual.PhoneNumber));
            //Assert.AreEqual(expected.LocaleCountry, actual.LocaleCountry);
            //Assert.AreEqual(expected.LocaleLanguage, actual.LocaleLanguage);
            //Assert.AreEqual(expected.LocalePostalCode, actual.LocalePostalCode);
            //Assert.AreEqual(expected.LocaleTimeZone, actual.LocaleTimeZone);
            //Assert.IsTrue(Helper.CheckEqual(expected.NameValueAttr, actual.NameValueAttr));
        }


        internal static void VerifyUserSparse(User expected, User actual)
        {
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
        }


        internal static void VerifyVendor(Vendor expected, Vendor actual)
        {
            Assert.AreEqual(expected.ContactName, actual.ContactName);
            Assert.AreEqual(expected.AltContactName, actual.AltContactName);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            Assert.AreEqual(expected.TaxCountry, actual.TaxCountry);
            //Assert.AreEqual(expected.TaxIdentifier, actual.TaxIdentifier);
            Assert.AreEqual(expected.BusinessNumber, actual.BusinessNumber);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            //Assert.AreEqual(expected.VendorTypeRef.name, actual.VendorTypeRef.name);
            //Assert.AreEqual(expected.VendorTypeRef.type, actual.VendorTypeRef.type);
            //Assert.AreEqual(expected.VendorTypeRef.Value, actual.VendorTypeRef.Value);
            //Assert.AreEqual(expected.TermRef.name, actual.TermRef.name);
            //Assert.AreEqual(expected.TermRef.type, actual.TermRef.type);
            //Assert.AreEqual(expected.TermRef.Value, actual.TermRef.Value);
            //Assert.AreEqual(expected.PrefillAccountRef.name, actual.PrefillAccountRef.name);
            //Assert.AreEqual(expected.PrefillAccountRef.type, actual.PrefillAccountRef.type);
            //Assert.AreEqual(expected.PrefillAccountRef.Value, actual.PrefillAccountRef.Value);
            Assert.AreEqual(expected.Balance, actual.Balance);
            Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.OpenBalanceDate, actual.OpenBalanceDate);
            //Assert.AreEqual(expected.OpenBalanceDateSpecified, actual.OpenBalanceDateSpecified);
            Assert.AreEqual(expected.CreditLimit, actual.CreditLimit);
            Assert.AreEqual(expected.CreditLimitSpecified, actual.CreditLimitSpecified);
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            Assert.AreEqual(expected.Vendor1099, actual.Vendor1099);
            Assert.AreEqual(expected.Vendor1099Specified, actual.Vendor1099Specified);
            Assert.AreEqual(expected.T4AEligible, actual.T4AEligible);
            Assert.AreEqual(expected.T4AEligibleSpecified, actual.T4AEligibleSpecified);
            Assert.AreEqual(expected.T5018Eligible, actual.T5018Eligible);
            Assert.AreEqual(expected.T5018EligibleSpecified, actual.T5018EligibleSpecified);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.VendorEx.Any, actual.VendorEx.Any);
            Assert.AreEqual(expected.Organization, actual.Organization);
            Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }


        internal static void VerifyVendorFrance(Vendor expected, Vendor actual)
        {

            Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);

            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            Assert.AreEqual(expected.UserId, actual.UserId);

        }


        internal static void VerifyVendorSparseUpdate(Vendor expected, Vendor actual)
        {
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
        }



        internal static void VerifyCustomerType(CustomerType expected, CustomerType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
        }


        internal static void VerifyCustomerTypeSparse(CustomerType expected, CustomerType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }

        internal static void VerifyEmployee(Employee expected, Employee actual)
        {
            //Assert.AreEqual(expected.EmployeeType, actual.EmployeeType);
            //Assert.AreEqual(expected.EmployeeNumber, actual.EmployeeNumber);
            // Assert.AreEqual(expected.SSN, actual.SSN);
            //    Assert.AreEqual(expected.PrimaryAddr.Line1, actual.PrimaryAddr.Line1);
            //    Assert.AreEqual(expected.PrimaryAddr.Line2, actual.PrimaryAddr.Line2);
            //    Assert.AreEqual(expected.PrimaryAddr.Line3, actual.PrimaryAddr.Line3);
            //    Assert.AreEqual(expected.PrimaryAddr.Line4, actual.PrimaryAddr.Line4);
            //    Assert.AreEqual(expected.PrimaryAddr.Line5, actual.PrimaryAddr.Line5);
            //    Assert.AreEqual(expected.PrimaryAddr.City, actual.PrimaryAddr.City);
            //    Assert.AreEqual(expected.PrimaryAddr.Country, actual.PrimaryAddr.Country);
            //    Assert.AreEqual(expected.PrimaryAddr.CountryCode, actual.PrimaryAddr.CountryCode);
            //    Assert.AreEqual(expected.PrimaryAddr.CountrySubDivisionCode, actual.PrimaryAddr.CountrySubDivisionCode);
            //    Assert.AreEqual(expected.PrimaryAddr.PostalCode, actual.PrimaryAddr.PostalCode);
            //    Assert.AreEqual(expected.PrimaryAddr.PostalCodeSuffix, actual.PrimaryAddr.PostalCodeSuffix);
            //    Assert.AreEqual(expected.PrimaryAddr.Lat, actual.PrimaryAddr.Lat);
            //    Assert.AreEqual(expected.PrimaryAddr.Long, actual.PrimaryAddr.Long);
            //    Assert.AreEqual(expected.PrimaryAddr.Tag, actual.PrimaryAddr.Tag);
            //    Assert.AreEqual(expected.PrimaryAddr.Note, actual.PrimaryAddr.Note);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            //Assert.AreEqual(expected.BillableTime, actual.BillableTime);
            //Assert.AreEqual(expected.BillableTimeSpecified, actual.BillableTimeSpecified);
            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
            Assert.AreEqual(expected.BirthDateSpecified, actual.BirthDateSpecified);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.GenderSpecified, actual.GenderSpecified);
            Assert.AreEqual(expected.HiredDate, actual.HiredDate);
            Assert.AreEqual(expected.HiredDateSpecified, actual.HiredDateSpecified);
            Assert.AreEqual(expected.ReleasedDate, actual.ReleasedDate);
            Assert.AreEqual(expected.ReleasedDateSpecified, actual.ReleasedDateSpecified);
            //Assert.AreEqual(expected.UseTimeEntrySpecified, actual.UseTimeEntrySpecified);
            //    Assert.AreEqual(expected.EmployeeEx.Any, actual.EmployeeEx.Any);
            //Assert.AreEqual(expected.Organization, actual.Organization);
            //Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            //Assert.AreEqual(expected.Suffix, actual.Suffix);
            //Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            //Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            //Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //    Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            //    Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            //    Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            //    Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            //    Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            //    Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            //    Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            //    Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            //    Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            //    Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            //    Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            //    Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            //    Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            //    Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            //    Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            //    Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            //    Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            //    Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            //    Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            //    Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            //    Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            //    Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            //    Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            //    Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            //    Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            //    Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            //    Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            //    Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            //    Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            //    Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            //    Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            //    Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            //    Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            //    Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            //    Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            //    Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            //    Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            //    Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            //    Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            //    Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            //    Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            //    Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            //    Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            //    Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }


        internal static void VerifyEmployeeSparseUpdate(Employee expected, Employee actual)
        {
            Assert.AreEqual(expected.GivenName, actual.GivenName);
        }


        internal static void VerifyJobType(JobType expected, JobType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
        }


        internal static void VerifyJobTypeSparse(JobType expected, JobType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }

        internal static void VerifyOtherName(OtherName expected, OtherName actual)
        {

            List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
            PhysicalAddress otherAddr = new PhysicalAddress();
            otherAddr.Line1 = "Other address Line1";
            otherAddr.Line2 = "Other address Line2";
            //otherAddr.Line3 = "Line3";
            //otherAddr.Line4 = "Line4";
            //otherAddr.Line5 = "Line5";
            otherAddr.City = "City";
            otherAddr.Country = "Country";
            //otherAddr.CountryCode = "CountryCode";
            //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
            otherAddr.PostalCode = "7438957";
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            Assert.AreEqual(expected.PrimaryAddr.Line1, actual.PrimaryAddr.Line1);
            Assert.AreEqual(expected.PrimaryAddr.Line2, actual.PrimaryAddr.Line2);
            Assert.AreEqual(expected.PrimaryAddr.Line3, actual.PrimaryAddr.Line3);
            Assert.AreEqual(expected.PrimaryAddr.Line4, actual.PrimaryAddr.Line4);
            Assert.AreEqual(expected.PrimaryAddr.Line5, actual.PrimaryAddr.Line5);
            Assert.AreEqual(expected.PrimaryAddr.City, actual.PrimaryAddr.City);
            Assert.AreEqual(expected.PrimaryAddr.Country, actual.PrimaryAddr.Country);
            Assert.AreEqual(expected.PrimaryAddr.CountryCode, actual.PrimaryAddr.CountryCode);
            Assert.AreEqual(expected.PrimaryAddr.CountrySubDivisionCode, actual.PrimaryAddr.CountrySubDivisionCode);
            Assert.AreEqual(expected.PrimaryAddr.PostalCode, actual.PrimaryAddr.PostalCode);
            Assert.AreEqual(expected.PrimaryAddr.PostalCodeSuffix, actual.PrimaryAddr.PostalCodeSuffix);
            Assert.AreEqual(expected.PrimaryAddr.Lat, actual.PrimaryAddr.Lat);
            Assert.AreEqual(expected.PrimaryAddr.Long, actual.PrimaryAddr.Long);
            Assert.AreEqual(expected.PrimaryAddr.Tag, actual.PrimaryAddr.Tag);
            Assert.AreEqual(expected.PrimaryAddr.Note, actual.PrimaryAddr.Note);
            Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            //Assert.AreEqual(expected.OtherNameEx.Any, actual.OtherNameEx.Any);
            Assert.AreEqual(expected.Organization, actual.Organization);
            Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            //Assert.AreEqual(expected.Suffix, actual.Suffix);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            //Assert.AreEqual(expected.UserId, actual.UserId);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            //Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            //Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            //Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            //Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            //Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            //Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            //Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            //Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            //Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            //Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            //Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            //Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            //Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            //Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            //Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            //Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            //Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            //Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            //    Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }


        internal static void VerifyOtherNameSparse(OtherName expected, OtherName actual)
        {
            Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            Assert.AreEqual(expected.GivenName, actual.GivenName);
        }

        internal static void VerifyVendorType(VendorType expected, VendorType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
        }

        internal static void VerifyVendorTypeSparse(VendorType expected, VendorType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
        }

        internal static void VerifyTaxAgency(TaxAgency expected, TaxAgency actual)
        {
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            //Assert.AreEqual(expected.SalesTaxCodeRef.name, actual.SalesTaxCodeRef.name);
            //Assert.AreEqual(expected.SalesTaxCodeRef.type, actual.SalesTaxCodeRef.type);
            //Assert.AreEqual(expected.SalesTaxCodeRef.Value, actual.SalesTaxCodeRef.Value);
            //Assert.AreEqual(expected.SalesTaxCountry, actual.SalesTaxCountry);
            //Assert.AreEqual(expected.SalesTaxReturnRef.name, actual.SalesTaxReturnRef.name);
            //Assert.AreEqual(expected.SalesTaxReturnRef.type, actual.SalesTaxReturnRef.type);
            //Assert.AreEqual(expected.SalesTaxReturnRef.Value, actual.SalesTaxReturnRef.Value);
            //Assert.AreEqual(expected.TaxRegistrationNumber, actual.TaxRegistrationNumber);
            //Assert.AreEqual(expected.ReportingPeriod, actual.ReportingPeriod);
            //Assert.AreEqual(expected.TaxTrackedOnPurchases, actual.TaxTrackedOnPurchases);
            //Assert.AreEqual(expected.TaxTrackedOnPurchasesSpecified, actual.TaxTrackedOnPurchasesSpecified);
            //Assert.AreEqual(expected.TaxOnPurchasesAccountRef.name, actual.TaxOnPurchasesAccountRef.name);
            //Assert.AreEqual(expected.TaxOnPurchasesAccountRef.type, actual.TaxOnPurchasesAccountRef.type);
            //Assert.AreEqual(expected.TaxOnPurchasesAccountRef.Value, actual.TaxOnPurchasesAccountRef.Value);
            //Assert.AreEqual(expected.TaxTrackedOnSales, actual.TaxTrackedOnSales);
            //Assert.AreEqual(expected.TaxTrackedOnSalesSpecified, actual.TaxTrackedOnSalesSpecified);
            //Assert.AreEqual(expected.TaxTrackedOnSalesAccountRef.name, actual.TaxTrackedOnSalesAccountRef.name);
            //Assert.AreEqual(expected.TaxTrackedOnSalesAccountRef.type, actual.TaxTrackedOnSalesAccountRef.type);
            //Assert.AreEqual(expected.TaxTrackedOnSalesAccountRef.Value, actual.TaxTrackedOnSalesAccountRef.Value);
            //Assert.AreEqual(expected.TaxOnTax, actual.TaxOnTax);
            //Assert.AreEqual(expected.TaxOnTaxSpecified, actual.TaxOnTaxSpecified);
            //Assert.AreEqual(expected.TaxAgencyExt.Any, actual.TaxAgencyExt.Any);
            //Assert.AreEqual(expected.ContactName, actual.ContactName);
            //Assert.AreEqual(expected.AltContactName, actual.AltContactName);
            //Assert.AreEqual(expected.Notes, actual.Notes);
            //Assert.AreEqual(expected.BillAddr.Line1, actual.BillAddr.Line1);
            //Assert.AreEqual(expected.BillAddr.Line2, actual.BillAddr.Line2);
            //Assert.AreEqual(expected.BillAddr.Line3, actual.BillAddr.Line3);
            //Assert.AreEqual(expected.BillAddr.Line4, actual.BillAddr.Line4);
            //Assert.AreEqual(expected.BillAddr.Line5, actual.BillAddr.Line5);
            //Assert.AreEqual(expected.BillAddr.City, actual.BillAddr.City);
            //Assert.AreEqual(expected.BillAddr.Country, actual.BillAddr.Country);
            //Assert.AreEqual(expected.BillAddr.CountryCode, actual.BillAddr.CountryCode);
            //Assert.AreEqual(expected.BillAddr.CountrySubDivisionCode, actual.BillAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.BillAddr.PostalCode, actual.BillAddr.PostalCode);
            //Assert.AreEqual(expected.BillAddr.PostalCodeSuffix, actual.BillAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.BillAddr.Lat, actual.BillAddr.Lat);
            //Assert.AreEqual(expected.BillAddr.Long, actual.BillAddr.Long);
            //Assert.AreEqual(expected.BillAddr.Tag, actual.BillAddr.Tag);
            //Assert.AreEqual(expected.BillAddr.Note, actual.BillAddr.Note);
            //Assert.AreEqual(expected.ShipAddr.Line1, actual.ShipAddr.Line1);
            //Assert.AreEqual(expected.ShipAddr.Line2, actual.ShipAddr.Line2);
            //Assert.AreEqual(expected.ShipAddr.Line3, actual.ShipAddr.Line3);
            //Assert.AreEqual(expected.ShipAddr.Line4, actual.ShipAddr.Line4);
            //Assert.AreEqual(expected.ShipAddr.Line5, actual.ShipAddr.Line5);
            //Assert.AreEqual(expected.ShipAddr.City, actual.ShipAddr.City);
            //Assert.AreEqual(expected.ShipAddr.Country, actual.ShipAddr.Country);
            //Assert.AreEqual(expected.ShipAddr.CountryCode, actual.ShipAddr.CountryCode);
            //Assert.AreEqual(expected.ShipAddr.CountrySubDivisionCode, actual.ShipAddr.CountrySubDivisionCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCode, actual.ShipAddr.PostalCode);
            //Assert.AreEqual(expected.ShipAddr.PostalCodeSuffix, actual.ShipAddr.PostalCodeSuffix);
            //Assert.AreEqual(expected.ShipAddr.Lat, actual.ShipAddr.Lat);
            //Assert.AreEqual(expected.ShipAddr.Long, actual.ShipAddr.Long);
            //Assert.AreEqual(expected.ShipAddr.Tag, actual.ShipAddr.Tag);
            //Assert.AreEqual(expected.ShipAddr.Note, actual.ShipAddr.Note);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherAddr, actual.OtherAddr));
            //Assert.AreEqual(expected.TaxCountry, actual.TaxCountry);
            //Assert.AreEqual(expected.TaxIdentifier, actual.TaxIdentifier);
            //Assert.AreEqual(expected.BusinessNumber, actual.BusinessNumber);
            //Assert.AreEqual(expected.ParentRef.name, actual.ParentRef.name);
            //Assert.AreEqual(expected.ParentRef.type, actual.ParentRef.type);
            //Assert.AreEqual(expected.ParentRef.Value, actual.ParentRef.Value);
            //Assert.AreEqual(expected.VendorTypeRef.name, actual.VendorTypeRef.name);
            //Assert.AreEqual(expected.VendorTypeRef.type, actual.VendorTypeRef.type);
            //Assert.AreEqual(expected.VendorTypeRef.Value, actual.VendorTypeRef.Value);
            //Assert.AreEqual(expected.TermRef.name, actual.TermRef.name);
            //Assert.AreEqual(expected.TermRef.type, actual.TermRef.type);
            //Assert.AreEqual(expected.TermRef.Value, actual.TermRef.Value);
            //Assert.AreEqual(expected.PrefillAccountRef.name, actual.PrefillAccountRef.name);
            //Assert.AreEqual(expected.PrefillAccountRef.type, actual.PrefillAccountRef.type);
            //Assert.AreEqual(expected.PrefillAccountRef.Value, actual.PrefillAccountRef.Value);
            //Assert.AreEqual(expected.Balance, actual.Balance);
            //Assert.AreEqual(expected.BalanceSpecified, actual.BalanceSpecified);
            //Assert.AreEqual(expected.OpenBalanceDate, actual.OpenBalanceDate);
            //Assert.AreEqual(expected.OpenBalanceDateSpecified, actual.OpenBalanceDateSpecified);
            //Assert.AreEqual(expected.CreditLimit, actual.CreditLimit);
            //Assert.AreEqual(expected.CreditLimitSpecified, actual.CreditLimitSpecified);
            //Assert.AreEqual(expected.AcctNum, actual.AcctNum);
            //Assert.AreEqual(expected.Vendor1099, actual.Vendor1099);
            //Assert.AreEqual(expected.Vendor1099Specified, actual.Vendor1099Specified);
            //Assert.AreEqual(expected.T4AEligible, actual.T4AEligible);
            //Assert.AreEqual(expected.T4AEligibleSpecified, actual.T4AEligibleSpecified);
            //Assert.AreEqual(expected.T5018Eligible, actual.T5018Eligible);
            //Assert.AreEqual(expected.T5018EligibleSpecified, actual.T5018EligibleSpecified);
            //Assert.AreEqual(expected.CurrencyRef.name, actual.CurrencyRef.name);
            //Assert.AreEqual(expected.CurrencyRef.type, actual.CurrencyRef.type);
            //Assert.AreEqual(expected.CurrencyRef.Value, actual.CurrencyRef.Value);
            //Assert.AreEqual(expected.VendorEx.Any, actual.VendorEx.Any);
            //Assert.AreEqual(expected.Organization, actual.Organization);
            //Assert.AreEqual(expected.OrganizationSpecified, actual.OrganizationSpecified);
            //Assert.AreEqual(expected.Title, actual.Title);
            //Assert.AreEqual(expected.GivenName, actual.GivenName);
            //Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            //Assert.AreEqual(expected.FamilyName, actual.FamilyName);
            //Assert.AreEqual(expected.Suffix, actual.Suffix);
            //Assert.AreEqual(expected.FullyQualifiedName, actual.FullyQualifiedName);
            //Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            //Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            //Assert.AreEqual(expected.PrintOnCheckName, actual.PrintOnCheckName);
            //Assert.AreEqual(expected.UserId, actual.UserId);
            //Assert.AreEqual(expected.Active, actual.Active);
            //Assert.AreEqual(expected.ActiveSpecified, actual.ActiveSpecified);
            //Assert.AreEqual(expected.PrimaryPhone.DeviceType, actual.PrimaryPhone.DeviceType);
            //Assert.AreEqual(expected.PrimaryPhone.CountryCode, actual.PrimaryPhone.CountryCode);
            //Assert.AreEqual(expected.PrimaryPhone.AreaCode, actual.PrimaryPhone.AreaCode);
            //Assert.AreEqual(expected.PrimaryPhone.ExchangeCode, actual.PrimaryPhone.ExchangeCode);
            //Assert.AreEqual(expected.PrimaryPhone.Extension, actual.PrimaryPhone.Extension);
            //Assert.AreEqual(expected.PrimaryPhone.FreeFormNumber, actual.PrimaryPhone.FreeFormNumber);
            //Assert.AreEqual(expected.PrimaryPhone.Default, actual.PrimaryPhone.Default);
            //Assert.AreEqual(expected.PrimaryPhone.DefaultSpecified, actual.PrimaryPhone.DefaultSpecified);
            //Assert.AreEqual(expected.PrimaryPhone.Tag, actual.PrimaryPhone.Tag);
            //Assert.AreEqual(expected.AlternatePhone.DeviceType, actual.AlternatePhone.DeviceType);
            //Assert.AreEqual(expected.AlternatePhone.CountryCode, actual.AlternatePhone.CountryCode);
            //Assert.AreEqual(expected.AlternatePhone.AreaCode, actual.AlternatePhone.AreaCode);
            //Assert.AreEqual(expected.AlternatePhone.ExchangeCode, actual.AlternatePhone.ExchangeCode);
            //Assert.AreEqual(expected.AlternatePhone.Extension, actual.AlternatePhone.Extension);
            //Assert.AreEqual(expected.AlternatePhone.FreeFormNumber, actual.AlternatePhone.FreeFormNumber);
            //Assert.AreEqual(expected.AlternatePhone.Default, actual.AlternatePhone.Default);
            //Assert.AreEqual(expected.AlternatePhone.DefaultSpecified, actual.AlternatePhone.DefaultSpecified);
            //Assert.AreEqual(expected.AlternatePhone.Tag, actual.AlternatePhone.Tag);
            //Assert.AreEqual(expected.Mobile.DeviceType, actual.Mobile.DeviceType);
            //Assert.AreEqual(expected.Mobile.CountryCode, actual.Mobile.CountryCode);
            //Assert.AreEqual(expected.Mobile.AreaCode, actual.Mobile.AreaCode);
            //Assert.AreEqual(expected.Mobile.ExchangeCode, actual.Mobile.ExchangeCode);
            //Assert.AreEqual(expected.Mobile.Extension, actual.Mobile.Extension);
            //Assert.AreEqual(expected.Mobile.FreeFormNumber, actual.Mobile.FreeFormNumber);
            //Assert.AreEqual(expected.Mobile.Default, actual.Mobile.Default);
            //Assert.AreEqual(expected.Mobile.DefaultSpecified, actual.Mobile.DefaultSpecified);
            //Assert.AreEqual(expected.Mobile.Tag, actual.Mobile.Tag);
            //Assert.AreEqual(expected.Fax.DeviceType, actual.Fax.DeviceType);
            //Assert.AreEqual(expected.Fax.CountryCode, actual.Fax.CountryCode);
            //Assert.AreEqual(expected.Fax.AreaCode, actual.Fax.AreaCode);
            //Assert.AreEqual(expected.Fax.ExchangeCode, actual.Fax.ExchangeCode);
            //Assert.AreEqual(expected.Fax.Extension, actual.Fax.Extension);
            //Assert.AreEqual(expected.Fax.FreeFormNumber, actual.Fax.FreeFormNumber);
            //Assert.AreEqual(expected.Fax.Default, actual.Fax.Default);
            //Assert.AreEqual(expected.Fax.DefaultSpecified, actual.Fax.DefaultSpecified);
            //Assert.AreEqual(expected.Fax.Tag, actual.Fax.Tag);
            //Assert.AreEqual(expected.PrimaryEmailAddr.Address, actual.PrimaryEmailAddr.Address);
            //Assert.AreEqual(expected.PrimaryEmailAddr.Default, actual.PrimaryEmailAddr.Default);
            //Assert.AreEqual(expected.PrimaryEmailAddr.DefaultSpecified, actual.PrimaryEmailAddr.DefaultSpecified);
            //Assert.AreEqual(expected.PrimaryEmailAddr.Tag, actual.PrimaryEmailAddr.Tag);
            //Assert.AreEqual(expected.WebAddr.URI, actual.WebAddr.URI);
            //Assert.AreEqual(expected.WebAddr.Default, actual.WebAddr.Default);
            //Assert.AreEqual(expected.WebAddr.DefaultSpecified, actual.WebAddr.DefaultSpecified);
            //Assert.AreEqual(expected.WebAddr.Tag, actual.WebAddr.Tag);
            //Assert.IsTrue(Helper.CheckEqual(expected.OtherContactInfo, actual.OtherContactInfo));
            //Assert.AreEqual(expected.DefaultTaxCodeRef.name, actual.DefaultTaxCodeRef.name);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.type, actual.DefaultTaxCodeRef.type);
            //Assert.AreEqual(expected.DefaultTaxCodeRef.Value, actual.DefaultTaxCodeRef.Value);
        }


        internal static void VerifyTaxAgencySparse(TaxAgency expected, TaxAgency actual)
        {
            Assert.AreEqual(expected.Notes, actual.Notes);
        }

        internal static void VerifyStatus(Status expected, Status actual)
        {
            Assert.AreEqual(expected.RequestId, actual.RequestId);
            Assert.AreEqual(expected.BatchId, actual.BatchId);
            Assert.AreEqual(expected.ObjectType, actual.ObjectType);
            Assert.AreEqual(expected.StateCode, actual.StateCode);
            Assert.AreEqual(expected.StateDesc, actual.StateDesc);
            Assert.AreEqual(expected.MessageCode, actual.MessageCode);
            Assert.AreEqual(expected.MessageDesc, actual.MessageDesc);
        }


        internal static void VerifyStatusSparse(Status expected, Status actual)
        {
            Assert.AreEqual(expected.StateCode, actual.StateCode);
            Assert.AreEqual(expected.StateDesc, actual.StateDesc);
        }

        internal static void VerifySyncActivity(SyncActivity expected, SyncActivity actual)
        {
            //Assert.AreEqual(expected.LatestUploadDateTime, actual.LatestUploadDateTime);
            //Assert.AreEqual(expected.LatestUploadDateTimeSpecified, actual.LatestUploadDateTimeSpecified);
            //Assert.AreEqual(expected.LatestWriteBackDateTime, actual.LatestWriteBackDateTime);
            //Assert.AreEqual(expected.LatestWriteBackDateTimeSpecified, actual.LatestWriteBackDateTimeSpecified);
            //Assert.AreEqual(expected.SyncTypeSpecified, actual.SyncTypeSpecified);
            Assert.AreEqual(expected.StartSyncTMS, actual.StartSyncTMS);
            Assert.AreEqual(expected.StartSyncTMSSpecified, actual.StartSyncTMSSpecified);
            Assert.AreEqual(expected.EndSyncTMS, actual.EndSyncTMS);
            Assert.AreEqual(expected.EndSyncTMSSpecified, actual.EndSyncTMSSpecified);
            Assert.AreEqual(expected.EntityName, actual.EntityName);
            //Assert.AreEqual(expected.EntityRowCount, actual.EntityRowCount);
            //Assert.AreEqual(expected.EntityRowCountSpecified, actual.EntityRowCountSpecified);
        }


        internal static void VerifySyncActivitySparse(SyncActivity expected, SyncActivity actual)
        {
            Assert.AreEqual(expected.EntityName, actual.EntityName);
        }

        #endregion

    }
}
