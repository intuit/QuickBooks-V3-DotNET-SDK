////********************************************************************
// <copyright file="SerializerTestHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains helper methods for serialization related unit tests </summary>
////********************************************************************

namespace Intuit.Ipp.Utility.Test
{
    using System;
    using System.Xml;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    internal class SerializerTestHelper
    {
        #region Create Entities
        /// <summary>
        /// Create the Account entity with dummy test data from unit test case.
        /// </summary>
        /// <returns></returns>
        internal static Account CreateAccountEntity()
        {
            Account serializableEntity = new Account();
            // set all the properties
            //serializableEntity.AccountEx = CreateIntuitAnyType();
            serializableEntity.AcctNum = "Account Num";
            serializableEntity.Active = true;
            serializableEntity.ActiveSpecified = true;
            serializableEntity.BankNum = "Bank Num";
            serializableEntity.CurrencyRef = CreateReferenceType();
            serializableEntity.CurrentBalance = 5000;
            serializableEntity.CurrentBalanceSpecified = true;
            serializableEntity.CurrentBalanceWithSubAccounts = 4000;
            serializableEntity.CurrentBalanceWithSubAccountsSpecified = true;
            serializableEntity.CustomField = CreateCustomFieldArrayObject();
            serializableEntity.Description = "Description";
            //serializableEntity.DetailedType = "Some Detail type";
            serializableEntity.domain = "www.intuit.com";
            //serializableEntity.ExternalKey = new string[] { "abc", "bcd" };
            serializableEntity.FullyQualifiedName = "My account Name";
            //serializableEntity.HasAttachment = true;
            //serializableEntity.HasAttachmentSpecified = true;
            serializableEntity.Id = "12345";
            serializableEntity.MetaData = CreateMetaDataObject();
            serializableEntity.Name = "Name";
            serializableEntity.OpeningBalance = 1000;
            serializableEntity.OpeningBalanceDate = DateTime.Now;
            serializableEntity.OpeningBalanceDateSpecified = true;
            serializableEntity.OpeningBalanceSpecified = true;
            serializableEntity.ParentRef = CreateReferenceType();
            serializableEntity.sparse = false;
            serializableEntity.sparseSpecified = false;
            serializableEntity.status = EntityStatusEnum.Draft;
            serializableEntity.statusSpecified = true;
            serializableEntity.SubAccount = true;
            serializableEntity.SubAccountSpecified = true;
            //serializableEntity.Subtype = AccountSubtypeEnum.AccountsPayable;
            //serializableEntity.SubtypeSpecified = true;
            serializableEntity.SyncToken = "Sync Token";
            serializableEntity.TaxAccount = false;
            serializableEntity.TaxAccountSpecified = false;
            serializableEntity.TaxCodeRef = CreateReferenceType();
            serializableEntity.AccountType = AccountTypeEnum.AccountsPayable;
            serializableEntity.AccountTypeSpecified = true;
            return serializableEntity;
        }

        #endregion 

        #region Compare deserialized object with serialized object

        /// <summary>
        /// Compares the account objects 
        /// </summary>
        /// <param name="deserializedEntity"></param>
        /// <param name="serializableEntity"></param>
        internal static void CompareAcconutObjects(Account deserializedEntity, Account serializableEntity)
        {
            //Assert.ReferenceEquals(deserializedEntity.AccountEx, serializableEntity.AccountEx);
            ReferenceEquals(deserializedEntity.AcctNum, serializableEntity.AcctNum);
            ReferenceEquals(deserializedEntity.Active, serializableEntity.Active);
            ReferenceEquals(deserializedEntity.ActiveSpecified, serializableEntity.ActiveSpecified);
            ReferenceEquals(deserializedEntity.BankNum, serializableEntity.BankNum);
            ReferenceEquals(deserializedEntity.CurrencyRef, serializableEntity.CurrencyRef);
            ReferenceEquals(deserializedEntity.CurrentBalance, serializableEntity.CurrentBalance);
            ReferenceEquals(deserializedEntity.CurrentBalanceSpecified, serializableEntity.CurrentBalanceSpecified);
            ReferenceEquals(deserializedEntity.CurrentBalanceWithSubAccounts, serializableEntity.CurrentBalanceWithSubAccounts);
            ReferenceEquals(deserializedEntity.CurrentBalanceWithSubAccountsSpecified, serializableEntity.CurrentBalanceWithSubAccountsSpecified);
            ReferenceEquals(deserializedEntity.Description, serializableEntity.Description);
            //Assert.ReferenceEquals(deserializedEntity.DetailedType, serializableEntity.DetailedType);
            ReferenceEquals(deserializedEntity.domain, serializableEntity.domain);
            //Assert.ReferenceEquals(deserializedEntity.ExternalKey, serializableEntity.ExternalKey);
            ReferenceEquals(deserializedEntity.FullyQualifiedName, serializableEntity.FullyQualifiedName);
            //Assert.ReferenceEquals(deserializedEntity.HasAttachment, serializableEntity.HasAttachment);
            //Assert.ReferenceEquals(deserializedEntity.HasAttachmentSpecified, serializableEntity.HasAttachmentSpecified);
            ReferenceEquals(deserializedEntity.Id, serializableEntity.Id);
            ReferenceEquals(deserializedEntity.MetaData, serializableEntity.MetaData);
            ReferenceEquals(deserializedEntity.Name, serializableEntity.Name);
            ReferenceEquals(deserializedEntity.OpeningBalance, serializableEntity.OpeningBalance);
            ReferenceEquals(deserializedEntity.OpeningBalanceDateSpecified, serializableEntity.OpeningBalanceDateSpecified);
            ReferenceEquals(deserializedEntity.OpeningBalanceSpecified, serializableEntity.OpeningBalanceSpecified);
            ReferenceEquals(deserializedEntity.OpeningBalanceDate, serializableEntity.OpeningBalanceDate);
            ReferenceEquals(deserializedEntity.ParentRef, serializableEntity.ParentRef);
            ReferenceEquals(deserializedEntity.sparse, serializableEntity.sparse);
            ReferenceEquals(deserializedEntity.sparseSpecified, serializableEntity.sparseSpecified);
            ReferenceEquals(deserializedEntity.status, serializableEntity.status);
            ReferenceEquals(deserializedEntity.statusSpecified, serializableEntity.statusSpecified);
            ReferenceEquals(deserializedEntity.SubAccount, serializableEntity.SubAccount);
            ReferenceEquals(deserializedEntity.SubAccountSpecified, serializableEntity.SubAccountSpecified);
            //Assert.ReferenceEquals(deserializedEntity.Subtype, serializableEntity.Subtype);
            //Assert.ReferenceEquals(deserializedEntity.SubtypeSpecified, serializableEntity.SubtypeSpecified);
            ReferenceEquals(deserializedEntity.SyncToken, serializableEntity.SyncToken);
            ReferenceEquals(deserializedEntity.TaxAccount, serializableEntity.TaxAccount);
            ReferenceEquals(deserializedEntity.TaxAccountSpecified, serializableEntity.TaxAccountSpecified);
            ReferenceEquals(deserializedEntity.TaxCodeRef, serializableEntity.TaxCodeRef);
            //Assert.ReferenceEquals(deserializedEntity.Type, serializableEntity.Type);
            //Assert.ReferenceEquals(deserializedEntity.TypeSpecified, serializableEntity.TypeSpecified);
        }

        #endregion 

        #region Helper Methods
        /// <summary>
        ///Creates the ReferenceType object
        /// </summary>
        /// <returns>Returns the ReferenceType object</returns>
        internal static ReferenceType CreateReferenceType()
        {
            ReferenceType referenceType = new ReferenceType();
            referenceType.name = "A";
            referenceType.type=Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account);
            //referenceType.typeSpecified=true;
            referenceType.Value="b";
            return referenceType;
        }

        internal static IntuitAnyType CreateIntuitAnyType()
        {
           IntuitAnyType anyType=new IntuitAnyType();
           XmlDocument xmlDoc1=new XmlDocument();
           xmlDoc1.LoadXml("<additionalinfo> <Field1>value1</Field1> </additionalinfo>");
           XmlElement element1= xmlDoc1.DocumentElement;
           XmlDocument xmlDoc2=new XmlDocument();
            xmlDoc2.LoadXml("<personalinfo> <Field1>value1</Field1> </personalinfo>");  
           XmlElement element2=xmlDoc2.DocumentElement; 
           anyType.Any=new XmlElement [] {element1, element2};
            return anyType;
        }   

        /// <summary>
        /// Create meta data object.
        /// </summary>
        /// <returns>Returns the ModificationMetaData object.</returns>
        internal static ModificationMetaData CreateMetaDataObject()
        {
            ModificationMetaData mmd = new ModificationMetaData();
            mmd.CreatedByRef = CreateReferenceType();
            mmd.CreateTime = DateTime.Now;
            mmd.CreateTimeSpecified = true;
            mmd.LastModifiedByRef = CreateReferenceType();
            mmd.LastUpdatedTime = DateTime.Now;
            mmd.LastUpdatedTimeSpecified = true;
            return mmd;
        }


        /// <summary>
        /// Create custom field array object.
        /// </summary>
        /// <returns>Returns the CustomField array object.</returns>
        internal static CustomField[] CreateCustomFieldArrayObject()
        {
            CustomField field1 = new CustomField();
            field1.DefinitionId ="1";
            field1.AnyIntuitObject  = "STCF Value";
            field1.Name="Field1";
            field1.Type=CustomFieldTypeEnum.StringType;
             

            CustomField field2 = new CustomField();
            field2.DefinitionId ="2";
            field2.AnyIntuitObject  =true;
            field2.Name="Field2";
            field2.Type=CustomFieldTypeEnum.BooleanType;

            CustomField field3 = new CustomField();
            field3.DefinitionId = "3";
            field3.AnyIntuitObject  =DateTime.UtcNow;
            field3.Name="Field3";
            field3.Type=CustomFieldTypeEnum.DateType;

            CustomField field4 = new CustomField();
            field4.DefinitionId ="4";
            field4.AnyIntuitObject  =(decimal)1.0;
            field4.Name="Field4";
            field4.Type=CustomFieldTypeEnum.NumberType;

            return new CustomField[] { field1, field2, field3, field4 };
        }

        /// <summary>
        /// Helper method to create a basic Customer entity
        /// </summary>
        /// <returns></returns>
        internal static Customer CreateCustomerEntity()
        {
            Customer customer = new Customer();

            Random rnd = new Random(10);
            string guid = Guid.NewGuid().ToString("N");

            int len = rnd.Next(guid.Length);
            customer.GivenName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.Title = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.MiddleName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.FamilyName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.DisplayName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.AcctNum = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.AltContactName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.CompanyName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.ContactName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.domain = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.FamilyName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.FullyQualifiedName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.Id = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.Notes = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.PreferredDeliveryMethod = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.PrintOnCheckName = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.ResaleNum = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.Suffix = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.SyncToken = guid.Substring(0, len);

            len = rnd.Next(guid.Length);
            customer.UserId = guid.Substring(0, len);

            customer.Active = true;
            customer.ActiveSpecified = true;

            customer.AlternatePhone = new TelephoneNumber();

            customer.AnyIntuitObject = new ReferenceType();
            customer.Balance = 100;
            customer.BalanceSpecified = true;
            customer.BalanceWithJobs = 100;
            customer.BalanceWithJobsSpecified = true;
            customer.BillAddr = new PhysicalAddress();
            customer.BillWithParent = true;
            customer.BillWithParentSpecified = true;
            customer.CCDetail = new CreditChargeInfo();
            customer.CreditLimit = 100;
            customer.CreditLimitSpecified = true;
            customer.CurrencyRef = new ReferenceType();
            customer.CustomerEx = new IntuitAnyType();
            customer.CustomerTypeRef = new ReferenceType();
            //customer.CustomField = new[] { new CustomField(), new CustomField() };
            customer.DefaultTaxCodeRef = new ReferenceType();
            //customer.ExternalKey = "ExternalKey".Split();
            customer.Fax = new TelephoneNumber();
            //customer.HasAttachment = true;
            //customer.HasAttachmentSpecified = true;
            customer.ItemElementName = ItemChoiceType6.TaxGroupCodeRef;
            customer.Job = true;
            customer.JobInfo = new JobInfo();
            customer.JobSpecified = true;
            customer.Level = 100;
            customer.LevelSpecified = true;
            customer.MetaData = new ModificationMetaData();
            customer.Mobile = new TelephoneNumber();
            customer.OpenBalanceDate = DateTime.Now;
            customer.OpenBalanceDateSpecified = true;
            customer.Organization = true;
            customer.OrganizationSpecified = true;
            //customer.OtherContactInfo = new[] { new ContactInfo() };
            //customer.OtherEmailAddresses = new[] { new EmailAddress() };
            customer.OverDueBalance = 100;
            customer.OverDueBalanceSpecified = true;
            customer.ParentRef = new ReferenceType();
            customer.PaymentMethodRef = new ReferenceType();
            customer.PriceLevelRef = new ReferenceType();
            customer.PrimaryEmailAddr = new EmailAddress();
            customer.PrimaryPhone = new TelephoneNumber();
            customer.RootCustomerRef = new ReferenceType();
            customer.SalesRepRef = new ReferenceType();
            customer.SalesTermRef = new ReferenceType();
            //customer.ShippingAddr = new PhysicalAddress();
            //customer.ShippingSameAsBilling = true;
            //customer.ShippingSameAsBillingSpecified = true;
            customer.sparse = true;
            customer.sparseSpecified = true;
            customer.status = EntityStatusEnum.Deleted;
            customer.statusSpecified = true;
            customer.Taxable = true;
            customer.TaxableSpecified = true;
            customer.TotalExpense = 100;
            customer.TotalExpenseSpecified = true;
            customer.TotalRevenue = 100;
            customer.TotalRevenueSpecified = true;
            customer.WebAddr = new WebSiteAddress();

            return customer;
        }

        /// <summary>
        /// A basic customer deserialized object comparer
        /// </summary>
        /// <param name="deserializedEntity">Deserialized object</param>
        /// <param name="serializableEntity">Object used for serialization/comparison</param>
        internal static void CompareCustomerObjects(Customer deserializedEntity, Customer serializableEntity)
        {
            Assert.AreEqual(deserializedEntity.AcctNum, serializableEntity.AcctNum);
            Assert.AreEqual(deserializedEntity.Active, serializableEntity.Active);
            Assert.AreEqual(deserializedEntity.ActiveSpecified, serializableEntity.ActiveSpecified);
            Assert.AreEqual(deserializedEntity.AltContactName, serializableEntity.AltContactName);
            Assert.IsNotNull(deserializedEntity.AlternatePhone);
            Assert.IsNotNull(deserializedEntity.AnyIntuitObject);
            Assert.AreEqual(deserializedEntity.Balance, serializableEntity.Balance);
            Assert.AreEqual(deserializedEntity.BalanceSpecified, serializableEntity.BalanceSpecified);
            Assert.AreEqual(deserializedEntity.BalanceWithJobs, serializableEntity.BalanceWithJobs);
            Assert.AreEqual(deserializedEntity.BalanceWithJobsSpecified, serializableEntity.BalanceWithJobsSpecified);
            Assert.IsNotNull(deserializedEntity.BillAddr);
            Assert.AreEqual(deserializedEntity.BillWithParent, serializableEntity.BillWithParent);
            Assert.AreEqual(deserializedEntity.BillWithParentSpecified, serializableEntity.BillWithParentSpecified);
            Assert.IsNotNull(deserializedEntity.CCDetail);
            Assert.AreEqual(deserializedEntity.CompanyName, serializableEntity.CompanyName);
            Assert.AreEqual(deserializedEntity.ContactName, serializableEntity.ContactName);
            Assert.AreEqual(deserializedEntity.CreditLimit, serializableEntity.CreditLimit);
            Assert.AreEqual(deserializedEntity.CreditLimitSpecified, serializableEntity.CreditLimitSpecified);
            Assert.IsNotNull(deserializedEntity.CurrencyRef);
            Assert.IsNotNull(deserializedEntity.CustomerEx);
            Assert.IsNotNull(deserializedEntity.CustomerTypeRef);
            //Assert.IsNotNull(deserializedEntity.CustomField);
            //Assert.IsNotNull(deserializedEntity.DefaultTaxCodeRef);
            Assert.AreEqual(deserializedEntity.DisplayName, serializableEntity.DisplayName);
            Assert.AreEqual(deserializedEntity.domain, serializableEntity.domain);
            //Assert.AreEqual(string.Concat(deserializedEntity.ExternalKey), "ExternalKey");
            Assert.AreEqual(deserializedEntity.FamilyName, serializableEntity.FamilyName);
            Assert.IsNotNull(deserializedEntity.Fax);
            Assert.AreEqual(deserializedEntity.FullyQualifiedName, serializableEntity.FullyQualifiedName);
            Assert.AreEqual(deserializedEntity.GivenName, serializableEntity.GivenName);
            //Assert.AreEqual(deserializedEntity.HasAttachment, serializableEntity.HasAttachment);
            //Assert.AreEqual(deserializedEntity.HasAttachmentSpecified, serializableEntity.HasAttachmentSpecified);
            Assert.AreEqual(deserializedEntity.Id, serializableEntity.Id);
            Assert.AreEqual(deserializedEntity.ItemElementName, serializableEntity.ItemElementName);
            Assert.AreEqual(deserializedEntity.Job, serializableEntity.Job);
            Assert.IsNotNull(deserializedEntity.JobInfo);
            Assert.AreEqual(deserializedEntity.JobSpecified, serializableEntity.JobSpecified);
            Assert.AreEqual(deserializedEntity.Level, serializableEntity.Level);
            Assert.AreEqual(deserializedEntity.LevelSpecified, serializableEntity.LevelSpecified);
            Assert.IsNotNull(deserializedEntity.MetaData);
            Assert.AreEqual(deserializedEntity.MiddleName, serializableEntity.MiddleName);
            Assert.IsNotNull(deserializedEntity.Mobile);
            Assert.AreEqual(deserializedEntity.Notes, serializableEntity.Notes);
            Assert.AreEqual(deserializedEntity.Organization, serializableEntity.Organization);
            Assert.AreEqual(deserializedEntity.OrganizationSpecified, serializableEntity.OrganizationSpecified);
            //Assert.IsNotNull(deserializedEntity.OtherContactInfo);
            //Assert.IsNotNull(deserializedEntity.OtherEmailAddresses);
            Assert.AreEqual(deserializedEntity.OverDueBalance, serializableEntity.OverDueBalance);
            Assert.AreEqual(deserializedEntity.OverDueBalanceSpecified, serializableEntity.OverDueBalanceSpecified);
            Assert.IsNotNull(deserializedEntity.ParentRef);
            Assert.IsNotNull(deserializedEntity.PaymentMethodRef);
            Assert.AreEqual(deserializedEntity.PreferredDeliveryMethod, serializableEntity.PreferredDeliveryMethod);
            Assert.IsNotNull(deserializedEntity.PriceLevelRef);
            Assert.IsNotNull(deserializedEntity.PrimaryEmailAddr);
            Assert.IsNotNull(deserializedEntity.PrimaryPhone);
            Assert.AreEqual(deserializedEntity.PrintOnCheckName, serializableEntity.PrintOnCheckName);
            Assert.AreEqual(deserializedEntity.ResaleNum, serializableEntity.ResaleNum);
            Assert.IsNotNull(deserializedEntity.RootCustomerRef);
            Assert.IsNotNull(deserializedEntity.SalesRepRef);
            Assert.IsNotNull(deserializedEntity.SalesTermRef);
            //Assert.IsNotNull(deserializedEntity.ShipAddr);
            //Assert.AreEqual(deserializedEntity.ShippingSameAsBilling, serializableEntity.ShippingSameAsBilling);
            //Assert.AreEqual(deserializedEntity.ShippingSameAsBillingSpecified, serializableEntity.ShippingSameAsBillingSpecified);
            Assert.AreEqual(deserializedEntity.sparse, serializableEntity.sparse);
            Assert.AreEqual(deserializedEntity.sparseSpecified, serializableEntity.sparseSpecified);
            Assert.AreEqual(deserializedEntity.status, EntityStatusEnum.Deleted);
            Assert.AreEqual(deserializedEntity.statusSpecified, serializableEntity.statusSpecified);
            Assert.AreEqual(deserializedEntity.Suffix, serializableEntity.Suffix);
            Assert.AreEqual(deserializedEntity.SyncToken, serializableEntity.SyncToken);
            Assert.AreEqual(deserializedEntity.Taxable, serializableEntity.Taxable);
            Assert.AreEqual(deserializedEntity.TaxableSpecified, serializableEntity.TaxableSpecified);
            Assert.AreEqual(deserializedEntity.Title, serializableEntity.Title);
            Assert.AreEqual(deserializedEntity.TotalExpense, serializableEntity.TotalExpense);
            Assert.AreEqual(deserializedEntity.TotalExpenseSpecified, serializableEntity.TotalExpenseSpecified);
            Assert.AreEqual(deserializedEntity.TotalRevenue, serializableEntity.TotalRevenue);
            Assert.AreEqual(deserializedEntity.TotalRevenueSpecified, serializableEntity.TotalRevenueSpecified);
            Assert.AreEqual(deserializedEntity.UserId, serializableEntity.UserId);
            Assert.IsNotNull(deserializedEntity.WebAddr);
        }

        /// <summary>
        /// Helper method to create a basic Term entity
        /// </summary>
        /// <returns></returns>
        internal static Term CreateTermEntity()
        {
            Term term = new Term();
            term.Name = "Name" + Guid.NewGuid().ToString("N").Substring(0, 15);
            term.Active = true;
            term.ActiveSpecified = true;
            term.Type = "STANDARD";
            term.DiscountPercent = new Decimal(50.00);
            term.DiscountPercentSpecified = true;
            term.AnyIntuitObjects = new Object[] { 10, 20 };
            term.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.DiscountDayOfMonth, ItemsChoiceType.DueNextMonthDays };
            return term;
        }

        /// <summary>
        /// A basic Term deserialized object comparer
        /// </summary>
        /// <param name="deserializedEntity">Deserialized object</param>
        /// <param name="serializableEntity">Object used for serialization/comparison</param>
        internal static void CompareTermObjects(Term deserializedEntity, Term serializableEntity)
        {
            Assert.AreEqual(deserializedEntity.Name, serializableEntity.Name);
            Assert.AreEqual(deserializedEntity.Active, serializableEntity.Active);
            Assert.AreEqual(deserializedEntity.ActiveSpecified, serializableEntity.ActiveSpecified);
            Assert.AreEqual(deserializedEntity.Type, serializableEntity.Type);
            Assert.AreEqual(deserializedEntity.DiscountPercent, serializableEntity.DiscountPercent);
            Assert.AreEqual(deserializedEntity.DiscountPercentSpecified, serializableEntity.DiscountPercentSpecified);
            //Assert.AreEqual(expected.DueDays, actual.DueDays);
            //Assert.IsTrue(Helper.CheckEqual(expected.ItemsElementName, actual.ItemsElementName));
            //    Assert.AreEqual(expected.SalesTermEx.Any, actual.SalesTermEx.Any);
        }

        /// <summary>
        /// Create the Preference entity with NameValue set.
        /// </summary>
        /// <returns></returns>
        internal static Preferences CreatePreferenceEntity()
        {
            Preferences pref = new Preferences();
            pref.OtherPrefs = new NameValue[] { 
                new NameValue(){ Name = "SalesFormsPrefs.DefaultCustomerMessage" },
                new NameValue() { Name = "SalesFormsPrefs.DefaultItem", Value = "1" }
            };
            pref.Id = "1";
            return pref;
        }


        #endregion        
    }
}
