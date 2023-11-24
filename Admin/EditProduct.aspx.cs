using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace HopeStore.Admin
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load product details for editing
                LoadProductDetails();
            }
        }

        private void LoadProductDetails()
        {
            if (Request.QueryString["ProductId"] != null)
            {
                string productId = Request.QueryString["ProductId"];
                lblProductId.Text = $"Product ID: {productId}";

                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                    {
                        con.Open();

                        string query = "SELECT Name, Category, Price, Quantity, Description, Image FROM Products WHERE Product_Id = @ProductId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Populate textboxes with existing data
                            txtProductName.Text = reader["Name"].ToString();
                            txtProductCategory.Text = reader["Category"].ToString();
                            txtProductPrice.Text = reader["Price"].ToString();
                            txtProductQuantity.Text = reader["Quantity"].ToString();
                            txtProductDescription.Text = reader["Description"].ToString();

                            // Load and display the current image
                            if (reader["Image"] != DBNull.Value)
                            {
                                LoadCurrentImage((byte[])reader["Image"]);
                            }
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                }
            }
        }


        private void LoadCurrentImage(byte[] imageData)
        {
            if (imageData != null)
            {
                // Convert the binary data to a base64-encoded string
                string base64Image = Convert.ToBase64String(imageData);

                // Set the ImageUrl property of the imgCurrentImage control
                imgCurrentImage.ImageUrl = $"data:image/jpeg;base64,{base64Image}";

                // Make the image control visible
                imgCurrentImage.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update product details
            UpdateProductDetails();
        }

        private void UpdateProductDetails()
        {
            byte[] newImageBytes = null;

            if (Request.QueryString["ProductId"] != null)
            {
                string productId = Request.QueryString["ProductId"];

                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                    {
                        con.Open();

                        string query = "UPDATE Products SET Name = @Name, Category = @Category, Price = @Price, Quantity = @Quantity, Description = @Description";

                        // Check if a new image is uploaded
                        byte[] imageData = UploadImage();
                        if (imageData != null)
                        {
                            // If a new image is uploaded, update the image data
                            query += ", Image = @Image";
                            newImageBytes = imageData;
                        }

                        query += " WHERE Product_Id = @ProductId";

                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@Name", txtProductName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", txtProductCategory.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtProductPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtProductQuantity.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Description", txtProductDescription.Text.Trim());

                        // Add the new image parameter if available
                        if (newImageBytes != null)
                        {
                            cmd.Parameters.AddWithValue("@Image", newImageBytes);
                        }

                        cmd.ExecuteNonQuery();

                        // Redirect back to the inventory page after updating
                        Response.Redirect("Inventory.aspx");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                }
            }
        }

        private byte[] UploadImage()
        {
            try
            {
                if (fileProductImage.HasFile)
                {
                    // Convert the file to a byte array
                    byte[] imageBytes = fileProductImage.FileBytes;

                    // Return the byte array to be stored in the database
                    return imageBytes;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                return null;
            }
        }

        private byte[] GetImageBytesFromPath(string imagePath)
        {
            try
            {
                // Convert the file to a byte array
                byte[] imageBytes = File.ReadAllBytes(Server.MapPath(imagePath));
                return imageBytes;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                return null;
            }
        }

        /*private string UploadImage()
        {
            try
            {
                // Specify the path where you want to save the image
                string uploadFolderPath = Server.MapPath("~/Uploads/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileProductImage.FileName);
                string filePath = Path.Combine(uploadFolderPath, fileName);

                // Save the file to the server
                fileProductImage.SaveAs(filePath);

                // Enable the TextBox to display the new image path
                txtNewImage.Text = "~/Uploads/" + fileName;
                txtNewImage.Enabled = true;

                // Return the relative path to be stored in the database
                return "~/Uploads/" + fileName;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                return null;
            }
        }*/
    }
}
