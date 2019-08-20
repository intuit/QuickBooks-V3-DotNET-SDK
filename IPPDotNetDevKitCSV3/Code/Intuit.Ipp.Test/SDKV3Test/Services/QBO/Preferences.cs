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
    public class PreferencesTest
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

        [TestMethod] [Ignore]
        public void PreferencesAddTestUsingoAuth()
        {
            //Creating the Bill for Add
            Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            //Verify the added Preferences
            QBOHelper.VerifyPreferences(preferences, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void PreferencesFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //PreferencesAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            List<Preferences> preferencess = Helper.FindAll<Preferences>(qboContextoAuth, new Preferences(), 1, 500);
            Assert.IsNotNull(preferencess);
            Assert.IsTrue(preferencess.Count<Preferences>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void PreferencesFindbyIdTestUsingoAuth()
        {
            //Creating the Preferences for Adding
          //  Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
          //  Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            List<Preferences> foundall = Helper.FindAll<Preferences>(qboContextoAuth, new Preferences());

            Assert.IsTrue(foundall.Count > 0);

            foreach (Preferences found in foundall)
            {
                Preferences findbyId = Helper.FindById<Preferences>(qboContextoAuth, found);
                QBOHelper.VerifyPreferences(found, findbyId);
            }
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void PreferencesUpdateTestUsingoAuth()
        {
            //Creating the Preferences for Adding
            //Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            //Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            //Change the data of added entity
            List<Preferences> foundall = Helper.FindAll<Preferences>(qboContextoAuth, new Preferences());

            Assert.IsTrue(foundall.Count > 0);

            foreach (Preferences found in foundall)
            {
                Preferences changed = QBOHelper.UpdatePreferences(qboContextoAuth, found);
                //Update the returned entity data
                Preferences updated = Helper.Update<Preferences>(qboContextoAuth, changed);//Verify the updated Preferences
                QBOHelper.VerifyPreferences(changed, updated);
            }
            
        }

        [TestMethod] [Ignore]
        public void PreferencesSparseUpdateTestUsingoAuth()
        {
            //Finding Or Adding the Preferences
            Preferences found = Helper.FindOrAdd<Preferences>(qboContextoAuth, new Preferences());
            //Change the data of added entity
            Preferences changed = QBOHelper.SparseUpdatePreferences(qboContextoAuth, found.Id, found.SyncToken);
            //Update the returned entity data
            Preferences updated = Helper.Update<Preferences>(qboContextoAuth, changed);//Verify the updated Preferences
            QBOHelper.VerifyPreferencesSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]
        public void PreferencesDeleteTestUsingoAuth()
        {
            //Creating the Preferences for Adding
            Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            //Delete the returned entity
            try
            {
                Preferences deleted = Helper.Delete<Preferences>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod] [Ignore]
        public void PreferencesVoidTestUsingoAuth()
        {
            //Creating the Preferences for Adding
            Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            //Void the returned entity
            try
            {
                Preferences voided = Helper.Void<Preferences>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod] [Ignore]
        public void PreferencesCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PreferencesAddTestUsingoAuth();

            //Retrieving the Preferences using CDC
            List<Preferences> entities = Helper.CDC(qboContextoAuth, new Preferences(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Preferences>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PreferencesBatchUsingoAuth()
        {
            DataService.DataService service = new DataService.DataService(qboContextoAuth);
            Preferences found = Helper.FindOrAdd<Preferences>(qboContextoAuth, new Preferences());
            
            DataService.Batch batch = service.CreateNewBatch();
            batch.Add(QBOHelper.UpdatePreferences(qboContextoAuth, found),"Update",OperationEnum.update);
            
            batch.Execute();

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = batch.IntuitBatchItemResponses;

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Preferences).Id));
                position++;
            }
        }

        #endregion

        #region Test cases for Query

        [TestMethod]
        public void PreferencesQueryUsingoAuth()
        {
            QueryService<Preferences> entityQuery = new QueryService<Preferences>(qboContextoAuth);
            Preferences existing = Helper.FindOrAdd<Preferences>(qboContextoAuth, new Preferences());
            //List<Preferences> entities = entityQuery.Where(c => c.MetaData.CreateTime == existing.MetaData.CreateTime).ToList();
            List<Preferences> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Preferences").ToList<Preferences>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion


        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod] [Ignore]
        public void PreferencesAddAsyncTestsUsingoAuth()
        {
            //Creating the Preferences for Add
            Preferences entity = QBOHelper.CreatePreferences(qboContextoAuth);

            Preferences added = Helper.AddAsync<Preferences>(qboContextoAuth, entity);
            QBOHelper.VerifyPreferences(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void PreferencesRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //PreferencesAddTestUsingoAuth();

            //Retrieving the Preferences using FindAll
            Helper.FindAllAsync<Preferences>(qboContextoAuth, new Preferences());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void PreferencesFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Preferences for Adding
            //  Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            //  Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            List<Preferences> foundall = Helper.FindAll<Preferences>(qboContextoAuth, new Preferences());

            Assert.IsTrue(foundall.Count > 0);

            foreach (Preferences found in foundall)
            {
                Helper.FindByIdAsync<Preferences>(qboContextoAuth, found);
            }
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void PreferencesUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Preferences for Adding
            //Preferences preferences = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            //Preferences added = Helper.Add<Preferences>(qboContextoAuth, preferences);
            //Change the data of added entity
            List<Preferences> foundall = Helper.FindAll<Preferences>(qboContextoAuth, new Preferences());

            Assert.IsTrue(foundall.Count > 0);

            foreach (Preferences found in foundall)
            {
                Preferences changed = QBOHelper.UpdatePreferences(qboContextoAuth, found);
                //Update the returned entity data
                Preferences updated = Helper.UpdateAsync<Preferences>(qboContextoAuth, changed);//Verify the updated Preferences
                QBOHelper.VerifyPreferences(changed, updated);
            }
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]
        public void PreferencesDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Preferences for Adding
            Preferences entity = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            Preferences added = Helper.Add<Preferences>(qboContextoAuth, entity);

            Helper.DeleteAsync<Preferences>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void PreferencesVoidAsyncTestsUsingoAuth()
        {
            //Creating the Preferences for Adding
            Preferences entity = QBOHelper.CreatePreferences(qboContextoAuth);
            //Adding the Preferences
            Preferences added = Helper.Add<Preferences>(qboContextoAuth, entity);

            Helper.VoidAsync<Preferences>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
