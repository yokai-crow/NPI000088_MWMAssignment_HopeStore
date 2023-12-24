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
                   
                    int userId = GetUserIdByEmail(email);

                    if (userId != -1)
                    {
                    
                        Session["UserId"] = userId;
                    }
                    else
                    {
                        
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

            // Declare imageUrl outside the if block
            string imageUrl = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT Product_Id, Name, Price, Category, Description, Image, Rating FROM Products WHERE Product_Id = @ProductId";
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
                        byte[] imageBytes = reader["Image"] as byte[];

                        // Convert the binary data to a Base64 string for display
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageUrl = $"data:image;base64,{Convert.ToBase64String(imageBytes)}";

                            // Set the ImageUrl for the Image control
                            ProductImage.ImageUrl = imageUrl;
                        }

                        // Display the current product rating
                        object ratingObj = reader["Rating"];
                        if (ratingObj != DBNull.Value)
                        {
                            double rating = Convert.ToDouble(ratingObj);
                            lblRating.Text = $"Rating: {rating}/5";
                        }
                        else
                        {
                            lblRating.Text = "Rating: N/A";
                        }

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
            // Check if the user is logged in
            if (UserIsLoggedIn())
            {
                // Get the current user ID from the session
                int userId = Convert.ToInt32(Session["UserId"]);

                // Get the product ID from the hidden field
                int productId = Convert.ToInt32(hfProductId.Value);

                // Get the selected quantity (you may need to validate this input)
                int quantity = Convert.ToInt32(txtQuantity.Text);

                // Call a method to perform the purchase logic
                ProcessPurchase(userId, productId, quantity);

                // Optionally, redirect or provide feedback to the user
                Response.Redirect("~/User/ShoppingHistory.aspx");
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

                // Display success message 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product added to cart.'); window.location='" + ResolveUrl("~/User/Cart.aspx") + "';", true);

            }

        }

        private void UpdateAddToCartButtonVisibility()
        {
            btnAddToCart.Visible = UserIsLoggedIn();
        }

        //addtocart end


        //buy

        private void ProcessPurchase(int userId, int productId, int quantity)
        {
          
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // database transaction (for data consistency)
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Call a method to update the product quantity and retrieve the price
                    double price = UpdateProductAndRetrievePrice(transaction, productId, quantity);

                    // Calculate total cost
                    double totalCost = quantity * price;

                    // Call a method to insert an order record into the OrderHistory table
                    InsertOrderRecord(transaction, userId, productId, quantity, totalCost);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // An error occurred, rollback the transaction
                    transaction.Rollback();

                    // Handle the error paxi
                    // For now, rethrow the exception 
                    throw ex;
                }
            }
        }

        private double UpdateProductAndRetrievePrice(SqlTransaction transaction, int productId, int quantity)
        {
           
            string updateQuery = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE Product_Id = @ProductId";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, transaction.Connection, transaction))
            {
                updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                updateCmd.Parameters.AddWithValue("@ProductId", productId);

                // Execute the update query
                updateCmd.ExecuteNonQuery();
            }

            
            string selectQuery = "SELECT Price FROM Products WHERE Product_Id = @ProductId";
            using (SqlCommand selectCmd = new SqlCommand(selectQuery, transaction.Connection, transaction))
            {
                selectCmd.Parameters.AddWithValue("@ProductId", productId);

            
                return Convert.ToDouble(selectCmd.ExecuteScalar());
            }
        }

        private void InsertOrderRecord(SqlTransaction transaction, int userId, int productId, int quantity, double totalCost)
        {
            
            string insertQuery = "INSERT INTO OrderHistory (user_id, ProductId, Quantity, TotalPrice, DeliveryStatus, OrderDate) " +
                                 "VALUES (@UserId, @ProductId, @Quantity, @TotalPrice, 'Pending', GETDATE())";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, transaction.Connection, transaction))
            {
                insertCmd.Parameters.AddWithValue("@UserId", userId);
                insertCmd.Parameters.AddWithValue("@ProductId", productId);
                insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                insertCmd.Parameters.AddWithValue("@TotalPrice", totalCost);

               
                insertCmd.ExecuteNonQuery();
            }
        }


        //

        protected void RateProduct(object sender, CommandEventArgs e)
        {
            if (UserIsLoggedIn())
            {
                int productId = Convert.ToInt32(hfProductId.Value);
                int userId = Convert.ToInt32(Session["UserId"]);
                int selectedRating = Convert.ToInt32(e.CommandArgument);

                // Call a method to update the product rating in the Products table
                UpdateProductRating(productId, selectedRating);

                // Optionally, refresh the product details to reflect the updated rating
                DisplayProductDetails(productId);
            }
            else
            {
                // User is not logged in, redirect to the login page
                Response.Redirect("~/login.aspx");
            }
        }


        protected void btnRate_Click(object sender, EventArgs e)
        {
            if (UserIsLoggedIn())
            {
                int productId = Convert.ToInt32(hfProductId.Value);
                int userId = Convert.ToInt32(Session["UserId"]);

                // Use int.TryParse to handle potential format issues
                if (int.TryParse(ratingRadioButtonList.SelectedValue, out int selectedRating))
                {
                    // Clear any previous validation error messages
                    txtQuantity.Text = "";

                    // Temporarily disable the quantity validator
                    rfvQuantity.Enabled = false;

                    try
                    {
                        // Call a method to update the product rating in the Products table
                        UpdateProductRating(productId, selectedRating);

                        // Optionally, refresh the product details to reflect the updated rating
                        DisplayProductDetails(productId);
                    }
                    finally
                    {
                        // Re-enable the quantity validator
                        rfvQuantity.Enabled = true;
                    }
                }
                else
                {
                    // Handle the case where the selected value is not a valid integer
                    // This might happen if the user hasn't selected a rating
                    // You can display an error message or take appropriate action
                }
            }
            else
            {
                // User is not logged in, redirect to the login page
                Response.Redirect("~/login.aspx");
            }
        }




        // Add a method to update the product rating in the Products table
        private void UpdateProductRating(int productId, int? rating)
        {
            // Check if the rating is not null before updating
            if (rating.HasValue)
            {
                // Use the provided connection string
                string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE Products SET Rating = @Rating WHERE Product_Id = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Rating", rating.Value);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}