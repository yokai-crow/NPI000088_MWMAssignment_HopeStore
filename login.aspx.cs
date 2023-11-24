using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace HopeStore
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            string email = Email.Value;
            string password = Password.Value;
            bool userTypeIsAdmin = !usertype.Checked;

            if (ValidateUser(email, password, userTypeIsAdmin))
            {
                Session["IsAuthenticated"] = true;
                // User is valid, redirect 
                if (userTypeIsAdmin)
                {
                    // Set session for admin
                    Session["AdminUser"] = email;
                    Response.Redirect("~/Admin/Dashboard.aspx");
                }
                else
                {
                    // Redirect user to another page
                    Session["CustomerUser"] = email;
                    Response.Redirect("~/User/UserDashboard.aspx");
                }
            }
            else
            {
                Session["IsAuthenticated"] = false;
                // Display an error message, invalid credentials
                ErrorMessage.Text = "Invalid credentials. Please try again.";
            }
        }

        private bool ValidateUser(string email, string password, bool userTypeIsAdmin)
        {
            // logic to validate the user from the database
            //  parameters to prevent SQL injection
            string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM tbUsers WHERE email = @Email AND password = @Password AND usertype = @UserType";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@UserType", userTypeIsAdmin ? "Admin" : "User");

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
