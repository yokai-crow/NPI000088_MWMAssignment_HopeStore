<%@ Page Title="Shopping History" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="ShoppingHistory.aspx.cs" Inherits="HopeStore.User.ShoppingHistory" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" class="container mt-5">
        <h2 id="title" style="margin-bottom:25px;">Shopping History</h2>

        <asp:Repeater ID="rptShoppingHistory" runat="server">
    <ItemTemplate>
        <div class="card mb-3">
            <div class="row g-0">
                <div class="col-md-4">
                    <img src='<%# GetImageURL(Eval("ProductId")) %>' alt='<%# Eval("ProductName") %>' class="img-fluid rounded-start" />
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("ProductName") %></h5>
                        <p class="card-text">Category: <%# Eval("Category") %></p>
                        <p class="card-text">Description: <%# Eval("Description") %></p>
                        <p class="card-text">Quantity: <%# Eval("Quantity") %></p>
                        <p class="card-text">Unit Price: रु <%# Eval("UnitPrice", "{0:N2}") %></p>
                        <p class="card-text">Total Price: रु <%# Eval("TotalPrice", "{0:N2}") %></p>
                        <p class="card-text">Delivery Status: <%# Eval("DeliveryStatus") %></p>
                        <p class="card-text">Order Date: <%# Eval("OrderDate", "{0:MM/dd/yyyy}") %></p>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>


    </main>
</asp:Content>
