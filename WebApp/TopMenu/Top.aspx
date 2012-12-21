<%@ Page Language="C#" AutoEventWireup="true" Inherits="TopMenu_YXTopTemp" Codebehind="Top.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/base.css" rel="stylesheet" type="text/css"/>
    <link href="../Styles/Default.css" rel="stylesheet" type="text/css"/>

    <script language="javascript" type="text/javascript">
	    function getModule(objvalue)
	    {
	        var url="";
		    if(objvalue=="welcome"){url="Home.aspx";}
		    parent.document.getElementById("main").src = url;
	    }
    </script>
    
    <style type="text/css">
        <!--
        body {
	        margin-left: 0px;
	        margin-top: 0px;
	        margin-right: 0px;
	        margin-bottom: 0px;
        }
        .STYLE1 {font-size: 12px}
        .STYLE2 {
	        color: #be0000;
	        font-size: 12px;
        }
        -->
        .menu {
	        color: #FFFFFF;
        font-size:12px; text-decoration:none; 
        }
        .menu a{
        font-size:12px; text-decoration:none; color:#FFFFFF;
        }
        .menu a:hover{font-size:12px; text-decoration:underline; color:#FFFFFF;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" background="../Images/topbg.gif">
                <table width="600" height="1" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td background="../Images/topbg.gif">
                <table width="600" border="0" cellpadding="0" cellspacing="0" class="menu">
                    <tr>
                        <td width="150">
                            <img src="../Images/logo.jpg" width="150" height="54" /></td>
                        <td width="44">
                            &nbsp;</td>
                        <td width="406">
                            <br />
                            <br />
                            <span style="color: #022490; font-weight: bold; font-size: 12px;">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>用户</span>：您好,欢迎使用短信服务平台 <asp:LinkButton ID="LinkLoginOut"
                                runat="server" OnClick="LinkLoginOut_Click">[安全退出]</asp:LinkButton></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
