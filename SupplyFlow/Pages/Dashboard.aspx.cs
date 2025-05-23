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
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                // Get user object from session
                var user = (SupplyFlow.Models.User)Session["User"];

                // Hide Create PO if not admin
                btnCreatePO.Visible = user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);
                LoadDraftPOs();
            }
        }

        private void LoadDraftPOs()
        {
            using (var context = new MyDbContext())
            {
                var draftPOs = context.PurchaseOrders
                    .Where(po => po.Status == "Draft")
                    .Select(po => new
                    {
                        po.PO_ID,
                        po.PO_Number,
                        po.PO_Date,
                        SupplierName = po.Supplier.Company_Name,
                        po.Total_Amount
                    })
                    .ToList();

                gvDraftPOs.DataSource = draftPOs;
                gvDraftPOs.DataBind();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();  // Clear all session data
            Session.Abandon(); // Abandon session
            Response.Redirect("Login.aspx"); // Redirect to login page
        }
    }
}