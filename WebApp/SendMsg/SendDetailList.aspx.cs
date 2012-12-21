/**
 * 文件：SendDetailList.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：查看短信明细页面
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

using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
//using WebApp.Public;

public partial class SendMsg_SendDetailList : BasePage
{
    protected string message = "";
    private int IsDelete = 0;

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        IsDelete = int.Parse(Request.QueryString["IsDelete"]);
        this.hdIsDelete.Value = IsDelete.ToString();
        if (!IsPostBack)
        {
            PageInit(1);
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    /// <param name="isTotal"></param>
    private void PageInit(int isTotal)
    {
        try
        {
            Msg msg = new Msg();
            msg.SeqNo = int.Parse(Session["SEQNO"].ToString());
            msg.PageIndex = AspNetPager1.CurrentPageIndex;
            msg.PageSize = AspNetPager1.PageSize;
            msg.IsTotal = isTotal;
            msg.MsgId = Request.QueryString["MsgId"].ToString();
            int TotalRows = 0;
            DataSet ds = Query.GetDetailList(msg.MsgId, msg.PageIndex, msg.PageSize, out TotalRows);
            if (isTotal == 1)
            {
                this.hdTotalRows.Value = TotalRows.ToString();
            }
            else
            {
                TotalRows = Convert.ToInt32(this.hdTotalRows.Value);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GroupSendDetailList.DataSource = ds.Tables[0].DefaultView;
                    GroupSendDetailList.DataBind();
                    AspNetPager1.RecordCount = Convert.ToInt32(TotalRows);
                    AspNetPager1.CustomInfoHTML = "记录总数：<font color=\"blue\"><b>" + AspNetPager1.RecordCount.ToString() + "</b></font>";
                    AspNetPager1.CustomInfoHTML += " 总页数：<font color=\"blue\"><b>" + AspNetPager1.PageCount.ToString() + "</b></font>";
                    AspNetPager1.CustomInfoHTML += " 当前页：<font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex.ToString() + "</b></font>";
                }
                else
                {
                    GroupSendDetailList.DataSource = null;
                    GroupSendDetailList.DataBind();
                    AspNetPager1.RecordCount = 0;
                    AspNetPager1.DataBind();
                    message = "<div align=\"center\">没有任何记录！</div>";
                    TipDiv.Text = "没有任何记录！";
                }
            }
            else
            {
                message = "<div align=\"center\">程序执行超时，请刷新重新加载！</div>";
            }
            ds.Dispose();
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        PageInit(0);
    }
    protected void GroupSendDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#fffdd7'");
        }
    }
}
