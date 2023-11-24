<%@ Page Title="Edit Product" Language="C#" MasterPageFile="~/Admins.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="HopeStore.Admin.EditProduct" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .current-image {
            max-width: 200px; /* Adjust the max-width as needed */
            max-height: 200px; /* Adjust the max-height as needed */
        }
        .form-group {
            margin-bottom: 15px; /* Adjust the margin as needed */
        }

        .edit-main-div{
              display: grid;
              justify-content: center;
              align-items: center;
              padding:10px;
              
              border-radius:7px;
              background-color:#FFF6F6;
        }
    </style>

    <div class="edit-main-div">
        <h2 id="title">Hope <%: Title %></h2>
        <asp:Label ID="lblProductId" runat="server" Text="Product ID:" CssClass="form-control"></asp:Label>

        <div class="form-group">
            <asp:Label ID="lblProductName" runat="server" Text="Product Name"></asp:Label>
            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Product Name"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblProductCategory" runat="server" Text="Product Category"></asp:Label>
            <asp:TextBox ID="txtProductCategory" runat="server" CssClass="form-control" placeholder="Product Category"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblProductPrice" runat="server" Text="Product Price"></asp:Label>
            <asp:TextBox ID="txtProductPrice" runat="server" CssClass="form-control" placeholder="Product Price"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblProductQuantity" runat="server" Text="Product Quantity"></asp:Label>
            <asp:TextBox ID="txtProductQuantity" runat="server" CssClass="form-control" placeholder="Product Quantity"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblProductDescription" runat="server" Text="Product Description"></asp:Label>
            <asp:TextBox ID="txtProductDescription" runat="server" CssClass="form-control" placeholder="Product Description"></asp:TextBox>
        </div>

        <!-- Display the current image -->
        <div class="form-group">
            <asp:Label ID="lblCurrentImage" runat="server" Text="Product Image" CssClass="form-control"></asp:Label>
            <asp:Image ID="imgCurrentImage" runat="server" CssClass="current-image" Visible="false" />
        </div>
        aa
        <!-- Allow uploading a new image -->
        <div class="form-group">
            <asp:Label ID="lblNewImage" runat="server" Text="New Image:" CssClass="form-control"></asp:Label>
            <asp:FileUpload ID="fileProductImage" runat="server" CssClass="form-control" />
        </div>

       <!-- <div class="form-group">
            <asp:Label ID="lblNewImagePath" runat="server" Text="New Image Path" CssClass="form-control"></asp:Label>
            <asp:TextBox ID="txtNewImage" runat="server" CssClass="form-control" placeholder="Update New Image" Enabled="false"></asp:TextBox>
        </div>-->

        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
    </div>
</asp:Content>
