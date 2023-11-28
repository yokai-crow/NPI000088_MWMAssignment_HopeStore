<%@ Page Title="Admin Report" Language="C#" MasterPageFile="~/Admins.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="HopeStore.Admin.Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
        <hr>
        <div class="report-section">
            <h3>Sales Overview</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvSalesOverview" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" />
                        <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" SortExpression="TotalSales" />
                        <asp:BoundField DataField="TotalExpenses" HeaderText="Total Expenses" SortExpression="TotalExpenses" />
                        <asp:BoundField DataField="NetProfit" HeaderText="Net Profit" SortExpression="NetProfit" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="report-section">
            <h3>Top Selling Products</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvTopSellingProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="Product_Id" HeaderText="Product ID" SortExpression="Product_Id" />
                        <asp:BoundField DataField="Name" HeaderText="Product Name" SortExpression="Name" />
                        <asp:BoundField DataField="TotalQuantitySold" HeaderText="Total Quantity Sold" SortExpression="TotalQuantitySold" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr>
        <div class="report-section">
            <h3>User Activity</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvUserActivity" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="user_id" HeaderText="User ID" SortExpression="user_id" />
                        <asp:BoundField DataField="fullname" HeaderText="Full Name" SortExpression="fullname" />
                        <asp:BoundField DataField="TotalOrders" HeaderText="Total Orders" SortExpression="TotalOrders" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </main>
</asp:Content>
