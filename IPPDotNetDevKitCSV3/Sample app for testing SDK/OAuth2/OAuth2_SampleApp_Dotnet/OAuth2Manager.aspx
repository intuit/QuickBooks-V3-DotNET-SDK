<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OAuth2Manager.aspx.cs" Inherits="OAuth2_SampleApp_Dotnet.OAuth2Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <% if (HttpContext.Current.Session["accessToken"]!= null)
         {
             Response.Write("<script> window.opener.location.reload();window.close(); </script>");
             //Response.Write("<script>window.close(); </script>");
         }

    %> 

</head>
<body>
    <form id="form1" runat="server">
    <div>

         <h3>Welcome to the Intuit OAuth2 Sample App!</h3>
  Before using this app, please make sure you do the following:
  <ul>
    <li>
      Update your Client ID, Client Secret, Redirect Url (found on <a href="https://developer.intuit.com">developer.intuit.com</a>)
        in web.config</li>
      <li>
          Update your Log file Path in web.config</li>
      <li>
          Each button click flow should be tested by stopping the application and running it again.
           </li>
      <li>
          In actual app you will have only one of these button click implementations. Testing them all at once will result in exceptions.</li>
  </ul>

  

 
    
         <p>
             &nbsp;</p>

  </div>
 <div id="connect" runat="server" visible ="false">
    
  <!-- Sign In With Intuit Button -->
  <b>Sign In With Intuit</b><br />
    <asp:ImageButton id="btnSIWI" runat="server" AlternateText="Sign In With Intuit"
           ImageAlign="left"
           ImageUrl="Images/IntuitSignIn-lg-white@2x.jpg"
           OnClick="ImgSIWI_Click" Height="40px" Width="200px"/>

    <%--      <asp:ImageButton id="btnSIWI" runat="server" AlternateText="Sign In With Intuit"
           ImageAlign="left"
           ImageUrl="Images/IntuitSignIn-lg-white@2x.jpg"
           OnClick="ImgSIWI_Click" OnClientClick="PrepareBlank()" Height="40px" Width="200px"/>--%>

     <br /><br /><br />

    <!-- Connect To QuickBooks Button -->
    <b>Connect To QuickBooks</b><br />
    <asp:ImageButton id="btnC2QB" runat="server" AlternateText="Connect to Quickbooks"
           ImageAlign="left"
           ImageUrl="Images/C2QB_white_btn_lg_default.png"
           OnClick="ImgC2QB_Click" Height="40px" Width="200px"/>
     <br /><br /><br />

   <!-- Get App Now -->
   <b>Get App Now</b><br />
   <asp:ImageButton id="btnOpenId" runat="server" AlternateText="Get App Now"
           ImageAlign="left"
           ImageUrl="Images/Get_App.png"
           OnClick="ImgOpenId_Click" CssClass="font-size:14px border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>
     <br /><br /><br />
 
    
    </div>

 <div id="revoke" runat="server" visible ="false">
    <p>
    <asp:label runat="server" id="lblConnected" visible="false">"Your application is connected!"</asp:label>
    </p>  
     <asp:ImageButton id="btnQBOAPICall" runat="server" AlternateText="Call QBO API"
           ImageAlign="left"
       
           OnClick="ImgQBOAPICall_Click" CssClass="font-size:14px border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>

     <asp:ImageButton id="btnRevoke" runat="server" AlternateText="Revoke Tokens"
           ImageAlign="left"
           
           OnClick="ImgRevoke_Click" CssClass="font-size:14px border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>

   
</div>
    </form>

    <script type="text/javascript">
        function PrepareBlank() {
            // this will make a child page popup
            window.open("OAuth2Manager.aspx", "MyWindow", "height=375,width=350");
        }
        //function PrepareBlank() {
        //    document.getElementById("form").target = "_blank";
        //}


        //if (window.opener != null) {
        //    window.opener.document.getElementById("form").target = "";
        //}
    </script>
</body>
</html>
