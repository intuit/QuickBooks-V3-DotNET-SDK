using System;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;

namespace IntuitSampleWebsite.utils
{
    /// <summary>
    /// This class stores and fetches Oauth Access token for an user from XML file. In real world it could be database or any other suitable storage.
    /// </summary>
    public static class OauthAccessTokenStorageHelper
    {
        /// <summary>
        /// Remove oauth access toekn from storage 
        /// </summary>
        internal static void RemoveInvalidOauthAccessToken(string emailID,Page page)
        {
            string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            string searchUserXpath = "//record[@usermailid='" + emailID + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode record = doc.SelectSingleNode(searchUserXpath);
            if (record != null)
            {
                doc.DocumentElement.RemoveChild(record);
                doc.Save(path);
            }

            //Rermove it from session
            page.Session.Remove("realm");
            page.Session.Remove("dataSource");
            page.Session.Remove("accessToken");
            page.Session.Remove("accessTokenSecret");
            page.Session.Remove("Flag");
        } 

        /// <summary>
        /// get the oauth access token for the user from OauthAccessTokenStorage.xml
        /// </summary>
        internal static void GetOauthAccessTokenForUser(string emailID, Page page)
        {
            string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            string searchUserXpath="//record[@usermailid='"+emailID+"']";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode record=doc.SelectSingleNode(searchUserXpath); 
            if(record !=null)
            {
             page.Session["realm"] = record.Attributes["realmid"].Value;
             page.Session["dataSource"] = record.Attributes["dataSource"].Value;
             string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
             page.Session["accessToken"] = CryptographyHelper.DecryptData(record.Attributes["encryptedaccesskey"].Value,secuirtyKey);
             page.Session["accessTokenSecret"] = CryptographyHelper.DecryptData(record.Attributes["encryptedaccesskeysecret"].Value, secuirtyKey);

             // Add flag to session which tells that accessToken is in session
             page.Session["Flag"] = true;
            }
            
        }

        /// <summary>
        /// persist the Oauth access token in OauthAccessTokenStorage.xml file
        /// </summary>
        internal static void StoreOauthAccessToken(Page page)
        {
            string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode node=doc.CreateElement("record");
            XmlAttribute userMailIdAttribute = doc.CreateAttribute("usermailid");
            userMailIdAttribute.Value = page.Session["FriendlyEmail"].ToString();
            node.Attributes.Append(userMailIdAttribute);

            XmlAttribute accessKeyAttribute = doc.CreateAttribute("encryptedaccesskey");
            string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
            accessKeyAttribute.Value =CryptographyHelper.EncryptData(page.Session["accessToken"].ToString(),secuirtyKey);
            node.Attributes.Append(accessKeyAttribute);

            XmlAttribute encryptedaccesskeysecretAttribute = doc.CreateAttribute("encryptedaccesskeysecret");
            encryptedaccesskeysecretAttribute.Value = CryptographyHelper.EncryptData(page.Session["accessTokenSecret"].ToString(),secuirtyKey);
            node.Attributes.Append(encryptedaccesskeysecretAttribute);

            XmlAttribute realmIdAttribute = doc.CreateAttribute("realmid");
            realmIdAttribute.Value = page.Session["realm"].ToString();
            node.Attributes.Append(realmIdAttribute);

            XmlAttribute dataSourceAttribute = doc.CreateAttribute("dataSource");
            dataSourceAttribute.Value = page.Session["dataSource"].ToString();
            node.Attributes.Append(dataSourceAttribute);

            doc.DocumentElement.AppendChild(node);
            doc.Save(path);
        } 

    
    }
}