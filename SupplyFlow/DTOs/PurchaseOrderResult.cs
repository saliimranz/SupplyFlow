using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyFlow.DTOs
{
    public class PurchaseOrderResult
    {
        public bool Success { get; set; }
        public int PO_ID { get; set; }
        public string ErrorMessage { get; set; }
    }
}