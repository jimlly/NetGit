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

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    private void testlogin()
    {
        string sessionID = Guid.NewGuid().ToString();
        string sessionKey = Guid.NewGuid().ToString();
        //Yuantel.Authentication.Web.UserAuthentication.CreateSession(62731, sessionID, sessionKey, 7, "", "");

        Yuantel.User.UserBaseEntity user = new Yuantel.User.UserBaseEntity();
        int i = 0;
        if(!int.TryParse(this.txtSeqno.Text.Trim(),out i))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无效输入')</script>");
            return;
        }

        user.UserSeqNo = i;
        user.UserName = Query.GetUserAccount(i);
        user.AppCode = "yuantel";
        Yuantel.Authentication.Web.UserAuthentication.CreateSession(user, sessionID, sessionKey, 7, "", "");
        //Response.Redirect("NewCommonLogin.aspx?SessionID=" + sessionID + "&SessionKey=" + sessionKey);


        Session["SEQNO"] = user.UserSeqNo;
        Session["USERNAME"] = user.UserName;
        Session["AppCode"] = user.AppCode;
        Yuantel.Authentication.Web.UserAuthentication.GetSessionKey(out sessionKey, out sessionID);
        Session["SessionID"] = sessionID;
        Session["SessionKey"] = sessionKey;

        Response.Redirect("index.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        testlogin();
    }
}
