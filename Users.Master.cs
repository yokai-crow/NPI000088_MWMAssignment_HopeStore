using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HopeStore
{
    public partial class Users : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            // End the session
            Session.Abandon();
            Session["IsAuthenticated"] = null;
            
            // Redirect to the login page
            Response.Redirect("~/");
        }
    }
}