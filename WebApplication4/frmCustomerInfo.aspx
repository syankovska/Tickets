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
       <td style="width:20%">
        <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
       </td>
       <td>
        <asp:TextBox ID="TextBoxName" runat="server" EnableTheming="False"></asp:TextBox>
        </td>
        </tr>
      <tr>
       <td>     <asp:Label ID="LabelPhone" runat="server" Text="Phone*"></asp:Label>     </td>
       <td>
     <asp:TextBox ID="TextBoxPhone" runat="server" AutoPostBack="True" OnTextChanged="TextBoxPhone_TextChanged"></asp:TextBox>
        </td>
        </tr>

    <tr>
       <td>   <asp:Label ID="LabelEmail" runat="server" Text="E-mail*"></asp:Label>    </td>
       <td>
           <asp:TextBox ID="TextBoxEmail" runat="server" OnTextChanged="TextBoxPhone_TextChanged" AutoPostBack="True"></asp:TextBox>
        </td>
        </tr> 
             <tr>
       <td>   <asp:Label ID="LabelIsBooking" runat="server" Text="Just Booking"></asp:Label>    </td>
       <td>
            <asp:CheckBox ID="CheckBoxIsBooking" runat="server" OnCheckedChanged="CheckBoxIsBooking_CheckedChanged" AutoPostBack="True" />
        </td>
        </tr> 
 </table>
</asp:Content>