<%@ Page Title="User View Product's Detail" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="UserViewFullProduct.aspx.cs" Inherits="HopeStore.User.UserViewFullProduct" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfProductId" runat="server" />
    <main aria-labelledby="title">
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-4">
                    <asp:Image ID="ProductImage" runat="server" AlternateText="Product Image" CssClass="img-fluid rounded" />
                </div>
                <div class="col-md-8">
                    <h2 class="display-4 mb-4"><asp:Label ID="lblProductName" runat="server" /></h2>
                    <p class="lead">Price: रु <asp:Label ID="lblPrice" runat="server" CssClass="font-weight-bold" /></p>
                    <p>Category: <asp:Label ID="lblCategory" runat="server" CssClass="text-muted" /></p>
                    <p>Description: <asp:Label ID="lblDescription" runat="server" CssClass="text-justify" /></p>

                    <!-- Display the current product rating -->
                    <div class="rating my-3">
                        <asp:Label ID="lblRating" runat="server" CssClass="mr-2 font-weight-bold" />
                        <asp:RadioButtonList ID="ratingRadioButtonList" runat="server" RepeatDirection="Horizontal" CssClass="star-rating">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                        </asp:RadioButtonList>
                        <asp:Button ID="btnRate" runat="server" Text="Rate" CssClass="btn btn-info ml-2" OnClick="btnRate_Click" />
                    </div>

                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtQuantity" runat="server" placeholder="Enter Quantity" CssClass="form-control" Text="1" />
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity"
                            ErrorMessage="Quantity is required." Display="Dynamic" ForeColor="Red" InitialValue="" />
                        <div class="input-group-append">
                            <asp:Button ID="btnBuy" runat="server" Text="Buy Now" CssClass="btn btn-primary" OnClientClick="return validateQuantity();" OnClick="btnBuy_Click" />
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

    <style>
        .rating {
            display: flex;
            align-items: center;
        }

        .star-rating {
            display: flex;
        }

        .star-rating label {
            font-size: 24px;
            padding: 5px;
            cursor: pointer;
        }

        .star-rating input {
            display: none;
        }

        .star-rating label:before {
            content: '\2605'; /* Unicode character for a star */
            color: #e4e4e4;
        }

        .star-rating input:checked ~ label:before {
            color: #ffcc00;
        }
    </style>

    <script>
        function validateQuantity() {
            var quantity = document.getElementById('<%= txtQuantity.ClientID %>').value;
            if (quantity.trim() === "") {
                alert("Quantity is required.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
