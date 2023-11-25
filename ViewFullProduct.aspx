<%@ Page Title="View Full Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewFullProduct.aspx.cs" Inherits="HopeStore.ViewFullProduct" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                    <!-- controls for errormess and quantity -->
                    
                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required." Display="Dynamic" ForeColor="Red" />
                    <div class="input-group mb-3">
        
                         <asp:TextBox ID="txtQuantity" runat="server" placeholder="Enter Quantity" CssClass="form-control" />



                        <div class="input-group-append">
                            <asp:Button ID="btnBuy" runat="server" Text="Buy" CssClass="btn btn-primary" OnClick="btnBuy_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
