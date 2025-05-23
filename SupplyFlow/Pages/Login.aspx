<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SupplyFlow.Pages.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f4f4f4;
            text-align: center;
            padding-top: 100px;
        }

        .login-container {
            display: inline-block;
            background-color: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
        }

        input, button {
            display: block;
            width: 250px;
            padding: 10px;
            margin: 10px auto;
            font-size: 16px;
        }

        .error {
            color: red;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" TextMode="Email" />
            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password" />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="error" />
        </div>
    </form>
</body>
</html>
