<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SysManageWeb.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信平台系统--用户登录</title>

	<link rel="stylesheet" type="text/css" href="Styles/login.css" />

    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/png_dd.js" type="text/javascript"></script>
    <script type="text/javascript">
        /*初始加载*/
        $(document).ready(function () {
            var clientheight = window.screen.availHeight;

            var clientwidth = window.document.body.scrollWidth;
            DD_belatedPNG.fix('.png,.png2,.png3,img');
            $("#login_boxer").css("margin-top", (clientheight / 2 - 327 / 2 - 61) + "px");

            $("#img").click(function () {

                getNewCheckCode();

            });
            var UserAccount = $("#txtUserAccount");
            var UserPassword = $("#txtPassword"); ;
            var msg = $("#p_msg");

            UserAccount.focus();

            /*登录按钮单击事件--表单验证*/
            $("#btnLogin").click(function () {
                if (UserAccount.val() == "") {
                    msg.text("用户名不能为空");
                    UserAccount.focus();
                    return false;
                }
                if (UserPassword.val() == "") {
                    msg.text("密码不能为空");
                    UserPassword.focus();
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="login_boxer" class="png">
     <div id="tag_img"><img src="images/login/tag_2.png" alt="" /></div>
    	<div id="logo"><img src="images/login/tag_1.png" alt="" /></div>
        <div id="boxer_from">
        	<span>用户名</span><asp:TextBox ID="txtUserAccount" runat="server" MaxLength="100" CssClass="textbox" Width="140px"
                                        TabIndex="1"></asp:TextBox>
            <div class="split"></div>
            <span>密&nbsp;码</span>&nbsp;<input id="txtPassword"  runat="server" type="password" style="width:140px"  />
            <div class="split"></div>
            <span>验证码</span><input type="text" id="txtCheckCode" name="txtCheckCode" runat="server" style="width:140px"  /> <img alt="看不清，换一张" title="看不清，换一张" id="img"  onclick="javascript:getNewCheckCode()" />
            <div class="split" ></div>
              &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="btnLogin" runat="server"  CssClass="png3" OnClick="btnLogin_Click"
                                        TabIndex="4" Height="26px" Width="132px" />

                                        <div  id="p_msg" runat="server" style="color:Red"></div>
                                    
        </div>
    </div>
    
    <div id="bg_tag" class="png2"></div>
    </form>
    <script language="javascript" type="text/javascript">
        function getNewCheckCode() {
            var num = Math.round((Math.random()) * 100000000);
            $("#img").attr("src", "ValidateCode.aspx?id=" + num);
        }
        getNewCheckCode();
    </script>
</body>
</html>
