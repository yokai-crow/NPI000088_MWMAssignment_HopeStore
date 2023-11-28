using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace HopeStore.User
{
    public partial class ShoppingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve user ID from session
            int userId = GetUserFromSession();

            // Check if the user is logged in
            if (userId != -1)
            {
                // Fetch shopping history for the current user
                FetchShoppingHistory(userId);
            }
            else
            {
                // Redirect to the login page if the user is not logged in
                Response.Redirect("~/login.aspx");
            }
        }

        private int GetUserFromSession()
        {
            // Retrieve user email from session
            string email = Session["CustomerUser"] as string;

            if (!string.IsNullOrEmpty(email))
            {
                // Get user_id based on email from the database
                return GetUserIdByEmail(email);
            }

            return -1;
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

                    // Store the userId in the session
                    Session["UserId"] = userId;
                }
            }

            return userId;
        }

        private void FetchShoppingHistory(int userId)
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();


                string query = "SELECT OH.order_id, OH.ProductId, P.Name AS ProductName, P.Category, P.Description, OH.Quantity, OH.DeliveryStatus, OH.OrderDate, P.Price AS UnitPrice, OH.TotalPrice " +
                 "FROM OrderHistory OH " +
                 "INNER JOIN Products P ON OH.ProductId = P.Product_Id " +
                 "WHERE OH.user_id = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptShoppingHistory.DataSource = reader;
                    rptShoppingHistory.DataBind();
                }
            }
        }


        protected string GetImageURL(object productId)
        {
            return $"../ImageHandler.ashx?ProductId={productId}";
        }


        //



        

        protected void btnHiddenCancelOrder_Click(object sender, EventArgs e)
        {
            // Retrieve order ID from the hidden field
            int orderId = Convert.ToInt32(hfCancelOrderId.Value);

            // Server-side cancellation logic
            CancelOrder(orderId);

            // Refresh shopping history
            FetchShoppingHistory(Convert.ToInt32(Session["UserId"]));
        }

        private void CancelOrder(int orderId)
        {
          
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "DELETE FROM OrderHistory WHERE user_id = @UserId AND order_id = @OrderId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                // Execute the query
                cmd.ExecuteNonQuery();
            }
        }





        //
    }
}
