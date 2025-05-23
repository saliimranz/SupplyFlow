using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SupplyFlow.Models
{
    public class PO_Actions
    {
        [Key]
        public int Action_ID { get; set; }

        public int User_ID { get; set; }
        public int PO_ID { get; set; }

        [MaxLength(20)]
        public string Action_Type { get; set; } = "EDIT";  // Default value for now

        public DateTime Action_Timestamp { get; set; }

        public string Before_Edit { get; set; }

        public string After_Edit { get; set; }

        // Navigation
        [ForeignKey("PO_ID")]
        public virtual PurchaseOrders PurchaseOrder { get; set; }

        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
    }
}