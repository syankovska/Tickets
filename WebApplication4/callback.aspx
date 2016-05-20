<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="callback.aspx.cs" Inherits="Tickets.callback" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">

    <table class="wrapper">
    <tr>
       <td style="width:20%">
         <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
       </td>
       <td>
             <asp:TextBox ID="TextBox1" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr>
      <tr>
       <td>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
       </td>
       <td>
        <asp:TextBox ID="TextBox2" runat="server" EnableTheming="False" Width="331px">not find</asp:TextBox>
        </td>
        </tr>
      <tr>
       <td>     <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>     </td>
       <td>
     <asp:TextBox ID="TextBox3" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr>

    <tr>
       <td>   <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>    </td>
       <td>
           <asp:TextBox ID="TextBox4" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
   
    <tr>
       <td>   <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBox5" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
 
 </table>

 </asp:Content>