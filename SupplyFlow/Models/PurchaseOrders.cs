using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SupplyFlow.Models
{
    public class PurchaseOrders
    {
        [Key]
        public int PO_ID { get; set; }

        [MaxLength(50)]
        public string PO_Number { get; set; }

        public int Supplier_ID { get; set; }

        public DateTime PO_Date { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public DateTime Delivery_Date { get; set; }

        [MaxLength(50)]
        public string Created_By { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Total_Amount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        [MaxLength(100)]
        public string Payment_Terms { get; set; }

        public string Remarks { get; set; }

        // Navigation properties
        [ForeignKey("Supplier_ID")]
        public Suppliers Supplier { get; set; }
        public ICollection<PurchaseOrderItems> Items { get; set; }
    }

}