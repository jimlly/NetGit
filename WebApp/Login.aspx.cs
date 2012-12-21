using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SysManageWeb
{
    public partial class Login1 : System.Web.UI.Page
    {
        private string userAccount;
        private string userPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.txtUserAccount.Text == "")
            {
                this.p_msg.InnerText = "用户名不能为空";
                return;
            }
             if (this.txtPassword.Value == "")
            {
                this.p_msg.InnerText = "密码不能为空";
                return;
            }
             if (this.txtCheckCode.Value == "")
            {
                this.p_msg.InnerText = "验证码不能为空";
                Global.CheckCode();
                return;
            }
            try
            {

                string checkcode = txtCheckCode.Value.Trim();
                if (checkcode != Global.GetCheckCode())
                {
                    txtCheckCode.Value = "";
                    Global.CheckCode();
                    this.p_msg.InnerText = ResultMsg(-2);
                    return;
                }

                Global.CheckCode();

            }
            catch
            {
            }

            //登录
            userAccount = this.txtUserAccount.Text.Trim();
            userPassword = this.txtPassword.Value.Trim();
             Yuantel.User.UserBaseEntity user=null;
             int result = 0;
             try
             {
            result =   Yuantel.Authentication.Web.UserAuthentication.LoginbyUserAccount(userAccount, userPassword, out user);
           // LoginInfoDb Op_logininfo = new LoginInfoDb();
            
            //Update(添加异常处理) By yxg 2010-12-10 Begin
           // LoginInfo logininfo = null;
           
               // logininfo = Op_logininfo.GetUserLoginInfo(userAccount, userPassword, out result);
            }
            catch (Exception ex)
            {
              
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alert('用户信息发生错误，请重试！');</script>");
                return;
            }
            //Update(添加异常处理) By yxg 2010-12-10 End 
            if (result == 1)
            {
                //加入客户端访问信息
               // logininfo.ClientIp = Global.GetClientIP();
//logininfo.ClientMac = Global.GetClientMac();
                //写Session
                Session["SEQNO"] = user.UserSeqNo;
                Session["USERNAME"] = user.UserName;
                Session["AppCode"] = user.AppCode;

                string sessionID = "";
                string sessionKey = "";
                Yuantel.Authentication.Web.UserAuthentication.GetSessionKey(out sessionKey, out sessionID);
                Session["SessionID"] = sessionID;
                Session["SessionKey"] = sessionKey;

                Response.Redirect("index.aspx");
            }
            else
            {
                this.p_msg.InnerText = ResultMsg(result);
            }
        }

        #region 将结果转换为消息字符串
        protected String ResultMsg(int result)
        {
            String rmsg = "";
            switch (result)
            {
                case -4:
                    rmsg = "服务器系统繁忙，请稍后再试";
                    break;
                //case -1:
                //    rmsg = "对不起，您没有权限";
                    //break;
                case 0:
                    rmsg = "用户名或密码不正确";
                    break;
                case 1:
                    rmsg = "LoginSuccess";
                    break;
                case -2:
                    rmsg = "验证码不正确";
                    break;
                default:
                    rmsg = "系统出现异常，请刷新页面重试或联系管理员";
                    break;
            }
            return rmsg;
        }
        #endregion
    }
}