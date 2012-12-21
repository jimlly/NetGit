using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage: System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.PreLoad += new EventHandler(BasePage_PreLoad);
    }

    protected void BasePage_PreLoad(object sender, EventArgs e)
    {
        if (Session["SEQNO"] == null)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.location.href='../logout.aspx'</script>");
        }
    }
}
