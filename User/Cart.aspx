<%@ Page Title="User Cart" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="HopeStore.User.Cart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" class="container mt-5">
        <h2 id="title">Your Cart</h2>

        <asp:Repeater ID="rptCartItems" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="row g-0">
                        <div class="col-md-4">
                            <img src='<%# GetImageURL(Eval("Product_Id")) %>' alt='<%# Eval("Name") %>' class="img-fluid rounded-start" />
                        </div>
                        <div class="col-md-8">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                <p class="card-text">Category: <%# Eval("Category") %></p>
                                <p class="card-text">Description: <%# Eval("Description") %></p>
                                <p class="card-text">Quantity: <%# Eval("Quantity") %></p>
                                <p class="card-text">Total Cost: रु <%# Eval("Total_Cost", "{0:N2}") %></p>

                                <asp:Button ID="btnRemoveItem" runat="server" Text="Remove from Cart" CssClass="btn btn-danger"
                                    OnClientClick='<%# "confirmRemove(" + Eval("cart_id") + "); return false;" %>' />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <script type="text/javascript">
            function confirmRemove(cartId) {
                var confirmResult = confirm('Are you sure you want to remove this product from the cart?');
                if (confirmResult) {
                    // If the user clicks OK, perform the removal logic on the client-side
                    // You can also consider calling a server-side method using AJAX for removal
                    // Here, we'll use a hidden field to store the cart ID and trigger a postback
                    document.getElementById('<%= hfCartId.ClientID %>').value = cartId;
                    document.getElementById('<%= btnHidden.ClientID %>').click();
                } else {
                    // If the user clicks Cancel, do nothing or provide feedback
                }
            }
        </script>

        <!-- Hidden Button and Field for Server-Side Handling -->
        <asp:Button ID="btnHidden" runat="server" Style="display: none" OnClick="btnHidden_Click" />
        <asp:HiddenField ID="hfCartId" runat="server" />
    </main>
</asp:Content>
