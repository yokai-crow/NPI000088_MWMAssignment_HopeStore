using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore.User
{
    public partial class UserProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFeaturedProducts();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindProducts(txtSearch.Text);
        }

        private void BindProducts(string searchKeyword)
        {
            List<ProductItem> products = SearchProductsFromDatabase(searchKeyword);
            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        private List<ProductItem> SearchProductsFromDatabase(string searchKeyword)
        {
            List<ProductItem> products = new List<ProductItem>();

            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT Product_Id, Name, Price, Image FROM Products";

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    query += " WHERE Name LIKE @SearchKeyword";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter if search keyword is provided
                    if (!string.IsNullOrEmpty(searchKeyword))
                    {
                        cmd.Parameters.AddWithValue("@SearchKeyword", $"%{searchKeyword}%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductItem product = new ProductItem
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
            }

            return products;
        }

        private void BindFeaturedProducts()
        {
            List<ProductItem> featuredProducts = GetFeaturedProductsFromDatabase();
            rptProducts.DataSource = featuredProducts;
            rptProducts.DataBind();
        }

        public class ProductItem
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string ImagePath { get; set; }
            public string ProductUrl { get; set; }
        }

        private List<ProductItem> GetFeaturedProductsFromDatabase()
        {
            List<ProductItem> products = new List<ProductItem>();

            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Use ORDER BY NEWID() to get random order
                string query = "SELECT TOP 6 Product_Id, Name, Price, Image FROM Products ORDER BY NEWID()";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductItem product = new ProductItem
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
