
<%@ Page Title="User View Product's Detail" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="UserViewFullProduct.aspx.cs" Inherits="HopeStore.User.UserViewFullProduct" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfProductId" runat="server" />
    <main aria-labelledby="title">
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-4">
                    <asp:Image ID="ProductImage" runat="server" AlternateText="Product Image" CssClass="img-fluid" />
                </div>
                <div class="col-md-8">
                    <h2 class="mb-4"><asp:Label ID="lblProductName" runat="server" /></h2>
                    <p class="lead">Price: रु <asp:Label ID="lblPrice" runat="server" /></p>
                    <p>Category: <asp:Label ID="lblCategory" runat="server" /></p>
                    <p>Description: <asp:Label ID="lblDescription" runat="server" /></p>
                    
                    
                    
                    <div class="input-group mb-3">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required." Display="Dynamic" ForeColor="Red" />
                         <asp:TextBox ID="txtQuantity" runat="server" placeholder="Enter Quantity" CssClass="form-control" />



                        <div class="input-group-append">
                            <asp:Button ID="btnBuy" runat="server" Text="Buy" CssClass="btn btn-primary" OnClick="btnBuy_Click" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <!-- Add to Cart button -->
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart 🛒" CssClass="btn btn-success" OnClick="btnAddToCart_Click" />
                        </div>
                    </div>

                </div>

                
            </div>
            
        </div>
    </main>
</asp:Content>
