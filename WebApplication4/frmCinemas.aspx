<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmCinemas.aspx.cs" Inherits="Tickets.frmCinemas" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Caption="Select Cinema" CellPadding="4" DataKeyNames="ID" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" AutoGenerateSelectButton="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Address1" HeaderText="Address1" SortExpression="Address1" />
                <asp:BoundField DataField="NameAlt" HeaderText="NameAlt" SortExpression="NameAlt" />
                <asp:BoundField DataField="ParkingInfo" HeaderText="ParkingInfo" SortExpression="ParkingInfo" />
                <asp:BoundField DataField="PublicTransport" HeaderText="PublicTransport" SortExpression="PublicTransport" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCinemasByCityAndScheduledFilmId" TypeName="Tickets.SyTicketsSvc.SessionsClient">
            <SelectParameters>
                <asp:SessionParameter Name="city" SessionField="CityName" Type="String" />
                <asp:SessionParameter Name="scheduledFilmId" SessionField="FilmId" Type="String" />
            </SelectParameters>
</asp:ObjectDataSource>
   
        </asp:Content>  
 