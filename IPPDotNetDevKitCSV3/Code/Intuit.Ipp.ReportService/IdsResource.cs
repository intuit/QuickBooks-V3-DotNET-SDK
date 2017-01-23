////*********************************************************
// <copyright file="IdsResource.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains enumeration for resource name.</summary>
////*********************************************************

namespace Intuit.Ipp.ReportService
{
    /// <summary>
    /// A list of Domain entities supported by IPS. Used by the REST infrastructure to construct relevant calls.
    /// </summary>
    internal enum IdsReportResource
    {
        /// <summary>
        /// none Resource
        /// </summary>
        none,

        /// <summary>
        /// account balances Resource
        /// </summary>
        accountbalances,

        /// <summary>
        /// balance sheet Resource
        /// </summary>
        balancesheet,

        /// <summary>
        /// customers show me Resource
        /// </summary>
        customerswhooweme,

        /// <summary>
        /// advanced report Resource
        /// </summary>
        advancedreport,

        /// <summary>
        /// income breakdown Resource
        /// </summary>
        incomebreakdown,

        /// <summary>
        /// profitandloss Resource
        /// </summary>
        profitandloss,

        /// <summary>
        /// sales summary Resource
        /// </summary>
        salessummary,

        /// <summary>
        /// top customers by sales Resource
        /// </summary>
        topcustomersbysales
    }

    /// <summary>
    /// A list of Domain entities supported by IDS. Used by the REST infrastructure to construct relevant calls.
    /// </summary>
    internal enum IdsResource
    {
        /// <summary>
        /// user Resource
        /// </summary>
        user,

        /// <summary>
        /// account Resource
        /// </summary>
        account,

        /// <summary>
        /// bill Resource
        /// </summary>
        bill,

        /// <summary>
        /// bill payment Resource
        /// </summary>
        billpayment,

        /// <summary>
        /// billpaymentcreditcard Resource
        /// </summary>
        billpaymentcreditcard,

        /// <summary>
        /// bom component Resource
        /// </summary>
        bomcomponent,

        /// <summary>
        /// build assembly Resource
        /// </summary>
        buildassembly,

        /// <summary>
        /// charge Resource
        /// </summary>
        charge,

        /// <summary>
        /// check Resource
        /// </summary>
        check,

        /// <summary>
        /// credit card charge Resource
        /// </summary>
        creditcardcharge,

        /// <summary>
        /// credit card credit Resource
        /// </summary>
        creditcardcredit,

        /// <summary>
        /// credit memo Resource
        /// </summary>
        creditmemo,

        /// <summary>
        /// currency info Resource
        /// </summary>
        currencyinfo,

        /// <summary>
        /// customer Resource
        /// </summary>
        customer,

        /// <summary>
        /// customer message Resource
        /// </summary>
        customermsg,

        /// <summary>
        /// customer type Resource
        /// </summary>
        customertype,

        /// <summary>
        /// deposit Resource
        /// </summary>
        deposit,

        /// <summary>
        /// discount Resource
        /// </summary>
        discount,

        /// <summary>
        /// employee Resource
        /// </summary>
        employee,

        /// <summary>
        /// estimate Resource
        /// </summary>
        estimate,

        /// <summary>
        /// fixedasset Resource
        /// </summary>
        fixedasset,

        /// <summary>
        /// incomebreakdown Resource
        /// </summary>
        incomebreakdown,

        /// <summary>
        /// inventoryadjustment Resource
        /// </summary>
        inventoryadjustment,

        /// <summary>
        /// inventorysite Resource
        /// </summary>
        inventorysite,

        /// <summary>
        /// inventorytransfer Resource
        /// </summary>
        inventorytransfer,

        /// <summary>
        /// invoice Resource
        /// </summary>
        invoice,

        /// <summary>
        /// item Resource
        /// </summary>
        item,

        /// <summary>
        /// itemconsolidated Resource
        /// </summary>
        itemconsolidated,

        /// <summary>
        /// itemreceipt Resource
        /// </summary>
        itemreceipt,

        /// <summary>
        /// job Resource
        /// </summary>
        job,

        /// <summary>
        /// jobtype Resource
        /// </summary>
        jobtype,

        /// <summary>
        /// journalentry Resource
        /// </summary>
        journalentry,

        /// <summary>
        /// othername Resource
        /// </summary>
        othername,

        /// <summary>
        /// payment Resource
        /// </summary>
        payment,

        /// <summary>
        /// paymentmethod Resource
        /// </summary>
        paymentmethod,

        /// <summary>
        /// payrollitem Resource
        /// </summary>
        payrollitem,

        /// <summary>
        /// payrollnonwageitem Resource
        /// </summary>
        payrollnonwageitem,

        /// <summary>
        /// purchaseorder Resource
        /// </summary>
        purchaseorder,

        /// <summary>
        /// salesorder Resource
        /// </summary>
        salesorder,

        /// <summary>
        /// salesreceipt Resource
        /// </summary>
        salesreceipt,

        /// <summary>
        /// salesrep Resource
        /// </summary>
        salesrep,

        /// <summary>
        /// salestax Resource
        /// </summary>
        salestax,

        /// <summary>
        /// salestaxcode Resource
        /// </summary>
        salestaxcode,

        /// <summary>
        /// salestaxgroup Resource
        /// </summary>
        salestaxgroup,

        /// <summary>
        /// shipmethod Resource
        /// </summary>
        shipmethod,

        /// <summary>
        /// status Resource
        /// </summary>
        status,

        /// <summary>
        /// task Resource
        /// </summary>
        task,

        /// <summary>
        /// templatename Resource
        /// </summary>
        templatename,

        /// <summary>
        /// timeactivity Resource
        /// </summary>
        timeactivity,

        /// <summary>
        /// unitofmeasure Resource
        /// </summary>
        uom,

        /// <summary>
        /// vendor Resource
        /// </summary>
        vendor,

        /// <summary>
        /// vendorcredit Resource
        /// </summary>
        vendorcredit,

        /// <summary>
        /// vendortype Resource
        /// </summary>
        vendortype,

        /// <summary>
        /// preferences Resource
        /// </summary>
        preferences,

        /// <summary>
        /// syncactivityrequest Resource
        /// </summary>
        syncactivityrequest,

        /// <summary>
        /// salesterm Resource
        /// </summary>
        salesterm,

        /// <summary>
        /// class Resource
        /// </summary>
        Class,

        /// <summary>
        /// creditcardrefun Resource
        /// </summary>
        creditcardrefund,

        /// <summary>
        /// company Resource
        /// </summary>
        companymetadata,

        /// <summary>
        /// customfielddefinition Resource
        /// </summary>
        customfielddefinition,

        /// <summary>
        /// namevalue Resource
        /// </summary>
        namevalue,

        /// <summary>
        /// recordcount Resource
        /// </summary>
        recordcount
    }
}