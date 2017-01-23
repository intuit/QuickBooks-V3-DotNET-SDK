using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDSLinq
{
    public class Customer
    {
        // Customer's properties are Header
        private string id;
        private string name;
        private bool shipSameAsBillingField;

        // PhysicalAddress's properties is Line
        private PhysicalAddress[] otherAddressesField;
        private string contactNameField;
        private decimal openBalanceField;

        public Object Header
        {
            get { return new { this.id, this.name, this.shipSameAsBillingField }; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public bool ShipSameAsBillingField
        {
            get { return shipSameAsBillingField; }
            set { shipSameAsBillingField = value; }
        }
        
        public PhysicalAddress[] OtherAddressesField
        {
            get { return otherAddressesField; }
            set { otherAddressesField = value; }
        }
        
        public string ContactNameField
        {
            get { return contactNameField; }
            set { contactNameField = value; }
        }
        
        public decimal OpenBalanceField
        {
            get { return openBalanceField; }
            set { openBalanceField = value; }
        }
    }

    public partial class PhysicalAddress
    {

        private string idField;

        private string line1Field;

        private string line2Field;

        private string line3Field;

        private string cityField;

        private string countryField;

        private string countryCodeField;

        private string countrySubDivisionCodeField;

        private string postalCodeField;

        private string postalCodeSuffixField;

        private string latField;

        private string longField;

        private string tagField;

        /// <remarks/>
        /// <summary>
        /// Specifies the Unique Identifier for an Intuit Object for the address, mainly used for modifying the address.
        /// Pl note, there is no SyncToken associated with this object as Address object is always associated with the IntuitEntity which is TOP level entity
        /// </summary>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the first line of the address.
        /// Length Restriction:
        /// 
        /// </summary>
        public string Line1
        {
            get
            {
                return this.line1Field;
            }
            set
            {
                this.line1Field = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the second line of the address.
        /// Length Restriction:
        /// 
        /// </summary>
        public string Line2
        {
            get
            {
                return this.line2Field;
            }
            set
            {
                this.line2Field = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the third line of the address.
        /// Length Restriction:
        /// 
        /// </summary>
        public string Line3
        {
            get
            {
                return this.line3Field;
            }
            set
            {
                this.line3Field = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the city name
        /// Length Restriction:
        /// QBO:255 bytes
        /// 
        /// </summary>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the country name
        /// Length Restriction:
        /// QBO:255 bytes
        /// 
        /// </summary>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the country code as ISO 3166.
        /// Unsupported field.
        /// 
        /// </summary>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the "globalized" form of representation of region. For example, this would represent "State" information in case of USA, "Province" in case of Canada
        /// Length Restriction:
        /// QBO:255 bytes
        /// 
        /// </summary>
        public string CountrySubDivisionCode
        {
            get
            {
                return this.countrySubDivisionCodeField;
            }
            set
            {
                this.countrySubDivisionCodeField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the Postal Code, essential represent ZipCode in case of USA and Canada
        /// Length Restriction:
        /// QBO:30 bytes
        /// 
        /// </summary>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the Postal Code extension, in case of USA it specifies 4 letter code extension of ZipCode
        /// </summary>
        public string PostalCodeSuffix
        {
            get
            {
                return this.postalCodeSuffixField;
            }
            set
            {
                this.postalCodeSuffixField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Latitude coordinate of Geocode (Geospatial Entity Object Code) is representation format of a geospatial coordinate measurement used
        /// to provide a standard representation of an exact geospatial point location at, below, or above the surface of the earth at a specified moment of time.
        /// Unsupported field.
        /// 
        /// </summary>
        public string Lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Longitude coordinate of Geocode (Geospatial Entity Object Code) is representation format of a geospatial coordinate measurement used
        /// to provide a standard representation of an exact geospatial point location at, below, or above the surface of the earth at a specified moment of time.
        /// Unsupported field.
        /// 
        /// </summary>
        public string Long
        {
            get
            {
                return this.longField;
            }
            set
            {
                this.longField = value;
            }
        }

        /// <remarks/>
        /// <summary>
        /// Specifies the tag (label) associated with the phycisal address. The expected values are list of enums "PhysicalAddressLabelType"
        /// </summary>
        public string Tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }
    }
}
