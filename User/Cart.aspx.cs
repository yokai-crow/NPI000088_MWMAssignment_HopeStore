using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

                }
            }
            else
            {
                // Handle the case where the email is not found in the session
            }

            // Check if the user is logged in
            if (UserIsLoggedIn())
            {
                // Retrieve user_id from the session
                int userId = Convert.ToInt32(Session["UserId"]);

                // Fetch cart items for the current user
                FetchCartItems(userId);
            }
            else
            {
                // Redirect to the login page if the user is not logged in
                Response.Redirect("~/login.aspx");
            }
        }
        private int GetUserIdByEmail(string email)
        {
            int userId = -1;

           
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
        private void FetchCartItems(int userId)
        {
          
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

              
                string query = "SELECT Cart.cart_id, Products.Product_Id, Products.Name, Products.Category, " +
                               "Products.Description, Cart.Quantity, Cart.Total_Cost " +
                               "FROM Cart " +
                               "INNER JOIN Products ON Cart.Product_Id = Products.Product_Id " +
                               "WHERE Cart.user_id = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptCartItems.DataSource = reader;
                    rptCartItems.DataBind();
                }
            }
        }

        // Method to check if the user is logged in
        private bool UserIsLoggedIn()
        {
            return Session["CustomerUser"] != null;
        }
        protected string GetImageURL(object productId)
        {
            return $"../ImageHandler.ashx?ProductId={productId}";
        }

        //btns

        protected void btnRemoveItem_Command(object sender, CommandEventArgs e)
        {
            int cartId = Convert.ToInt32(e.CommandArgument);

            // Show confirmation dialog before removing
            string removeScript = $"confirmRemove('{cartId}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "RemoveConfirmation", removeScript, true);


            RemoveCartItem(cartId);
        }

        private void RemoveCartItem(int cartId)
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "DELETE FROM Cart WHERE cart_id = @CartId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CartId", cartId);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
        }

        // Server-side click event for the hidden button
        protected void btnHidden_Click(object sender, EventArgs e)
        {
            // Retrieve cart ID from the hidden field
            int cartId = Convert.ToInt32(hfCartId.Value);

            // server-side removal logic
            RemoveCartItem(cartId);

            // Refresh cart items
            FetchCartItems(Convert.ToInt32(Session["UserId"]));
        }

        protected void btnBuyHidden_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int productId = Convert.ToInt32(hfProductId.Value);
            int quantity = Convert.ToInt32(hfQuantity.Value);
            decimal totalCost = Convert.ToDecimal(hfTotalCost.Value);

            // Call a method to add the order to the OrderHistory table
            AddOrderToHistory(userId, productId, quantity, totalCost);

            // Call a method to remove the purchased items from the Cart table
            RemovePurchasedItems(userId, productId);

            // Optionally, refresh the cart items or provide feedback to the user
            FetchCartItems(userId);

            // Show success message
            lblSuccessMessage.Text = "Items brought successfully!";
            lblSuccessMessage.Visible = true;
        }


        private void RemovePurchasedItems(int userId, int productId)
        {
          
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query to delete purchased items from the Cart table
                string query = "DELETE FROM Cart WHERE user_id = @UserId AND Product_Id = @ProductId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
        }


        private void AddOrderToHistory(int userId, int productId, int quantity, decimal totalCost)
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

               
                string query = "INSERT INTO OrderHistory (user_id, ProductId, Quantity, TotalPrice, DeliveryStatus, OrderDate) " +
                               "VALUES (@UserId, @ProductId, @Quantity, @TotalPrice, 'Pending', GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", totalCost);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
        }

        //






    }
}
