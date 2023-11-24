<%@ Page Title="User Profile" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="HopeStore.User.UserProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../MyHopeCustom/contact.css" rel="stylesheet" />
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
        <style>
            
            .profile-group {
                margin: 7px;
                border-radius:7px;
                background-color:#AEDEFC;
                text-align:center;
                padding:5px;
            }

            .user-profile-container{
                  display: grid;
                  justify-content: center;
                  align-items: center;
                  padding:10px;
          
                  border-radius:7px;
                  background-color:#99B080;
                  margin: 10px;
                  background-image:url(../Elements/profileback.jpg);
            }
        </style>
        <div class="user-profile-container">
            <div class="profile-group">
            <div class="profile-label">User ID:</div>
            <asp:Label ID="lblUserId" runat="server" CssClass="profile-value"></asp:Label>
            </div>
            
            <div class="profile-group">
            <div class="profile-label">Full Name</div>
            <asp:Label ID="lblUserName" runat="server" CssClass="profile-value"></asp:Label>
            </div>

            <div class="profile-group">
            <div class="profile-label">Email</div>
            <asp:Label ID="lblUserEmail" runat="server" CssClass="profile-value"></asp:Label>
            </div>

            <div class="profile-group">            
            <div class="profile-label">User Type</div>
            <asp:Label ID="lblUserType" runat="server" CssClass="profile-value"></asp:Label>
            </div>

            <div class="profile-group">
            <div class="profile-label">Date of Birth</div>
            <asp:Label ID="lblUserDOB" runat="server" CssClass="profile-value"></asp:Label>
            </div>

            <div class="profile-group">
            <div class="profile-label">Address</div>
            <asp:Label ID="lblUserAddress" runat="server" CssClass="profile-value"></asp:Label>
            </div>

            <div class="profile-group">
            <div class="profile-label">Contact</div>
            <asp:Label ID="lblUserContact" runat="server" CssClass="profile-value"></asp:Label>
            </div>
            

        </div>

    </main>
</asp:Content>
