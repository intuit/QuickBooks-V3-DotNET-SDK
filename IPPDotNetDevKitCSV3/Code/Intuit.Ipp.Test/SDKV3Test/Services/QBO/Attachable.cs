using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Threading;
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class AttachableTest
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
        public void AttachableAddTestUsingoAuth()
        {
            //Creating the Attachable for Add
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            //Verify the added Attachable
            QBOHelper.VerifyAttachable(attachable, added);
        }

        [DeploymentItem("Services\\Resource\\image.jpg", "Services\\Resource")]
        [TestMethod]
        public void AttachableUploadDownloadAddTestUsingoAuth()
        {
            //Creating the Bill for Add

            string imagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\", "Services\\Resource\\image.jpg");
            System.IO.FileInfo file = new System.IO.FileInfo(imagePath);
            Attachable attachable = QBOHelper.CreateAttachableUpload(qboContextoAuth);
            using (System.IO.FileStream fs = file.OpenRead())
                {
                    attachable.ContentType = "image/jpeg";
                    attachable.FileName = file.Name;
                    attachable = Helper.Upload(qboContextoAuth, attachable, fs);
                }

            byte[] uploadedByte = null;
            using (System.IO.FileStream fs = file.OpenRead())
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    uploadedByte = binaryReader.ReadBytes((int) fs.Length);
                }
            }

            //Verify the added Attachable
            Assert.IsNotNull(attachable.Id);

            byte[] responseByte = Helper.Download(qboContextoAuth, attachable);

            for(int i=0; i<responseByte.Length; i++)
                Assert.AreEqual(uploadedByte[i], responseByte[i]);

        }
        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void AttachableFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AttachableAddTestUsingoAuth();

            //Retrieving the Attachable using FindAll
            List<Attachable> attachables = Helper.FindAll<Attachable>(qboContextoAuth, new Attachable(), 1, 500);
            Assert.IsNotNull(attachables);
            Assert.IsTrue(attachables.Count<Attachable>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void AttachableFindbyIdTestUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            Attachable found = Helper.FindById<Attachable>(qboContextoAuth, added);
            QBOHelper.VerifyAttachable(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void AttachableUpdateTestUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            //Change the data of added entity
            Attachable changed = QBOHelper.UpdateAttachable(qboContextoAuth, added);
            //Update the returned entity data
            Attachable updated = Helper.Update<Attachable>(qboContextoAuth, changed);//Verify the updated Attachable
            QBOHelper.VerifyAttachable(changed, updated);
        }

        [TestMethod]
        public void AttachableSparseUpdateTestUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            //Change the data of added entity
            Attachable changed = QBOHelper.SparseUpdateAttachable(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Attachable updated = Helper.Update<Attachable>(qboContextoAuth, changed);//Verify the updated Attachable
            QBOHelper.VerifyAttachableSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void AttachableDeleteTestUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            //Delete the returned entity
            try
            {
                Attachable deleted = Helper.Delete<Attachable>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void AttachableVoidTestUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable attachable = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, attachable);
            //Delete the returned entity
            try
            {
                Attachable voided = Helper.Void<Attachable>(qboContextoAuth, added);
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
        public void AttachableCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AttachableAddTestUsingoAuth();

            //Retrieving the Attachable using CDC
            List<Attachable> entities = Helper.CDC(qboContextoAuth, new Attachable(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Attachable>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void AttachableBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Attachable existing = Helper.FindOrAdd(qboContextoAuth, new Attachable());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateAttachable(qboContextoAuth));

            //batchEntries.Add(OperationEnum.update, QBOHelper.UpdateAttachable(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Attachable");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Attachable>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Attachable).Id));
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
        public void AttachableQueryUsingoAuth()
        {
            QueryService<Attachable> entityQuery = new QueryService<Attachable>(qboContextoAuth);
            Attachable existing = Helper.FindOrAdd<Attachable>(qboContextoAuth, new Attachable());
            //List<Attachable> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Attachable> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Attachable where Id='" + existing.Id + "'").ToList<Attachable>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void AttachableAddAsyncTestsUsingoAuth()
        {
            //Creating the Attachable for Add
            Attachable entity = QBOHelper.CreateAttachable(qboContextoAuth);

            Attachable added = Helper.AddAsync<Attachable>(qboContextoAuth, entity);
            QBOHelper.VerifyAttachable(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void AttachableRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AttachableAddTestUsingoAuth();

            //Retrieving the Attachable using FindAll
            Helper.FindAllAsync<Attachable>(qboContextoAuth, new Attachable());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void AttachableFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable entity = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Attachable>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void AttachableUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable entity = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, entity);

            //Update the Attachable
            Attachable updated = QBOHelper.UpdateAttachable(qboContextoAuth, added);
            //Call the service
            Attachable updatedReturned = Helper.UpdateAsync<Attachable>(qboContextoAuth, updated);
            //Verify updated Attachable
            QBOHelper.VerifyAttachable(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void AttachableDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable entity = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, entity);

            Helper.DeleteAsync<Attachable>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void AttachableVoidAsyncTestsUsingoAuth()
        {
            //Creating the Attachable for Adding
            Attachable entity = QBOHelper.CreateAttachable(qboContextoAuth);
            //Adding the Attachable
            Attachable added = Helper.Add<Attachable>(qboContextoAuth, entity);

            Helper.VoidAsync<Attachable>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
