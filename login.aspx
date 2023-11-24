<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HopeStore.login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="./MyHopeCustom/contact.css" rel="stylesheet" />
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
        <style>
            .formbody{
                background-image:url(./Elements/login.png);
            }

            .switch {
              position: relative;
              display: inline-block;
              width: 60px;
              height: 34px;
            }

            .switch input { 
              opacity: 0;
              width: 0;
              height: 0;
            }

            .slider {
              position: absolute;
              cursor: pointer;
              top: 0;
              left: 0;
              right: 0;
              bottom: 0;
              background-color: #ccc;
              -webkit-transition: .4s;
              transition: .4s;
            }

            .slider:before {
              position: absolute;
              content: "";
              height: 26px;
              width: 26px;
              left: 4px;
              bottom: 4px;
              background-color: white;
              -webkit-transition: .4s;
              transition: .4s;
            }

            input:checked + .slider {
              background-color: #2196F3;
            }

            input:focus + .slider {
              box-shadow: 0 0 1px #2196F3;
            }

            input:checked + .slider:before {
              -webkit-transform: translateX(26px);
              -ms-transform: translateX(26px);
              transform: translateX(26px);
            }

            /* Rounded sliders */
            .slider.round {
              border-radius: 34px;
            }

            .slider.round:before {
              border-radius: 50%;
            }
        </style>
        <div class="formbody">
            

            <div class="main1">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                 <form id="form" class="formclass">
                     
                    <div class="input__container">
                        <box style="margin-left:auto; margin-right:0;">Admin  | User</box>
                        <label class="switch" style="margin-left:70%; margin-right:0;">
                            <input runat="server" type="checkbox" id="usertype" checked>
                            <span class="slider round"></span>
                        </label>
                     </div>
                   <div class="input__container">
                      <label for="Email">E-mail</label>
                      <input runat="server" type="Email" id="Email" name="Email" required />
                    </div>
                    <div class="input__container">
                        <label for="Password">Password</label>
                        <input runat="server" type="password" id="Password" name="Password" required />
                     </div>
                     <asp:Button class="formbutton" ID="loginbutton" runat="server" Text="Login" OnClick="loginbutton_Click" />

                  
                 </form>
            </div>
        </div>
    </main>

</asp:Content>