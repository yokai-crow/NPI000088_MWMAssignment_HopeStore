using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore.Admin
{
    public partial class Addproducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Product_Click(object sender, EventArgs e)
        {
            /*  using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
              {
                  con.Open();
                  SqlCommand cmd = new SqlCommand("Insert into Products(Name,Category,Price,Image,Quantity,Description) Values('" + Pd_Name + "', '" + Pd_Category + "', '" + Pd_Price + "', '" + Pd_Image + "', '" + Pd_Quantity + "', '" + Pd_Description + "')", con);
                  cmd.ExecuteNonQuery();
                  Response.Write("<script> alert('Registration Successfully done..'); </script>");
                  // Clr();
                  con.Close();
                  //lblMsg = "Registration Successfully done..";
                  // lblMsg.ForeColor = System.Drawing.Color.Green;
              }
              Response.Redirect("Addproducts.aspx");*/
            

            bool registrationSuccess = false; // Default value for failure
            try
            {
                // Get the file from the input control
                HttpPostedFile postedFile = Pd_Image.PostedFile;

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    // Convert the file to a byte array
                    byte[] imageBytes = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(imageBytes, 0, postedFile.ContentLength);

                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                    {
                        con.Open();

                        string query = "INSERT INTO Products (Name, Category, Price, Image, Quantity, Description) VALUES (@Name, @Category, @Price, @Image, @Quantity, @Description)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            
                            cmd.Parameters.AddWithValue("@Name", Pd_Name.Value);
                            cmd.Parameters.AddWithValue("@Category", Pd_Category.Value);
                            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(Pd_Price.Value));
                            cmd.Parameters.AddWithValue("@Image", imageBytes);
                            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(Pd_Quantity.Value));
                            cmd.Parameters.AddWithValue("@Description", Pd_Description.Value);

                            cmd.ExecuteNonQuery();
                        }

                        
                    }
                }
                else
                {
                    // Handle the case where no file is uploaded
                    Response.Write("<script> alert('Please upload an image.'); </script>");
                }
                
                registrationSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                
                registrationSuccess = false;
            }
            finally
            {
                // Display the success message and redirect if there was no exception
                if (registrationSuccess)
                {
                    lblSuccessMessage.Text = "Product Successfully Added.";
                    lblSuccessMessage.Visible = true;

                    // Redirect to another page after a delay 2000=2sec ho
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = 'Addproducts.aspx'; }, 2000);", true);
                }
            }
            
         
        }
    }
}