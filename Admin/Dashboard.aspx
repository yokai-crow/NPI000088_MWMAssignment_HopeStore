<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Admins.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HopeStore.Admin.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="./MyHopeCustom/contact.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <style>
        #gvOrderHistory {
            margin-top: 20px;
        }
    </style>

    <script type="text/javascript">
        function ConfirmDeliver() {
            return confirm("You are about to assign and deliver this product, ok?");
        }
    </script>

    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
        <h3>Product Sales</h3>
        <div class="container">
            <div class="row">
                <div class="col">
                    <div class="table-responsive">
                        <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="gvOrderHistory_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="order_id" HeaderText="Order ID" SortExpression="order_id" />
                                <asp:BoundField DataField="user_id" HeaderText="User ID" SortExpression="user_id" />
                                <asp:BoundField DataField="ProductId" HeaderText="Product ID" SortExpression="ProductId" />
                                <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" SortExpression="TotalPrice" />
                                <asp:BoundField DataField="deliverystatus" HeaderText="Delivery Status" SortExpression="deliverystatus" />
                                <asp:BoundField DataField="orderdate" HeaderText="Order Date" SortExpression="orderdate" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Deliver" CssClass="btn btn-primary" CommandName="Deliver" CommandArgument='<%# Eval("order_id") %>' OnClientClick="return ConfirmDeliver();" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
