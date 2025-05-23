<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPO.aspx.cs" Inherits="SupplyFlow.Views.ViewPO" %>
<%@ Register Src="~/Controls/UserStatus.ascx" TagName="UserStatus" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Purchase Order</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-bottom: 20px;">
            <h2>Enter PO ID</h2>

            <div style="display: flex; justify-content: flex-end; margin-bottom: 15px;">
                <uc:UserStatus runat="server" id="UserStatusControl" />
            </div>
            <asp:TextBox ID="txtPOID" runat="server" Width="200px" placeholder="Enter PO ID"></asp:TextBox>
            <asp:Button ID="btnFetch" runat="server" Text="Submit" OnClick="btnFetch_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
            <h2>Purchase Order Details</h2>
            <asp:Label ID="lblPONumber" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblSupplierLabel" runat="server" Text="Supplier: " />
            <asp:HyperLink ID="lnkSupplier" runat="server" Target="_blank" /><br />
            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label><br />

            <h3>Items</h3>
            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Product_Code" HeaderText="Product Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Unit_Price" HeaderText="Unit Price" />
                    <asp:BoundField DataField="Tax_Rate" HeaderText="Tax Rate" />
                    <asp:BoundField DataField="Total_Amount" HeaderText="Total" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlUpdateStatus" runat="server" Visible="false">
            <h4>Update Status</h4>
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text="-- Select Status --" Value="" Selected="True" />
                <asp:ListItem Text="Approved" Value="Approved" />
                <asp:ListItem Text="Dispatched" Value="Dispatched" />
            </asp:DropDownList>
            <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblStatusMessage" runat="server" ForeColor="Green" />
        </asp:Panel>
    </form>
</body>
</html>
