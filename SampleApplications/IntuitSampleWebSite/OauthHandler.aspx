<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OauthHandler.aspx.cs" Inherits="IntuitSampleWebsite.OauthHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    

</head>
<body>
    <form id="form1" runat="server">
    <div>
    Loading...
    <script type="text/javascript">
        try {
            
            var parentlocation = window.parent.opener.location.hostname;
            var currentlocation = window.location.hostname;
            if (parentlocation != currentlocation) {

                window.location = "/default.aspx";
            }
            else {

                window.opener.location.href = window.opener.location.href;

                window.close();
            }
        }
        catch (e) {
           
            window.location = "/default.aspx";
        }

    </script>
    </div>
    </form>
</body>
</html>
