using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Core;
using Intuit.Ipp.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.LinqExtender;
using Intuit.Ipp.ReportService;


namespace Intuit.Ipp.Test
{
    public class Helper
    {
        internal static T Add<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            //Adding the Bill using Dataservice object

            T added = service.Add<T>(entity);

            //get IntuitEntity from entity
            if (added.GetType().IsSubclassOf(typeof(IntuitEntity)))
            {
                IntuitEntity addedEntity = (IntuitEntity)(object)added;

                //Checking id of added Bill is not Null. If it is Null, Bill is not added properly
                Assert.IsNotNull(addedEntity.Id);
            }

            return added;

        }

        internal static List<T> FindAll<T>(ServiceContext context, T entity, int startPosition = 1, int maxResults = 100) where T : IEntity
        {
            DataService.DataService service = new DataService.DataService(context);

            ReadOnlyCollection<T> entityList = service.FindAll(entity, startPosition, maxResults);

            return entityList.ToList<T>();
        }

        internal static T FindById<T>(ServiceContext context, T entity) where T : IEntity
        {
            DataService.DataService service = new DataService.DataService(context);
            T foundEntity = service.FindById(entity);

            return foundEntity;
        }

        internal static T Update<T>(ServiceContext context, T entity) where T : IEntity
        {
            //initializing the dataservice object with servicecontext
            DataService.DataService service = new DataService.DataService(context);

            //updating the entity
            T updated = service.Update<T>(entity);

            return updated;
        }

        
        internal static T UpdateAccountOnTxnsFrance<T>(ServiceContext context, T entity) where T : IEntity
        {
            //initializing the dataservice object with servicecontext
            DataService.DataService service = new DataService.DataService(context);

            //updating the entity
            // update account for historical transactions 
            T updated = service.UpdateAccountOnTxns<T>(entity);

            return updated;
        }
       

        internal static T DonotUpdateAccountOnTxnsFrance<T>(ServiceContext context, T entity) where T : IEntity
        {
            //initializing the dataservice object with servicecontext
            DataService.DataService service = new DataService.DataService(context);

            //updating the entity
            // do not update account for historical transactions 
            T updated = service.DoNotUpdateAccountOnTxns<T>(entity);

            return updated;
        }


        internal static T SparseUpdate<T>(ServiceContext context, T entity) where T : IEntity
        {
            //initializing the dataservice object with servicecontext
            DataService.DataService service = new DataService.DataService(context);

            //updating the entity
            T updated = service.Update<T>(entity);

            return updated;
        }

        internal static T Delete<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            //Deleting the Bill using the service
            T deleted = service.Delete<T>(entity);

            return deleted;
        }


        internal static T Void<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            //Voiding the entity using the service
            service.Void<T>(entity);

            try
            {
                //Retrieving the voided entity
                T found = service.FindById<T>(entity);
                Assert.IsNotNull(found);
                return found;
            }
            catch (IdsException)
            {
            }

            return entity;
        }

        internal static List<T> CDC<T>(ServiceContext context, T entity, DateTime changedSince) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            List<IEntity> entityList = new List<IEntity>();
            entityList.Add(entity);

            IntuitCDCResponse response = service.CDC(entityList, changedSince);
            if (response.entities.Count == 0)
            {
                return null;
            }
            //Retrieving the entity List
            List<T> found = response.getEntity(entity.GetType().Name).Cast<T>().ToList();
            Assert.IsNotNull(found);
            return found;
        }

        internal static Attachable Upload(ServiceContext context, Attachable attachable, System.IO.Stream stream)
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            Attachable uploaded = service.Upload(attachable, stream);
            return uploaded;
        }

        internal static byte[] Download(ServiceContext context, Attachable entity)
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            return service.Download(entity);
        }

        internal static ReadOnlyCollection<IntuitBatchResponse> BatchTest<T>(ServiceContext context, Dictionary<OperationEnum, object> operationDictionary) where T : IEntity
        {
            DataService.DataService service = new DataService.DataService(context);
            List<T> addedList = new List<T>();
            List<T> newList = new List<T>();


            QueryService<T> entityContext = new QueryService<T>(context);

            DataService.Batch batch = service.CreateNewBatch();

            foreach (KeyValuePair<OperationEnum, object> entry in operationDictionary)
            {
                if (entry.Value.GetType().Name.Equals(typeof(T).Name))
                    batch.Add(entry.Value as IEntity, entry.Key.ToString() + entry.Value.GetType().Name, entry.Key);
                else
                    batch.Add(entry.Value as string, "Query" + typeof(T).Name);
            }


            batch.Execute();

            return batch.IntuitBatchItemResponses;
        }


        internal static Boolean CheckEqual(Object expected, Object actual)
        {
            return true;
        }

        internal static String GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        internal static Report GetReportAsync(ServiceContext context, string reportName)
        {
            //Initializing the Dataservice object with ServiceContext
            ReportService.ReportService service = new ReportService.ReportService(context);

            IdsException exp = null;
            Boolean reportReturned = false;
            Report actual = null;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnExecuteReportAsyncCompleted += (sender, e) =>
            {
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                if (exp == null)
                {
                    if (e.Report != null)
                    {
                        reportReturned = true;
                        actual = e.Report;
                    }
                }
            };

            // Call the service method
            service.ExecuteReportAsync(reportName);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }

            // Check if we completed the async call, or fail the test if we timed out.    
            if (!reportReturned)
            {
                Assert.Fail(String.Format("Retrieving {0} Report Failed", reportName));
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            Assert.IsNotNull(actual);
            return actual;

        }

        internal static T AddAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isAdded = false;

            IdsException exp = null;

            T actual = (T)Activator.CreateInstance(entity.GetType());
            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnAddAsyncCompleted += (sender, e) =>
            {
                isAdded = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                if (exp == null)
                {
                    if (e.Entity != null)
                    {
                        actual = (T)e.Entity;
                    }
                }
            };

            // Call the service method
            service.AddAsync(entity);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }

            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isAdded)
            {
                Assert.Fail("Adding Entity Failed");
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            return actual;

        }

        internal static List<T> FindAllAsync<T>(ServiceContext context, T entity, int startPosition = 1, int maxResults = 500) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isFindAll = false;

            IdsException exp = null;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            List<T> entities = new List<T>();

            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnFindAllAsyncCompleted += (sender, e) =>
            {
                isFindAll = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                if (exp == null)
                {
                    if (e.Entities != null)
                    {
                        foreach (IEntity en in e.Entities)
                        {
                            entities.Add((T)en);
                        }
                    }
                }
            };

            // Call the service method
            service.FindAllAsync<T>(entity, 1, 10);

            manualEvent.WaitOne(60000, false); Thread.Sleep(10000);


            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isFindAll)
            {
                Assert.Fail("Find All Bill Failed");
            }

            if (exp != null)
            {
                throw exp;
            }

            if (entities != null)
            {
                Assert.IsTrue(entities.Count >= 0);
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();
            return entities;
        }

        internal static T FindByIdAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isFindById = false;

            IdsException exp = null;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            T returnedEntity = default(T);

            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnFindByIdAsyncCompleted += (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Entity);
                manualEvent.Set();
                isFindById = true;
                returnedEntity = (T)e.Entity;
            };

            // Call the service method
            service.FindByIdAsync<T>(entity);
            manualEvent.WaitOne(60000, false); Thread.Sleep(10000);

            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isFindById)
            {
                Assert.Fail("FindByID Failed");
            }

            if (exp != null)
            {
                throw exp;
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();
            return returnedEntity;
        }

        internal static T UpdateAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isUpdated = false;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            IdsException exp = null;

            T returnedEntity = entity;
            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnUpdateAsyncCompleted += (sender, e) =>
            {

                isUpdated = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                else
                {
                    if (e.Entity != null)
                    {
                        returnedEntity = (T)e.Entity;
                    }
                }
            };

            // Call the service method
            service.UpdateAsync(entity);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }
            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isUpdated)
            {
                Assert.Fail("Update Failed");
            }

            //if entity is returned returnedEntity will be set with new object with same values
            Assert.AreNotEqual(returnedEntity, entity);

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            return returnedEntity;
        }

     
        internal static T UpdateAccountOnTxnsAsyncFrance<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isUpdated = false;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            IdsException exp = null;

            T returnedEntity = entity;
            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    

            //check this line for change
            service.OnUpdateAccAsyncCompleted += (sender, e) =>
            {

                isUpdated = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                else
                {
                    if (e.Entity != null)
                    {
                        returnedEntity = (T)e.Entity;
                    }
                }
            };

            // Call the service method
            service.UpdateAccAsync(entity);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }
            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isUpdated)
            {
                Assert.Fail("UpdateAccountOnTxns Failed");
            }

            //if entity is returned returnedEntity will be set with new object with same values
            Assert.AreNotEqual(returnedEntity, entity);

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            return returnedEntity;
        }

        internal static T DonotUpdateAccountOnTxnsAsyncFrance<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isUpdated = false;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            IdsException exp = null;

            T returnedEntity = entity;
            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    

            //check this line for change
            service.OnDoNotUpdateAccAsyncCompleted += (sender, e) =>
            {

                isUpdated = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                else
                {
                    if (e.Entity != null)
                    {
                        returnedEntity = (T)e.Entity;
                    }
                }
            };

            // Call the service method
            service.DoNotUpdateAccountOnTxns(entity);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }
            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isUpdated)
            {
                Assert.Fail("DoNotUpdateAccountOnTxns Failed");
            }

            //if entity is returned returnedEntity will be set with new object with same values
            Assert.AreNotEqual(returnedEntity, entity);

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            return returnedEntity;
        }


        internal static T DeleteAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isDeleted = false;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            IdsException exp = null;
            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            T returnedEntity = entity;
            service.OnDeleteAsyncCompleted += (sender, e) =>
            {
                isDeleted = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }
                else
                {
                    if (e.Entity != null)
                    {
                        returnedEntity = (T)e.Entity;
                    }
                }
            };

            // Call the service method
            service.DeleteAsync(entity);

            manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }
            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isDeleted)
            {
                Assert.Fail("Delete Failed");
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();

            return returnedEntity;
        }

        internal static void VoidAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            //Initializing the Dataservice object with ServiceContext
            DataService.DataService service = new DataService.DataService(context);

            bool isVoided = false;

            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            IdsException exp = null;
            // Async callback events are anonomous and are in the same scope as the test code,    
            // and therefore have access to the manualEvent variable.    
            service.OnVoidAsyncCompleted += (sender, e) =>
            {
                isVoided = true;
                manualEvent.Set();
                if (e.Error != null)
                {
                    exp = e.Error;
                }

            };

            // Call the service method
            service.VoidAsync(entity);

            manualEvent.WaitOne(30000, false);
            Thread.Sleep(10000);

            if (exp != null)
            {
                throw exp;
            }
            // Check if we completed the async call, or fail the test if we timed out.    
            if (!isVoided)
            {
                Assert.Fail("Void Failed");
            }

            // Set the event to non-signaled before making next async call.    
            manualEvent.Reset();
        }

        internal static T FindOrAdd<T>(ServiceContext context, T entity) where T : IEntity
        {
            List<T> returnedEntities = FindAll<T>(context, entity, 1, 500);
            int count = 0;
            foreach (T en in returnedEntities)
            {
                if ((returnedEntities[count] as IntuitEntity).status != EntityStatusEnum.SyncError)
                    return returnedEntities[count];
                count++;
            }


            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {
                if ((context.ServiceType == IntuitServicesType.QBO && type.Name == "QBOHelper"))
                {
                    String methodName = "Create" + entity.GetType().Name;
                    MethodInfo method = type.GetMethod("Create" + entity.GetType().Name, bindingFlags);
                    entity = (T)method.Invoke(null, new object[] { context });
                    T returnEntity = Add(context, entity);
                    return returnEntity;
                }
            }
            throw new System.ApplicationException("Could not find QBOHelper");
        }

        internal static Account FindOrAddAccount(ServiceContext context, AccountTypeEnum accountType, AccountClassificationEnum classification)
        {
            Account typeOfAccount = null;
            List<Account> listOfAccount = FindAll<Account>(context, new Account(), 1, 500);
            if (listOfAccount.Count > 0)
            {
                foreach (Account acc in listOfAccount)
                {
                    if (acc.AccountType == accountType && acc.Classification == classification && acc.status != EntityStatusEnum.SyncError)
                    {
                        typeOfAccount = acc;
                        break;
                    }
                }
            }

            if (typeOfAccount == null)
            {
                DataService.DataService service = new DataService.DataService(context);
                Account account;
                
                account = QBOHelper.CreateAccount(context, accountType, classification);
                account.Classification = classification;
                account.AccountType = accountType;
                Account createdAccount = service.Add<Account>(account);
                typeOfAccount = createdAccount;
                

            }

            return typeOfAccount;
        }

        internal static Purchase FindOrAddPurchase(ServiceContext context, PaymentTypeEnum paymentType)
        {
            Purchase typeOfPurchase = null;
            List<Purchase> listOfPurchase = FindAll<Purchase>(context, new Purchase(), 1, 10).Where(p => p.status != EntityStatusEnum.SyncError).ToList();
            if (listOfPurchase.Count > 0)
            {
                if (context.ServiceType == IntuitServicesType.QBO)
                {

                    foreach (Purchase payment in listOfPurchase)
                    {
                        if (payment.PaymentType == paymentType)
                        {
                            typeOfPurchase = payment;
                            break;
                        }
                    }

                    if (typeOfPurchase == null)
                    {
                        //create a new purchase account
                        DataService.DataService service = new DataService.DataService(context);
                        Purchase purchase;
                        purchase = QBOHelper.CreatePurchase(context, PaymentTypeEnum.Cash);
                        //purchase.PaymentType = paymentType;
                        //purchase.PaymentTypeSpecified = true;
                        Purchase createdPurchase = service.Add<Purchase>(purchase);
                        typeOfPurchase = createdPurchase;
                    }
                }
                
            }

            return typeOfPurchase;
        }


        internal static PaymentMethod FindOrAddPaymentMethod(ServiceContext context, string paymentType)
        {
            PaymentMethod typeOfPayment = null;
            List<PaymentMethod> listOfPayment = FindAll<PaymentMethod>(context, new PaymentMethod(), 1, 10).Where(p => p.status != EntityStatusEnum.SyncError).ToList();
            if (listOfPayment.Count > 0)
            {
                if (context.ServiceType == IntuitServicesType.QBO)
                {

                    foreach (PaymentMethod payment in listOfPayment)
                    {
                        if (payment.Type == paymentType)
                        {
                            typeOfPayment = payment;
                            break;
                        }
                    }

                    if (typeOfPayment == null)
                    {
                        //Create a new purchase account
                        DataService.DataService service = new DataService.DataService(context);
                        PaymentMethod payment;
                        payment = QBOHelper.CreatePaymentMethod(context);
                        PaymentMethod createdPurchase = service.Add<PaymentMethod>(payment);
                        typeOfPayment = createdPurchase;
                    }
                }
               
            }

            return typeOfPayment;
        }

        internal static Item FindOrAddItem(ServiceContext context, ItemTypeEnum type)
        {
            Item typeOfItem = null;
            List<Item> listOfItem = FindAll<Item>(context, new Item(), 1, 500).Where(i => i.status != EntityStatusEnum.SyncError).ToList();
            if (listOfItem.Count > 0)
            {
                foreach (Item item in listOfItem)
                {
                    if (item.Type == type)
                    {
                        typeOfItem = item;
                        break;
                    }
                }
            }

            if (typeOfItem == null)
            {
                DataService.DataService service = new DataService.DataService(context);
                Item item;
                
                    item = QBOHelper.CreateItem(context);
                    Item createdItem = service.Add<Item>(item);
                    typeOfItem = createdItem;
                

            }

            return typeOfItem;
        }

       
    }
}
