using SupplyFlow.Context;
using SupplyFlow.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplyFlow.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string password = "123";
            string hashedPassword = PasswordHelper.HashPassword(password);
            System.Diagnostics.Debug.WriteLine("Hashed Password: "+hashedPassword);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            using (var db = new MyDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null && PasswordHelper.VerifyPassword(password, user.Password_Hash))
                {
                    Session["User"] = user; // Optionally store user.Role etc. if needed
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                }
            }
        }
    }
}