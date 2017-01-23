<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="IntuitSampleWebsite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Intuit Anywhere
    </h2>
    <p>
        To learn more about Intuit Anywhere, visit the <a href="http://developer.intuit.com/"
            title="Intuit Developer Network" target="_blank">Intuit Developer Network</a>.
    </p>
    <p>
        You can also find our <a href=" https://ipp.developer.intuit.com/0010_Intuit_Partner_Platform/0025_Intuit_Anywhere/1000_Getting_Started_With_IA"
            title="Intuit Anywhere Getting Started guide" target="_blank">Intuit Anywhere Getting Started guide</a>
        in our documentation.
    </p>
    <!-- Code Written for Demonstration Purpose Only -->
    <div id="intuit OpenId Code Region">
        <div runat="server" id="IntuitSignin">
            <ipp:login href="OpenIdHandler.aspx"></ipp:login>
        </div>
        <div runat="server" id="IntuitInfo">
            <strong>Open Id Information:</strong><br />
            Welcome:
            <asp:Label Visible="true" Text="" runat="server" ID="friendlyName" />
            <br />
            E-mail Address:
            <asp:Label Visible="true" Text="" runat="server" ID="friendlyEmail" /><br />
            Friendly Identifier
            <asp:Label Visible="true" Text="" runat="server" ID="friendlyIdentifier" /><br />
            <br />
            <div runat="server" id="connectToIntuitDiv">
                <ipp:connecttointuit></ipp:connecttointuit>
            </div>
            <br />
            <br />
        </div>
        <div runat="server" id="oAuthinfo" visible="false">
            <a href="QuickBooksCustomers.aspx" id="QBCustomers">Get QuickBooks Customer List</a><br />
            <br />
            <br />
            <a href="#" id="Disconnect" onclick="confirmPost('/Disconnect.aspx');">Disconnect from
                QuickBooks</a><br />
        </div>
    </div>
</asp:Content>
