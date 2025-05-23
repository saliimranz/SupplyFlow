using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyFlow.DTOs
{
    public class PurchaseOrderItemDto
    {
        public int ItemMasterId { get; set; } // <-- Add this line
        public string Product_Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Tax_Rate { get; set; }
    }

    public class PurchaseOrderCreateDto
    {
        public int Supplier_ID { get; set; }
        public DateTime PO_Date { get; set; }
        public DateTime Delivery_Date { get; set; }
        public string Created_By { get; set; }
        public string Currency { get; set; }
        public string Payment_Terms { get; set; }
        public string Remarks { get; set; }

        public List<PurchaseOrderItemDto> Items { get; set; }
    }
}