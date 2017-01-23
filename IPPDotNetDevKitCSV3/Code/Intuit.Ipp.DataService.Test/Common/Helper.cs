using System;
using System.Collections.Generic;
using System.IO;
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


namespace Intuit.Ipp.Core.Test.Common
{
    /// <summary>
    /// Summary description for Helper.
    /// </summary>

    public class Helper
    {
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
        internal static T Update<T>(ServiceContext context, T entity) where T : IEntity
        {
            //initializing the dataservice object with servicecontext
            DataService.DataService service = new DataService.DataService(context);

            //updating the entity
            T updated = service.Update<T>(entity);

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

        internal static T GetPdfAsync<T>(ServiceContext context, T entity) where T : IEntity
        {
            try
            {
                //Initializing the Dataservice object with ServiceContext
                DataService.DataService service = new DataService.DataService(context);

                

                IdsException exp = null;

                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);

                T returnedEntity = default(T);
                string fileName = String.Empty;
                byte[] originalBytes = new byte[0];

                // Async callback events are anonomous and are in the same scope as the test code,    
                // and therefore have access to the manualEvent variable.    
                service.OnGetPdfAsyncCompleted += (sender, e) =>
                {
                    //bool isFindById = false;
                    Assert.IsNotNull(e);
                    manualEvent.Set();
                    //isFindById = true;
                    fileName = String.Format(@"C:\{0}_{1}.pdf", entity.GetType().FullName, Guid.NewGuid());
                    File.WriteAllBytes(fileName, e.PdfBytes);
                    originalBytes = e.PdfBytes;
                };

                // Call the service method
                service.GetPdfAsync<T>(entity);
                manualEvent.WaitOne(200000, false); Thread.Sleep(10000);

                //check if file exists
                Assert.IsTrue(File.Exists(fileName));

                //read the file from bytes and compare bytes
                byte[] readFromFile = File.ReadAllBytes(fileName);

                //bytes read from file should be greater than 0
                Assert.IsTrue(readFromFile.Length > 0);

                for (int i = 0; i < readFromFile.Length; i++)
                    Assert.AreEqual(originalBytes[i], readFromFile[i]);

                //cleanup
                if (File.Exists(fileName))
                    File.Delete(fileName);

                if (exp != null)
                {
                    throw exp;
                }

                // Set the event to non-signaled before making next async call.    
                manualEvent.Reset();
                return returnedEntity;
            }
            catch (IdsException idsEx)
            {
                
                throw idsEx;
            }
        }

        internal static void GetPdfAsyncNullEntity<T>(ServiceContext context, T entity) where T : IEntity
        {
            try
            {
                //Initializing the Dataservice object with ServiceContext
                DataService.DataService service = new DataService.DataService(context);

                //bool isFindById = false;

                //IdsException exp = null;

                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);

                //T returnedEntity = default(T);
                string fileName = String.Empty;
                byte[] originalBytes = new byte[0];

                // Async callback events are anonomous and are in the same scope as the test code,    
                // and therefore have access to the manualEvent variable.    
                service.OnGetPdfAsyncCompleted += (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Error);
                    manualEvent.Set();
                };

                // Call the service method
                service.GetPdfAsync(entity);
                manualEvent.WaitOne(30000);

            }
            catch (IdsException idsEx)
            {

                throw idsEx;
            }
        }
        
        internal static T SendEmail<T>(ServiceContext context, T entity, String sendToEmail = null) where T : IEntity
        {
            try
            {
                //Initializing the Dataservice object with ServiceContext
                DataService.DataService service = new DataService.DataService(context);

                IdsException exp = null;

                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);

                T returnedEntity = default(T);
                

                // Async callback events are anonomous and are in the same scope as the test code,    
                // and therefore have access to the manualEvent variable.    
                service.OnSendEmailAsyncCompleted += (sender, e) =>
                {
                    if (e.Entity == null)
                        throw new IdsException(e.Error.Message);
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Entity);
                    manualEvent.Set();
                    returnedEntity = (T)e.Entity;
                };

                // Call the service method
                service.SendEmailAsync<T>(entity, sendToEmail);
                manualEvent.WaitOne(20000, false); Thread.Sleep(10000);

                
                if (exp != null)
                {
                    throw exp;
                }

                // Set the event to non-signaled before making next async call.    
                manualEvent.Reset();
                return returnedEntity;
            }
            catch (IdsException idsEx)
            {

                throw idsEx;
            }
        }
    }
}
