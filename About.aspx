<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HopeStore.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/MyHopeCustom/hope.css" rel="stylesheet">
    <main aria-labelledby="title" style="overflow:hidden;">
        <section class="about-bg">
            <img src="./Elements/aboutBg.gif" width="100%" style="border-radius:6px;" />
        </section>
        <section>
            <h2 id="title" style="text-align:center; padding-top:20px;"><%: Title %> Us</h2>
        </section>
    </main>
</asp:Content>
