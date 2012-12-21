<%@ Page Language="C#" AutoEventWireup="true" Inherits="Smsmarketing_Test" Codebehind="Test.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 516px">
            <tr>
                <td style="width: 72px">
                    短信内容</td>
                <td style="width: 312px">
                    <asp:TextBox ID="txtContent" runat="server" Width="300px"></asp:TextBox></td>
                <td>
        <asp:Button ID="btnTest" runat="server" Text="短信营销测试" OnClick="btnTest_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
