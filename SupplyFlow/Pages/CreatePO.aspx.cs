using SupplyFlow.Context;
using SupplyFlow.DTOs;
using SupplyFlow.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplyFlow.Pages
{
    public partial class CreatePO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Debug.WriteLine("[Page_Load] Initializing empty grid and binding dropdowns.");
                BindEmptyGrid();
                BindSupplierDropdown();
            }
        }

        private void BindSupplierDropdown()
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var suppliers = db.Suppliers.ToList();
                    ddlSuppliers.DataSource = suppliers;
                    ddlSuppliers.DataTextField = "Company_Name";
                    ddlSuppliers.DataValueField = "Supplier_ID";
                    ddlSuppliers.DataBind();

                    ddlSuppliers.Items.Insert(0, new ListItem("-- Select Supplier --", ""));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[BindSupplierDropdown] Error: {ex.Message}");
                }
            }
        }

        private void BindEmptyGrid()
        {
            var items = new List<PurchaseOrderItemDto>() { new PurchaseOrderItemDto() };
            gvItems.DataSource = items;
            gvItems.DataBind();

            if (gvItems.FooterRow != null)
            {
                var ddl = gvItems.FooterRow.FindControl("ddlNewItemMaster") as DropDownList;
                if (ddl != null)
                    BindItemDropdown(ddl);
                else
                    Debug.WriteLine("[BindEmptyGrid] ddlNewItemMaster not found in footer.");
            }
            else
            {
                Debug.WriteLine("[BindEmptyGrid] FooterRow is null. Ensure the GridView renders the footer.");
            }
        }

        private void BindItemDropdown(DropDownList ddl)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var itemList = db.ItemMaster.ToList();
                    Debug.WriteLine($"[BindItemDropdown] Fetched {itemList.Count} items from ItemMaster table.");

                    ddl.DataSource = itemList;
                    ddl.DataTextField = "Item_Name";
                    ddl.DataValueField = "Item_Id";
                    ddl.DataBind();

                    ddl.Items.Insert(0, new ListItem("-- Select --", ""));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[BindItemDropdown] Error fetching items: {ex.Message}");
                }
            }
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddItem")
            {
                var items = Session["POItems"] as List<PurchaseOrderItemDto> ?? new List<PurchaseOrderItemDto>();

                try
                {
                    DropDownList ddl = gvItems.FooterRow.FindControl("ddlNewItemMaster") as DropDownList;
                    TextBox txtQty = gvItems.FooterRow.FindControl("txtNewQuantity") as TextBox;
                    TextBox txtUnitPrice = gvItems.FooterRow.FindControl("txtNewUnitPrice") as TextBox;
                    TextBox txtTaxRate = gvItems.FooterRow.FindControl("txtNewTaxRate") as TextBox;

                    if (ddl != null && !string.IsNullOrEmpty(ddl.SelectedValue))
                    {
                        var item = new PurchaseOrderItemDto
                        {
                            ItemMasterId = int.Parse(ddl.SelectedValue),
                            Quantity = int.Parse(txtQty.Text),
                            Unit_Price = decimal.Parse(txtUnitPrice.Text),
                            Tax_Rate = decimal.Parse(txtTaxRate.Text)
                        };

                        items.Add(item);
                        Debug.WriteLine($"[gvItems_RowCommand] Added item: ItemMasterId={item.ItemMasterId}, Qty={item.Quantity}");
                    }
                    else
                    {
                        Debug.WriteLine("[gvItems_RowCommand] No item selected.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[gvItems_RowCommand] Error: {ex.Message}");
                }

                Session["POItems"] = items;
                gvItems.DataSource = items;
                gvItems.DataBind();

                if (gvItems.FooterRow != null)
                {
                    DropDownList ddl = gvItems.FooterRow.FindControl("ddlNewItemMaster") as DropDownList;
                    if (ddl != null)
                    {
                        BindItemDropdown(ddl);
                        Debug.WriteLine("[gvItems_RowCommand] Footer dropdown rebound.");
                    }
                    else
                    {
                        Debug.WriteLine("[gvItems_RowCommand] Footer dropdown not found.");
                    }
                }
                else
                {
                    Debug.WriteLine("[gvItems_RowCommand] FooterRow is null after DataBind.");
                }
            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Rebind dropdown inside each row if needed (optional)
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
            {
                var ddl = e.Row.FindControl("ddlItemMaster") as DropDownList;
                if (ddl != null && ddl.Items.Count == 0)
                {
                    BindItemDropdown(ddl);
                }
            }
        }

        protected string GetItemName(int itemId)
        {
            using (var db = new MyDbContext())
            {
                var item = db.ItemMaster.FirstOrDefault(i => i.Item_Id == itemId);
                return item != null ? item.Item_Name : "--None--";
            }
        }

        private void ClearForm()
        {
            // Clear dropdowns
            ddlSuppliers.ClearSelection();
            ddlCurrency.ClearSelection();

            // Clear textboxes
            txtPODate.Text = string.Empty;
            txtDeliveryDate.Text = string.Empty;
            txtCreatedBy.Text = string.Empty;
            txtPaymentTerms.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            // Clear session items
            Session["POItems"] = null;

            // Rebind empty grid to clear item rows
            BindEmptyGrid();

            // Hide any error label
            lblError.Visible = false;
            lblError.Text = string.Empty;
        }


        protected async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var items = Session["POItems"] as List<PurchaseOrderItemDto> ?? new List<PurchaseOrderItemDto>();
                Debug.WriteLine($"[btnCreate_Click] Creating PO with {items.Count} items.");

                var dto = new PurchaseOrderCreateDto
                {
                    Supplier_ID = int.Parse(ddlSuppliers.SelectedValue),
                    PO_Date = DateTime.Parse(txtPODate.Text),
                    Delivery_Date = DateTime.Parse(txtDeliveryDate.Text),
                    Created_By = txtCreatedBy.Text,
                    Currency = ddlCurrency.SelectedValue,
                    Payment_Terms = txtPaymentTerms.Text,
                    Remarks = txtRemarks.Text,
                    Items = items
                };

                var user = (User)Session["User"];

                var service = new PurchaseOrderService(new MyDbContext());
                var result = await service.CreatePurchaseOrderAsync(dto, user);

                if (result.Success)
                {
                    Response.Write("<script>alert('Purchase Order created successfully!')</script>");
                    Session["POItems"] = null;
                    ClearForm();
                }
                else
                {
                    lblError.Text = result.ErrorMessage;
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Unexpected error: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}