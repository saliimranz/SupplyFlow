using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SupplyFlow.Context;

namespace SupplyFlow.Views
{
    public partial class ViewPO : System.Web.UI.Page
    {
        private readonly PurchaseOrderService _service;

        public ViewPO()
        {
            // Assuming MyDbContext is properly configured and available
            var context = new MyDbContext();
            _service = new PurchaseOrderService(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlDetails.Visible = false;

                // Check if a PO_ID is passed in the query string
                string poIdQuery = Request.QueryString["po_id"];
                if (!string.IsNullOrEmpty(poIdQuery))
                {
                    int poId;
                    if (int.TryParse(poIdQuery, out poId))
                    {
                        txtPOID.Text = poId.ToString();  // Set the textbox (so user can still use it later)
                        FetchAndDisplayPO(poId);         // Custom method for DRY code
                    }
                }
            }
        }

        private void FetchAndDisplayPO(int poId)
        {
            lblMessage.Text = "";

            var po = _service.GetPurchaseOrderById(poId);
            if (po == null)
            {
                lblMessage.Text = $"No Purchase Order found for PO ID {poId}.";
                pnlDetails.Visible = false;
                return;
            }

            LoadPurchaseOrder(po);
            LoadPurchaseOrderItems(poId);
            pnlDetails.Visible = true;
            var user = (SupplyFlow.Models.User)Session["User"];

           pnlUpdateStatus.Visible =
                (po.Status == "Draft" || po.Status == "Approved") &&
                user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);
        }

        protected void btnFetch_Click(object sender, EventArgs e)
        {
            int poId;
            if (!int.TryParse(txtPOID.Text.Trim(), out poId))
            {
                lblMessage.Text = "Please enter a valid PO ID.";
                pnlDetails.Visible = false;
                return;
            }

            FetchAndDisplayPO(poId);
        }

        private void LoadPurchaseOrder(PurchaseOrderDTO po) // Fix: Update parameter type to match the expected type
        {
            if (po != null)
            {
                lblPONumber.Text = "PO Number: " + po.PO_Number;
                lnkSupplier.Text = po.Supplier_Name;
                lnkSupplier.NavigateUrl = "~/Views/ViewSupplier.aspx?supplierId=" + po.Supplier_ID;
                lblDate.Text = "Date: " + po.PO_Date.ToString("dd MMM yyyy");
                lblStatus.Text = "Status: " + po.Status;
                lblTotalAmount.Text = "Total: " + po.Total_Amount.ToString("C2");
                lblCurrency.Text = "Currency: " + po.Currency;
                lblRemarks.Text = "Remarks: " + po.Remarks;
            }
            else
            {
                Response.Write("Purchase Order not found.");
            }
        }

        private void LoadPurchaseOrderItems(int poId)
        {
            var items = _service.GetPurchaseOrderItems(poId);
            gvItems.DataSource = items;
            gvItems.DataBind();
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            // Validate dropdown selection
            if (string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                lblStatusMessage.ForeColor = System.Drawing.Color.Red;
                lblStatusMessage.Text = "Please select a valid status before updating.";
                return; // Stop further execution
            }

            int poId = int.Parse(txtPOID.Text);
            string newStatus = ddlStatus.SelectedValue;
            var user = (SupplyFlow.Models.User)Session["User"];
            var success = _service.UpdatePOStatus(poId, newStatus, user);
            if (success)
            {
                lblStatusMessage.ForeColor = System.Drawing.Color.Green;
                lblStatusMessage.Text = "Status updated successfully.";
                // Refresh PO view
                btnFetch_Click(null, null);
            }
            else
            {
                lblStatusMessage.ForeColor = System.Drawing.Color.Red;
                lblStatusMessage.Text = "Failed to update status.";
            }
        }

    }
}