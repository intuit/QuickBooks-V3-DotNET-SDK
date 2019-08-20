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

using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] 
    public class DepositTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }


        #region TestCases for QBOContextOAuth

        #region Sync Methods

        #region Test cases for Add Operations

        [TestMethod]
  
        public void DepositAddTestUsingoAuth()
        {
            //Creating the Deposit for Add
            Deposit deposit = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, deposit);
            //Verify the added Deposit
            QBOHelper.VerifyDeposit(deposit, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void DepositFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //DepositAddTestUsingoAuth();

            //Retrieving the Deposit using FindAll
            List<Deposit> deposits = Helper.FindAll<Deposit>(qboContextoAuth, new Deposit(), 1, 500);
            Assert.IsNotNull(deposits);
            Assert.IsTrue(deposits.Count<Deposit>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void DepositFindbyIdTestUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit deposit = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, deposit);
            Deposit found = Helper.FindById<Deposit>(qboContextoAuth, added);
            QBOHelper.VerifyDeposit(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
      
        public void DepositUpdateTestUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit deposit = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, deposit);
            //Change the data of added entity
            Deposit changed = QBOHelper.UpdateDeposit(qboContextoAuth, added);
            //Update the returned entity data
            Deposit updated = Helper.Update<Deposit>(qboContextoAuth, changed);//Verify the updated Deposit
            QBOHelper.VerifyDeposit(changed, updated);
        }


        [TestMethod]
    
        public void DepositSparseUpdateTestUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit deposit = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, deposit);
            //Change the data of added entity
            Deposit changed = QBOHelper.UpdateDepositSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Deposit updated = Helper.Update<Deposit>(qboContextoAuth, changed);//Verify the updated Deposit
            QBOHelper.VerifyDepositSparse(changed, updated);
        }
        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
 
        public void DepositDeleteTestUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit deposit = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, deposit);
            //Delete the returned entity
            try
            {
                Deposit deleted = Helper.Delete<Deposit>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void DepositVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Deposit entity = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the entity
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Deposit voided = Helper.Void<Deposit>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
  
        public void DepositCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            DepositAddTestUsingoAuth();

            //Retrieving the Deposit using CDC
            List<Deposit> entities = Helper.CDC(qboContextoAuth, new Deposit(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Deposit>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
 
        public void DepositBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Deposit existing = Helper.FindOrAdd(qboContextoAuth, new Deposit());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateDeposit(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateDeposit(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Deposit");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Deposit>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Deposit).Id));
                }
                else if (resp.ResponseType == ResponseType.Query)
                {
                    Assert.IsTrue(resp.Entities != null && resp.Entities.Count > 0);
                }
                else if (resp.ResponseType == ResponseType.CdcQuery)
                {
                    Assert.IsTrue(resp.CDCResponse != null && resp.CDCResponse.entities != null && resp.CDCResponse.entities.Count > 0);
                }

                position++;
            }
        }

        #endregion

        #region Test cases for Query
        [TestMethod]
        public void DepositQueryUsingoAuth()
        {
            QueryService<Deposit> entityQuery = new QueryService<Deposit>(qboContextoAuth);
            Deposit existing = Helper.FindOrAdd<Deposit>(qboContextoAuth, new Deposit());
            //List<Deposit> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Deposit> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Deposit where Id='" + existing.Id + "'").ToList<Deposit>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
    
        public void DepositAddAsyncTestsUsingoAuth()
        {
            //Creating the Deposit for Add
            Deposit entity = QBOHelper.CreateDeposit(qboContextoAuth);

            Deposit added = Helper.AddAsync<Deposit>(qboContextoAuth, entity);
            QBOHelper.VerifyDeposit(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void DepositRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            DepositAddTestUsingoAuth();

            //Retrieving the Deposit using FindAll
            Helper.FindAllAsync<Deposit>(qboContextoAuth, new Deposit());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void DepositFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit entity = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Deposit>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
       
        public void DepositUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit entity = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, entity);

            //Update the Deposit
            Deposit updated = QBOHelper.UpdateDeposit(qboContextoAuth, added);
            //Call the service
            Deposit updatedReturned = Helper.UpdateAsync<Deposit>(qboContextoAuth, updated);
            //Verify updated Deposit
            QBOHelper.VerifyDeposit(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
       
        public void DepositDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Deposit for Adding
            Deposit entity = QBOHelper.CreateDeposit(qboContextoAuth);
            //Adding the Deposit
            Deposit added = Helper.Add<Deposit>(qboContextoAuth, entity);

            Helper.DeleteAsync<Deposit>(qboContextoAuth, added);
        }

        #endregion


        #endregion

        #endregion

    }
}
