<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSupplier.aspx.cs" Inherits="SupplyFlow.Views.ViewSupplier" %>
<%@ Register Src="~/Controls/UserStatus.ascx" TagName="UserStatus" TagPrefix="uc" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>View Supplier</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>View Supplier Details</h2>
        <div style="display: flex; justify-content: flex-end; margin-bottom: 15px;">
            <uc:UserStatus runat="server" id="UserStatusControl" />
        </div>

        <div style="margin-bottom: 20px;">
            <asp:TextBox ID="txtSupplierID" runat="server" Width="200px" placeholder="Enter Supplier ID"></asp:TextBox>
            <asp:Button ID="btnFetch" runat="server" Text="Submit" OnClick="btnFetch_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
            <h3>Supplier Information</h3>
            <asp:Label ID="lblCompanyName" runat="server" /><br />
            <asp:Label ID="lblContactPerson" runat="server" /><br />
            <asp:Label ID="lblPhone" runat="server" /><br />
            <asp:Label ID="lblEmail" runat="server" /><br />
            <asp:Label ID="lblVAT" runat="server" /><br />
            <asp:Label ID="lblAddress" runat="server" /><br />

            <h3>Purchase Orders</h3>
            <asp:GridView ID="gvPurchaseOrders" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="PO_ID" HeaderText="PO ID" />
                    <asp:BoundField DataField="PO_Number" HeaderText="PO Number" />
                    <asp:BoundField DataField="PO_Date" HeaderText="PO Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Created_By" HeaderText="Created By" />
                    <asp:BoundField DataField="Total_Amount" HeaderText="Total Amount" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                    <asp:BoundField DataField="Payment_Terms" HeaderText ="Payment Terms" />
                    <asp:BoundField DataField="Remarks" HeaderText ="Remarks" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </form>
</body>
</html>
