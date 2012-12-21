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

public partial class AddrLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddrAPIWrapper addr = new AddrAPIWrapper();
        if (!IsPostBack)
        {
            string SessionID = "";string SessionKey = "";

            if (!addr.IsIdentityInfoValid())
            {
                return;
            }
            addr.GetIdentityInfo(out SessionID,out SessionKey);

            string url = ConfigurationManager.AppSettings["AddrLoginUrl"];
            url = url + "?interType=application/yt-addrCode-loginSystem&sessionId=" + SessionID + "&sessionKey=" + SessionKey;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.location.href=\""+url+"\"</script>");
        }
    }
}
