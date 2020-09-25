using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Utility;

namespace Intuit.Ipp.Utility.Test
{
    /// <summary>
    ///This is a test class for IntuitErrorHandler 
    ///</summary>
    [TestClass()]
    public class IntuitErrorHandlerTest
    {
        /// <summary>
        /// The test Context Instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        /// <value>
        /// The test context.
        /// </value>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void HandleErrors_XML_MethodTest()
        {
            string xml = "<some></some>";
            XmlDocument respXML = new XmlDocument();
            respXML.LoadXml(xml);
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "API response without Error code element.")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<errcode>401</errcode>";
            respXML.LoadXml(xml);
            try
            {
                IntuitErrorHandler.HandleErrors(respXML);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "Error 401")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<errcode>0</errcode>";
            respXML.LoadXml(xml);
            IntuitErrorHandler.HandleErrors(respXML);

            xml = "<errcode>failure</errcode>";
            respXML.LoadXml(xml);
            try
            {
                IntuitErrorHandler.HandleErrors(respXML);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "Error code \"0\" not numeric!")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<error><errcode>401</errcode><errtext>there is an error</errtext></error>";
            respXML.LoadXml(xml);
            try
            {
                IntuitErrorHandler.HandleErrors(respXML);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "there is an error (Error 401)")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<error><errcode>401</errcode><errtext>there is an error</errtext><errdetail>error detail</errdetail></error>";
            respXML.LoadXml(xml);
            try
            {
                IntuitErrorHandler.HandleErrors(respXML);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "there is an error (Error 401, Detail: error detail)")
                {
                    Assert.Fail(ex.ToString());
                }
            }
        }

        [TestMethod]
        public void HandleErrors_String_MethodTest()
        {
            string xml = "<some></some>";
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "API response without Error code element.")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<errcode>401</errcode>";
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "Error 401")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<errcode>0</errcode>";
            IntuitErrorHandler.HandleErrors(xml);

            xml = "<errcode>failure</errcode>";
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "Error code \"0\" not numeric!")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<error><errcode>401</errcode><errtext>there is an error</errtext></error>";
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "there is an error (Error 401)")
                {
                    Assert.Fail(ex.ToString());
                }
            }
            xml = "<error><errcode>401</errcode><errtext>there is an error</errtext><errdetail>error detail</errdetail></error>";
            try
            {
                IntuitErrorHandler.HandleErrors(xml);
            }
            catch (IdsException ex)
            {
                if (ex != null && ex.Message != "there is an error (Error 401, Detail: error detail)")
                {
                    Assert.Fail(ex.ToString());
                }
            }
        }
    }
}
