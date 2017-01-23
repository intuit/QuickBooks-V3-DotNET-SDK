<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="QuickBooksCustomers.aspx.cs" Inherits="IntuitSampleWebsite.QuickBooksCustomers" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div>
      <br />
    <br />
   
   <br /><br /><br />
    QuickBooks Customer Data<br />
   
   <br />
   <div class="test" style="overflow: scroll; width: 100%" runat="server" id="GridLocation">
    <div style="width: 1000px"> 
        <asp:GridView ID="GridView1" runat="server" >
        
        </asp:GridView>
        </div>
        </div>
        <br /><br />
    </div>
    <div runat="server" id="MessageLocation">
    No Customer Data Found!
    </div>
    
</asp:Content>
