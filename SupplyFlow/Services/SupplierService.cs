using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupplyFlow.Context;

public class SupplierService
{
    private readonly MyDbContext _context;
    public SupplierService(MyDbContext context)
    {
        _context = context;
    }

    public SupplierDTO GetSupplierById(int supplierId)
    {
        // Replacing 'SupplyDbContext' with 'MyDbContext' to fix the CS0246 error
        using (var db = new MyDbContext())
        {
            var s = db.Suppliers.FirstOrDefault(x => x.Supplier_ID == supplierId);
            if (s == null) return null;

            return new SupplierDTO
            {
                Supplier_ID = s.Supplier_ID,
                Company_Name = s.Company_Name,
                Address = s.Address,
                Contact_Person = s.Contact_Person,
                Phone = s.Phone,
                Email = s.Email,
                VAT_Number = s.VAT_Number
            };
        }
    }

    public List<PurchaseOrderDTO> GetSupplierPurchaseOrders(int supplierId)
    {
        using (var db = new MyDbContext())
        {
            return db.PurchaseOrders
                .Where(po => po.Supplier_ID == supplierId)
                .Select(po => new PurchaseOrderDTO
                {
                    PO_ID = po.PO_ID,
                    PO_Number = po.PO_Number,
                    PO_Date = po.PO_Date,
                    Status = po.Status,
                    Total_Amount = po.Total_Amount,
                    Currency = po.Currency,
                    Delivery_Date = po.Delivery_Date,
                    Created_By = po.Created_By,
                    Payment_Terms = po.Payment_Terms,
                    Remarks = po.Remarks
                })
                .ToList();
        }
    }
}