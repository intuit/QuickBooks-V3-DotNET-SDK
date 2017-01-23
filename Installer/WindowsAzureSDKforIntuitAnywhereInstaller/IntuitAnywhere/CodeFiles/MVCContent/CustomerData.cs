#region Using

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Caching;
using System.Collections.Specialized;
using System.Collections.Generic;

#endregion

public class CustomerData
{

    public string ServiceEndPoint
    {
        get;
        set;
    }

    public string QuickBookType
    {
        get;
        set;
    }

    public string getCustomerData()
    {
        return "This has to be implemented using SDK M1 Components";
    }
}
