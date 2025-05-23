using SupplyFlow.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplyFlow.Views
{
    public partial class ViewItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int itemId;
            if (int.TryParse(txtItemId.Text, out itemId))
            {
                using (var db = new MyDbContext())
                {
                    var item = db.ItemMaster.Find(itemId);
                    if (item != null)
                    {
                        lblItemName.Text = item.Item_Name;
                        lblItemDesc.Text = item.Item_Description;
                        pnlItemDetails.Visible = true;

                        var poItems = db.PurchaseOrderItems
                            .Where(i => i.ItemMasterId == itemId)
                            .Select(i => new
                            {
                                PONumber = i.PurchaseOrder.PO_Number,
                                i.Product_Code,
                                i.Description,
                                i.Quantity,
                                i.Unit_Price,
                                i.Total_Amount
                            }).ToList();

                        gvPOItems.DataSource = poItems;
                        gvPOItems.DataBind();

                        pnlPurchaseOrderItems.Visible = poItems.Any();
                        lblError.Visible = false;
                    }
                    else
                    {
                        lblError.Text = "Item not found.";
                        lblError.Visible = true;
                        pnlItemDetails.Visible = false;
                        pnlPurchaseOrderItems.Visible = false;
                    }
                }
            }
            else
            {
                lblError.Text = "Invalid Item ID.";
                lblError.Visible = true;
                pnlItemDetails.Visible = false;
                pnlPurchaseOrderItems.Visible = false;
            }
        }
    }
}