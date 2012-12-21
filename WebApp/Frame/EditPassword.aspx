<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPassword.aspx.cs" Inherits="Yuantel.YtServerLogin.frame.EditPassword"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <link href="../Styles/master.css" rel="stylesheet" type="text/css" />
    <script src="../js/PasswordLength.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/ymPrompt/ymPrompt_Ex.js"></script>
    <script language="javascript" type="text/javascript">

        function ModifyPassword() {

            if ((document.getElementById("txtPassword").value) == "") {
                alert("密码不能为空！");

                return false;
            }
            if (document.getElementById("txtPassword").value.length < 8) {
                alert("密码和确认密码长度必须是8-20位！");
                return false;
            }
            if (document.getElementById("txtPassword").value.length > 20) {
                alert("密码和确认密码长度必须是8-20位！");
                return false;
            }
            if ((document.getElementById("txtPassword").value) != (document.getElementById("txtConfirmPassword").value)) {
                alert("密码不一致,请重新输入！");

                return false;
            }
            if (!CheckPassword(document.getElementById("txtPassword").value)) {
                alert("密码和确认密码必须是数字和字符的组合！");
                return false;
            }
            return true;
        }
        function CheckPassword(password) {
            var OnlyNum = /^[\d]+$/;                //纯数字
            var count = password.length;
            var numCount = 0;
            var charCount = 0;
            for (i = 0; i < count; i++) {
                if (OnlyNum.test(password.charAt(i))) {
                    numCount = numCount + 1;
                }
                else {
                    charCount = charCount + 1;
                }
            }
            if (numCount != 0 && charCount != 0) {
                return true;
            }
            else {
                return false;
            }
        }

        function pwStrength(pwd) {
            O_color = "#EBEBEB";
            L_color = "#FF0000";
            M_color = "#FF9900";
            H_color = "#33CC00";
            if (pwd == null || pwd == '') {
                Lcolor = Mcolor = Hcolor = O_color;
//                document.getElementById("strength_L").innerText = "";
//                document.getElementById("strength_M").innerText = "";
//                document.getElementById("strength_H").innerText = "";
            }
            else {
                S_level = checkStrong(pwd);
                switch (S_level) {
                    case 0:
                        Lcolor = Mcolor = Hcolor = O_color;
//                        document.getElementById("strength_L").innerText = "";
//                        document.getElementById("strength_M").innerText = "";
//                        document.getElementById("strength_H").innerText = "";
                    case 1:
                        Lcolor = L_color;
                        Mcolor = Hcolor = O_color;
//                        document.getElementById("strength_L").innerText = "弱";
//                        document.getElementById("strength_M").innerText = "";
//                        document.getElementById("strength_H").innerText = "";
                        break;
                    case 2:
                        Lcolor = Mcolor = M_color;
                        Hcolor = O_color;
//                        document.getElementById("strength_L").innerText = "";
//                        document.getElementById("strength_M").innerText = "中";
//                        document.getElementById("strength_H").innerText = "";
                        break;
                    default:
                        Lcolor = Mcolor = Hcolor = H_color;
//                        document.getElementById("strength_L").innerText = "";
//                        document.getElementById("strength_M").innerText = "";
//                        document.getElementById("strength_H").innerText = "强";
                }
            }

            document.getElementById("strength_L").style.background = Lcolor;
            document.getElementById("strength_M").style.background = Mcolor;
            document.getElementById("strength_H").style.background = Hcolor;
            return;
        }
    </script>
    <style>
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            font-size: 12px;
            color: #000;
        }
        .tipDiv
        {
            border: 1px solid #7C9ECB; /* margin-left: 25px;         margin-right: 12px;*/
            margin-top: 2px;
            margin-bottom: 5px;
            padding: 10px 10px 10px 20px;
            line-height: 20px;
            background-color: #ebf2f8;
            color: #006699;
        }
        .style1
        {
            height: 10px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tipDiv">
        <div>
            <table cellspacing="0" cellpadding="0" align="center" border="0">
                <tbody>
                    <tr>
                        <td class="blue" width="120" height="40">
                            旧密码：
                        </td>
                        <td colspan="2" width="120">
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="input" TextMode="Password"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="blue" height="40">
                            新密码：
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="20" onKeyUp="pwStrength(this.value)" onBlur="pwStrength(this.value)" CssClass="input"></asp:TextBox>
                        </td>
                        <td width="160">
                            <table border="0" cellpadding="1" height="5px" cellspacing="1" style="width: 150px;
                                height: 5px; display: inline">
                                <tr>
                                    <td id="strength_L" style="width: 100px; height: 5px; font-size:small; font-weight: bold"
                                        align="center">弱
                                    </td>
                                    <td id="strength_M" style="font-size: small; font-weight: bold" align="center"
                                        class="style1">中
                                    </td>
                                    <td id="strength_H" style="width: 100px; height: 5px; font-size: small; font-weight: bold"
                                        align="center">强
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="blue" height="40">
                            确认密码：
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20"
                                CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="3" align="center">
                            <strong>温馨提示：</strong>为了保证您的帐号安全，密码必需是数字 + 字母组合(区分大小写)，长度最高为20位。
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="3" align="center">
                            <asp:Button ID="btnPasswrod" runat="server" OnClick="btnPasswrod_Click" Text="确  定"
                                OnClientClick="return ModifyPassword();" CssClass="btn" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div>
    </div>
    </form>
</body>
</html>
