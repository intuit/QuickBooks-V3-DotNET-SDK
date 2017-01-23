// <copyright file="CodeDOMHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>
//  File holds helper class for Code DOM operations
// </summary>
////*********************************************************

namespace Intuit.Ipp.XsdExtension
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;

    /// <summary>
    /// Helper class to generate or parse CodeDom
    /// </summary>
    internal static class CodeDomHelper
    {        
        /// <summary>
        /// Sets passed comments to CodeDOM
        /// </summary>
        /// <param name="codeTypeMember">CodeDom object to be used to set comments</param>
        /// <param name="comment">Comment to be set</param>
        public static void SetSummaryComment(CodeTypeMember codeTypeMember, string comment)
        {
            if (string.IsNullOrEmpty(comment) || codeTypeMember == null)
            {
                return;
            }

            CodeCommentStatementCollection codeStatmentColl = codeTypeMember.Comments;
            codeStatmentColl.Add(new CodeCommentStatement("<summary>", true));
            string[] lines = comment.Split(new[] { '\n' });
            foreach (string line in lines)
            {
                codeStatmentColl.Add(new CodeCommentStatement(line.Trim(), true));
            }

            codeStatmentColl.Add(new CodeCommentStatement("</summary>", true));
        }

        /// <summary>
        /// Extracts annotation section from passes XmlSchema object
        /// </summary>
        /// <param name="annotatedElement">Annotation element of XSD schema with documentation and appinfo tags</param>
        /// <returns>Extracted Documentation and AppInfo</returns>
        public static Dictionary<string, string> GetDocumentationForElement(XmlSchemaAnnotated annotatedElement)
        {
            Dictionary<string, string> annotationElements = new Dictionary<string, string>();
            StringBuilder documatationHolder = new StringBuilder(); 
            StringBuilder appInfoHolder = new StringBuilder(); 
            XmlSchemaAnnotation annotation = annotatedElement.Annotation;
            
            // Look inside the Annotation element
            if (annotation != null)
            {
                XmlSchemaObjectCollection annotationItems = annotation.Items;
                if (annotationItems.Count > 0)
                {
                    // Cannot use for..each
                    for (int elementCount = 0; elementCount < annotationItems.Count; elementCount++)
                    {                        
                        XmlSchemaDocumentation xsdDocumentation = annotationItems[elementCount] as XmlSchemaDocumentation;
                        if (xsdDocumentation != null)
                        {
                            XmlNode[] documentationMarkups = xsdDocumentation.Markup;
                            if (documentationMarkups.Length > 0)
                            {
                                documatationHolder.Append(documentationMarkups[0].InnerText);
                                documatationHolder.Append(" ");                              
                            }
                        }

                        XmlSchemaAppInfo xsdAppInfo = annotationItems[elementCount] as XmlSchemaAppInfo;
                        if (xsdAppInfo != null)
                        {
                            XmlNode[] appInfoMarkups = xsdAppInfo.Markup;
                            if (appInfoMarkups.Length > 0)
                            {
                                appInfoHolder.Append(appInfoMarkups[0].InnerText);
                                appInfoHolder.Append(" ");                               
                            }
                        }
                    }

                    if (documatationHolder.Length > 0)
                    {                        
                        annotationElements.Add("DOC", documatationHolder.ToString());
                    }

                    if (appInfoHolder.Length > 0)
                    {
                        annotationElements.Add("APPINFO", appInfoHolder.ToString());
                    }
                }
            }

            return annotationElements;
        }

        /// <summary>
        /// Method to add EditorBrowsable attributes to passed property element
        /// </summary>
        /// <param name="memberProperty">property element to be modified</param>
        public static void AddEditorBrowsableAttribute(CodeMemberProperty memberProperty)
        {
            CodeTypeReferenceExpression refEnum = new CodeTypeReferenceExpression(typeof(System.ComponentModel.EditorBrowsableState));
            CodeExpression enumExpr = new CodeFieldReferenceExpression(refEnum, "Never");
            CodeAttributeDeclaration attributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.ComponentModel.EditorBrowsableAttribute)));
            CodeAttributeArgument attrArg = new CodeAttributeArgument(enumExpr);
            attributeDeclaration.Arguments.Add(attrArg);
            memberProperty.CustomAttributes.Add(attributeDeclaration);
        }       
    }
}
