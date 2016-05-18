<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmTicketTypes.aspx.cs" Inherits="Tickets.frmTicketTypes" %>

<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">
    
        <asp:Table ID="Table1" runat="server" CssClass="wrapper">
        </asp:Table>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Select" />

</asp:Content>