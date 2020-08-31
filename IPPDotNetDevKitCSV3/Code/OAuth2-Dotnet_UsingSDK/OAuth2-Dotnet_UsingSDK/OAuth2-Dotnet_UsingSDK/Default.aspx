<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OAuth2_Dotnet_UsingSDK.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <% if (dictionary.ContainsKey("accessToken"))
            {
                Response.Write("<script> window.opener.location.reload();window.close(); </script>");
            }
        %> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                 <h3>Welcome to the Intuit OAuth2 Sample App!</h3>
                 Before using this app, please make sure you do the following:
                 <ul>
                    <li>Note:This sample app is just for reference and works great for Chrome and Firefox. IE throws up some javascript errors so it is advisable to test for the specific browser you are working with and make desired changes.</li>
                    <li>Update your Client ID, Client Secret, Redirect Url (found on <a href="https://developer.intuit.com">developer.intuit.com</a>) and app environment in web.config</li>
                    <li>Also Update your Log file Path in web.config</li>
                    <li>Each button click flow should be tested by stopping the application and running it again.</li>
                    <li>QBO API call can be made for only for C2QB or Get App Now flows. You will not see any Output for QBO call on the screen.You need to debug the call.</li>
                    <li>In actual app you will have only one of these button click implementations. Testing them all at once will result in exceptions.</li>
                </ul>
                <p>&nbsp;</p>
            </div>
            <div id="mainButtons" runat="server" visible ="false">
            <!-- Sign In With Intuit Button -->
                <b>Sign In With Intuit</b><br />
                <asp:ImageButton id="btnSIWI" runat="server" AlternateText="Sign In With Intuit"
                   ImageAlign="left"
                   ImageUrl="Images/IntuitSignIn-lg-white@2x.jpg"
                   OnClick="ImgOpenId_Click" Height="40px" Width="200px"/>
                <br /><br /><br />

                <!-- Connect To QuickBooks Button -->
                <b>Connect To QuickBooks</b><br />
                <asp:ImageButton id="btnOAuth" runat="server" AlternateText="Connect to Quickbooks"
                       ImageAlign="left"
                       ImageUrl="Images/C2QB_white_btn_lg_default.png"
                       OnClick="ImgC2QB_Click" Height="40px" Width="200px"/>
                 <br /><br /><br />

               <!-- Get App Now -->
               <b>Get App Now</b><br />
               <asp:ImageButton id="btnOpenId" runat="server" AlternateText="Get App Now"
                       ImageAlign="left"
                       ImageUrl="Images/Get_App.png"
                       OnClick="ImgGetAppNow_Click" CssClass="font-size:14px; border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>
                 <br /><br /><br />
            </div>

             <div id="connected" runat="server" visible ="false">
                <p><asp:label runat="server" id="lblConnected" visible="false">"Your application is connected!"</asp:label></p> 
                 
                <asp:Button id="btnQBOAPICall" runat="server" Text="Call QBO API" OnClick="btnQBOAPICall_Click"/>
                 <br />
                
                <p><asp:label runat="server" id="lblQBOCall" visible="false"></asp:label></p>
                <br /><br />

                 <asp:Button id="btnUserInfo" runat="server" Text="Get User Info" OnClick="btnUserInfo_Click" />
                <br />

                 <p><asp:label runat="server" id="lblUserInfo" visible="false"></asp:label></p>
                <br /><br />

                 <asp:Button id="btnRefresh" runat="server" Text="Refresh Tokens" OnClick="btnRefresh_Click"/>
                <br /><br /><br />

                <asp:Button id="btnRevoke" runat="server" Text="Revoke Tokens"  OnClick="btnRevoke_Click"/>
                <br /><br /><br /> 
            </div>
        </form>
    </body>
</html>