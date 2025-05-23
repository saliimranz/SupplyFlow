using SupplyFlow.Context;
using SupplyFlow.DTOs;
using SupplyFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PurchaseOrderService
{
    private readonly MyDbContext _context;
    public PurchaseOrderService(MyDbContext context)
    {
        _context = context;
    }

    public PurchaseOrderDTO GetPurchaseOrderById(int poId)
    {
        var po = _context.PurchaseOrders
            .Include("Supplier")
            .FirstOrDefault(p => p.PO_ID == poId);

        if (po == null) return null;

        return new PurchaseOrderDTO
        {
            PO_ID = po.PO_ID,
            PO_Number = po.PO_Number,
            Supplier_ID = po.Supplier.Supplier_ID,
            Supplier_Name = po.Supplier.Company_Name,
            PO_Date = po.PO_Date,
            Status = po.Status,
            Delivery_Date = po.Delivery_Date,
            Created_By = po.Created_By,
            Total_Amount = po.Total_Amount,
            Currency = po.Currency,
            Payment_Terms = po.Payment_Terms,
            Remarks = po.Remarks
        };
    }

    public bool UpdatePOStatus(int poId, string newStatus, User user)
    {
        if (user == null || !user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            return false; // Not authorized

        var po = _context.PurchaseOrders.FirstOrDefault(p => p.PO_ID == poId);
        if (po != null && (po.Status == "Draft" || po.Status == "Approved"))
        {
            string before = $"Status: {po.Status}";
            po.Status = newStatus;
            _context.SaveChanges();

            string after = $"Status: {po.Status}";
            var action = new PO_Actions
            {
                User_ID = user.User_ID,
                PO_ID = poId,
                Action_Type = "EDIT",
                Action_Timestamp = DateTime.Now,
                Before_Edit = before,
                After_Edit = after
            };

            _context.PO_Actions.Add(action);
            _context.SaveChanges();
            return true;
        }
        return false;
    }


    public List<PurchaseOrderItemDTO> GetPurchaseOrderItems(int poId)
    {
        return _context.PurchaseOrderItems
            .Where(i => i.PO_ID == poId)
            .Select(i => new PurchaseOrderItemDTO
            {
                Item_ID = i.Item_ID,
                Product_Code = i.Product_Code,
                Description = i.Description,
                Quantity = i.Quantity,
                Unit_Price = i.Unit_Price,
                Tax_Rate = i.Tax_Rate,
                Total_Amount = i.Total_Amount
            }).ToList();
    }
    public async Task<PurchaseOrderResult> CreatePurchaseOrderAsync(PurchaseOrderCreateDto dto, User user)
    {
        var result = new PurchaseOrderResult();
        if (!user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
        {
            result.Success = false;
            result.ErrorMessage = "Only Admin can create Purchase Order";
            return result;
        } // Not admin, no update
        
        
        // === VALIDATION ===
        if (dto.Supplier_ID <= 0)
        {
            result.Success = false;
            result.ErrorMessage = "Supplier is required.";
            return result;
        }

        if (dto.Delivery_Date < dto.PO_Date)
        {
            result.Success = false;
            result.ErrorMessage = "Delivery date must be on or after PO date.";
            return result;
        }

        if (dto.Items == null || dto.Items.Count == 0)
        {
            result.Success = false;
            result.ErrorMessage = "At least one item is required.";
            return result;
        }

        foreach (var item in dto.Items)
        {
            if (item.Quantity <= 0)
            {
                result.Success = false;
                result.ErrorMessage = $"Quantity must be positive for item {item.Product_Code}.";
                return result;
            }
            if (item.Unit_Price < 0)
            {
                result.Success = false;
                result.ErrorMessage = $"Unit price cannot be negative for item {item.Product_Code}.";
                return result;
            }
            if (item.Tax_Rate < 0)
            {
                result.Success = false;
                result.ErrorMessage = $"Tax rate cannot be negative for item {item.Product_Code}.";
                return result;
            }

            if (item.Tax_Rate > 0 && dto.Currency == "AED" && item.Tax_Rate != 5)
            {
                result.Success = false;
                result.ErrorMessage = $"Tax rate for UAE (AED) must be exactly 5% VAT. Invalid rate for {item.Product_Code}.";
                return result;
            }
        }

        // === CALCULATE TOTAL ===
        decimal totalAmount = 0;
        var poItems = new List<PurchaseOrderItems>();

        foreach (var item in dto.Items)
        {
            var itemMaster = _context.ItemMaster.FirstOrDefault(im => im.Item_Id == item.ItemMasterId);
            if (itemMaster == null)
            {
                result.Success = false;
                result.ErrorMessage = $"Item with ID {item.ItemMasterId} not found in Item Master.";
                return result;
            }
            var lineTotal = item.Quantity * item.Unit_Price;
            var taxAmount = lineTotal * (item.Tax_Rate / 100);
            totalAmount += lineTotal + taxAmount;

            poItems.Add(new PurchaseOrderItems
            {
                Product_Code = item.Product_Code,
                Description = itemMaster.Item_Description,
                Quantity = item.Quantity,
                Unit_Price = item.Unit_Price,
                Tax_Rate = item.Tax_Rate,
                Total_Amount = lineTotal + taxAmount,
                ItemMasterId = item.ItemMasterId
            });
        }

        var purchaseOrder = new PurchaseOrders
        {
            PO_Number = $"PO-{DateTime.Now:yyyyMMddHHmmss}",
            Supplier_ID = dto.Supplier_ID,
            PO_Date = dto.PO_Date,
            Delivery_Date = dto.Delivery_Date,
            Created_By = dto.Created_By,
            Currency = dto.Currency,
            Payment_Terms = dto.Payment_Terms,
            Remarks = dto.Remarks,
            Status = "Draft",
            Total_Amount = totalAmount,
            Items = new List<PurchaseOrderItems>()
        };

        // Add to context
        _context.PurchaseOrders.Add(purchaseOrder);

        foreach (var item in poItems)
        {
            purchaseOrder.Items.Add(item); 
        }

       // === SAVE TO DATABASE ===
        await _context.SaveChangesAsync();

        // === LOG ACTION ===
        var afterData = new
        {
            purchaseOrder.PO_Number,
            purchaseOrder.Supplier_ID,
            purchaseOrder.PO_Date,
            purchaseOrder.Delivery_Date,
            purchaseOrder.Created_By,
            purchaseOrder.Currency,
            purchaseOrder.Payment_Terms,
            purchaseOrder.Remarks,
            purchaseOrder.Status,
            purchaseOrder.Total_Amount,
            Items = poItems.Select(i => new
            {
                i.Product_Code,
                i.Description,
                i.Quantity,
                i.Unit_Price,
                i.Tax_Rate,
                i.Total_Amount,
                i.ItemMasterId
            })
        };

        var action = new PO_Actions
        {
            User_ID = user.User_ID,
            PO_ID = purchaseOrder.PO_ID,
            Action_Type = "CREATE",
            Action_Timestamp = DateTime.Now,
            Before_Edit = null,
            After_Edit = Newtonsoft.Json.JsonConvert.SerializeObject(afterData)
        };

        _context.PO_Actions.Add(action);
        await _context.SaveChangesAsync();

        result.Success = true;
        result.PO_ID = purchaseOrder.PO_ID;
        return result;
    }
}
