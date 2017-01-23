// <copyright file="HierarchyManager.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File that contains class which manages all hierchy implementation
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Manager class for hierchy implementation
    /// </summary>
    internal class HierarchyManager : IXsdExtensionTask
    {
        /// <summary>
        /// Adds required interface implementatio
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext">Holds codeDomTree</param>
        public void Execute(XsdContext xsdContext, CodeDomContext codeDomContext)
        {
            foreach (CodeTypeDeclaration codedomElement in codeDomContext.CodeNamespace.Types)
            {
                if (codedomElement.IsClass)
                {
                    // Implement IEntity
                    this.ImplementIEntity(codedomElement, xsdContext);

                    // Replace DataType="date" with DataType="dateTime" in System.Xml.Serialization.XmlElementAttribute
                    this.ReplaceDateTime(codedomElement);

                    // Replace Item and Items property names to AnyIntuitObject and AnyIntuitObjects
                    this.ReplaceItem(codedomElement);

                    // It adds [JsonPropertyAttribute] to Section1 property of Section class
                    this.ModifySectionClass(codedomElement);

                    // adds [JsonIgnore] attribute if [XmlIgnore] presents
                    this.AddJsonIgnore(codedomElement);
                }
                else if (codedomElement.IsEnum)
                {
                    // Adds Query and Report enums
                    this.AddOperationEnums(codedomElement);

                    //This adds [JsonPropertyAttribute] attribute to the members of AccountTypeEnum 
                    this.ModifyAccountTypeEnumeration(codedomElement);
                }
            }
        }

        /// <summary>
        /// Adds interface implementation to class
        /// </summary>
        /// <param name="codedomClass">class in which interface to be implemented</param>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        private void ImplementIEntity(CodeTypeDeclaration codedomClass, XsdContext xsdContext)
        {
            CodeTypeReference ientityRef = null;

            ientityRef = new CodeTypeReference(DataObjectConstants.FMSENTITYNAMESPACE + ".IntuitEntity");

            // Add IEntity interface implementation if class is inherited from CdmBase
            for (int baseTypeCount = 0; baseTypeCount < codedomClass.BaseTypes.Count; baseTypeCount++)
            {
                if (string.Equals(codedomClass.BaseTypes[baseTypeCount].BaseType, ientityRef.BaseType) || string.Equals(codedomClass.BaseTypes[baseTypeCount].BaseType, "IntuitEntity"))
                {
                    codedomClass.BaseTypes.Add(new CodeTypeReference(DataObjectConstants.IENTITYINTERFACE));
                }
            }

            // Implement IEntity for specific classes
            if (string.Equals(codedomClass.Name, "NameValue") || string.Equals(codedomClass.Name, "Header") || string.Equals(codedomClass.Name, "Rows") || string.Equals(codedomClass.Name, "Row") || string.Equals(codedomClass.Name, "ColData"))
            {
                codedomClass.BaseTypes.Add(new CodeTypeReference(DataObjectConstants.IENTITYINTERFACE));
            }
        }

        /// <summary>
        /// Replaces DataType="date" with DataType="dateTime" in System.Xml.Serialization.XmlElementAttribute
        /// </summary>
        /// <param name="codedomElement">Codedom element to be modified</param>
        private void ReplaceDateTime(CodeTypeDeclaration codedomElement)
        {
            CodeTypeMemberCollection typeMembers = codedomElement.Members;

            // Loop thru each member/property of the class
            for (int memberCount = 0; memberCount < typeMembers.Count; memberCount++)
            {
                CodeMemberProperty typeProperty = typeMembers[memberCount] as CodeMemberProperty;

                if (typeProperty != null)
                {
                    // Loop for each xml attribute in the propery
                    foreach (CodeAttributeDeclaration xmlAttribute in typeProperty.CustomAttributes)
                    {
                        if (string.Equals(xmlAttribute.Name, DataObjectConstants.XMLSERIALIZEATTRIBUTE))
                        {
                            CodeAttributeArgumentCollection argColl = xmlAttribute.Arguments;

                            // loop for each arguments in the attribute
                            foreach (CodeAttributeArgument avaiArg in argColl)
                            {
                                CodePrimitiveExpression dateExpression = avaiArg.Value as CodePrimitiveExpression;

                                if (dateExpression != null && dateExpression.Value != null)
                                {
                                    if (string.Equals(dateExpression.Value.ToString().ToUpper(), "DATE"))
                                    {
                                        avaiArg.Value = new CodePrimitiveExpression("dateTime");
                                    }
                                }
                            } // loop for each arguments
                        }
                    } // Loop for each xml attribute
                }
            }
        }

        /// <summary>
        /// Replaces Term to SalesTerm as per service url
        /// </summary>
        /// <param name="enumElement">Element to be preplaced</param>
        private void ReplaceSalesTerm(CodeTypeDeclaration enumElement)
        {
            CodeTypeMemberCollection enumMembers = enumElement.Members;

            // Loop thru each member/property of the class
            for (int memberCount = 0; memberCount < enumMembers.Count; memberCount++)
            {
                if (string.Equals("TERM", enumMembers[memberCount].Name.ToUpper()))
                {
                    enumMembers[memberCount].Name = "SalesTerm";
                }
            }
        }

        /// <summary>
        /// Replaces Item and Items property names to AnyIntuitObject and AnyIntuitObjects
        /// </summary>
        /// <param name="codedomElement">Class that has Item or Items property</param>
        private void ReplaceItem(CodeTypeDeclaration codedomElement)
        {
            CodeTypeMemberCollection typeMembers = codedomElement.Members;

            // Loop thru each member/property of the class
            for (int memberCount = 0; memberCount < typeMembers.Count; memberCount++)
            {
                CodeMemberProperty typeProperty = typeMembers[memberCount] as CodeMemberProperty;
                if (typeProperty != null)
                {
                    if (string.Equals(
                        typeProperty.Name.ToUpper(CultureInfo.InvariantCulture),
                        DataObjectConstants.ITEM.ToUpper(CultureInfo.InvariantCulture)))
                    {
                        typeProperty.Name = DataObjectConstants.ANYINTUITOBJECT;
                    }
                    else if (string.Equals(
                        typeProperty.Name.ToUpper(CultureInfo.InvariantCulture),
                        DataObjectConstants.ITEMS.ToUpper(CultureInfo.InvariantCulture)))
                    {
                        typeProperty.Name = DataObjectConstants.ANYINTUITOBJECTS;
                    }
                }
            }
        }

        /// <summary>
        /// Section class has Section property with data type of Section[]. Since name of the class
        /// and property are same, .Net APIs creates property name as Section1. This would cause issue while
        /// using JsonSerializer.
        /// </summary>
        /// <param name="codedomElement">Section class</param>
        private void ModifySectionClass(CodeTypeDeclaration codedomElement)
        {
            if (string.Equals(
                codedomElement.Name.ToUpper(CultureInfo.InvariantCulture),
                DataObjectConstants.SECTION.ToUpper(CultureInfo.InvariantCulture)))
            {
                CodeTypeMemberCollection typeMembers = codedomElement.Members;

                // Loop thru each member/property of the class
                for (int memberCount = 0; memberCount < typeMembers.Count; memberCount++)
                {
                    CodeMemberProperty typeProperty = typeMembers[memberCount] as CodeMemberProperty;
                    if (typeProperty != null)
                    {
                        if (string.Equals(
                            typeProperty.Name.ToUpper(CultureInfo.InvariantCulture),
                            DataObjectConstants.SECTION1.ToUpper(CultureInfo.InvariantCulture)))
                        {
                            CodeAttributeDeclaration codeAttrDec = new CodeAttributeDeclaration(new CodeTypeReference("JsonPropertyAttribute"));
                            CodeAttributeArgument nameAttr = new CodeAttributeArgument(new CodePrimitiveExpression("Section"));
                            codeAttrDec.Arguments.Add(nameAttr);
                            typeProperty.CustomAttributes.Add(codeAttrDec);
                        }
                        else if (string.Equals(
                            typeProperty.Name.ToUpper(CultureInfo.InvariantCulture),
                            DataObjectConstants.SUMMARY.ToUpper(CultureInfo.InvariantCulture)))
                        {
                            typeProperty.Type = new CodeTypeReference(DataObjectConstants.SUMMARYROW, 1);
                        }
                    }

                    CodeMemberField pvtVariable = typeMembers[memberCount] as CodeMemberField;
                    {
                        if (pvtVariable != null)
                        {
                            if (string.Equals(
                                pvtVariable.Name.ToUpper(CultureInfo.InvariantCulture),
                                DataObjectConstants.SUMMARYFIELD.ToUpper(CultureInfo.InvariantCulture)))
                            {
                                pvtVariable.Type = new CodeTypeReference(DataObjectConstants.SUMMARYROW, 1);
                            }
                        }
                    }
                }
            } // for class check
        }



        /// <summary>
        /// This adds [JsonPropertyAttribute] attribute to the members of AccountTypeEnum. 
        /// </summary>
        /// <param name="codedomElement">Section class</param>
        private void ModifyAccountTypeEnumeration(CodeTypeDeclaration codedomElement)
        {
            if (string.Equals(
                codedomElement.Name.ToUpper(CultureInfo.InvariantCulture),
                DataObjectConstants.ACCOUNTTYPEENUM.ToUpper(CultureInfo.InvariantCulture)))
            {
                CodeTypeMemberCollection typeMembers = codedomElement.Members;

                // Loop thru each member/property of the class
                for (int memberCount = 0; memberCount < typeMembers.Count; memberCount++)
                {
                    CodeMemberField typeProperty = typeMembers[memberCount] as CodeMemberField;
                    if (typeProperty != null)
                    {
                        switch (typeProperty.Name)
                        {

                            case DataObjectConstants.ACCOUNTSRECEIVABLE:
                                AddJsonAttribute(typeProperty,"Accounts Receivable");
                                break;
                            case DataObjectConstants.OTHERCURRENTASSET:
                                AddJsonAttribute(typeProperty,"Other Current Asset");
                                break;
                            case DataObjectConstants.FIXEDASSET:
                                AddJsonAttribute(typeProperty,"Fixed Asset");
                                break;
                            case DataObjectConstants.OTHERASSET:
                                AddJsonAttribute(typeProperty,"Other Asset");
                                break;
                            case DataObjectConstants.ACCOUNTPAYABLE:
                                AddJsonAttribute(typeProperty,"Accounts Payable");
                                break;
                            case DataObjectConstants.CREDITCARD:
                                AddJsonAttribute(typeProperty,"Credit Card");
                                break;
                            case DataObjectConstants.OTHERCURRENTLIABILITY:
                                 AddJsonAttribute(typeProperty,"Other Current Liability");
                                break;
                            case DataObjectConstants.LONGTERMLIABILITY:
                                 AddJsonAttribute(typeProperty,"Long Term Liability");
                                break;
                            case DataObjectConstants.COSTOFGOODSSOLD:
                                 AddJsonAttribute(typeProperty,"Cost of Goods Sold");
                                break;
                            case DataObjectConstants.OTHERINCOME:
                                 AddJsonAttribute(typeProperty,"Other Income");
                                break;
                            case DataObjectConstants.OTHEREXPENSE:
                                 AddJsonAttribute(typeProperty,"Other Expense");
                                break;
                            case DataObjectConstants.NONPOSTING:
                                 AddJsonAttribute(typeProperty,"Non-Posting");
                                break;
                        }
                        
                    }
                }
            } // for class check
        }

        /// <summary>
        /// This adds [JsonPropertyAttribute] attribute to the given field. 
        /// </summary>
        private void AddJsonAttribute(CodeMemberField typeProperty,string attributeName)
        {
            CodeAttributeDeclaration codeAttrDec = new CodeAttributeDeclaration(new CodeTypeReference("JsonPropertyAttribute"));
            CodeAttributeArgument nameAttr = new CodeAttributeArgument(new CodePrimitiveExpression(attributeName));
            codeAttrDec.Arguments.Add(nameAttr);
            typeProperty.CustomAttributes.Add(codeAttrDec);
        
        } 

        /// <summary>
        /// Adds Query and Report enum types to OperationEnum
        /// </summary>
        /// <param name="codedomElement">OperationEnum enum type</param>
        private void AddOperationEnums(CodeTypeDeclaration codedomElement)
        {
            if (string.Equals(codedomElement.Name.ToUpper(CultureInfo.InvariantCulture),
                DataObjectConstants.OPERATIONENUM.ToUpper(CultureInfo.InvariantCulture)))
            {
                // Add Query
                CodeTypeReference queryRef = new CodeTypeReference(typeof(string));
                CodeMemberField queryEnum = new CodeMemberField("string", DataObjectConstants.QUERY);
                CodeDomHelper.SetSummaryComment(queryEnum, DataObjectConstants.ADDEDBY);
                codedomElement.Members.Add(queryEnum);

                // Add Report
                CodeTypeReference reportRef = new CodeTypeReference(typeof(string));
                CodeMemberField reportEnum = new CodeMemberField("string", DataObjectConstants.REPORT);
                CodeDomHelper.SetSummaryComment(reportEnum, DataObjectConstants.ADDEDBY);
                codedomElement.Members.Add(reportEnum);
            }
        }

        /// <summary>
        /// Adds [JsonIgnore] attribute if [XmlIgnore] found
        /// </summary>
        /// <param name="codedomElement">Class in which JsonIgnore to be added</param>
        private void AddJsonIgnore(CodeTypeDeclaration codedomElement)
        {
            bool xmlIgnoreFound = false;
            
            CodeTypeMemberCollection typeMembers = codedomElement.Members;

            // Loop thru each member/property of the class
            for (int memberCount = 0; memberCount < typeMembers.Count; memberCount++)
            {
                CodeMemberProperty typeProperty = typeMembers[memberCount] as CodeMemberProperty;
                if (typeProperty != null)
                {
                    foreach (CodeAttributeDeclaration xmlAttribute in typeProperty.CustomAttributes)
                    {
                        if (string.Equals(
                            xmlAttribute.AttributeType.BaseType.ToUpper(CultureInfo.InvariantCulture),
                            DataObjectConstants.XMLIGNORE.ToUpper(CultureInfo.InvariantCulture)))
                        {
                            xmlIgnoreFound = true;
                        }
                    } // Loop for each xml attribute

                    if (xmlIgnoreFound)
                    {
                        CodeAttributeDeclaration codeAttrDec = new CodeAttributeDeclaration(new CodeTypeReference("JsonIgnore"));
                        typeProperty.CustomAttributes.Add(codeAttrDec);
                        xmlIgnoreFound = false;
                    }
                }
            }
        }
    }
}
