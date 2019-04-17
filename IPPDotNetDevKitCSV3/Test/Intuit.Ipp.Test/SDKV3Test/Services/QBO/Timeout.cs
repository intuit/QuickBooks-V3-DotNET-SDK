using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Threading;
using Intuit.Ipp.QueryFilter;
 
using Intuit.Ipp.DataService;
using System.Collections.ObjectModel;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class TimeoutTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }


        #region TestCases for Timeout

        #region Sync Methods

        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(IdsException))]
        public void SyncTimeoutAdd()
        {
            qboContextoAuth.Timeout =500;
            //Creating the Customer for Add
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
        }

        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(IdsException))]
        public void SyncTimeoutUpdate()
        {
            qboContextoAuth.Timeout = 100000;
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            QBOHelper.VerifyCustomer(customer, added);
            //Change the data of added entity
            qboContextoAuth.Timeout = 500;
            Customer updated = QBOHelper.UpdateCustomer(qboContextoAuth, added);
            //Update the returned entity data
            Customer updatedreturned = Helper.Update<Customer>(qboContextoAuth, updated);
        }

        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(IdsException))]
        public void SyncTimeoutUpdateSparseUpdate()
        {
            qboContextoAuth.Timeout = 100000;
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            QBOHelper.VerifyCustomer(customer, added);
            qboContextoAuth.Timeout = 50;
            //Change the data of added entity
            Customer updated = QBOHelper.SparseUpdateCustomer(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Customer updatedreturned = Helper.Update<Customer>(qboContextoAuth, updated);
        }

        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(IdsException))]
        public void SyncTimeoutQuery()
        {
            qboContextoAuth.Timeout = 10;
            //Retrieving the Customer using FindAll
            List<Customer> customers = Helper.FindAll<Customer>(qboContextoAuth, new Customer(), 1, 2);
        }


        #endregion

        #region Async Methods

        [TestMethod]
        public void AsyncNoTimeout()
        {
            qboContextoAuth.Timeout = 500;
            //Creating the Customer for Add
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);
            Customer added = Helper.AddAsync<Customer>(qboContextoAuth, entity);
            QBOHelper.VerifyCustomer(entity, added);
        }


        #endregion

        #endregion

    }
}
