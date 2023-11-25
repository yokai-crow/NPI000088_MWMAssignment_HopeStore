using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace HopeStore
{
    public partial class ViewFullProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the ProductId from the query string
                if (Request.QueryString["ProductId"] != null)
                {
                    int productId = Convert.ToInt32(Request.QueryString["ProductId"]);

                    // Fetch product details based on productId and display them
                    DisplayProductDetails(productId);
                }
            }
        }

        private void DisplayProductDetails(int productId)
        {
            // Use the provided connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT Product_Id, Name, Price, Category, Description, Image FROM Products WHERE Product_Id = @ProductId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Display product details
                        lblProductName.Text = Convert.ToString(reader["Name"]);
                        lblPrice.Text = Convert.ToDouble(reader["Price"]).ToString("0.00");
                        lblCategory.Text = Convert.ToString(reader["Category"]);
                        lblDescription.Text = Convert.ToString(reader["Description"]);

                        // Retrieve the binary image data
                        byte[] imageBytes = (byte[])reader["Image"];

                        // Convert the binary data to a Base64 string for display
                        string base64String = Convert.ToBase64String(imageBytes);
                        string imageUrl = $"data:image;base64,{base64String}";

                        // Set the ImageUrl for the Image control
                        ProductImage.ImageUrl = imageUrl;

                        // Debugging statements
                        System.Diagnostics.Debug.WriteLine($"Constructed Image URL: {imageUrl}");
                    }
                }
            }
        }


        protected void btnBuy_Click(object sender, EventArgs e)
        {
            //dont need to buy for public user without account
            if (UserIsLoggedIn())
            {
             
                int quantity = Convert.ToInt32(txtQuantity.Text);

                //yesma buy garne code parena
            }
            else
            {
                // User is not logged in, redirect to the login page
                Response.Redirect("~/login.aspx");
            }
        }

        // Method to check if the user is logged in
        private bool UserIsLoggedIn()
        {
            
            return Session["UserId"] != null; 
        }
    }
}
