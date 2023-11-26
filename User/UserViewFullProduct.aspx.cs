using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore.User
{
    public partial class UserViewFullProduct : System.Web.UI.Page
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

                    //login vaye matra button dekhaune
                   // UpdateAddToCartButtonVisibility();
                }

                //Retrieve user email from session
                string email = Session["CustomerUser"] as string;

                if (!string.IsNullOrEmpty(email))
                {
                    // Get user_id based on email from the database
                    int userId = GetUserIdByEmail(email);

                    if (userId != -1)
                    {
                        // Assign the userId to a session variable or use it as needed
                        Session["UserId"] = userId;
                    }
                    else
                    {
                        // Handle the case where the user_id couldn't be retrieved
                        // You might want to redirect the user or show an error message
                    }
                }
                else
                {
                    // Handle the case where the email is not found in the session
                }
            }
        }


        //extract user id from session id
        private int GetUserIdByEmail(string email)
        {
            int userId = -1;

            // Use your database connection and query logic here
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT user_id FROM tbUsers WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
            }

            return userId;
        }
        

        //

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
                    // storing the productId in a hidden field for later use
                    hfProductId.Value = productId.ToString();
                }
            }
        }


        protected void btnBuy_Click(object sender, EventArgs e)
        {
            //dont need to buy for public user without account
            if (UserIsLoggedIn())
            {

                int quantity = Convert.ToInt32(txtQuantity.Text);

                //yesma buy garne code parxa
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
            //jpaye tei code vayo
            return Session["CustomerUser"] != null;
            //return Session["UserId"] != null;
        }

        //addto cart start

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (UserIsLoggedIn())
            {
                // Get the current user ID from the session
                int userId = Convert.ToInt32(Session["UserId"]);

                // Get product details
                int productId = Convert.ToInt32(hfProductId.Value);
                int quantity = Convert.ToInt32(txtQuantity.Text);
                double price = Convert.ToDouble(lblPrice.Text);

                // Calculate total cost
                double totalCost = quantity * price;

                // Save to Cart table
                SaveToCart(userId, productId, quantity, totalCost);

                // Update product quantity and display success message
                UpdateProductAndDisplayMessage(productId, quantity);

                // Clear the quantity textbox
                txtQuantity.Text = "";
            }
            else
            {
                // User is not logged in, redirect to the login page
                Response.Redirect("~/login.aspx");
            }
        }
        private void SaveToCart(int userId, int productId, int quantity, double totalCost)
        {
            // Use the provided connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO Cart (user_id, product_id, quantity, total_cost) VALUES (@UserId, @ProductId, @Quantity, @TotalCost)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@TotalCost", totalCost);

                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateProductAndDisplayMessage(int productId, int quantity)
        {
            // Use the provided connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Update product quantity
                string updateQuery = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE Product_Id = @ProductId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                updateCmd.Parameters.AddWithValue("@ProductId", productId);

                updateCmd.ExecuteNonQuery();

                // Display success message (you can customize this part)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product added to cart.'); window.location='" + ResolveUrl("~/User/Cart.aspx") + "';", true);

            }

        }

        private void UpdateAddToCartButtonVisibility()
        {
            btnAddToCart.Visible = UserIsLoggedIn();
        }

        //addtocart end

    }
}