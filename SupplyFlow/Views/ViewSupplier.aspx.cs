using System;
using System.Linq;
using SupplyFlow.DTOs;
using SupplyFlow.Context;

namespace SupplyFlow.Views
{
    public partial class ViewSupplier : System.Web.UI.Page
    {
        private readonly SupplierService _supplierService;

        public ViewSupplier()
        {
            var dbContext = new MyDbContext(); // Create an instance of MyDbContext  
            _supplierService = new SupplierService(dbContext); // Pass it to the SupplierService constructor  
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlDetails.Visible = false;
                if (Request.QueryString["supplierId"] != null)
                {
                    int supplierId;
                    if (int.TryParse(Request.QueryString["supplierId"], out supplierId))
                    {
                        txtSupplierID.Text = supplierId.ToString();
                        FetchSupplier(supplierId);
                    }
                }
            }
        }

        protected void btnFetch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            int supplierId;
            if (!int.TryParse(txtSupplierID.Text.Trim(), out supplierId))
            {
                lblMessage.Text = "Please enter a valid Supplier ID.";
                pnlDetails.Visible = false;
                return;
            }

            FetchSupplier(supplierId);
        }

        private void FetchSupplier(int supplierId)
        {
            var supplier = _supplierService.GetSupplierById(supplierId);
            if (supplier == null)
            {
                lblMessage.Text = $"No supplier found for ID {supplierId}.";
                pnlDetails.Visible = false;
                return;
            }

            LoadSupplierInfo(supplier);
            LoadSupplierPurchaseOrders(supplierId);
            pnlDetails.Visible = true;
        }

        private void LoadSupplierInfo(SupplierDTO s)
        {
            lblCompanyName.Text = "Company Name: " + s.Company_Name;
            lblContactPerson.Text = "Contact Person: " + s.Contact_Person;
            lblPhone.Text = "Phone: " + s.Phone;
            lblEmail.Text = "Email: " + s.Email;
            lblVAT.Text = "VAT Number: " + s.VAT_Number;
            lblAddress.Text = "Address: " + s.Address;
        }

        private void LoadSupplierPurchaseOrders(int supplierId)
        {
            var poList = _supplierService.GetSupplierPurchaseOrders(supplierId);
            gvPurchaseOrders.DataSource = poList;
            gvPurchaseOrders.DataBind();
        }
    }
}
