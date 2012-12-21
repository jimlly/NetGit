<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhraseSelect.aspx.cs" Inherits="WebApp.Settings.PhraseSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>插入常用短语</title>
    <base target="_self">
    <style>
        .STYLE10 {
	font-size: 12px;
	color: #000000;
	background-color: #F0F7FF;
	border: 1px solid #637EAB;
}
    </style>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script src="../JS/ymPrompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../JS/ymPrompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ok() {
            var inputs = document.getElementsByTagName("input");
            var s = "";
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i].checked == true) {
                        s=inputs[i + 1].value;
                        break;
                    }
                }
            }
            window.parent.ymPrompt.doHandler(s, true);
        }
        function closeWin() {
            window.parent.ymPrompt.doHandler('', true);
        }
    </script>
</head>
<body style="overflow: hidden; background-color: #F6FBFF;">
    <form id="form1" runat="server">
    <div align="center" valign="middle" style="height: 220px; position: relative;">
        <table style="width: 359px; margin-top: 10px;" cellpadding="0" cellspacing="0" class="STYLE10">
            
            <asp:Repeater ID="rpPhrase" runat="server">            
                <ItemTemplate>
                    <tr align="left">
                        <td align="center">
                            <input id='<%#Eval("Id") %>' type="radio" name="phrase" />
                            <input type="hidden" value='<%#Eval("Phrase") %>' />
                        </td>
                        <td style="width: 300px; ">
                            <span title='<%#Eval("Phrase").ToString() %>'><%#Eval("Phrase").ToString().Length > 23 ? Eval("Phrase").ToString().Substring(0, 23) + "..." : Eval("Phrase").ToString()%></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>   
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="labEmpty" runat="server" Text="您没有设置短语" Visible="<%#rpPhrase.Items.Count==0 %>"></asp:Label>
                        </td>
                    </tr>   
            </FooterTemplate>
            </asp:Repeater>
        </table>
        <div style="position: absolute; bottom: 0px; right: 0px;">
            <%--<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />--%>
            <input id="btnOK" type="button" value="确定" onclick="ok()" />
            &nbsp;
            <input id="btnCancel" type="button" value="取消" onclick="closeWin()" />
        </div>
    </div>
    </form>
</body>
</html>
