<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HopeStore.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="./MyHopeCustom/contact.css" rel="stylesheet">
    <main aria-labelledby="title">
        <h2 id="title">Feel Free To <%: Title %> Us</h2>

        <div class="formbody"><div class="main1">
             <form id="form" class="formclass">
               <div class="input__container">
                 <label for="name">Name</label>
                 <input type="text" id="name" name="name" required />
               </div>
                <div class="input__container">
                    <label for="subject">Subject</label>
                    <input type="text" id="subject" name="subject" value="" required />
                 </div>
               <div class="input__container">
                 <label for="message">Message</label>
                 <textarea class="formtextarea" id="message" name="message" rows="4" required></textarea>
               </div>
               <button id="button" class="formbutton" onclick="submitForm()">Submit</button>
             </form>
        </div></div>
        <script>
            "use strict";

            function submitForm() {
                const name = document.getElementById("name").value;
                const subject = document.getElementById("subject").value;
                const message = document.getElementById("message").value;

                const destinationEmail = "arunsaru166@gmail.com";
                const mailtoLink = `mailto:${destinationEmail}?subject=${encodeURIComponent(subject)}&body=Name:%20${name}%0D%0ASubject:%20${subject}%0D%0AMessage:%20${message}`;

                // Open the default email client with the pre-populated email
                window.location.href = mailtoLink;
            }

        </script>
    </main>

</asp:Content>

