<%@ Page Language="C#" AutoEventWireup="true" Inherits="Home" Codebehind="Home.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="Images/content.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="JS/ymPrompt/ymPrompt.js"></script>
    <link href="JS/ymPrompt/skin/qq/ymPrompt.css"  rel="stylesheet"  type="text/css" />
    <script type="text/javascript">
        function openAccountList() {
//            window.open('AccountList.aspx', 'newwindow', 'height=400, width=570, top=200, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=yes,location=no, status=no')
            this.ymPrompt.win({ message: 'AccountList.aspx', width: 570, height: 350, title: '金额账户详情', maxBtn: false, minBtn: false, iframe: true, useSlide: false, winAlpha: 1.0 });
        }
    </script>
</head>
<body>
    <div id="maintitle">
        当前帐号信息</div>
    <div id="usermessages">
        <table width="800" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="459" valign="top" style="width: 45%">
                    <table width="409" border="0" cellpadding="0" cellspacing="1" bgcolor="#a4b2bd"
                        id="content">
                        <tr>
                            <td width="108" align="right" bgcolor="#FFFFFF">
                                当前账户：</td>
                            <td width="298" bgcolor="#FFFFFF">
                                &nbsp;
                          <asp:Literal ID="lt_UserName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#FFFFFF">
                                可用余额：</td>
                            <td bgcolor="#FFFFFF">
                                <a title="点击查看金额账户详情" href="#" onclick="openAccountList()" style="text-decoration:none">&nbsp;<asp:Literal ID="lt_Balance" runat="server"></asp:Literal>
                                元&nbsp;&nbsp;</a></td>
                        </tr>
                        <%--<tr>
                            <td align="right" bgcolor="#FFFFFF">
                                发送状态：</td>
                            <td bgcolor="#FFFFFF">
                                <strong>&nbsp;<asp:Literal ID="lt_SendingRows" runat="server"></asp:Literal></strong>&nbsp;&nbsp;条正在发送</td>
                        </tr>--%>
                        <tr>
                            <td align="right" bgcolor="#FFFFFF">
                                资费标准：</td>
                            <td bgcolor="#FFFFFF">
                                &nbsp;0.1元/条</td>
                        </tr>
                  </table>
              </td>
                <td width="341" valign="top" style="width: 55%;">
                    <div class="noteout">
                        <div class="notein">
                            <div class="title">
                                <asp:Literal ID="lt_Title" runat="server">系统公告</asp:Literal></div>
                            <div id="NoticeContent">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="lt_Notice" runat="server"></asp:Literal>
                            </div>
                            <div style="bottom: 0px; text-align: right;">
                                <br />
                        
                                <asp:Literal ID="lt_Footer" runat="server"></asp:Literal></div>
                        </div>
                    </div>
              </td>
            </tr>
      </table>
    </div>
</body>
</html>
