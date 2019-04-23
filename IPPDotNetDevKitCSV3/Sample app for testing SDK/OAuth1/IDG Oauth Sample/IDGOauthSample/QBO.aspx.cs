using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.LinqExtender;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Threading;

//This sample app is just for some random testing of SDK. This hsould not be used as is in Prod. 
//You should use the sample apps we have for all SDKs on https://github.com/IntuitDeveloper 

namespace IDGOauthSample
{
    public partial class QBO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string realmId = HttpContext.Current.Session["realm"].ToString();
            string accessToken = HttpContext.Current.Session["accessToken"].ToString();
            string accessTokenSecret = HttpContext.Current.Session["accessTokenSecret"].ToString();
            string consumerKey = HttpContext.Current.Session["consumerKey"].ToString();
            string consumerSecret = HttpContext.Current.Session["consumerSecret"].ToString();


       


            OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);

            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            //serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://quickbooks.api.intuit.com/";

            serviceContext.IppConfiguration.Message.Request.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Xml;
            serviceContext.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Xml;
            serviceContext.IppConfiguration.MinorVersion.Qbo = "11";


           // serviceContext.IppConfiguration.RetryPolicy = new Intuit.Ipp.Retry.IntuitRetryPolicy(serviceContext,3, new TimeSpan(0, 0, 10));
           // serviceContext.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
           // serviceContext.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"F:\Logs";

            //serviceContext.RequestId = "897kjhjjhkh9";
           
            DataService commonServiceQBO = new DataService(serviceContext);

            #region 'testing for Retry'
             
            //try
            //{
            //    Item it = new Item();
            //    it.Name = "Hindi";

            //    var ResultIt1 = commonServiceQBO.Add<Item>(it);

            //    //var ResultIt = AddAsync<Item>(serviceContext, it);
            //}

            //catch (RetryExceededException ex)
            //{
            //    throw ex;
            //}
            //catch (IdsException ex)
            //{
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


            #endregion

            try
            {
                QueryService<VendorCredit> inService1 = new QueryService<VendorCredit>(serviceContext);
                VendorCredit In1 = inService1.ExecuteIdsQuery("SELECT * FROM VendorCredit").FirstOrDefault();
            }
            catch(RetryExceededException ex)
            {
                throw ex;
            }
            catch (IdsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }



            //var bill = new Intuit.Ipp.Data.Bill() { Id = "‼" };
            //var result = commonServiceQBO.FindById(bill);
            //QueryService<Item> inService1 = new QueryService<Item>(serviceContext);
            //Item In1 = inService1.ExecuteIdsQuery("SELECT * FROM Item").FirstOrDefault();

            //List<Account> accounts = commonServiceQBO.FindAll<Account>(new Account(), 1, 100).ToList<Account>();


            //QueryService <Invoice> inService = new QueryService<Invoice>(serviceContext);
            //Invoice In = inService.ExecuteIdsQuery("SELECT * FROM Invoice where Id='337'").FirstOrDefault();
            //var j = inService.ExecuteIdsQuery("SELECT * FROM Invoice where Id='337'").ToList<Invoice>();

            #region Attachable
            //string imagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\", "Resource\\my_data.xlsx");
            //System.IO.FileInfo file = new System.IO.FileInfo(imagePath);
            //Attachable attachable = new Attachable();
            //attachable.Lat = "25.293112341223";
            //attachable.Long = "-21.3253249834";
            //attachable.PlaceName = "Fake Place";
            //attachable.Note = "Attachable note123 ";
            //attachable.Tag = "Attachable tag123 ";

            //AttachableRef[] attachments = new AttachableRef[1];
            //AttachableRef ar = new AttachableRef();
            //ar.EntityRef = new ReferenceType();
            //ar.EntityRef.type = objectNameEnumType.Invoice.ToString();
            //ar.EntityRef.name = objectNameEnumType.Invoice.ToString();
            //ar.EntityRef.Value = "337";
            ////ar.EntityRef.type = objectNameEnumType.Bill.ToString();
            ////ar.EntityRef.name = objectNameEnumType.Bill.ToString();
            ////ar.EntityRef.Value = "1484";
            //attachments[0] = ar;
            //attachable.AttachableRef = attachments;

            //using (System.IO.FileStream fs = file.OpenRead())
            //{
            //    //attachable.ContentType = "image/bmp";
            //    ////attachable.FileName = file.Name;
            //    //attachable.FileName = "image.bmp";
            //    //Attachable attachableUploaded = commonServiceQBO.Upload(attachable, fs);
            //    //byte[] responseByte = commonServiceQBO.Download(attachableUploaded);
            //    //fs.Close();
            //    attachable.ContentType = "application/vnd.ms-excel";//application pdf
            //    //attachable.FileName = file.Name;
            //    attachable.FileName = "my_data.xlsx";
            //    Attachable attachableUploaded = commonServiceQBO.Upload(attachable, fs);
            //    byte[] responseByte = commonServiceQBO.Download(attachableUploaded);
            //    fs.Close();
            //}

            //Attachable res11 = commonServiceQBO.Add<Attachable>(attachable);
            #endregion

        }

        #region Retry Async
        //public static T AddAsync<T>(ServiceContext context, T entity) where T : IEntity
        //{
        //    //Initializing the Dataservice object with ServiceContext
        //    DataService service = new DataService(context);

        //    bool isAdded = false;

        //    IdsException exp = null;

        //    T actual = (T)Activator.CreateInstance(entity.GetType());
        //    // Used to signal the waiting test thread that a async operation have completed.    
        //    ManualResetEvent manualEvent = new ManualResetEvent(false);

        //    // Async callback events are anonomous and are in the same scope as the test code,    
        //    // and therefore have access to the manualEvent variable.    
        //    service.OnAddAsyncCompleted += (sender, e) =>
        //    {
        //        isAdded = true;
        //        manualEvent.Set();
        //        if (e.Error != null)
        //        {
        //            exp = e.Error;
        //        }
        //        if (exp == null)
        //        {
        //            if (e.Entity != null)
        //            {
        //                actual = (T)e.Entity;
        //            }
        //        }
        //    };

        //    // Call the service method
        //    service.AddAsync(entity);

        //    manualEvent.WaitOne(30000, false); Thread.Sleep(10000);

        //    if (exp != null)
        //    {
        //        throw exp;
        //    }

        //    // Check if we completed the async call, or fail the test if we timed out.    
        //    if (!isAdded)
        //    {
        //        Exception ex = new Exception();
        //        throw ex;
        //    }

        //    // Set the event to non-signaled before making next async call.    
        //    manualEvent.Reset();

        //    return actual;

        //}
        #endregion

    }
}