<%@ Page Title="Admin Inventory" Language="C#" MasterPageFile="~/Admins.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="HopeStore.Admin.Inventory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../MyHopeCustom/contact.css" rel="stylesheet" />
    <style>
        .table_body {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        #table_container {
            height: 80%;
            width: 100%;
            
            border-radius: 7px;
            overflow-y: auto;
        }

        #Inventory_GridView {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            border: 2px solid #ddd; /* Add border to the table */
        }

        #Inventory_GridView th, #Inventory_GridView td {
            border: 2px solid #ddd;
            padding: 8px;
            text-align: center;
        }

        #Inventory_GridView th {
            background-color: #f2f2f2;
        }

        #Inventory_GridView tbody tr:nth-child(even) {
            background-color: #f9f9f9; /* Zebra stripe style for even rows */
        }

        #Inventory_GridView tbody tr:nth-child(odd) {
            background-color: #ffffff; /* Zebra stripe style for odd rows */
        }

        #searchBar {
            margin-bottom: 1px;
            justify-content: inherit;
        }

        /* Styles for the edit button */
        .edit-button {
            background-color: yellow;
            color: black; /* Text color */
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius:7px;
        }

        /*   style for selected row */
        .selected-row {
            background-color: #a6a6a6;
        }

        /* Styles for the delete button */
        .delete-button {
            background-color: red;
            color: white; /* Text color */
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius:7px;
        }

        #Inventory_GridView tbody tr:nth-child(even) {
             background-color: #f2f2f2; /* Zebra stripe style for even rows */
        }

        #Inventory_GridView tbody tr:nth-child(odd) {
            background-color: #ffffff; /* Zebra stripe style for odd rows */
        }
    </style>

    <script type="text/javascript">
        // Add this script to handle row selection
        function SelectRow(row) {
            if (row.className === "selected-row") {
                row.className = "";
            } else {
                row.className = "selected-row";
            }
        }

        function ConfirmDelete() {
            return confirm("Are you sure you want to delete the selected product(s)?");
        }
    </script>
    <main aria-labelledby="title">
        <h2 id="title">Hope <%: Title %></h2>

        <div id="searchBar">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search..." />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>

        <div class="table_body">
            <div id="table_container">
                <asp:GridView ID="Inventory_GridView" runat="server" AutoGenerateColumns="False" 
    OnRowDataBound="Inventory_GridView_RowDataBound" OnSelectedIndexChanged="Inventory_GridView_SelectedIndexChanged" 
    OnRowCommand="Inventory_GridView_RowCommand" OnRowDeleting="Inventory_GridView_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Product_Id" HeaderText="Product ID" SortExpression="Product_Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Edit" CssClass="edit-button" CommandName="Edit" CommandArgument='<%# Eval("Product_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Delete" CssClass="delete-button" CommandName="Delete" CommandArgument='<%# Eval("Product_Id") %>' OnClientClick="return ConfirmDelete();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </main>
</asp:Content>
