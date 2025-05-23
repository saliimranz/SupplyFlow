<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewItem.aspx.cs" Inherits="SupplyFlow.Views.ViewItem" %>
<%@ Register Src="~/Controls/UserStatus.ascx" TagName="UserStatus" TagPrefix="uc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Item Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Search Item by ID</h2>
            <asp:TextBox ID="txtItemId" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

            <div style="display: flex; justify-content: flex-end; margin-bottom: 15px;">
                <uc:UserStatus runat="server" id="UserStatusControl" />
            </div>

            <asp:Panel ID="pnlItemDetails" runat="server" Visible="false">
                <h2>Item Details</h2>
                <asp:Label ID="lblItemNameLabel" runat="server" Text="Item Name: " />
                <asp:Label ID="lblItemName" runat="server" /><br />
                <asp:Label ID="lblItemDescLabel" runat="server" Text="Item Description: " />
                <asp:Label ID="lblItemDesc" runat="server" /><br />
            </asp:Panel>

            <asp:Panel ID="pnlPurchaseOrderItems" runat="server" Visible="false">
                <h3>Used in Purchase Orders</h3>
                <asp:GridView ID="gvPOItems" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="PO Number" DataField="PONumber" />
                        <asp:BoundField HeaderText="Product Code" DataField="Product_Code" />
                        <asp:BoundField HeaderText="Description" DataField="Description" />
                        <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                        <asp:BoundField HeaderText="Unit Price" DataField="Unit_Price" DataFormatString="{0:C}" />
                        <asp:BoundField HeaderText="Total Amount" DataField="Total_Amount" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" />
        </div>
    </form>
</body>
</html>
