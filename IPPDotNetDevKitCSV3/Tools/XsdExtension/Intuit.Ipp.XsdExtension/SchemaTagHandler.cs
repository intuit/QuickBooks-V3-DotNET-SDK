// <copyright file="SchemaTagHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File contains class definition which parses annotation
//  enumeration and extenstion tags
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Schema;
    using Intuit.Ipp.Diagnostics;

    /// <summary>
    /// Parses annotation section of XSD and adds to the CodeDOM 
    /// </summary>
    internal class SchemaTagHandler : IXsdExtensionTask
    {
        private CodeNamespace codeDom = null;
        private List<string> intuitEntities = new List<string>();
        TraceLogger target = new TraceLogger();

        /// <summary>
        /// Executes operation which would parse annotation section of XSD and adds to the CodeDOM 
        /// </summary>
        /// <param name="xsdContext">Holds data about XSD schema</param>
        /// <param name="codeDomContext">Code dom tree</param>
        public void Execute(XsdContext xsdContext, CodeDomContext codeDomContext)
        {
            codeDom = codeDomContext.CodeNamespace;
            XmlSchema customerSchema = null;

            // Fetch each schema and update annotation
            foreach (XmlSchema xmlSchema in xsdContext.XmlSchemaList)
            {
                customerSchema = xmlSchema;

                XmlSchemaObjectCollection schemObjects = customerSchema.Items;

                foreach (XmlSchemaObject schemaObject in schemObjects)
                {
                    HandleSchemaObject(schemaObject);
                }
            }

        }

        /// <summary>
        /// Updates annotation and choice element if presents in xsd schema
        /// </summary>
        /// <param name="schemaObject">Single schema object to update annotation</param>
        private void HandleSchemaObject(XmlSchemaObject schemaObject)
        {

            XmlSchemaElement element = schemaObject as XmlSchemaElement;
            if (element != null)
            {
                //target.Log(TraceLevel.Verbose, "Updates documentation for Simple Type : " + simpleType.Name);
                //UpdateAnnotation(simpleType);
                GetIntuitEntities(element);
            }

            // Schemaobject type is Simple Type
            XmlSchemaSimpleType simpleType = schemaObject as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                target.Log(TraceLevel.Verbose, "Updates documentation for Simple Type : " + simpleType.Name);
                UpdateAnnotation(simpleType);
            }

            // Schemaobject type is Complex Type
            XmlSchemaComplexType complexType = schemaObject as XmlSchemaComplexType;
            if (complexType != null)
            {

                // Handle properties within schema object (complex type)
                XmlSchemaObjectCollection properties = complexType.Attributes;
                foreach (XmlSchemaObject property in properties)
                {
                    target.Log(TraceLevel.Verbose, "Updates documentation for Property Type under " + complexType.Name + " Complex type");
                    UpdateAnnotation(property, complexType.Name);
                }

                UpdateAnnotation(complexType);
                target.Log(TraceLevel.Verbose, complexType.Name);
            }


            XmlSchemaSequence sequence = null;
            if (complexType != null) { sequence = complexType.ContentTypeParticle as XmlSchemaSequence; } else { sequence = schemaObject as XmlSchemaSequence; }
            if (sequence != null)
            {
                string complexTypeName = "";
                if (complexType != null) { complexTypeName = complexType.Name; } else { complexTypeName = "Generic Nested"; }
                // Handle Sequence within schema object (complex type)
                if (sequence != null)
                {
                    target.Log(TraceLevel.Verbose, "Sequence elements found under :" + complexTypeName + " Complex type and counts are : " + sequence.Items.Count.ToString());
                    foreach (XmlSchemaObject childElementInSequence in sequence.Items)
                    {
                        XmlSchemaElement elementInClass = childElementInSequence as XmlSchemaElement;
                        if (elementInClass != null)
                        {
                            target.Log(TraceLevel.Verbose, "Updates documentation for Sequence Type under " + complexTypeName + " Complex type");
                            UpdateAnnotation(childElementInSequence, complexTypeName);
                        }

                        // Handle choice element within sequence element (complex type)
                        XmlSchemaChoice choiceElement = childElementInSequence as XmlSchemaChoice;
                        if (choiceElement != null)
                        {
                            target.Log(TraceLevel.Verbose, "Choice elements found under :" + complexTypeName + " Complex type and under Sequence");
                            XmlSchemaObjectCollection choiceObjects = choiceElement.Items;
                            foreach (XmlSchemaObject choiceObject in choiceObjects)
                            {
                                XmlSchemaSequence seqInsideChoice = choiceObject as XmlSchemaSequence;

                                // Handle sequence element within choice element (complex type)
                                if (seqInsideChoice != null)
                                    HandleSchemaObject(seqInsideChoice);
                                else if (choiceElement.Parent.Parent == complexType && choiceElement.MaxOccurs == 1)
                                {
                                    foreach (XmlSchemaObject choiceItem in choiceElement.Items)
                                    {
                                        XmlSchemaElement choiceChild = choiceItem as XmlSchemaElement;
                                        if (choiceChild != null)
                                        {
                                            UpdateAnnotation(choiceChild, complexTypeName);
                                            if (choiceChild.MaxOccurs == 1)
                                            {
                                                target.Log(TraceLevel.Verbose, "Called SeperateItem method for  :" + choiceChild.Name + " Choice element under :" + complexTypeName + " Complex type and under Sequence");
                                                SeperateItem(choiceChild, complexTypeName);
                                                HandleSchemaObject(choiceChild);
                                            }
                                        }
                                        else
                                        {
                                            XmlSchemaSequence seqInsideChoiceItem = choiceItem as XmlSchemaSequence;
                                            if (seqInsideChoiceItem != null) { HandleSchemaObject(seqInsideChoiceItem); } else { throw new System.Exception("Unknown XML schema choice type"); }
                                        }
                                    }
                                } // closing sequence element within choice element
                            }

                        } // closing choice element within sequence element

                    }
                }
            }

            // Handle Attribute element within complex type
            XmlSchemaAttribute attribute = schemaObject as XmlSchemaAttribute;
            if (attribute != null)
            {
                UpdateAnnotation(attribute);
            }
        }

        /// <summary>
        /// Updates annotation in the Code DOM objects
        /// </summary>
        /// <param name="schemaObject">Schema object to update annotation</param>
        /// <param name="className">it will be present if schema object is property type</param>
        private void UpdateAnnotation(XmlSchemaObject schemaObject, string className = null)
        {
            // Documentation for Simple type
            XmlSchemaSimpleType simpleType = schemaObject as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                DocumentSchemaObjectType(schemaObject, simpleType.Name);
            }

            // Documentation for Complex type
            XmlSchemaComplexType classProperty = schemaObject as XmlSchemaComplexType;
            if (classProperty != null)
            {
                if (className == null)
                {
                    // Fetch class in code dom
                    DocumentSchemaObjectType(schemaObject, classProperty.Name);
                }
                else
                {
                    DocumentClassProperties(schemaObject, className, classProperty.Name);
                }
            }

            // Documentation for Attribute
            XmlSchemaAttribute attribute = schemaObject as XmlSchemaAttribute;
            if (attribute != null)
            {
                DocumentClassProperties(schemaObject, className, attribute.Name);
            }

            // Documentation for Sequence type
            XmlSchemaElement elementInSequence = schemaObject as XmlSchemaElement;
            {
                if (elementInSequence != null)
                {
                    DocumentClassProperties(schemaObject, className, elementInSequence.Name);
                }
            }

        }

        /// <summary>
        /// Updates documenatation to Property element
        /// </summary>
        /// <param name="schemaObject">Property element to be updated with annotation</param>
        /// <param name="className">Complex type name in which Property elements belongs to</param>
        /// <param name="propertyName">Name of the Property element to be updated</param>
        private void DocumentClassProperties(XmlSchemaObject schemaObject, string className, string propertyName)
        {
            // Get all code type declaration from CodeDOM
            foreach (CodeTypeDeclaration codeDomType in codeDom.Types)
            {
                // Check it is class type and name is same as parent schema object name
                if (codeDomType.IsClass && string.Equals(codeDomType.Name, className))
                {
                    // Loop thru all code type members inside codedom type
                    foreach (CodeTypeMember codeTypeMember in codeDomType.Members)
                    {
                        if (string.Equals(codeTypeMember.GetType().Name, DataObjectConstants.CODEMEMBERPROPERTY))
                        {
                            CodeMemberProperty codeMemField = (CodeMemberProperty)codeTypeMember;
                            if (string.Equals(codeMemField.Name, propertyName))
                            {
                                XmlSchemaAnnotated annotatedElement = schemaObject as XmlSchemaAnnotated;
                                Dictionary<string, string> annotation = CodeDomHelper.GetDocumentationForElement(annotatedElement);

                                // Handle "APPINFO" tag
                                if (annotation.Count > 0)
                                {
                                    if (annotation.ContainsKey("APPINFO") && !string.IsNullOrEmpty(annotation["APPINFO"]))
                                    {
                                        IAppInfoHandler appInfoHandler = new AppInfoIgnoreHandler();
                                        appInfoHandler.HandleAppInfo((CodeMemberProperty)codeTypeMember, annotation["APPINFO"].ToString());
                                    }

                                    if (annotation.ContainsKey("DOC"))
                                    {
                                        CodeDomHelper.SetSummaryComment(codeTypeMember, annotation["DOC"]);
                                    }
                                } // closing of Handle "APPINFO" tag
                            }
                        }
                    } // closing of Loop thru all code type members inside codedom type
                }
            }
        }

        /// <summary>
        /// Sets documentation to comlext or simple type elements
        /// </summary>
        /// <param name="schemaObject">Schema object to set documentation</param>
        /// <param name="elementName">Schema elment name to set documentation</param>
        private void DocumentSchemaObjectType(XmlSchemaObject schemaObject, string elementName)
        {
            foreach (CodeTypeDeclaration codeTypeDec in codeDom.Types)
            {
                if (string.Equals(codeTypeDec.Name, elementName))
                {
                    Dictionary<string, string> documentation = CodeDomHelper.GetDocumentationForElement(schemaObject as XmlSchemaType);
                    if (documentation.ContainsKey("DOC"))
                        CodeDomHelper.SetSummaryComment(codeTypeDec, documentation["DOC"]);
                }
            }
        }

        /// <summary>
        /// If choice elements has single and array of objects, this method creates "Item" property for single objects
        /// </summary>
        /// <param name="choiceChildElement">Attribute elements to be added in the "Item" property</param>
        /// <param name="className">Class name in which "Item" element is present</param>
        private void SeperateItem(XmlSchemaElement choiceChildElement, string className)
        {

            // Get all code type declaration from CodeDOM
            foreach (CodeTypeDeclaration codeDomType in codeDom.Types)
            {
                // Check it is class type and name is same as parent schema object name
                if (codeDomType.IsClass && string.Equals(codeDomType.Name, className))
                {
                    if (choiceChildElement.QualifiedName.Name == "IntuitObject" && string.IsNullOrEmpty(choiceChildElement.Name))
                    {
                        foreach (string intuitEntityName in this.intuitEntities)
                        {
                            // Create Item property if not available and add
                            this.HandleChoiceElement(codeDomType, intuitEntityName);
                            // Remove this class type from Items property
                            this.RemoveXmlAttributeFromItems(codeDomType, intuitEntityName);
                        }

                    }
                    else
                    {
                        // Create Item property if not available and add
                        this.HandleChoiceElement(codeDomType, choiceChildElement.Name);
                        // Remove this class type from Items property
                        this.RemoveXmlAttributeFromItems(codeDomType, choiceChildElement.Name);
                    }
                }
            }

        }

        /// <summary>
        /// Adds if it is type of IntuitObject
        /// </summary>
        /// <param name="element"></param>
        private void GetIntuitEntities(XmlSchemaElement element)
        {
            if (string.Equals(element.SubstitutionGroup.Name.ToUpper(CultureInfo.InvariantCulture), DataObjectConstants.INTUITOBJECT)
                && !this.intuitEntities.Contains(element.Name))
            {
                this.intuitEntities.Add(element.Name);
            }
        }

        /// <summary>
        /// Adds "Item" property if not exists already
        /// Adds attributes to "Item" property
        /// </summary>
        /// <param name="classToAddItem">Class in which "Item" property to be added</param>
        /// <param name="XmlAttributeName">Xml attribute to be added to "Item" property</param>
        private void HandleChoiceElement(CodeTypeDeclaration classToAddItem, string XmlAttributeName)
        {
            bool itemPropertyFound = false;
            bool attrFound = false;

            foreach (CodeTypeMember codeTypeMember in classToAddItem.Members)
            {
                CodeMemberProperty codeMemField = codeTypeMember as CodeMemberProperty;
                if (codeMemField != null)
                {
                    if ((codeMemField != null) && (string.Equals(codeMemField.Name.ToUpper(CultureInfo.InvariantCulture),
                        DataObjectConstants.ITEM.ToUpper(CultureInfo.InvariantCulture))))
                    {
                        // check whether attributes already added or not
                        CodeAttributeDeclarationCollection attributes = codeMemField.CustomAttributes;

                        foreach (CodeAttributeDeclaration attribute in attributes)
                        {
                            CodeAttributeArgumentCollection arguments = attribute.Arguments;

                            // find arguments for each attributes
                            foreach (CodeAttributeArgument argument in arguments)
                            {
                                CodePrimitiveExpression primitiveExp = argument.Value as CodePrimitiveExpression;
                                if (primitiveExp != null && (primitiveExp.Value != null) &&
                                    string.Equals(primitiveExp.Value.ToString(), XmlAttributeName)
                                    && !string.IsNullOrEmpty(XmlAttributeName))
                                {
                                    attrFound = true;
                                    break;
                                }
                            }
                        }

                        if (!attrFound && !string.IsNullOrEmpty(XmlAttributeName))
                        {
                            // Add attributes to Item property
                            CodeAttributeDeclaration codeAttrDec = new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Xml.Serialization.XmlElementAttribute)));
                            CodeAttributeArgument nameAttr = new CodeAttributeArgument(new CodePrimitiveExpression(XmlAttributeName));
                            CodeAttributeArgument typeOfAttr = new CodeAttributeArgument(new CodeTypeOfExpression(XmlAttributeName));
                            codeAttrDec.Arguments.Add(nameAttr);
                            codeAttrDec.Arguments.Add(typeOfAttr);
                            codeMemField.CustomAttributes.Add(codeAttrDec);

                        }
                        itemPropertyFound = true;
                        break;
                    }

                }

            }

            // Add Item proerpty if not exists already
            if (!itemPropertyFound && !string.IsNullOrEmpty(XmlAttributeName))
            {
                this.AddItemProperty(classToAddItem, XmlAttributeName);
            }

        }

        /// <summary>
        /// Adds "Item" property to class
        /// </summary>
        /// <param name="classToAddItem">Class in which "Item" property to be added</param>
        /// <param name="XmlAttributeName">Xml attribute to be added to "Item" property</param>
        private void AddItemProperty(CodeTypeDeclaration classToAddItem, string XmlAttributeName)
        {
            // Create private valiable "item" 
            CodeMemberField itemPvtVar = new CodeMemberField(DataObjectConstants.OBJECTCLASS, DataObjectConstants.ITEMFIELD);
            classToAddItem.Members.Add(itemPvtVar);

            // Create Public property "Item"
            CodeMemberProperty itemProperty = new CodeMemberProperty();
            itemProperty.Name = DataObjectConstants.ITEM;
            itemProperty.Type = new CodeTypeReference(DataObjectConstants.OBJECTCLASS);
            itemProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            itemProperty.HasGet = true;
            itemProperty.HasSet = true;
            itemProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), DataObjectConstants.ITEMFIELD)));
            itemProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), DataObjectConstants.ITEMFIELD),
                new CodePropertySetValueReferenceExpression()));


            CodeAttributeDeclaration codeAttrDec = new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Xml.Serialization.XmlElementAttribute)));
            CodeAttributeArgument nameAttr = new CodeAttributeArgument(new CodePrimitiveExpression(XmlAttributeName));
            CodeAttributeArgument typeOfAttr = new CodeAttributeArgument(new CodeTypeOfExpression(XmlAttributeName));

            codeAttrDec.Arguments.Add(nameAttr);
            codeAttrDec.Arguments.Add(typeOfAttr);
            itemProperty.CustomAttributes.Add(codeAttrDec);
            classToAddItem.Members.Add(itemProperty);
        }

        /// <summary>
        /// Removes attributes from Items property
        /// </summary>
        /// <param name="classToAddItem">Class in which "Items" property is present</param>
        /// <param name="XmlAttributeName">Xml attribute to be revemod from class</param>
        private void RemoveXmlAttributeFromItems(CodeTypeDeclaration classToAddItem, string XmlAttributeName)
        {
            int attrCount = 0;
            bool attrFound = false;

            foreach (CodeTypeMember codeTypeMember in classToAddItem.Members)
            {
                CodeMemberProperty codeMemField = codeTypeMember as CodeMemberProperty;
                if (codeMemField != null)
                {
                    // Check whether class has "Items" property
                    if ((codeMemField != null) &&
                        (string.Equals(codeMemField.Name.ToUpper(), DataObjectConstants.ITEMS.ToUpper())))
                    {
                        CodeAttributeDeclarationCollection attributes = codeMemField.CustomAttributes;

                        // Check Attribute to be removed is present
                        foreach (CodeAttributeDeclaration attribute in attributes)
                        {
                            CodeAttributeArgumentCollection arguments = attribute.Arguments;

                            foreach (CodeAttributeArgument argument in arguments)
                            {
                                CodePrimitiveExpression primitiveExp = argument.Value as CodePrimitiveExpression;
                                if (primitiveExp != null && string.Equals(primitiveExp.Value.ToString(), XmlAttributeName))
                                {
                                    attrFound = true;
                                    break;
                                }
                            }

                            if (attrFound)
                                break;
                            attrCount++;

                        } // closing Check Attribute to be removed is present

                        if (attrFound)
                        {
                            attributes.RemoveAt(attrCount);
                            attrFound = false;
                        }
                    } // closing Check whether class has "Items" property
                }
            }

        }
    }


}
