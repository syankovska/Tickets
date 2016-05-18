<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="frmMain.aspx.cs" Inherits="Tickets.frmMain" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="MainContent"
    runat="server">

        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/frmCinemas.aspx">Выбор кинотеатра</asp:HyperLink>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Caption="Select Film" CellPadding="4" DataKeyNames="ScheduledFilmId" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" AutoGenerateSelectButton="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="ScheduledFilmId" HeaderText="ScheduledFilmId" SortExpression="ScheduledFilmId" Visible="False" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllFilms" TypeName="Tickets.SyTicketsSvc.SessionsClient"></asp:ObjectDataSource>
   
</asp:Content>
