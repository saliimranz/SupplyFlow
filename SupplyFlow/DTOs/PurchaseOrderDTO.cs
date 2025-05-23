using System;

public class PurchaseOrderDTO
{
    public int PO_ID { get; set; }
    public string PO_Number { get; set; }
    public int Supplier_ID { get; set; }
    public string Supplier_Name { get; set; }
    public DateTime PO_Date { get; set; }
    public string Status { get; set; }
    public DateTime Delivery_Date { get; set; }
    public string Created_By { get; set; }
    public decimal Total_Amount { get; set; }
    public string Currency { get; set; }
    public string Payment_Terms { get; set; }
    public string Remarks { get; set; }
}

public class PurchaseOrderItemDTO
{
    public int Item_ID { get; set; }
    public string Product_Code { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Unit_Price { get; set; }
    public decimal Tax_Rate { get; set; }
    public decimal Total_Amount { get; set; }
}