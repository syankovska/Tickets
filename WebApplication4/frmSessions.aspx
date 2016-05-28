
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmSessions.aspx.cs" Inherits="Tickets.frmSessions" %>
<%@ PreviousPageType VirtualPath="~/frmMain.aspx" %> 
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Caption="Select Session" CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="SessionId,CinemaId,ScheduledFilmId">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="SessionId" HeaderText="SessionId" SortExpression="SessionId" Visible="False" />
                <asp:BoundField DataField="CinemaId" HeaderText="CinemaId" SortExpression="CinemaId" Visible="False" />
                <asp:BoundField DataField="ScheduledFilmId" HeaderText="ScheduledFilmId" SortExpression="ScheduledFilmId" Visible="False" />
                <asp:BoundField DataField="CinemaName" HeaderText="CinemaName" SortExpression="CinemaName" />
                <asp:BoundField DataField="SeatsAvailable" HeaderText="SeatsAvailable" SortExpression="SeatsAvailable" />
                <asp:BoundField DataField="Showtime" HeaderText="Showtime" SortExpression="Showtime" />
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
        
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSessionsByScheduledFilmAndSessionBusinessDateAndCity" TypeName="Tickets.SyTicketsSvc.SessionsClient">
            <SelectParameters>
                <asp:Parameter DefaultValue="Session(&quot;FilmId&quot;)" Name="scheduledFilm" Type="String" />
                <asp:SessionParameter DefaultValue="" Name="sessionBusinessDate" SessionField="TicketDate" Type="DateTime" />
                <asp:SessionParameter DefaultValue="" Name="city" SessionField="CityName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
</asp:Content>
