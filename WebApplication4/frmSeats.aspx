<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmSeats.aspx.cs" Inherits="Tickets.frmSeats" %>

<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">

     <table style="width: 550px; margin-right: 0px;">
        <tr>
          <td style="width:61%; height: 25px;">
            <asp:Label ID="LabelNote" runat="server"></asp:Label>
           </td>
                 <td style="width:20%; height: 25px;">
                     </td>
        </tr>
        <tr>
            <td style="width:400px;">
                <asp:Table ID="Table1" runat="server" CssClass="wrapper" >
                </asp:Table>
                <asp:Table ID="TableTransp" runat="server" CssClass="wrapper" >
                </asp:Table>
            </td>
              <td class="tdcell">
                  <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel Order" Enabled="False" /> <br/><br/>
                       <asp:Label ID="LabelTotalOrderCount" runat="server">TotalOrderCount:</asp:Label><br/>
                <asp:TextBox ID="TextBoxTotalOrderCount" runat="server" ReadOnly="True" Width="96px"></asp:TextBox><br/> 
                   <asp:Label ID="LabelPayment" runat="server">TotalValueCents:</asp:Label><br/>
                <asp:TextBox ID="TextBoxTotalValueCents" runat="server" ReadOnly="True" Width="96px"></asp:TextBox><br/>
                 <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Submit Order" />
            </td>
        </tr>
         <tr>
             <td style="width:61%">
                 <asp:Label ID="LabelResponseResult" runat="server"></asp:Label>
             </td>
             <td style="width:20%" >
                 &nbsp;</td>
        </tr>
       </table>

</asp:Content>
