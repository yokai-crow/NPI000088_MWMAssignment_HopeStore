using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace HopeStore.User
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                // Check if the user is authenticated
                if (Session["CustomerUser"] != null)
                {
                    // Retrieve the user's email from the session
                    string userEmail = Session["CustomerUser"].ToString();

                    // Load and display user profile data based on the email
                    LoadUserProfile(userEmail);
                }
                else
                {
                    // Redirect the user to the login page if not authenticated
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        private void LoadUserProfile(string userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM tbUsers WHERE email = @Email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Display user profile data 
                        lblUserId.Text = reader["user_id"].ToString();
                        lblUserName.Text = reader["fullname"].ToString();
                        lblUserEmail.Text = reader["email"].ToString();
                        lblUserType.Text = reader["usertype"].ToString();
                        lblUserDOB.Text = reader["DoB"].ToString();
                        lblUserAddress.Text = reader["Address"].ToString();
                        lblUserContact.Text = reader["Contact"].ToString();

                       
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error) later
            }
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/EditProfile.aspx");
        }

        protected void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            DeleteUserProfile();
        }

        private void DeleteUserProfile()
        {
            string userEmail = Session["CustomerUser"] as string;

            if (!string.IsNullOrEmpty(userEmail))
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["hopedb"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "DELETE FROM tbUsers WHERE email = @UserEmail";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    cmd.ExecuteNonQuery();

                    // Clear the session and redirect to a page after deletion
                  //  Session["IsAuthenticated"] = null;
                    Session.Clear();
                    Response.Redirect("~/login.aspx");
                }
            }
        }

    }
}
