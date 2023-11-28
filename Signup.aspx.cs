using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Drawing.Printing;

namespace HopeStore
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SignUpButton_Click(object sender, EventArgs e)
        {

            /* using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
             {
                 con.Open();
                 SqlCommand cmd = new SqlCommand("Insert into tbUsers(fullname,password,email,DoB,Address,Contact,usertype) Values('" + Fullname + "', '" + Password + "', '" + Email + "', '" + DoB + "', '" + Address + "', '" + Contact + "','User')", con);
                 cmd.ExecuteNonQuery();
                 Response.Write("<script> alert('Registration Successfully done..'); </script>");
                 // Clr();
                 con.Close();
                 //lblMsg = "Registration Successfully done..";
                 // lblMsg.ForeColor = System.Drawing.Color.Green;
             }*/
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hopedb"].ConnectionString))
            {
                con.Open();

                string query = "INSERT INTO tbUsers (fullname, password, email, DoB, Address, Contact, usertype) VALUES " +
                               "(@Fullname, @Password, @Email, @DoB, @Address, @Contact, 'User')";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    //  Fullname, Password, Email, DoB, Address, and Contact are HTML input controls
                    //  need to retrieve their values and convert them to appropriate types

                    cmd.Parameters.AddWithValue("@Fullname", Fullname.Value);  //  Fullname is an HtmlInputText
                    cmd.Parameters.AddWithValue("@Password", Password.Value);  //  Password is an HtmlInputText
                    cmd.Parameters.AddWithValue("@Email", Email.Value);        //  Email is an HtmlInputText

                    //  DoB is an HtmlInputText,   need to parse the value to a DateTime
                    if (DateTime.TryParse(DoB.Value, out DateTime dob))
                    {
                        cmd.Parameters.AddWithValue("@DoB", dob);
                    }
                    else
                    {
                        // Handle invalid date format
                        Response.Write("Invalid date format for Date of Birth. Please enter a valid date.");
                        return;
                        
                    }

                    cmd.Parameters.AddWithValue("@Address", Address.Value);    //  Address is an HtmlInputText
                    cmd.Parameters.AddWithValue("@Contact", Contact.Value);    //  Contact is an HtmlInputText

                    cmd.ExecuteNonQuery();
                }
            }


            Response.Redirect("login.aspx");

        }

      
    }
}
