/**
 * 文件：Top.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：框架顶部页面
*/

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

public partial class Frame_Top : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //if (!IsPostBack)
        //{
        //    Literal1.Text = (string)Session["USERNAME"];
        //}
    }
    protected void LinkLoginOut_Click(object sender, EventArgs e)
    {
        //Session.Abandon();
        //Session.Clear();
        ////string url = 
        ////Response.Redirect("~/logout.aspx");
        //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.location.href='../logout.aspx'</script>");
    }
}
