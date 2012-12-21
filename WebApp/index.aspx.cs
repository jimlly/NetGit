/**
 * 文件：index.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：首页页面
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

using AjaxPro;
using Com.Yuantel.MobileMsg.DAL;
//using WebApp.Public;

public partial class index : System.Web.UI.Page
{
    private string logoName;
    private string logoutUrl;
    private string copyRight;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataSet ds = UserInfo.GetAppInfo(Session["AppCode"].ToString());
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    logoName = ds.Tables[0].Rows[0]["LogoName"].ToString();
                    logoutUrl = ds.Tables[0].Rows[0]["LogoutUrl"].ToString();
                    copyRight = ds.Tables[0].Rows[0]["CopyRight"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
            }
        }
    }

    public string[] GetUrl()
    {
        string top = "LogoName=" + logoName + "&LogoutUrl=" + logoutUrl;
        string bottom = "CopyRight=" + copyRight;

        return new string[] { top, bottom };
    }
}
