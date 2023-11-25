<%@ Page Title="Edit Profile" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="HopeStore.User.EditProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../MyHopeCustom/contact.css" rel="stylesheet" />
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
        <div class="container mt-4">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                   
                    <div class="mb-3">
                        <label for="txtFullname" class="form-label">Fullname</label>
                        <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Fullname"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="********" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtAddress" class="form-label">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtContact" class="form-label">Contact</label>
                        <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Contact"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </main>
</asp:Content>
