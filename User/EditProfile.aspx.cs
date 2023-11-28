using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace HopeStore.User
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load user's current profile information
                LoadUserProfile();
            }
        }

        private void LoadUserProfile()
        {
            // Retrieve user information from the database using Session["CustomerUser"]
            string userEmail = Session["CustomerUser"] as string;

            if (!string.IsNullOrEmpty(userEmail))
            {
             
                string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT Fullname, Password, Address, Contact FROM tbUsers WHERE email = @UserEmail";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the TextBoxes with the retrieved information
                        txtFullname.Text = reader["Fullname"].ToString();
                        
                        txtAddress.Text = reader["Address"].ToString();
                        txtContact.Text = reader["Contact"].ToString();
                    }

                    reader.Close();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update user's profile information in the database
            UpdateUserProfile();
        }

        private void UpdateUserProfile()
        {
            string userEmail = Session["CustomerUser"] as string;

            if (!string.IsNullOrEmpty(userEmail))
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE tbUsers SET Fullname = @Fullname, Address = @Address, Contact = @Contact WHERE email = @UserEmail";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Fullname", txtFullname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    cmd.ExecuteNonQuery();

                    // Display alert and redirect using client-side script
                    string script = "alert('Profile updated successfully.'); window.location.href='UserProfile.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
        }

    }
}
