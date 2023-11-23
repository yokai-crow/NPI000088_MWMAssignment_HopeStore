<%@ Page Title="Add Products" Language="C#" MasterPageFile="~/Admins.Master" AutoEventWireup="true" CodeBehind="Addproducts.aspx.cs" Inherits="HopeStore.Admin.Addproducts" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--<link href="./MyHopeCustom/contact.css" rel="stylesheet" />-->
    <link href="../MyHopeCustom/contact.css" rel="stylesheet">
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>
         <style>
             .formbody{
                 background-image:url(../Elements/login.png);
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

             #successMessage {
                    background-color: #4CAF50;
                    color: white;
                    padding: 15px;
                    margin-bottom: 10px;
             }
         </style>
       

         <div class="formbody">
     
             <div class="main1">
    
                  <form id="form" class="formclass">
              
                     <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>

                    <div class="input__container">
                      <label for="ProductName">Product Name</label>
                      <input runat="server" type="text" id="Pd_Name" name="Product_Name" required />
                    </div>

                      <div class="input__container">
                       <label for="Product">Category</label>
                        <select runat="server" type="text" name="Pd_Category" id="Pd_Category" required>
                          <option value="Apparel">Apparel</option>
                          <option value="Glocery">Glocery</option>
                          <option value="Decoration">Decoration</option>
                          <option value="Electronics & Tech">Electronic & Tech</option>
                        </select>
                     </div>

                      <div class="input__container">
                           <label for="Pd_Price">Price</label>
                           <input runat="server" type="number" id="Pd_Price" name="Pd_Price" required />
                       </div>

                      <div class="input__container">
                          <label for="Pd_Quantity">Quantity</label>
                          <input runat="server" type="number" id="Pd_Quantity" name="Pd_Quantity" required />
                      </div>

                       <div class="input__container">
                           <label for="Product_Image">Image</label>
                           <input runat="server" type="file" id="Pd_Image" name="Pd_Image" required />
                       </div>

                       <div class="input__container">
                            <label for="Pd_Description">Description</label>
                            <input runat="server" type="text" id="Pd_Description" name="Pd_Description" required />
                        </div>

                       

                      <asp:Button ID="Add_Product" runat="server" class="formbutton" Text="Add Product" OnClick="Add_Product_Click" />
                      
            
                  </form>
             </div>
         </div>
    </main>
     
</asp:Content>

