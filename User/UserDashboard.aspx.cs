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
    public partial class UserDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                // Get a list of featured products from the database
                List<Product> featuredProducts = GetFeaturedProductsFromDatabase();

                // Bind the data to the Repeater
                rptProducts.DataSource = featuredProducts;
                rptProducts.DataBind();
            }
        }
        public class Product
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string ImagePath { get; set; }
            public string ProductUrl { get; set; }
        }

        private List<Product> GetFeaturedProductsFromDatabase()
        {
            List<Product> products = new List<Product>();

            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Use ORDER BY NEWID() to get random order
                string query = "SELECT TOP 6 Product_Id, Name, Price, Image FROM Products ORDER BY NEWID()";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = Convert.ToInt32(reader["Product_Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            ImagePath = Convert.ToString(reader["Image"]),
                        };

                        // Constructing the correct image URL
                        product.ImagePath = $"~/Images/{product.ImagePath}"; 

                        // construct the ProductUrl based on the ProductId
                        product.ProductUrl = $"ProductDetails.aspx?ProductId={product.ProductId}";

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                // Retrieve the selected ProductId
                int productId = Convert.ToInt32(e.CommandArgument);

                // Redirect to the ViewFullProduct page with the selected ProductId
                Response.Redirect($"ViewFullProduct.aspx?ProductId={productId}");
            }
        }
    }
}