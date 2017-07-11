using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.Ipp.OAuth2PlatformClient
{

    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

        


    }

    public enum OidcScopes:int
    {


        [StringValue("openid")]
        OpenId = 1,
        [StringValue("email")]
        Email = 2,
        [StringValue("profile")]
        Profile = 3,
        [StringValue("phone")]
        Phone = 4,
        [StringValue("address")]
        Address = 5,
        [StringValue("com.intuit.quickbooks.accounting")]
        Accounting = 6,
        [StringValue("com.intuit.quickbooks.payment")]
        Payment = 7

    

}


    

}
