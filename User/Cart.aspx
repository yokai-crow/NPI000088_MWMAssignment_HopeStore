<%@ Page Title="User Cart" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="HopeStore.User.Cart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" class="container mt-5">
        <h2 id="title">Your Cart</h2>

        <div style="text-align:center; margin-bottom:5px;">
        <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-success" EnableViewState="false" Visible="false"></asp:Label>
        </div>
        
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


                                <asp:Button ID="btnBuy" runat="server" Text="Buy Now" CssClass="btn btn-success"
            OnClientClick='<%# "buyProduct(" + Eval("Product_Id") + "," + Eval("Quantity") + "," + Eval("Total_Cost") + "); return false;" %>' />

                                <asp:Button ID="btnRemoveItem" runat="server" Text="Remove from Cart" CssClass="btn btn-danger"
                                    OnClientClick='<%# "confirmRemove(" + Eval("cart_id") + "); return false;" %>' />
                            
                                
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <script type="text/javascript">

            function buyProduct(productId, quantity, totalCost) {
                var confirmResult = confirm('Are you sure you want to buy this product?');
                if (confirmResult) {
                    // If the user clicks OK, perform the purchase logic on the client-side
                    // use a hidden field to store product details and trigger a postback
                    document.getElementById('<%= hfProductId.ClientID %>').value = productId;
            document.getElementById('<%= hfQuantity.ClientID %>').value = quantity;
            document.getElementById('<%= hfTotalCost.ClientID %>').value = totalCost;
                    document.getElementById('<%= btnBuyHidden.ClientID %>').click();
                } else {
                    // If the user clicks Cancel, do nothing 
                }
            }

            function confirmRemove(cartId) {
                var confirmResult = confirm('Are you sure you want to remove this product from the cart?');
                if (confirmResult) {
                    
                    document.getElementById('<%= hfCartId.ClientID %>').value = cartId;
                    document.getElementById('<%= btnHidden.ClientID %>').click();
                } else {
                    
                }
            }
        </script>

        <!-- Hidden Button and Field for Server-Side Handling -->
        <asp:Button ID="btnHidden" runat="server" Style="display: none" OnClick="btnHidden_Click" />
        <asp:HiddenField ID="hfCartId" runat="server" />

        <!-- Hidden Button and Fields for Server-Side Handling -->
        <asp:Button ID="Button1" runat="server" Style="display: none" OnClick="btnHidden_Click" />
        <asp:Button ID="btnBuyHidden" runat="server" Style="display: none" OnClick="btnBuyHidden_Click" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hfProductId" runat="server" />
        <asp:HiddenField ID="hfQuantity" runat="server" />
        <asp:HiddenField ID="hfTotalCost" runat="server" />


    </main>
</asp:Content>
