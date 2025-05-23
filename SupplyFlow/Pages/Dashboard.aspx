<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SupplyFlow.Pages.Dashboard" %>
<%@ Register Src="~/Controls/UserStatus.ascx" TagName="UserStatus" TagPrefix="uc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SupplyFlow Dashboard</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            text-align: center;
            padding: 50px;
            margin: 0;
            position: relative;
        }

        /* Container for the header row: h1 left/center and logout right */
        .header-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            max-width: 800px;
            margin: 0 auto 40px auto;
            padding: 0 10px;
        }

        h1 {
            color: #333;
            margin: 0; /* remove default margin to align nicely */
            flex-grow: 1;
            text-align: center;
        }

        /* Logout button style */
        .logout-button {
            background-color: #dc3545;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            text-decoration: none;
            cursor: pointer;
            white-space: nowrap;
        }
        .logout-button:hover {
            background-color: #a71d2a;
        }

        .dashboard-button {
            display: block;
            width: 250px;
            margin: 10px auto;
            padding: 15px;
            font-size: 16px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            text-decoration: none;
            cursor: pointer;
        }

        .dashboard-button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Header with centered title and logout top-right -->
        <div class="header-container">
            <h1>Welcome to SupplyFlow</h1>

            <uc:UserStatus runat="server" id="UserStatusControl" />
        </div>

        <!-- Other buttons centered below -->
        <a id="btnCreatePO" runat="server" href="CreatePO.aspx" class="dashboard-button">Create Purchase Order</a>
        <a href="../Views/ViewPO.aspx" class="dashboard-button">View Purchase Order</a>
        <a href="../Views/ViewSupplier.aspx" class="dashboard-button">View Supplier</a>
        <a href="../Views/ViewItem.aspx" class="dashboard-button">View Item</a>

        <h3>Open Purchase Orders</h3>
        <div style="display: flex; justify-content: center;">
            <asp:GridView ID="gvDraftPOs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="PO_ID" HeaderText="PO Number" />
                    <asp:BoundField DataField="PO_Number" HeaderText="PO Number" />
                    <asp:BoundField DataField="PO_Date" HeaderText="PO Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                    <asp:BoundField DataField="Total_Amount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <a href='<%# ResolveUrl(string.Format("~/Views/ViewPO.aspx?po_id={0}", Eval("PO_ID"))) %>' class="btn btn-sm btn-primary">View</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
