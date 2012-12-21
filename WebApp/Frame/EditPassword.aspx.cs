using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Com.Yuantel.MobileMsg.DAL;

namespace Yuantel.YtServerLogin.frame
{
    public partial class EditPassword : System.Web.UI.Page
    {
        Yuantel.User.UserBaseEntity user = null;
        private int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // user = (Yuantel.User.UserBaseEntity)Session["user"];
            //user = Yuantel.Authentication.Web.UserAuthentication.GetUserBaseEntity();
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //if (user == null)
            //{
            //    return;
            //}
            //if (Request.QueryString["type"] != null)
            //{
            //    type = Convert.ToInt32(Request.QueryString["type"]);
            //}
            if (!IsPostBack)
            {

            }
        }

        protected void btnPasswrod_Click(object sender, EventArgs e)
        {
            string oldPassword = this.txtOldPassword.Text;
            string newPassword = this.txtPassword.Text;
            //if (newPassword == oldPassword) return;
            if (newPassword != this.txtConfirmPassword.Text) return;

            if (UserInfo.ChangePwd(int.Parse(Session["SEQNO"].ToString()), GetMD5String(oldPassword), GetMD5String(newPassword)) == 100)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert(\"密码修改成功！\");parent.ymPrompt.close()</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert(\"密码错误，修改失败！\");</script>");
            }
        }

        private string GetMD5String(string NotEncryptString)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(NotEncryptString, "MD5");
        }
    }
}
