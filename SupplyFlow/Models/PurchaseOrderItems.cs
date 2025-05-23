using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SupplyFlow.Models
{
    public class PurchaseOrderItems
    {
        [Key]
        public int Item_ID { get; set; }

        public int PO_ID { get; set; }
        public int? ItemMasterId { get; set; }

        [MaxLength(50)]
        public string Product_Code { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Unit_Price { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Tax_Rate { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Total_Amount { get; set; }

        // Navigation

        [ForeignKey("PO_ID")]
        public virtual PurchaseOrders PurchaseOrder { get; set; }

        [ForeignKey("ItemMasterId")]
        public virtual ItemMaster Item { get; set; }
    }

}