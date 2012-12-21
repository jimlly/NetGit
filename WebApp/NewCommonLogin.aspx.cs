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

public partial class NewCommonLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Yuantel.User.UserBaseEntity user = Yuantel.Authentication.Web.UserAuthentication.GetUserBaseEntity();

            if (user != null)
            {
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
                LogHelper.Info("user is null");

                Response.Redirect("logout.aspx");
            }
        }
    }
}
