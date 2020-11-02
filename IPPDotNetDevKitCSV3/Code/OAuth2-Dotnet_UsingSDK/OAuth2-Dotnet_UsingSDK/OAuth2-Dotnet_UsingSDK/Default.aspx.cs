

/******************************************************
 * Intuit sample app for Oauth2 using Intuit .Net SDK
 * RFC docs- https://tools.ietf.org/html/rfc6749
 * ****************************************************/

//https://stackoverflow.com/questions/23562044/window-opener-is-undefined-on-internet-explorer/26359243#26359243
//IE issue- https://stackoverflow.com/questions/7648231/javascript-issue-in-ie-with-window-opener

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web.UI;
using System.Configuration;
using System.Web;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Threading.Tasks;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Linq;
using Intuit.Ipp.ReportService;
using Intuit.Ipp.Diagnostics;
using Serilog;


namespace OAuth2_Dotnet_UsingSDK
{
    public partial class Default : System.Web.UI.Page
    {
        // OAuth2 client configuration
        static string redirectURI = ConfigurationManager.AppSettings["redirectURI"];
        static string clientID = ConfigurationManager.AppSettings["clientID"];
        static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        static string logPath = ConfigurationManager.AppSettings["logPath"];
        static string appEnvironment = ConfigurationManager.AppSettings["appEnvironment"];
        //static string appEnvironment = "https://developer-e2e.api.intuit.com/.well-known/openid_configuration";
        static OAuth2Client oauthClient = new OAuth2Client(clientID, clientSecret, redirectURI, appEnvironment);
        static string authCode;
        static string idToken;
        public static IList<JsonWebKey> keys;
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!dictionary.ContainsKey("accessToken"))
            {
                mainButtons.Visible = true;
                connected.Visible = false;
            }
            else
            {
                mainButtons.Visible = false;
                connected.Visible = true;
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
    
            oauthClient.EnableAdvancedLoggerInfoMode = true;
            oauthClient.EnableSerilogRequestResponseLoggingForConsole = true;
            oauthClient.EnableSerilogRequestResponseLoggingForDebug = true;
            oauthClient.EnableSerilogRequestResponseLoggingForRollingFile = true;
            oauthClient.EnableSerilogRequestResponseLoggingForTrace = true;
            oauthClient.ServiceRequestLoggingLocationForFile = @"C:\Documents\Serilog_log";//check correct path on machine

            //SeriLogger seri = new SeriLogger();
            //seri.Log(TraceLevel.Verbose, "Nimisha typing");
            AsyncMode = true;
            if (!dictionary.ContainsKey("accessToken"))
            {
                if (Request.QueryString.Count > 0)
                {
                    var response = new AuthorizeResponse(Request.QueryString.ToString());

                    if (response.State != null)
                    {
                        //if (oauthClient.CSRFToken == response.State)
                        //{
                            if (response.RealmId != null)
                            {
                              // seri.Log(TraceLevel.Verbose, response.RealmId);
                                if (!dictionary.ContainsKey("realmId"))
                                {
                                    dictionary.Add("realmId", response.RealmId);
                                }
                            }

                            if (response.Code != null)
                            {
                                authCode = response.Code;

                                Output("Authorization code obtained.");
                                PageAsyncTask t = new PageAsyncTask(PerformCodeExchange);
                                Page.RegisterAsyncTask(t);
                                Page.ExecuteRegisteredAsyncTasks();
                            }
                        //}
                        //else
                        //{
                        //    Output("Invalid State");
                        //    dictionary.Clear();
                        //}
                    }
                }
            }
            else
            {
                mainButtons.Visible = false;
                connected.Visible = true;
            }
        }

        #region button click events

       protected void ImgOpenId_Click(object sender, ImageClickEventArgs e)
       {
        //    Output("Intiating OpenId call.");
        //    try
        //    {
        //        if (!dictionary.ContainsKey("accessToken"))
        //        {
        //            List<OidcScopes> scopes = new List<OidcScopes>();
        //            scopes.Add(OidcScopes.OpenId);
        //            scopes.Add(OidcScopes.Phone);
        //            scopes.Add(OidcScopes.Profile);
        //            scopes.Add(OidcScopes.Address);
        //            scopes.Add(OidcScopes.Email);

        //            //string authorizationRequest = oauthClient.GetAuthorizationURL(scopes);
        //            string authorizationRequest = oauthClient.GetAuthorizationURL(scopes);
        //            TraceLogger target = new TraceLogger();

        //            target.Log(TraceLevel.Info, authorizationRequest);
        //            string newstr= string.Format(authorizationRequest+ "&claims=" + Uri.EscapeDataString("{\"id_token\":{\"realmId\":null}}"));
        //            Response.Redirect((newstr), "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");

        //            //Response.Redirect((authorizationRequest), "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Output(ex.Message);
        //    }
        }

        protected void ImgC2QB_Click(object sender, ImageClickEventArgs e)
        {
            //    Output("Intiating OAuth2 call.");
            //    try
            //    {
            //        if (!dictionary.ContainsKey("accessToken"))
            //        {
            //            List<OidcScopes> scopes = new List<OidcScopes>();
            //            scopes.Add(OidcScopes.Accounting);
            //            var authorizationRequest = oauthClient.GetAuthorizationURL(scopes);
            //            Response.Redirect(authorizationRequest, "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Output(ex.Message);
            //    }
        }

        protected void ImgGetAppNow_Click(object sender, ImageClickEventArgs e)
        {
            Output("Intiating Get App Now call.");
            try
            {
                if (!dictionary.ContainsKey("accessToken"))
                {
                    List<OidcScopes> scopes = new List<OidcScopes>();
                    scopes.Add(OidcScopes.Accounting);
                    scopes.Add(OidcScopes.OpenId);
                    scopes.Add(OidcScopes.Phone);
                    scopes.Add(OidcScopes.Profile);
                    scopes.Add(OidcScopes.Address);
                    scopes.Add(OidcScopes.Email);

                   //string authorizationRequest = "https://appcenter.intuit.com/connect/oauth2?client_id=Q0joY7aqSROhDAspO4JLoH6ChEtpBvYalZIUwQOg3u5cwbVEaL&response_type=code&scope=com.intuit.quickbooks.accounting%20openid%20phone%20profile%20address%20email&redirect_uri=http%3A%2F%2Flocalhost%3A59785%2FDefault.aspx&state=06b6121265a883cf3b42e9585fc93a318df592bd47b6b1e81725c2bc54523ab9";

                    var authorizationRequest = oauthClient.GetAuthorizationURL(scopes);
                    Response.Redirect(authorizationRequest, "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
                }
            }
            catch (Exception ex)
            {
                Output(ex.Message);
            }
        }

        protected async void btnQBOAPICall_Click(object sender, EventArgs e)
        {
            if (dictionary.ContainsKey("accessToken") && dictionary.ContainsKey("realmId"))
            {
                await QboApiCall();
            }
            else
            {
                Output("Access token not found.");
                lblQBOCall.Visible = true;
                lblQBOCall.Text = "Access token not found.";
            }
        }

        protected async void btnUserInfo_Click(object sender, EventArgs e)
        {
            //if (idToken != null)
            //{
            //    var userInfoResp = await oauthClient.GetUserInfoAsync(dictionary["accessToken"]);
            //    lblUserInfo.Visible = true;
            //    lblUserInfo.Text = userInfoResp.Raw;
            //}
            //else
            //{
            //    lblUserInfo.Visible = true;
            //    lblUserInfo.Text = "UserInfo call is available through OpenId/GetAppNow flow first.";
            //    Output("Go through OpenId flow first.");
            //}
        }

        protected async void btnRefresh_Click(object sender, EventArgs e)
        {
            //if ((dictionary.ContainsKey("accessToken")) && (dictionary.ContainsKey("refreshToken")))
            //{
            //    Output("Exchanging refresh token for access token.");
            //    var tokenResp = await oauthClient.RefreshTokenAsync(dictionary["refreshToken"]);
            //}
        }

        protected async void btnRevoke_Click(object sender, EventArgs e)
        {
            //Output("Performing Revoke tokens.");
            //if ((dictionary.ContainsKey("accessToken")) && (dictionary.ContainsKey("refreshToken")))
            //{
            //    var revokeTokenResp = await oauthClient.RevokeTokenAsync(dictionary["refreshToken"]);
            //    if (revokeTokenResp.HttpStatusCode == HttpStatusCode.OK)
            //    {
            //        dictionary.Clear();
            //        if (Request.Url.Query == "")
            //            Response.Redirect(Request.RawUrl);
            //        else
            //            Response.Redirect(Request.RawUrl.Replace(Request.Url.Query, ""));
            //    }
            //    Output("Token revoked.");
            //}
        }
        #endregion

        /// <summary>
        /// Start code exchange to get the Access Token and Refresh Token
        /// </summary>
        public async System.Threading.Tasks.Task PerformCodeExchange()
        {
            Output("Exchanging code for tokens.");
            try
            {

                TokenClient t = new TokenClient("https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer", clientID, clientSecret);
                var tokenResp = await t.RequestTokenFromCodeAsync(authCode, redirectURI);
                //var tokenResp = await oauthClient.GetBearerTokenAsync(authCode);
         
            
                if (!dictionary.ContainsKey("accessToken"))
                    dictionary.Add("accessToken", tokenResp.AccessToken);
                else
                    dictionary["accessToken"] = tokenResp.AccessToken;

                if (!dictionary.ContainsKey("refreshToken"))
                    dictionary.Add("refreshToken", tokenResp.RefreshToken);
                else
                    dictionary["refreshToken"] = tokenResp.RefreshToken;

                if (tokenResp.IdentityToken != null)
                    idToken = tokenResp.IdentityToken;
             
                
                if (Request.Url.Query == "")
                {
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Redirect(Request.RawUrl.Replace(Request.Url.Query, ""));
                }
            }
            catch (Exception ex)
            {
                Output("Problem while getting bearer tokens.");
            }
        }

        /// <summary>
        /// Test QBO api call
        /// </summary>
        public async System.Threading.Tasks.Task QboApiCall()
        {
            //try
            //{
            //    if ((dictionary.ContainsKey("accessToken")) && (dictionary.ContainsKey("realmId")))
            //    {
            //        Output("Making QBO API Call.");
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(dictionary["accessToken"]);
            ServiceContext context = new ServiceContext(dictionary["realmId"], IntuitServicesType.QBO, oauthValidator);
            context.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            //        //serviceContext.IppConfiguration.BaseUrl.Qbo = "https://quickbooks.api.intuit.com/";//prod
            context.IppConfiguration.MinorVersion.Qbo = "54";
            //context.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //context.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\Documents\Serilog_log";
            //context.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForRollingFile = true;
            //context.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForConsole = true;
            //context.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForTrace = true;
            //context.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForDebug = true;
            //context.IppConfiguration.AdvancedLogger.RequestAdvancedLog.ServiceRequestLoggingLocationForFile = @"C:\Documents\Serilog_log";

            QueryService<ReimburseCharge> rb1 = new QueryService<ReimburseCharge>(context);
            ReimburseCharge rbb1 = rb1.ExecuteIdsQuery("Select * From ReimburseCharge StartPosition 1 MaxResults 1").First();


            QueryService<Invoice> in1 = new QueryService<Invoice>(context);
            Invoice inn1 = in1.ExecuteIdsQuery("Select * From Invoice where Id='27' StartPosition 1 MaxResults 1").First();

            QueryService<RecurringTransaction> re1 = new QueryService<RecurringTransaction>(context);
            RecurringTransaction r1 = re1.ExecuteIdsQuery("Select * From RecurringTransaction StartPosition 1 MaxResults 1").First();


            DataService dataService = new DataService(context);
            //List<Intuit.Ipp.Data.IEntity> entityList1 = new List<Intuit.Ipp.Data.IEntity>();
            //entityList1.Add(new Intuit.Ipp.Data.Customer());
            //DateTimeOffset do1 = new DateTimeOffset(2020, 7, 1, 4, 29, 52, new TimeSpan(-7, 0, 0));

            //var CDCResponse1 = dataService.CDC(entityList1, do1.LocalDateTime).entities;


            //Customer cust = new Customer();
            //cust.DisplayName = "memo11";



            //Batch batch = dataService.CreateNewBatch();
            //batch.Add(cust, "CreateCustomer", OperationEnum.create);
            //batch.Add("select * from Customer", "CustomerQuery");
            //batch.Execute();

            //RecurringTransaction recur1 = new RecurringTransaction();
            //Invoice inv1 = new Invoice();
            //inv1.Id = r1.AnyIntuitObject.Id;
            //inv1.SyncToken = r1.AnyIntuitObject.SyncToken;
            //recur1.AnyIntuitObject = inv1;
            //var s = dataService.Delete(recur1);

            RecurringTransaction recur = new RecurringTransaction();
            //Find Customer
            QueryService<Customer> customerQueryService = new QueryService<Customer>(context);
            Customer customer = customerQueryService.ExecuteIdsQuery("Select * From Customer StartPosition 1 MaxResults 1").FirstOrDefault<Customer>();

            //Find Tax Code for Invoice - Searching for a tax code named 'StateSalesTax' in this example
            QueryService<TaxCode> stateTaxCodeQueryService = new QueryService<TaxCode>(context);
            TaxCode stateTaxCode = stateTaxCodeQueryService.ExecuteIdsQuery("Select * From TaxCode StartPosition 1 MaxResults 1").FirstOrDefault<TaxCode>();

            //Find Account - Accounts Receivable account required
            QueryService<Account> accountQueryService = new QueryService<Account>(context);
            Account account = accountQueryService.ExecuteIdsQuery("Select * From Account Where AccountType='Accounts Receivable' StartPosition 1 MaxResults 1").FirstOrDefault<Account>();

            //Find Item
            QueryService<Item> itemQueryService = new QueryService<Item>(context);
            Item item = itemQueryService.ExecuteIdsQuery("Select * From Item StartPosition 1 MaxResults 1").FirstOrDefault<Item>();

            //Find Term
            QueryService<Term> termQueryService = new QueryService<Term>(context);
            Term term = termQueryService.ExecuteIdsQuery("Select * From Term StartPosition 1 MaxResults 1").FirstOrDefault<Term>();

            Invoice invoice = new Invoice();
            //SalesReceipt invoice = new SalesReceipt();

            //DocNumber - QBO Only, otherwise use DocNumber
            invoice.AutoDocNumber = true;
            invoice.AutoDocNumberSpecified = true;

            //TxnDate
            invoice.TxnDate = DateTime.Now.Date;
            invoice.TxnDateSpecified = true;

            //PrivateNote
            invoice.PrivateNote = "This is a private note";

            //Line
            Line invoiceLine = new Line();
            //Line Description
            invoiceLine.Description = "Invoice line description.";
            //Line Amount
            invoiceLine.Amount = 330m;
            invoiceLine.AmountSpecified = true;
            //Line Detail Type
            invoiceLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            invoiceLine.DetailTypeSpecified = true;
            //Line Sales Item Line Detail
            SalesItemLineDetail lineSalesItemLineDetail = new SalesItemLineDetail();
            //Line Sales Item Line Detail - ItemRef
            lineSalesItemLineDetail.ItemRef = new ReferenceType()
            {
                name = item.Name,
                
                Value = item.Id
            };
            //Line Sales Item Line Detail - UnitPrice
            lineSalesItemLineDetail.AnyIntuitObject = 33m;
            lineSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            //Line Sales Item Line Detail - Qty
            lineSalesItemLineDetail.Qty = 10;
            lineSalesItemLineDetail.QtySpecified = true;
            //Line Sales Item Line Detail - TaxCodeRef
            //For US companies, this can be 'TAX' or 'NON'
            lineSalesItemLineDetail.TaxCodeRef = new ReferenceType()
            {
                Value = "NON"
            };
            //Line Sales Item Line Detail - ServiceDate 
            //lineSalesItemLineDetail.ServiceDate = DateTime.Now.Date;
            //lineSalesItemLineDetail.ServiceDateSpecified = true;
            //Assign Sales Item Line Detail to Line Item
            invoiceLine.AnyIntuitObject = lineSalesItemLineDetail;
            //Assign Line Item to Invoice
            invoice.Line = new Line[] { invoiceLine };

            //Line
            Line invoiceReimburseLine = new Line();
            //Line Description
            invoiceReimburseLine.Description = "Invoice line description.";
            //Line Amount
            invoiceReimburseLine.Amount = 330m;
            invoiceReimburseLine.AmountSpecified = true;
            //Line Detail Type
            invoiceReimburseLine.DetailType = LineDetailTypeEnum.ReimburseLineDetail;
            invoiceReimburseLine.DetailTypeSpecified = true;
            //Line Sales Item Line Detail
            ReimburseLineDetail lineReimburseLineDetail = new ReimburseLineDetail();
            //Line Sales Item Line Detail - ItemRef
            lineReimburseLineDetail.ItemRef = new ReferenceType()
            {
                name = item.Name,

                Value = item.Id
            };
            //Line Sales Item Line Detail - UnitPrice
            lineReimburseLineDetail.AnyIntuitObject = 33m;
            lineReimburseLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            //Line Sales Item Line Detail - Qty
            lineReimburseLineDetail.Qty = 10;
            lineReimburseLineDetail.QtySpecified = true;
            //Line Sales Item Line Detail - TaxCodeRef
            //For US companies, this can be 'TAX' or 'NON'
            lineReimburseLineDetail.TaxCodeRef = new ReferenceType()
            {
                Value = "NON"
            };
            //Line Sales Item Line Detail - ServiceDate 
            //lineSalesItemLineDetail.ServiceDate = DateTime.Now.Date;
            //lineSalesItemLineDetail.ServiceDateSpecified = true;
            //Assign Sales Item Line Detail to Line Item
            invoiceReimburseLine.AnyIntuitObject = lineReimburseLineDetail;
            //Assign Line Item to Invoice
            invoice.Line.Append(invoiceReimburseLine);

            //TxnTaxDetail
            TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            txnTaxDetail.TxnTaxCodeRef = new ReferenceType()
            {
                name = stateTaxCode.Name,
                Value = stateTaxCode.Id
            };
            //Line taxLine = new Line();
            //taxLine.DetailType = LineDetailTypeEnum.TaxLineDetail;
            //TaxLineDetail taxLineDetail = new TaxLineDetail();
            ////Assigning the fist Tax Rate in this Tax Code
            //taxLineDetail.TaxRateRef = stateTaxCode.SalesTaxRateList.TaxRateDetail[0].TaxRateRef;
            //taxLine.AnyIntuitObject = taxLineDetail;
            //txnTaxDetail.TaxLine = new Line[] { taxLine };
            invoice.TxnTaxDetail = txnTaxDetail;

            //Customer (Client)
            invoice.CustomerRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };

            //Billing Address
            PhysicalAddress billAddr = new PhysicalAddress();
            billAddr.Line1 = "123 Main St.";
            billAddr.Line2 = "Unit 506";
            billAddr.City = "Brockton";
            billAddr.CountrySubDivisionCode = "MA";
            billAddr.Country = "United States";
            billAddr.PostalCode = "02301";
            billAddr.Note = "Billing Address Note";
            invoice.BillAddr = billAddr;

            //Shipping Address
            PhysicalAddress shipAddr = new PhysicalAddress();
            shipAddr.Line1 = "100 Fifth Ave.";
            shipAddr.City = "Waltham";
            shipAddr.CountrySubDivisionCode = "MA";
            shipAddr.Country = "United States";
            shipAddr.PostalCode = "02452";
            shipAddr.Note = "Shipping Address Note";
            invoice.ShipAddr = shipAddr;

            //SalesTermRef
            invoice.SalesTermRef = new ReferenceType()
            {
                name = term.Name,
                Value = term.Id
            };

            //DueDate
            invoice.DueDate = DateTime.Now.AddDays(30).Date;
            invoice.DueDateSpecified = true;

            //ARAccountRef
            invoice.ARAccountRef = new ReferenceType()
            {
                name = account.Name,
                Value = account.Id
            };
            invoice.RecurringInfo = new RecurringInfo()
            {
                Active = true,
                ActiveSpecified = true,
                Name = "RecurTemplate143",
                RecurType = "Automated",
                ScheduleInfo = new RecurringScheduleInfo()
                {
                    DayOfMonth = 1,
                    DayOfMonthSpecified = true,
                    DaysBefore = 2,
                    DaysBeforeSpecified = true,
                    IntervalType = "Monthly",
                    MaxOccurrences = 2,
                    MaxOccurrencesSpecified = true,
                    NumInterval = 1,
                    NumIntervalSpecified = true
                }

            };

            recur.AnyIntuitObject = invoice;
            
            
            

            RecurringTransaction recurAdded = dataService.Add<RecurringTransaction>(recur);
          
       

            //QueryService<Customer> customerQueryService = new QueryService<Customer>(context);
            //Customer customer = customerQueryService.ExecuteIdsQuery("Select * From Customer StartPosition 1 MaxResults 1").FirstOrDefault<Customer>();


            
            ReportService reportService = new ReportService(context);

            //Date should be in the format YYYY - MM - DD
            //Response format hsold be JSON as that is only supported rigth now for reports
            //reportService.accounting_method = "Index";
            //reportService.start_date = "2018-01-01";
            //reportService.end_date = "2018-07-01";
            reportService.low_pp_date = "2020-01-01";
            reportService.high_pp_date = "2020-06-01";
            reportService.custom_pp = "yes";
            //reportService.classid = "2800000000000634813"; 
            //reportService.date_macro = "Last Month";
            //reportService.summarize_column_by = "Month";


            //List<String> columndata = new List<String>();
            //columndata.Add("tx_date");
            //columndata.Add("dept_name");
            //string coldata = String.Join(",", columndata);
            //reportService.columns = coldata;

            var report1 = reportService.ExecuteReport("ProfitAndLoss");

            //        try
            //        {
            //            //Find Customer
            //            QueryService<Customer> customerQueryService = new QueryService<Customer>(context);
            //            Customer customer = customerQueryService.ExecuteIdsQuery("Select * From Customer StartPosition 1 MaxResults 1").FirstOrDefault<Customer>();

            //            //Find Tax Code for Invoice - Searching for a tax code named 'StateSalesTax' in this example
            //            QueryService<TaxCode> stateTaxCodeQueryService = new QueryService<TaxCode>(context);
            //            TaxCode stateTaxCode = stateTaxCodeQueryService.ExecuteIdsQuery("Select * From TaxCode StartPosition 1 MaxResults 1").FirstOrDefault<TaxCode>();

            //            //Find Account - Accounts Receivable account required
            //            QueryService<Account> accountQueryService = new QueryService<Account>(context);
            //            Account account = accountQueryService.ExecuteIdsQuery("Select * From Account Where AccountType='Accounts Receivable' StartPosition 1 MaxResults 1").FirstOrDefault<Account>();

            //            //Find Item
            //            QueryService<Item> itemQueryService = new QueryService<Item>(context);
            //            Item item = itemQueryService.ExecuteIdsQuery("Select * From Item StartPosition 1 MaxResults 1").FirstOrDefault<Item>();

            //            //Find Term
            //            QueryService<Term> termQueryService = new QueryService<Term>(context);
            //            Term term = termQueryService.ExecuteIdsQuery("Select * From Term StartPosition 1 MaxResults 1").FirstOrDefault<Term>();

            //            Invoice invoice = new Invoice();

            //            //DocNumber - QBO Only, otherwise use DocNumber
            //            invoice.AutoDocNumber = true;
            //            invoice.AutoDocNumberSpecified = true;

            //            //TxnDate
            //            invoice.TxnDate = DateTime.Now.Date;
            //            invoice.TxnDateSpecified = true;

            //            //PrivateNote
            //            invoice.PrivateNote = "This is a private note";

            //            //Line
            //            Line invoiceLine = new Line();
            //            //Line Description
            //            invoiceLine.Description = "Invoice line description.";
            //            //Line Amount
            //            invoiceLine.Amount = 330m;
            //            invoiceLine.AmountSpecified = true;
            //            //Line Detail Type
            //            invoiceLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            //            invoiceLine.DetailTypeSpecified = true;
            //            //Line Sales Item Line Detail
            //            SalesItemLineDetail lineSalesItemLineDetail = new SalesItemLineDetail();
            //            //Line Sales Item Line Detail - ItemRef
            //            lineSalesItemLineDetail.ItemRef = new ReferenceType()
            //            {
            //                //name = item.Name,
            //                Value = "9321"
            //                //Value = item.Id
            //            };
            //            //Line Sales Item Line Detail - UnitPrice
            //            lineSalesItemLineDetail.AnyIntuitObject = 33m;
            //            lineSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            //            //Line Sales Item Line Detail - Qty
            //            lineSalesItemLineDetail.Qty = 10;
            //            lineSalesItemLineDetail.QtySpecified = true;
            //            //Line Sales Item Line Detail - TaxCodeRef
            //            //For US companies, this can be 'TAX' or 'NON'
            //            lineSalesItemLineDetail.TaxCodeRef = new ReferenceType()
            //            {
            //                Value = "NON"
            //            };
            //            //Line Sales Item Line Detail - ServiceDate 
            //            //lineSalesItemLineDetail.ServiceDate = DateTime.Now.Date;
            //            //lineSalesItemLineDetail.ServiceDateSpecified = true;
            //            //Assign Sales Item Line Detail to Line Item
            //            invoiceLine.AnyIntuitObject = lineSalesItemLineDetail;
            //            //Assign Line Item to Invoice
            //            invoice.Line = new Line[] { invoiceLine };

            //            //TxnTaxDetail
            //            //TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            //            //txnTaxDetail.TxnTaxCodeRef = new ReferenceType()
            //            //{
            //            //    name = stateTaxCode.Name,
            //            //    Value = stateTaxCode.Id
            //            //};
            //            //Line taxLine = new Line();
            //            //taxLine.DetailType = LineDetailTypeEnum.TaxLineDetail;
            //            //TaxLineDetail taxLineDetail = new TaxLineDetail();
            //            ////Assigning the fist Tax Rate in this Tax Code
            //            //taxLineDetail.TaxRateRef = stateTaxCode.SalesTaxRateList.TaxRateDetail[0].TaxRateRef;
            //            //taxLine.AnyIntuitObject = taxLineDetail;
            //            //txnTaxDetail.TaxLine = new Line[] { taxLine };
            //            //invoice.TxnTaxDetail = txnTaxDetail;

            //            //Customer (Client)
            //            invoice.CustomerRef = new ReferenceType()
            //            {
            //                name = customer.DisplayName,
            //                Value = customer.Id
            //            };

            //            //Billing Address
            //            PhysicalAddress billAddr = new PhysicalAddress();
            //            billAddr.Line1 = "123 Main St.";
            //            billAddr.Line2 = "Unit 506";
            //            billAddr.City = "Brockton";
            //            billAddr.CountrySubDivisionCode = "MA";
            //            billAddr.Country = "United States";
            //            billAddr.PostalCode = "02301";
            //            billAddr.Note = "Billing Address Note";
            //            invoice.BillAddr = billAddr;

            //            //Shipping Address
            //            PhysicalAddress shipAddr = new PhysicalAddress();
            //            shipAddr.Line1 = "100 Fifth Ave.";
            //            shipAddr.City = "Waltham";
            //            shipAddr.CountrySubDivisionCode = "MA";
            //            shipAddr.Country = "United States";
            //            shipAddr.PostalCode = "02452";
            //            shipAddr.Note = "Shipping Address Note";
            //            invoice.ShipAddr = shipAddr;

            //            //SalesTermRef
            //            invoice.SalesTermRef = new ReferenceType()
            //            {
            //                name = term.Name,
            //                Value = term.Id
            //            };

            //            //DueDate
            //            invoice.DueDate = DateTime.Now.AddDays(30).Date;
            //            invoice.DueDateSpecified = true;

            //            //ARAccountRef
            //            invoice.ARAccountRef = new ReferenceType()
            //            {
            //                name = account.Name,
            //                Value = account.Id
            //            };

            //            Invoice invoiceAdded = dataService.Add<Invoice>(invoice);



            //            //ReportService reportService = new ReportService(serviceContext);

            //            ////Date should be in the format YYYY-MM-DD 
            //            ////Response format hsold be JSON as that is pnly supported rigth now for reports 
            //            //reportService.accounting_method = "Index";
            //            //reportService.start_date = "2018-01-01";
            //            //reportService.end_date = "2018-07-01";
            //            //////reportService.classid = "2800000000000634813"; 
            //            ////reportService.date_macro = "Last Month"; 
            //            //reportService.summarize_column_by = "Month";


            //            ////List<String> columndata = new List<String>();
            //            ////columndata.Add("tx_date");
            //            ////columndata.Add("dept_name");
            //            ////string coldata = String.Join(",", columndata);
            //            ////reportService.columns = coldata;

            //            //var report1 = reportService.ExecuteReport("TrialBalance");
            //        }
            //        catch(IdsException ex)
            //        {
            //            throw ex;

            //        }

            //        //List<BatchItem> bItems = new List<BatchItem>();
            //        //BatchItem b1 = new BatchItem(OperationEnum.create, new Customer());
            //        //BatchItem b2= new BatchItem(OperationEnum.create, new Customer());

            //        //bItems.Add(b1);
            //        //bItems.Add(b2);
            //        //var result = Batch<Customer>(serviceContext, bItems);







            //        QueryService<Invoice> inv = new QueryService<Invoice>(context);
            //        var respInvoice = inv.ExecuteIdsQuery("select * from Invoice where Id='8633'").FirstOrDefault();
            //        DataService objService = new DataService(context);
            //        var respVoidedInvoice=objService.Void(respInvoice);




            //        DataService commonServiceQBO = new DataService(context);
            //        //Item item = new Item();
            //        //List<Item> results = commonServiceQBO.FindAll<Item>(item, 1, 1).ToList<Item>();
            //        QueryService<Invoice> inService = new QueryService<Invoice>(context);
            //        var In = inService.ExecuteIdsQuery("SELECT count(*) FROM Invoice").Count();



            //        Batch batch = commonServiceQBO.CreateNewBatch();


            //        batch.Add("select count(*) from Account", "queryAccount");
            //        batch.Execute();

            //        if (batch.IntuitBatchItemResponses != null && batch.IntuitBatchItemResponses.Count() > 0)
            //        {
            //            IntuitBatchResponse res = batch.IntuitBatchItemResponses.FirstOrDefault();
            //            List<Account> acc = res.Entities.ToList().ConvertAll(item => item as Account);
            //        };
            //            Output("QBO call successful.");
            //        lblQBOCall.Visible = true;
            //        lblQBOCall.Text = "QBO Call successful";
            //    }
            //}
            //catch (IdsException ex)
            //{
            //    //if (ex.Message == "Unauthorized-401")
            //    //{
            //    //    Output("Invalid/Expired Access Token.");

            //    //    var tokenResp = await oauthClient.RefreshTokenAsync(dictionary["refreshToken"]);
            //    //    if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
            //    //    {
            //    //        dictionary["accessToken"] = tokenResp.AccessToken;
            //    //        dictionary["refreshToken"] = tokenResp.RefreshToken;
            //    //        await QboApiCall();
            //    //    }
            //    //    else
            //    //    {
            //    //        Output("Error while refreshing tokens: " + tokenResp.Raw);
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    Output(ex.Message);
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    Output("Invalid/Expired Access Token.");
            //}
        }
        //public IEnumerable<T> Batch<T>(ServiceContext context, List<BatchItem> batchItems) where T : IEntity
        //{
        //    DataService service = new DataService(context);
        //    var batch = service.CreateNewBatch();
        //    foreach (var entry in batchItems)
        //    {
        //        batch.Add(entry.Entity as IEntity, entry.Id, entry.Operation);
        //    }
        //    batch.Execute();
        //    return batch.IntuitBatchItemResponses.Where(r => r.ResponseType == ResponseType.Entity).Select(r => (T)r.Entity);
        //}

        //public class BatchItem
        //{
        //    public string Id { get; set; }
        //    public OperationEnum Operation { get; set; }
        //    public object Entity { get; set; }
        //    public BatchItem(OperationEnum operation, object entity)
        //    {
        //        Operation = operation;
        //        Entity = entity;
        //        Id = string.Concat(operation.ToString("g"), "_", entity.GetType().Name, "_", Guid.NewGuid().ToString());
        //    }
        //}

        #region Helper methods for logging
        /// <summary>
        /// Gets log path
        /// </summary>
        public string GetLogPath()
        {
            try
            {
                if (logPath == "")
                {
                    logPath = Environment.GetEnvironmentVariable("TEMP");
                    if (!logPath.EndsWith("\\")) logPath += "\\";
                }
            }
            catch
            {
                Output("Log error path not found.");
            }
            return logPath;
        }

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="logMsg">string to be appended</param>
        public void Output(string logMsg)
        {
            StreamWriter sw = File.AppendText(GetLogPath() + "OAuth2SampleAppLogs1.txt");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, logMsg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
        #endregion
    }

    /// <summary>
    /// Helper for calling self
    /// </summary>
    public static class ResponseHelper
    {
        public static void Redirect(this HttpResponse response, string url, string target, string windowFeatures)
        {
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures))
            {
                response.Redirect(url);
            }
            else
            {
                Page page = (Page)HttpContext.Current.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }
                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }
    }
}


