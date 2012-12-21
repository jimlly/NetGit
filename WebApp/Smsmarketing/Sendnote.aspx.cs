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
using System.Data;
using System.Data.SqlClient;
using Com.Yuantel.MobileMsg.DAL;

public partial class Smsmarketing_Sendnote : System.Web.UI.Page
{
    private string msgId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( this.Request.QueryString["MsgId"] != null )
        {
            this.msgId = AddrPublic.md5Decrypt(this.Request.QueryString["MsgId"].Trim().ToString());
        }
        if (!this.IsPostBack)
        {
            this.PopulateData();
        }
    }

    private void PopulateData()
    {
        DataSet dsResult = null;
        int allRowCount = 0;
        try
        {
            dsResult = YxSms.QueryDetail(Convert.ToInt64(msgId), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out allRowCount);
        }
        catch (Exception ex)
        {
            dsResult = null;
            allRowCount = 0;
        }

        if ((null != dsResult) && (0 != dsResult.Tables.Count) && (0 != dsResult.Tables[0].Rows.Count))
        {
            this.AspNetPager1.Visible = true;
            this.smsList.DataSource = dsResult;
            this.smsList.DataBind();
            BindPager(this.AspNetPager1, allRowCount);
        }
        else
        {
            this.smsList.DataSource = null;
            this.smsList.DataBind();
            this.AspNetPager1.Visible = false;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.PopulateData();
    }

    #region 分页控件列表绑定
    public static void BindPager(Wuqi.Webdiyer.AspNetPager pager, int allCount)
    {
        pager.RecordCount = allCount;
        pager.CustomInfoHTML = "&nbsp;&nbsp;记录总数：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>";
        pager.CustomInfoHTML += "&nbsp; 总页数：<font color=\"blue\"><b>" + pager.PageCount.ToString() + "</b></font>";
        pager.CustomInfoHTML += "&nbsp; 当前页：<font color=\"red\"><b>" + pager.CurrentPageIndex.ToString() + "</b></font>";
    }
    #endregion

    protected void smsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void smsList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#fffdd7'");
        }
    }
}
