<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmBooking.aspx.cs" Inherits="Tickets.frmBooking" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">

    <table class="wrapper">
  
    <tr>
       <td>   <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBox5" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
        <tr>
            <td> 
                   <asp:HyperLink ID="HyperLinkDownload" runat="server"  NavigateUrl="~/GenPDFHandle.ashx">Download your tickets</asp:HyperLink>
            </td>
 </tr>
 </table>
</asp:Content>