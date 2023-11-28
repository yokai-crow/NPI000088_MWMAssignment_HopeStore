using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore.Admin
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateProductTable();
            }
        }

        private void BindProductTable(SqlDataReader reader)
        {
            // Bind the data to the GridView
            Inventory_GridView.DataSource = reader;
            Inventory_GridView.DataBind();
        }

        private void PopulateProductTable()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                {
                    con.Open();

                    string query = "SELECT Product_Id, Name, Category, Price, Quantity, Description FROM Products";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    BindProductTable(reader);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        protected void Inventory_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string productId = Inventory_GridView.DataKeys[e.RowIndex].Value.ToString();
                DeleteProduct(productId);
                PopulateProductTable();
            }
            catch (Exception ex)
            {
                
            }
        }






        // Event handler for GridView RowDataBound
        protected void Inventory_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        //del
        protected void Inventory_GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnEdit = new Button();
                btnEdit.Text = "Edit";
                btnEdit.CssClass = "edit-button";
                btnEdit.CommandName = "Edit";
                btnEdit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "Product_Id").ToString();

                // Add client-side script to prevent automatic postback dherai chainxa :)
                btnEdit.Attributes.Add("onclick", "return false;");

                e.Row.Cells[6].Controls.Add(btnEdit);

                Button btnDelete = new Button();
                btnDelete.Text = "Delete";
                btnDelete.CssClass = "delete-button";
                btnDelete.CommandName = "Delete";
                btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "Product_Id").ToString();

                // Add client-side script to confirm deletion
                btnDelete.OnClientClick = "return ConfirmDelete();";

                e.Row.Cells[7].Controls.Add(btnDelete);
            }
        }

        //dell



        // Event handler for GridView SelectedIndexChanged
        protected void Inventory_GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Access the Product ID from the selected row
            string productId = Inventory_GridView.SelectedRow.Cells[0].Text;

            
            DeleteProduct(productId);

            // Re-populate the table after deletion
            PopulateProductTable();
        }

        private void DeleteProduct(string productId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                {
                    con.Open();

                    string query = "DELETE FROM Products WHERE Product_Id = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    // Execute the delete command
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        // Event handler for GridView RowCommand (button clicks)
        protected void Inventory_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Retrieve the product ID from the CommandArgument of the clicked button
                string productId = e.CommandArgument.ToString();

                // Redirect to the edit page with the product ID in the query string
                Response.Redirect($"EditProduct.aspx?ProductId={productId}");
            }
            else if (e.CommandName == "Delete")
            {
                // Retrieve the product ID from the CommandArgument of the clicked button
                string productId = e.CommandArgument.ToString();

                // Implement the logic to delete the product with the specified ID
                DeleteProduct(productId);

                // Re-populate the table after deletion
                PopulateProductTable();
            }
        }

        //search start
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                SearchAndDisplayResults(searchQuery);
            }
            else
            {
                // If the search query is empty, display all products
                PopulateProductTable();
            }
        }

        private void SearchAndDisplayResults(string searchQuery)
        {
            // Perform a search query on your database
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                {
                    con.Open();

                    string query = "SELECT Product_Id, Name, Category, Price, Quantity, Description " +
                                   "FROM Products " +
                                   "WHERE Name LIKE @SearchQuery OR Category LIKE @SearchQuery OR Description LIKE @SearchQuery";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Bind the search results to the GridView
                    Inventory_GridView.DataSource = reader;
                    Inventory_GridView.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        //search ends
    }
}
