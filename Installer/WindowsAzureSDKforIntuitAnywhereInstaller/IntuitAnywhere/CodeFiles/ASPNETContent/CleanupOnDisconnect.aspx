<%@ Page Title="Cleanup on Disconnect Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CleanupOnDisconnect.aspx.cs" Inherits="IntuitSampleWebSite.CleanupOnDisconnect" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <div>
        <h2>
            DISCONNECT SUCCESS</h2>
        <p>
            We are sorry to see you have disconnected your Quickbooks company file.
        </p>
        <p>
            If you did this in error, please click the Connect to Quickbooks button below to
            reconnect.</p>
        <p>
            <ipp:connecttointuit></ipp:connecttointuit>
        </p>
        <p>
            <b>Please note: </b>Your subscription to IntuitSampleWebSite has not been cancelled.
            To cancel your subscription, please contact support.</p>
    </div>
</asp:Content>