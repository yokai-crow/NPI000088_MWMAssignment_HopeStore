using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HopeStore.Admin
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindSalesOverview();
                BindTopSellingProducts();
                BindUserActivity();
            }
        }

        private void BindSalesOverview()
        {
            // Same as before
        }

        private void BindTopSellingProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT TOP 5 p.Product_Id, p.Name, SUM(oh.quantity) AS TotalQuantitySold " +
                               "FROM OrderHistory oh " +
                               "JOIN Products p ON oh.ProductId = p.Product_Id " +
                               "GROUP BY p.Product_Id, p.Name " +
                               "ORDER BY TotalQuantitySold DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvTopSellingProducts.DataSource = dt;
                    gvTopSellingProducts.DataBind();
                }
            }
        }


        private void BindUserActivity()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT " +
                               "user_id, " +
                               "fullname, " +
                               "(SELECT COUNT(*) FROM OrderHistory WHERE user_id = u.user_id) AS TotalOrders " +
                               "FROM tbUsers u " +
                               "WHERE user_id <> 10"; //excluding admin

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvUserActivity.DataSource = dt;
                    gvUserActivity.DataBind();
                }
            }
        }

    }
}
