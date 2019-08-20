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
    [TestClass] [Ignore]
    public class TemplateNameTest
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
        public void TemplateNameAddTestUsingoAuth()
        {
            //Creating the TemplateName for Add
            TemplateName templateName = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, templateName);
            //Verify the added TemplateName
            QBOHelper.VerifyTemplateName(templateName, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TemplateNameFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TemplateNameAddTestUsingoAuth();

            //Retrieving the TemplateName using FindAll
            List<TemplateName> templateNames = Helper.FindAll<TemplateName>(qboContextoAuth, new TemplateName(), 1, 500);
            Assert.IsNotNull(templateNames);
            Assert.IsTrue(templateNames.Count<TemplateName>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TemplateNameFindbyIdTestUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName templateName = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, templateName);
            TemplateName found = Helper.FindById<TemplateName>(qboContextoAuth, added);
            QBOHelper.VerifyTemplateName(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void TemplateNameUpdateTestUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName templateName = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, templateName);
            //Change the data of added entity
            TemplateName changed = QBOHelper.UpdateTemplateName(qboContextoAuth, added);
            //Update the returned entity data
            TemplateName updated = Helper.Update<TemplateName>(qboContextoAuth, changed);//Verify the updated TemplateName
            QBOHelper.VerifyTemplateName(changed, updated);
        }

        [TestMethod]
        public void TemplateNameSparseUpdateTestUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName templateName = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, templateName);
            //Change the data of added entity
            TemplateName changed = QBOHelper.UpdateTemplateNameSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            TemplateName updated = Helper.Update<TemplateName>(qboContextoAuth, changed);//Verify the updated TemplateName
            QBOHelper.VerifyTemplateNameSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void TemplateNameDeleteTestUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName templateName = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, templateName);
            //Delete the returned entity
            try
            {
                TemplateName deleted = Helper.Delete<TemplateName>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TemplateNameVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the entity
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                TemplateName voided = Helper.Void<TemplateName>(qboContextoAuth, added);
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
        public void TemplateNameCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TemplateNameAddTestUsingoAuth();

            //Retrieving the TemplateName using CDC
            List<TemplateName> entities = Helper.CDC(qboContextoAuth, new TemplateName(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<TemplateName>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TemplateNameBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            TemplateName existing = Helper.FindOrAdd(qboContextoAuth, new TemplateName());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateTemplateName(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTemplateName(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from TemplateName");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<TemplateName>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as TemplateName).Id));
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
        public void TemplateNameQueryUsingoAuth()
        {
            QueryService<TemplateName> entityQuery = new QueryService<TemplateName>(qboContextoAuth);
            TemplateName existing = Helper.FindOrAdd<TemplateName>(qboContextoAuth, new TemplateName());
            //List<TemplateName> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from TemplateName where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void TemplateNameAddAsyncTestsUsingoAuth()
        {
            //Creating the TemplateName for Add
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);

            TemplateName added = Helper.AddAsync<TemplateName>(qboContextoAuth, entity);
            QBOHelper.VerifyTemplateName(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TemplateNameRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TemplateNameAddTestUsingoAuth();

            //Retrieving the TemplateName using FindAll
            Helper.FindAllAsync<TemplateName>(qboContextoAuth, new TemplateName());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TemplateNameFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<TemplateName>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void TemplateNameUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, entity);

            //Update the TemplateName
            TemplateName updated = QBOHelper.UpdateTemplateName(qboContextoAuth, added);
            //Call the service
            TemplateName updatedReturned = Helper.UpdateAsync<TemplateName>(qboContextoAuth, updated);
            //Verify updated TemplateName
            QBOHelper.VerifyTemplateName(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void TemplateNameDeleteAsyncTestsUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, entity);

            Helper.DeleteAsync<TemplateName>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void TemplateNameVoidAsyncTestsUsingoAuth()
        {
            //Creating the TemplateName for Adding
            TemplateName entity = QBOHelper.CreateTemplateName(qboContextoAuth);
            //Adding the TemplateName
            TemplateName added = Helper.Add<TemplateName>(qboContextoAuth, entity);

            Helper.VoidAsync<TemplateName>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
