<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmCustomerInfo.aspx.cs" Inherits="Tickets.frmCustomerInfo" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">
        <table class="wrapper">
    <tr>
       <td colspan="2">
         <asp:Label ID="Label1" runat="server" Text="Please put on your info for feedback:"></asp:Label>
       </td>
      </tr>
      <tr>
       <td style="width:10%">
        <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
       </td>
       <td>
        <asp:TextBox ID="TextBoxName" runat="server" EnableTheming="False"></asp:TextBox>
        </td>
        </tr>
      <tr>
       <td>     <asp:Label ID="LabelPhone" runat="server" Text="Phone*"></asp:Label>     </td>
       <td>
     <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
        </td>
        </tr>

    <tr>
       <td>   <asp:Label ID="LabelEmail" runat="server" Text="E-mail*"></asp:Label>    </td>
       <td>
           <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
        </td>
        </tr> 
<tr>
       <td colspan="2">
           <asp:Button ID="ButtonInfo" runat="server" Text="Confirm info" OnClick="ButtonInfo_Click" />
       </td>
      </tr>
 </table>
</asp:Content>