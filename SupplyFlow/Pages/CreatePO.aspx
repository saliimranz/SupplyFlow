<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePO.aspx.cs" Inherits="SupplyFlow.Pages.CreatePO" Async="true"%>
<%@ Register Src="~/Controls/UserStatus.ascx" TagName="UserStatus" TagPrefix="uc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Purchase Order</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 20px; max-width: 600px;">
            <h2>Create Purchase Order</h2>
            
            <div style="display: flex; justify-content: flex-end; margin-bottom: 15px;">
                <uc:UserStatus runat="server" id="UserStatusControl" />
            </div>

            <div style="min-height: 24px;">
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
            </div>

            <label>Supplier:</label><br />
            <asp:DropDownList ID="ddlSuppliers" runat="server" /><br /><br />

            <label>PO Date:</label><br />
            <asp:TextBox ID="txtPODate" runat="server" TextMode="Date"/><br /><br />

            <label>Delivery Date:</label><br />
            <asp:TextBox ID="txtDeliveryDate" runat="server" TextMode="Date"/><br /><br />

            <label>Created By:</label><br />
            <asp:TextBox ID="txtCreatedBy" runat="server" /><br /><br />

            <table>
                <tr>
                    <td>Currency:</td>
                    <td>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select Currency --" Value="" />
                            <asp:ListItem Text="USD" Value="USD" />
                            <asp:ListItem Text="AED" Value="AED" />
                            <asp:ListItem Text="EUR" Value="EUR" />
                        </asp:DropDownList>
                    </td>
                    <td style="padding-left:20px;">Payment Terms:</td>
                    <td>
                        <asp:TextBox ID="txtPaymentTerms" runat="server" CssClass="form-control" />
                    </td>
                </tr>
            </table>


            <label>Remarks:</label><br />
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" /><br /><br />

            <br /><h3>Items</h3>

            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                OnRowCommand="gvItems_RowCommand" OnRowDataBound="gvItems_RowDataBound">
                <Columns>

                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <%# GetItemName((int)Eval("ItemMasterId")) %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewItemMaster" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewQuantity" runat="server" Width="60px" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("Unit_Price") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewUnitPrice" runat="server" Width="80px" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tax %">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("Tax_Rate") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewTaxRate" runat="server" Width="60px" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnAddItem" runat="server" CommandName="AddItem" Text="Add" />
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Button ID="btnCreate" runat="server" Text="Create PO" OnClick="btnCreate_Click" />
        </div>
    </form>
</body>
</html>
