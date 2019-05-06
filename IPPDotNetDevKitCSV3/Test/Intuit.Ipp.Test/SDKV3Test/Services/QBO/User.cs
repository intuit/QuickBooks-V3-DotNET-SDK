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
    public class UserTest
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
        public void UserAddTestUsingoAuth()
        {
            //Creating the User for Add
            User user = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, user);
            //Verify the added User
            QBOHelper.VerifyUser(user, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void UserFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UserAddTestUsingoAuth();

            //Retrieving the User using FindAll
            List<User> users = Helper.FindAll<User>(qboContextoAuth, new User(), 1, 500);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count<User>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void UserFindbyIdTestUsingoAuth()
        {
            //Creating the User for Adding
            User user = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, user);
            User found = Helper.FindById<User>(qboContextoAuth, added);
            QBOHelper.VerifyUser(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void UserUpdateTestUsingoAuth()
        {
            //Creating the User for Adding
            User user = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, user);
            //Change the data of added entity
            User changed = QBOHelper.UpdateUser(qboContextoAuth, added);
            //Update the returned entity data
            User updated = Helper.Update<User>(qboContextoAuth, changed);//Verify the updated User
            QBOHelper.VerifyUser(changed, updated);
        }

        [TestMethod]
        public void UserSparseUpdateTestUsingoAuth()
        {
            //Creating the User for Adding
            User user = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, user);
            //Change the data of added entity
            User changed = QBOHelper.UpdateUserSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            User updated = Helper.Update<User>(qboContextoAuth, changed);//Verify the updated User
            QBOHelper.VerifyUserSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void UserDeleteTestUsingoAuth()
        {
            //Creating the User for Adding
            User user = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, user);
            //Delete the returned entity
            try
            {
                User deleted = Helper.Delete<User>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UserVoidTestUsingoAuth()
        {
            //Creating the User for Adding
            User User = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, User);
            //Void the returned entity
            try
            {
                User voided = Helper.Void<User>(qboContextoAuth, added);
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
        public void UserCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UserAddTestUsingoAuth();

            //Retrieving the User using CDC
            List<User> entities = Helper.CDC(qboContextoAuth, new User(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<User>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void UserBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            User existing = Helper.FindOrAdd(qboContextoAuth, new User());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateUser(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateUser(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from User");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<User>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as User).Id));
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
        public void UserQueryUsingoAuth()
        {
            QueryService<User> entityQuery = new QueryService<User>(qboContextoAuth);
            User existing = Helper.FindOrAdd<User>(qboContextoAuth, new User());
            //List<User> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
           int count= entityQuery.ExecuteIdsQuery("Select * from User where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void UserAddAsyncTestsUsingoAuth()
        {
            //Creating the User for Add
            User entity = QBOHelper.CreateUser(qboContextoAuth);

            User added = Helper.AddAsync<User>(qboContextoAuth, entity);
            QBOHelper.VerifyUser(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void UserRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UserAddTestUsingoAuth();

            //Retrieving the User using FindAll
            Helper.FindAllAsync<User>(qboContextoAuth, new User());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void UserFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the User for Adding
            User entity = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<User>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void UserUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the User for Adding
            User entity = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, entity);

            //Update the User
            User updated = QBOHelper.UpdateUser(qboContextoAuth, added);
            //Call the service
            User updatedReturned = Helper.UpdateAsync<User>(qboContextoAuth, updated);
            //Verify updated User
            QBOHelper.VerifyUser(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void UserDeleteAsyncTestsUsingoAuth()
        {
            //Creating the User for Adding
            User entity = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, entity);

            Helper.DeleteAsync<User>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void UserVoidAsyncTestsUsingoAuth()
        {
            //Creating the User for Adding
            User entity = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the User
            User added = Helper.Add<User>(qboContextoAuth, entity);

            Helper.VoidAsync<User>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
