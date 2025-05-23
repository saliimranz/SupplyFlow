using SupplyFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplyFlow.Controls
{
    public partial class UserStatus : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                // Fix: Use InnerText property for HtmlGenericControl
                lblUserEmail.InnerText = Server.HtmlEncode(user.Email);
            }
            else
            {
                // Optional: Redirect to login if no user in session
                Response.Redirect("/Pages/Login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Pages/Login.aspx");
        }
    }
}