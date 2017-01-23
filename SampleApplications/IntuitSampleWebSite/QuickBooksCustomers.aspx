<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="QuickBooksCustomers.aspx.cs"
    Inherits="IntuitSampleWebsite.QuickBooksCustomers" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div class="test" style="overflow: scroll; width: 100%" runat="server" id="GridLocation">
            QuickBooks Customer Data<br />
            <div style="width: 1000px">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </div>
        </div>
        <br />
        <br />
    </div>
    <div runat="server" id="MessageLocation">
        No Customer Data Found!
        <br />
        <br />
        <a href="Default.aspx">Back to Home Page</a>
    </div>
</asp:Content>
