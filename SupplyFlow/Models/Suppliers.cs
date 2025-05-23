using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SupplyFlow.Models
{
    public class Suppliers
    {
        [Key]
        public int Supplier_ID { get; set; }

        [MaxLength(100)]
        public string Company_Name { get; set; }

        public string Address { get; set; }

        public string Contact_Person { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string VAT_Number { get; set; }

        // Navigation
        public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    }

}