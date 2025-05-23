<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatus.ascx.cs" Inherits="SupplyFlow.Controls.UserStatus" %>

<style>

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

</style>

<div style="text-align:right; margin-bottom: 10px;">
    <span id="lblUserEmail" runat="server"></span>
    &nbsp; | &nbsp;
    <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click" Class ="logout-button">Logout</asp:LinkButton>
</div>