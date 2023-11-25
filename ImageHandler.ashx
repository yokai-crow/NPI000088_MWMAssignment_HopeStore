<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public class ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            int productId = Convert.ToInt32(context.Request.QueryString["ProductId"]);

            string query = "SELECT Image FROM Products WHERE Product_Id = @ProductId";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                byte[] imageBytes = (byte[])cmd.ExecuteScalar();

                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(imageBytes);
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
