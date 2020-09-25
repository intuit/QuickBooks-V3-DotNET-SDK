////********************************************************************
// <copyright file="XmlSerializerTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains test cases for XmlObjectSerializer methods. </summary>
////********************************************************************

namespace Intuit.Ipp.Utility.Test
{
    using Data;
    using Diagnostics;
    using Exception;
    using Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for XmlObjectSerializerTest and is intended
    /// to contain all XmlObjectSerializerTest Unit Tests
    /// </summary>
    [TestClass()]
    public class XmlSerializerTest
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

        /// <summary>
        /// Gets or sets the IDS logger.
        /// </summary>
        /// <value>
        /// The IDS logger.
        /// </value>
        internal ILogger IDSLogger { get; set; }

        /// <summary>
        /// Serialize constructor test.
        /// </summary>
        [TestMethod]
        public void SerializerConstructorTest()
        {
            XmlObjectSerializer serializer = new XmlObjectSerializer();
        }

        /// <summary>
        /// Serialize constructor test.
        /// </summary>
        [TestMethod]
        public void SerializerConstructorWithArgsTest()
        {
            XmlObjectSerializer serializer = new XmlObjectSerializer(IDSLogger);
        }
        

        /// <summary>
        /// Entity generic de serialize test for exception.
        /// </summary>
        [TestMethod()]
        public void EntityGenericDeSerializerTestForException()
        {
            Account serializableEntity = SerializerTestHelper.CreateAccountEntity();
            XmlObjectSerializer serializer = new XmlObjectSerializer();
            string serializedXml = serializer.Serialize(serializableEntity);
            try
            {
                Bill deserializedEntity = (Bill)serializer.Deserialize<Bill>(serializedXml);
                Assert.Fail();
            }
            catch (SerializationException)
            {
                
            }
        }

        /// <summary>
        /// Account entity serialize test.
        /// </summary>
        [TestMethod()]
        public void AccountEntitySerializerTest()
        {
            Account serializableEntity = SerializerTestHelper.CreateAccountEntity();

            XmlObjectSerializer serializer = new XmlObjectSerializer();
            string serializedXml = serializer.Serialize(serializableEntity);
            Account deserializedEntity = (Account)serializer.Deserialize<Account>(serializedXml);
            SerializerTestHelper.CompareAcconutObjects(deserializedEntity, serializableEntity);

        }

        /// <summary>
        /// Exception block test for serialize method
        /// </summary>
        [TestMethod()]
        public void AccountEntitySerializerExceptionBlockTest()
        {
            Account serializableEntity = SerializerTestHelper.CreateAccountEntity();
            serializableEntity.CustomField[3].AnyIntuitObject = serializableEntity; // this is invalid;
            XmlObjectSerializer serializer = new XmlObjectSerializer();
            try
            {
                string serializedXml = serializer.Serialize(serializableEntity);
                Assert.Fail();
            }
            catch (SerializationException)
            {

            }
        }

    }
}

