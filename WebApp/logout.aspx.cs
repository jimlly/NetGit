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

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Clear();
        Session.Abandon();
        string domain = "";
        string url="";
        try
        {
           
            Yuantel.Authentication.Web.UserAuthentication.UserLogout();
        }
        catch
        {
        }
        this.ClientScript.RegisterStartupScript(this.GetType(), "logout", "<script>top.location.href='Login.aspx'</script>");
    }
}
