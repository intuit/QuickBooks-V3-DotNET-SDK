using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Exception;
using Intuit.Ipp.PlatformServices;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using IppDotNetDevKit_MvcWebRole.Models;
using LinqExtender;

namespace IppDotNetDevKit_MvcWebRole.ServiceOperations
{
    public class ServiceOperations
    {
        #region Private Members

        private SessionManagement.SessionManagement sessionManagement;
        private OAuthRequestValidator validator;
        private ServiceContext context, platformContext;
        private DataService dataServices;
        private PlatformService platformService;
        private static string requestXml, responseXml;
        private static string codeSnippet;
        private QueryService<Invoice> invoiceQuery;
        private Batch batchService;

        #endregion

        private ServiceOperations()
        {
            this.sessionManagement = new SessionManagement.SessionManagement();
        }

        public ServiceOperations(ServiceType serviceType)
            : this()
        {
            SettingsModel settingsModel = this.sessionManagement.GetSettingsModelFromSession("SettingsModel");
            switch (serviceType)
            {
                case ServiceType.QBD:
                    //OAuthRequestValidator qbdRrequestValidator = new OAuthRequestValidator(settingsModel.QbdAccessToken, settingsModel.QbdAccessTokenSecret, settingsModel.QbdConsumerKey, settingsModel.QbdConsumerSecret);
                    //this.context = new ServiceContext(settingsModel.QbdRealmId, IntuitServicesType.QBD, qbdRrequestValidator);
                    //this.context.IppConfiguration.Logger.CustomLogger = new SampleLogging();
                    this.context = new ServiceContext("498127830", IntuitServicesType.QBD);
                    this.dataServices = new DataService(this.context);
                    break;
                case ServiceType.QBO:
                    // TODO
                    break;
                case ServiceType.IPS:
                    OAuthRequestValidator ippRrequestValidator = new OAuthRequestValidator(
                        "qyprdKp0k78HcVs5mqbCmwD9kbOatTxc9zRG6WUqGuhIVkmc", 
                        "GJglKFLFOda2qxgfSPCcaAjMfD9JHbcrL7qxSVZ4", 
                        "qyprdhtVWI7KWRSePnCA0D3FcL9UFN", 
                        "Nt6oZvlQJxZYB3abCs9P6U6J4kFTenDlw8a4SQOs");
                    this.platformContext = ServiceContext.Create(appToken: "cqn5r9sc9m8qkvc9yi43pipgniu", appDbid: "bg7rqchr5", requestValidator: ippRrequestValidator);
                    //this.platformContext.IppConfiguration.Logger.CustomLogger = new SampleLogging();
                    this.platformService = new PlatformService(this.platformContext);
                    break;
                case ServiceType.Query:
                    //OAuthRequestValidator queryRrequestValidator = new OAuthRequestValidator(settingsModel.QbdAccessToken, settingsModel.QbdAccessTokenSecret, settingsModel.QbdConsumerKey, settingsModel.QbdConsumerSecret);
                    //this.context = new ServiceContext(settingsModel.QbdRealmId, IntuitServicesType.QBD, queryRrequestValidator);
                    //this.context.IppConfiguration.Logger.CustomLogger = new SampleLogging();
                    this.context = new ServiceContext("498127830", IntuitServicesType.QBD);
                    this.invoiceQuery = new QueryService<Invoice>(this.context);
                    break;
            }
        }

        #region QBD

        #region Qbd Customer

        internal Customer FindByIdQbdCustomer(string id)
        {
            try
            {
                Customer toFindCustomer = new Customer
                {
                    Id = id,
                    domain = idDomainEnum.NG.ToString()
                };

                return this.dataServices.FindById(toFindCustomer);
            }
            catch (IdsException ex)
            {
                // handle exception
            }

            return null;
        }

        internal OperationStatus CreateQbdCustomer(Customer customer)
        {
            OperationStatus status = new OperationStatus();
            try
            {
                this.ValidateCustomer(customer);
                Customer addedCustomer = this.dataServices.Add<Customer>(customer);
                status.Success = true;
                this.sessionManagement.AddToSession("Operation", "Create");
                this.sessionManagement.AddCustomerToSession("Entity", addedCustomer);
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", ex.Message, ex.ErrorCode);
                ValidationException valEx = ex.InnerException as ValidationException;
                if (valEx != null)
                {
                    responseXml += string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", valEx.Message, valEx.ErrorCode);
                    foreach (var item in valEx.InnerExceptions)
                    {
                        responseXml += string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", item.Message, item.ErrorCode);
                    }
                }

                // set the error.
                status.Success = false;
                status.Error = responseXml;
            }

            return status;
        }

        internal OperationStatus UpdateQbdCustomer(UpdateModel customer)
        {
            OperationStatus status = new OperationStatus();
            try
            {
                Customer toFindCustomer = new Customer();
                toFindCustomer.Id = customer.Id;
                Customer toUpdateCustomer = this.dataServices.FindById(toFindCustomer);
                if (toUpdateCustomer != null)
                {
                    toUpdateCustomer.GivenName = customer.GivenName;
                    toUpdateCustomer.MiddleName = customer.MiddleName;
                    toUpdateCustomer.FamilyName = customer.FamilyName;
                    toUpdateCustomer.sparse = customer.Sparse;
                    toUpdateCustomer.sparseSpecified = true;
                    Customer updatedCustomer = this.dataServices.Update(toUpdateCustomer);
                    status.Success = true;
                    this.sessionManagement.AddToSession("Operation", "Update");
                    this.sessionManagement.AddCustomerToSession("Entity", updatedCustomer);
                }
                else
                {
                    status.Success = false;
                    status.Error = "Could not find the resource.";
                }
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                // set the error
                status.Success = false;
                status.Error = responseXml;
            }

            return status;
        }

        internal OperationStatus DeleteQbdCustomer(Customer customer)
        {
            OperationStatus status = new OperationStatus();
            try
            {
                this.dataServices.Delete(customer);
                status.Success = true;
                this.sessionManagement.AddToSession("Operation", "Delete");
                this.sessionManagement.AddCustomerToSession("Entity", customer);
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                // set the error
                status.Success = false;
                status.Error = responseXml;
            }

            return status;
        }

        internal IEnumerable<Customer> FindAllQbdCustomer(int pageNo, int pageSize, bool hitService)
        {
            IEnumerable<Customer> customers = null;
            customers = this.dataServices.FindAll(new Customer(), pageNo, pageSize);
            //customerXml1 = this.context.Serializer.Serialize(customers.ToList());
            return customers;
        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer.BillingAddr != null)
            {
                if (string.IsNullOrWhiteSpace(customer.BillingAddr.Tag))
                {
                    customer.BillingAddr = null;
                }
            }

            if (customer.Mobile != null)
            {
                if (string.IsNullOrWhiteSpace(customer.Mobile.Tag))
                {
                    customer.Mobile = null;
                }
            }

            if (customer.PrimaryEmailAddr != null)
            {
                if (string.IsNullOrWhiteSpace(customer.PrimaryEmailAddr.Tag))
                {
                    customer.PrimaryEmailAddr = null;
                }
            }

            if (customer.WebAddr != null)
            {
                if (string.IsNullOrWhiteSpace(customer.WebAddr.Tag))
                {
                    customer.WebAddr = null;
                }
            }
        }

        internal OperationStatus VoidQbdCustomer(Customer customer)
        {
            OperationStatus status = new OperationStatus();
            try
            {
                this.dataServices.Void(customer);
                status.Success = true;
                this.sessionManagement.AddToSession("Operation", "Void");
                this.sessionManagement.AddCustomerToSession("Entity", customer);
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                // set the error
                status.Success = false;
                status.Error = responseXml;
            }

            return status;
        }

        #endregion

        #region Qbd Query

        internal QueryModel.WhereQuery WhereQbdInvoiceQuery(QueryModel.WhereQuery whereQuery)
        {
            try
            {
                IQueryable<Invoice> query = null;
                switch (whereQuery.OperationEnum)
                {
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.EQUAL:
                        query = this.invoiceQuery.Where(i => i.TotalAmt == whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.NOT_EQUAL:
                        query = this.invoiceQuery.Where(i => i.TotalAmt != whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.GREATER_THAN:
                        query = this.invoiceQuery.Where(i => i.TotalAmt > whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.LESS_THAN:
                        query = this.invoiceQuery.Where(i => i.TotalAmt < whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.GREATER_THAN_EQUAL:
                        query = this.invoiceQuery.Where(i => i.TotalAmt >= whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.LESS_THAN_EQUAL:
                        query = this.invoiceQuery.Where(i => i.TotalAmt <= whereQuery.TotalAmt);
                        break;
                    case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.NOT:
                        query = this.invoiceQuery.Where(i => !(i.TotalAmt == whereQuery.TotalAmt));
                        break;
                }

                whereQuery.Entities = query.ToList();
                whereQuery.Success = true;
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                whereQuery.Success = false;
                whereQuery.Error = responseXml;
            }

            return whereQuery;
        }

        internal QueryModel.SelectQuery SelectQbdInvoiceQuery(QueryModel.SelectQuery selectQuery)
        {
            List<QueryModel.Customer> clientCustomer = new List<QueryModel.Customer>();
            if (selectQuery.Status && selectQuery.TotalAmt)
            {
                var query = this.invoiceQuery.Select(s => new { s.Id, s.status, s.TotalAmt });
                foreach (var item in query)
                {
                    clientCustomer.Add(new QueryModel.Customer
                    {
                        Id = item.Id,
                        Status = item.status.ToString(),
                        TotalAmt = item.TotalAmt
                    });
                }
            }
            else if (selectQuery.Status)
            {
                var query = this.invoiceQuery.Select(s => new { s.Id, s.status });
                foreach (var item in query)
                {
                    clientCustomer.Add(new QueryModel.Customer
                    {
                        Id = item.Id,
                        Status = item.status.ToString()
                    });
                }
            }
            else if (selectQuery.TotalAmt)
            {
                var query = this.invoiceQuery.Select(s => new { s.Id, s.TotalAmt });
                foreach (var item in query)
                {
                    clientCustomer.Add(new QueryModel.Customer
                    {
                        Id = item.Id,
                        TotalAmt = item.TotalAmt,
                        Status = Constants.NotSpecified
                    });
                }
            }
            else
            {
                var query = this.invoiceQuery.Select(s => s.Id);
                foreach (var item in query)
                {
                    clientCustomer.Add(new QueryModel.Customer
                    {
                        Id = item,
                        Status = Constants.NotSpecified
                    });
                }
            }

            selectQuery.ClientCustomers = clientCustomer;
            return selectQuery;
        }

        internal QueryModel.CountQuery CountQbdInvoiceQuery(QueryModel.CountQuery countQuery)
        {
            if (countQuery.StatusSpecified)
            {
                countQuery.Count = this.invoiceQuery.Where(i => i.status == countQuery.Status).Count();
            }
            else
            {
                countQuery.Count = this.invoiceQuery.Count();
            }

            return countQuery;
        }

        internal IEnumerable<Invoice> FindAllQbdInvoice(FindAllModel findAllModel)
        {
            return this.invoiceQuery.Skip(findAllModel.PageNumber).Take(findAllModel.PageSize).ToList();
        }

        internal IEnumerable<Invoice> OrderByQbdInvoice(QueryModel.OrderByModel orderByModel)
        {
            IQueryable<Invoice> query = null;
            if (orderByModel.MetaData_CreateTime)
            {
                if (orderByModel.MetaData_CreateTime_Descending)
                {
                    query = this.invoiceQuery.OrderByDescending(o => o.MetaData.CreateTime);
                }
                else
                {
                    query = this.invoiceQuery.OrderBy(o => o.MetaData.CreateTime);
                }
            }
            else
            {
                if (orderByModel.MetaData_LastUpdatedTime_Descending)
                {
                    query = this.invoiceQuery.OrderByDescending(o => o.MetaData.CreateTime);
                }
                else
                {
                    query = this.invoiceQuery.OrderBy(o => o.MetaData.CreateTime);
                }
            }

            return query.ToList();
        }

        internal IEnumerable<Invoice> ComplexQueryQbdInvoice(QueryModel.ComplexQueryModel complexModel)
        {
            IQueryable<Invoice> query = null;
            switch (complexModel.OperationEnum)
            {
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.EQUAL:
                    query = this.invoiceQuery.Where(i => i.TotalAmt == complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.NOT_EQUAL:
                    query = this.invoiceQuery.Where(i => i.TotalAmt != complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.GREATER_THAN:
                    query = this.invoiceQuery.Where(i => i.TotalAmt > complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.LESS_THAN:
                    query = this.invoiceQuery.Where(i => i.TotalAmt < complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.GREATER_THAN_EQUAL:
                    query = this.invoiceQuery.Where(i => i.TotalAmt >= complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.LESS_THAN_EQUAL:
                    query = this.invoiceQuery.Where(i => i.TotalAmt <= complexModel.TotalAmt);
                    break;
                case IppDotNetDevKit_MvcWebRole.Models.OperationEnum.NOT:
                    query = this.invoiceQuery.Where(i => !(i.TotalAmt == complexModel.TotalAmt));
                    break;
            }

            if (complexModel.MetaData_CreateTime)
            {
                if (complexModel.MetaData_CreateTime_Descending)
                {
                    if (query != null)
                    {
                        query = query.OrderByDescending(o => o.MetaData.CreateTime);
                        //query = this.invoiceQuery.OrderByDescending(o => o.MetaData.CreateTime);
                    }
                }
                else
                {
                    if (query != null)
                    {
                        query = query.OrderBy(o => o.MetaData.CreateTime);
                        //query = this.invoiceQuery.OrderBy(o => o.MetaData.CreateTime);
                    }
                }
            }

            if (complexModel.PageNumber >= 0)
            {
                query = query.Skip(complexModel.PageNumber);
            }

            if (complexModel.PageSize > 0)
            {
                query = query.Take(complexModel.PageSize);
            }

            if (query == null)
            {
                query = this.invoiceQuery.Select(i => i);
            }

            return query.ToList();
        }

        internal QueryModel.QueryStringModel ExecuteIdsQuery(QueryModel.QueryStringModel queryString)
        {
            try
            {
                queryString.Entities = this.invoiceQuery.ExecuteIdsQuery(queryString.SimpleQuery, QueryOperationType.query);
                queryString.Error = null;
                queryString.Success = true;
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", ex.Message, ex.ErrorCode);
                if (ex.InnerExceptions != null)
                {
                    foreach (var item in ex.InnerExceptions)
                    {
                        responseXml += string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", item.Message, item.ErrorCode);
                    }
                }

                queryString.Success = false;
                queryString.Error = responseXml;
            }

            return queryString;
        }

        internal QueryModel.MultipleQueriesModel ExecuteMultipleQuery(QueryModel.MultipleQueriesModel multipleQueries)
        {
            try
            {
                multipleQueries.InvoicesList = this.invoiceQuery.ExecuteMultipleEntityQueries<Invoice>(new List<string> { multipleQueries.Query1, multipleQueries.Query2 }.AsReadOnly());
                multipleQueries.Error = null;
                multipleQueries.Success = true;
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", ex.Message, ex.ErrorCode);
                if (ex.InnerExceptions != null)
                {
                    foreach (var item in ex.InnerExceptions)
                    {
                        responseXml += string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]\n", item.Message, item.ErrorCode);
                    }
                }

                multipleQueries.Success = false;
                multipleQueries.Error = responseXml;
            }

            return multipleQueries;
        }

        #endregion

        #endregion

        #region Platform Services

        internal void GetIdsRealm(PlatformModel model)
        {
            try
            {
                model.Result = this.platformService.GetIdsRealm(model.Value);
                model.Success = true;
                model.Error = null;
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                model.Success = false;
                model.Error = responseXml;
            }
        }

        internal void GetIsRealmQbo(PlatformModel model)
        {
            try
            {
                model.Result = this.platformService.GetIsRealmQbo(model.Value).ToString();
                model.Success = true;
                model.Error = null;
            }
            catch (IdsException ex)
            {
                responseXml = string.Format("------ Message: [ {0} ] ------ ErrorCode: [ {1} ]", ex.Message, ex.ErrorCode);
                model.Success = false;
                model.Error = responseXml;
            }
        }

        #endregion

        #region Batch

        internal void BatchExecute(Models.Qbd.BatchModel batchModel)
        {
            if (batchModel.Response.UseCompression)
            {
                this.context.IppConfiguration.Message.Response.CompressionFormat = Intuit.Ipp.Core.Configuration.CompressionFormat.GZip;
            }

            if (batchModel.Response.UseJson)
            {
                this.context.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Json;
            }

            Batch batch = this.dataServices.CreateNewBatch();
            if (batchModel.CustomerAdd)
            {
                Customer customer = new Customer();
                customer.DisplayName = batchModel.DisplayName_Customer;
                batch.Add(customer, "addCustomer", Intuit.Ipp.Data.OperationEnum.create);
            }

            if (batchModel.CustomerFindById)
            {
                QueryService<Customer> customerQueryService = new QueryService<Customer>(this.context);
                string customerQuery = customerQueryService.Where(c => c.Id == batchModel.Id_Customer).ToIdsQuery();
                batch.Add(customerQuery, "customerFindById");
            }

            if (batchModel.InvoiceFindById)
            {
                QueryService<Invoice> invoiceQueryService = new QueryService<Invoice>(this.context);
                string invoiceQuery = invoiceQueryService.Where(c => c.Id == batchModel.Id_Invoice).ToIdsQuery();
                batch.Add(invoiceQuery, "invoiceFindById");
            }

            if (batchModel.CustomerFindAll)
            {
                QueryService<Customer> customerQueryService = new QueryService<Customer>(this.context);
                string customerQuery = customerQueryService.ToIdsQuery();
                batch.Add(customerQuery, "customerFindAll");
            }

            if (batchModel.InvoiceFindAll)
            {
                QueryService<Invoice> invoiceQueryService = new QueryService<Invoice>(this.context);
                string invoiceQuery = invoiceQueryService.ToIdsQuery();
                batch.Add(invoiceQuery, "invoiceFindAll");
            }

            try
            {

                batch.Execute();
                batchModel.Exception = null;
                batchModel.IntuitBatchResponse = batch.IntuitBatchItemResponses;
            }
            catch (IdsException ex)
            {
                batchModel.Exception = ex;
                batchModel.IntuitBatchResponse = null;
            }

            this.context.IppConfiguration.Message.Response.CompressionFormat = Intuit.Ipp.Core.Configuration.CompressionFormat.None;
            this.context.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Xml;
        }

        #endregion
    }
}