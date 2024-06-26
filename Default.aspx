﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HopeStore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">Featured Products</h2>

        <div class="row">
           <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100">
                            <a href='<%# $"ViewFullProduct.aspx?ProductId={Eval("ProductId")}" %>'>
                                <img src='<%# $"ImageHandler.ashx?ProductId={Eval("ProductId")}" %>' class="card-img-top" alt='<%# Eval("Name") %>' />
                            </a>
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                <p class="card-text">Price: रु <%# Eval("Price", "{0:N2}") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>



        </div>
    </main>
</asp:Content>
