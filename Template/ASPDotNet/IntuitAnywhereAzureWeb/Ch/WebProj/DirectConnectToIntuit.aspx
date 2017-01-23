<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectConnectToIntuit.aspx.cs" Inherits="$safeprojectname$.DirectConnectToIntuit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:ipp="">

<head id="Head1" runat="server">
    <title></title>
    <!--
The intuit.ipp.anywhere.directConnectToIntuit() function initiates the three-legged OAuth flow, 
which occurs when the user authorizes the app to connect to a QuickBooks company.  
Calling this function is analogous to clicking the Connect to QuickBooks button. 
Before calling this function, call the intuit.ipp.anywhere.setup() function, 
specifying the grantUrl parameter.  
-->
    <script type="text/javascript" src="https://appcenter.intuit.com/Content/IA/intuit.ipp.anywhere.js"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.3/jquery.min.js"></script>
<script type="text/javascript">    intuit.ipp.anywhere.setup({
        menuProxy: '<%=  Request.Url.GetLeftPart(UriPartial.Authority) +  "/" + ConfigurationManager.AppSettings["menuProxy"].ToString()  %>',
        grantUrl: '<%=  Request.Url.GetLeftPart(UriPartial.Authority) + "/" + ConfigurationManager.AppSettings["grantUrl"].ToString()  %>'
    });

    intuit.ipp.anywhere.directConnectToIntuit();
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
