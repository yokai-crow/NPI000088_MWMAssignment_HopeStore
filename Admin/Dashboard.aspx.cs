using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace HopeStore.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                Response.Redirect("~/Login.aspx");
            }

            
            if (!IsPostBack)
            {
                BindOrderHistoryData();
            }
        }

        private void BindOrderHistoryData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM OrderHistory WHERE deliverystatus <> 'Received'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvOrderHistory.DataSource = dt;
                    gvOrderHistory.DataBind();
                }
            }
        }


        protected void gvOrderHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deliver")
            {
                
                int orderId = Convert.ToInt32(e.CommandArgument);

                
                UpdateDeliveryStatus(orderId);

               
                BindOrderHistoryData();
            }
        }

        private void UpdateDeliveryStatus(int orderId)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

           
            string updateQuery = "UPDATE OrderHistory SET deliverystatus = 'Received' WHERE order_id = @OrderId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                   
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }

}
