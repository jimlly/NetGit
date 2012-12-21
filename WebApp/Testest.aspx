<%@ Page Language="C#" AutoEventWireup="true" Inherits="Test" Codebehind="Testest.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span><strong><em>SMS TEST LOGIN PAGE</em></strong></span>
        <div>
        <asp:TextBox ID="txtSeqno" runat="server"></asp:TextBox>
        <asp:Button ID="btnLogin"
            runat="server" Text="Sign In" onclick="btnLogin_Click" />
        </div>
    </div>
    </form>
</body>
</html>
