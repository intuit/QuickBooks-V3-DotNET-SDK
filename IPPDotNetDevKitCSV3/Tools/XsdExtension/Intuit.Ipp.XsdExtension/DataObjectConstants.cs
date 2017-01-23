// <copyright file="DataObjectConstants.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  Used to hold all constansts
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    /// <summary>
    /// Used to hold all constansts
    /// </summary>
    internal static class DataObjectConstants
    {
        /// <summary>
        /// Holds type of CodeMemeberProperty
        /// </summary>
        public const string CODEMEMBERPROPERTY = "CodeMemberProperty";

        /// <summary>
        /// Holds Qbo entity root namespaces
        /// </summary>
        public const string FMSENTITYNAMESPACE = "Intuit.Ipp.Data";

        /// <summary>
        /// Holds Qbo specific entity class file
        /// </summary>
        public const string FMSOUTPUTFILE = "Fms.cs";

        /// <summary>
        /// Holds path of Intuit.Ipp.Data project
        /// </summary>
        public const string DATAPROJECTPATH = "\\Intuit.Ipp.Data\\CdmEntities";

        /// <summary>
        /// holds interface for Qbo(common) entities
        /// </summary>
        public const string IENTITYINTERFACE = "IEntity";

        /// <summary>
        /// Holds location of schema files of QBO
        /// </summary>
        public const string INPUTSCHEMAFOLDER = "InputSchemaFolder";

        /// <summary>
        /// New property to handle choice element
        /// </summary>
        public const string ITEM = "Item";

        /// <summary>
        /// Private variable to handle choice element
        /// </summary>
        public const string ITEMFIELD = "itemField";

        /// <summary>
        /// Items property to handle choice element
        /// </summary>
        public const string ITEMS = "Items";

        /// <summary>
        /// Type of Item property
        /// </summary>
        public const string OBJECTCLASS = "System.Object";

        /// <summary>
        /// Folder in which Fms.cs will be created
        /// </summary>
        public const string OUTPUTCLASSFILEFOLDER = "OutputClassFileFolder";

        /// <summary>
        /// Xml attribute to be added to Item Property
        /// </summary>
        public const string XMLSERIALIZEATTRIBUTE = "System.Xml.Serialization.XmlElementAttribute";

        /// <summary>
        /// Default folder name to fetch xsd files
        /// </summary>
        public const string DEFAULTFOLDERNAME = "Schema";

        /// <summary>
        /// File extensions to be considered
        /// </summary>
        public const string FILESEXTENSIONS = "*.xsd";

        /// <summary>
        /// Default "Item" property will be renamed to AnyIntuitObject
        /// </summary>
        public const string ANYINTUITOBJECT = "AnyIntuitObject";

        /// <summary>
        /// Default "Items" property will be renamed to AnyIntuitObjects
        /// </summary>
        public const string ANYINTUITOBJECTS = "AnyIntuitObjects";

        /// <summary>
        /// Root object of all Intuit objects
        /// </summary>
        public const string INTUITOBJECT = "INTUITOBJECT";

        /// <summary>
        /// Section1 prperty name in Section class
        /// </summary>
        public const string SECTION1 = "Section1";

        /// <summary>
        /// Section class name
        /// </summary>
        public const string SECTION = "Section";

        /// <summary>
        /// Name of AccountTypeEnum class
        /// </summary>
        public const string ACCOUNTTYPEENUM="AccountTypeEnum";

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string ACCOUNTSRECEIVABLE = "AccountsReceivable";

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string OTHERCURRENTASSET="OtherCurrentAsset";

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string FIXEDASSET="FixedAsset"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string OTHERASSET="OtherAsset"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string ACCOUNTPAYABLE="AccountsPayable"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string CREDITCARD="CreditCard"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string OTHERCURRENTLIABILITY="OtherCurrentLiability"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string LONGTERMLIABILITY="LongTermLiability"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string COSTOFGOODSSOLD="CostofGoodsSold"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string OTHERINCOME="OtherIncome"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string OTHEREXPENSE="OtherExpense"; 

        /// <summary>
        /// AccountTypeEnum member name
        /// </summary>
        public const string NONPOSTING="NonPosting"; 

        /// <summary>
        /// Summary class name
        /// </summary>
        public const string SUMMARY = "Summary";

        /// <summary>
        /// Private variable of Summary property
        /// </summary>
        public const string SUMMARYFIELD = "summaryField";

        /// <summary>
        /// String[] to be replaced with SummaryRow[]
        /// </summary>
        public const string SUMMARYROW = "SummaryRow";

        /// <summary>
        /// To add OperationEnum Type
        /// </summary>
        public const string QUERY = "query";

        /// <summary>
        /// To add OperationEnum type
        /// </summary>
        public const string REPORT = "report";

        /// <summary>
        /// Enum types to be added
        /// </summary>
        public const string OPERATIONENUM = "OperationEnum";

        /// <summary>
        /// Required to add [JsonIgnore]
        /// </summary>
        public const string XMLIGNORE = "System.Xml.Serialization.XmlIgnoreAttribute";

        #region Messages

        /// <summary>
        /// Error message if xsd file directory is invalid
        /// </summary>
        public const string XSDDIRINVALID = "QBO xsd file directory is invalid";

        /// <summary>
        /// Directory from which xsd files are fetched
        /// </summary>
        public const string FILESFROM = "Getting xsd files from ";

        /// <summary>
        /// Xsd files are not found in the specified directory
        /// </summary>
        public const string XSDFILESNOTFOUND = "Xsd Files not found ";

        /// <summary>
        /// Xsd files loaded from specified directory
        /// </summary>
        public const string XSDFILESLOADEDFROM = "Xsd Files loaded from :";

        /// <summary>
        /// Output directory to save Fms.cs is not found
        /// </summary>
        public const string OUTPUTDIRNOTFOUND = "Output directory does not exists";

        /// <summary>
        /// Add documentation to mention its added by tool
        /// </summary>
        public const string ADDEDBY = "Added by XsdExtension";

        #endregion
    }
}
