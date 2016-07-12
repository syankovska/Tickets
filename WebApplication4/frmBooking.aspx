<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmBooking.aspx.cs" Inherits="Tickets.frmBooking" %>

<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
     <table class="wrapper">
  <tr>
       <td>   <asp:Label ID="LabelCinemaName" runat="server" Text="CinemaName"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxCinemaName" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
         <tr>
       <td>   <asp:Label ID="LabelFilmName" runat="server" Text="Film"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxFilmName" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
         <tr>
       <td>   <asp:Label ID="LabelShowTime" runat="server" Text="ShowTime"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxShowTime" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
         <tr>
       <td>   <asp:Label ID="LabelTotalValueCents" runat="server" Text="TotalValueCents"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxTotalValueCents" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
         <tr>
       <td>   <asp:Label ID="LabelTotalOrderCount" runat="server" Text="TotalOrderCount"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxTotalOrderCount" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
            <tr>
       <td>   <asp:Label ID="LabelOrderedSeats" runat="server" Text="OrderedSeats"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxOrderedSeats" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
    <tr>
       <td>   <asp:Label ID="Label5" runat="server" Text="Complete Order Result"></asp:Label>    </td>
       <td>
              <asp:TextBox ID="TextBoxOrderCompleteResult" runat="server" Width="330px"></asp:TextBox>
        </td>
        </tr> 
        <tr>
            <td> 
                   <asp:HyperLink ID="HyperLinkDownload" runat="server"  NavigateUrl="~/GenPDFHandle.ashx">Download your tickets</asp:HyperLink>
            </td>
 </tr>
 </table>
    </div>
</asp:Content>
