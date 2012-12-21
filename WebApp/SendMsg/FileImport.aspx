<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMsg_FileImport" Codebehind="FileImport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件导入</title>
    <base target="_self" />
    <script defer="defer" src="../JS/SendSms.js" type="text/javascript" language="javascript"></script>

    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
</head>
<body style="overflow:hidden">
    <form id="form1" runat="server">
    <div align="center" valign="middle">
        <table style="width: 359px; height: 70px;">
            <tr>
                <td style="width: 358px; height: 21px">文件：<asp:FileUpload ID="SrcFile" runat="server" Width="307px" /></td>
            </tr>
            <tr align="left">
                <td style="width: 358px; height: 21px">
                           &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;目前支持的文件类型：<a href="javascript:" onclick="return downTxT();"><font style="color:Red" title="查看范例">TXT</font></a>、<a href="javascript:" onclick="return downCSV();"><font style="color:Red" title="查看范例">CSV</font></a></td>
            </tr>
            <tr align="center">
                <td style="width: 358px; height: 21px">
                    <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <input id="btnCancel" type="button" value="取消" onclick="javascript:window.close()" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
